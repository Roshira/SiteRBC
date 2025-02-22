using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using SiteRBC.Services;
using System.Threading.Tasks;

public class AccountServiceTests
{
    private readonly SiteRBCContext _context;
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        var options = new DbContextOptionsBuilder<SiteRBCContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new SiteRBCContext(options);
        _service = new AccountService(_context);
    }

    [Fact]
    public async Task RegisterAsync_ShouldCreateUser_WhenDataIsValid()
    {
        var model = new RegisterViewModel
        {
            FullName = "John Doe",
            Email = "johndoe@example.com",
            Password = "Test1234!"
        };

        var result = await _service.RegisterAsync(model);

        Assert.True(result.Success);
        Assert.Equal("Registration successful! You can now log in.", result.Message);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        Assert.NotNull(user);
        Assert.Equal("John Doe", user.FullName);
    }

    [Fact]
    public async Task RegisterAsync_ShouldFail_WhenEmailAlreadyExists()
    {
        _context.Users.Add(new Users
        {
            FullName = "Jane Doe",
            Email = "existing@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("password123"),
            Role = "User"
        });
        await _context.SaveChangesAsync();

        var model = new RegisterViewModel
        {
            FullName = "New User",
            Email = "existing@example.com",
            Password = "Test1234!"
        };

        var result = await _service.RegisterAsync(model);

        Assert.False(result.Success);
        Assert.Equal("Email or name already registered.", result.Message);
    }
}
