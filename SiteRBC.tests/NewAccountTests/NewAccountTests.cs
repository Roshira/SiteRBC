using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using SiteRBC.Services.AccountSevice;

public class UserServiceTests
{
    private readonly SiteRBCContext _context;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        // Налаштування InMemoryDatabase для тестів
        var options = new DbContextOptionsBuilder<SiteRBCContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new SiteRBCContext(options);
        _context.Database.EnsureDeleted(); // Видаляємо попередні дані
        _context.Database.EnsureCreated(); // Гарантуємо створення БД

        _userService = new UserService(_context);
    }

    [Fact]
    public async Task AuthenticateUser_ValidUser_ReturnsUser()
    {
        // Arrange
        var email = "test@example.com";
        var password = "password123";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        _context.Users.Add(new Users
        {
            FullName = "Test User",
            Email = email,
            Password = hashedPassword,
            Role = "User"
        });

        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.AuthenticateUser(email, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
    }

    [Fact]
    public async Task AuthenticateUser_InvalidPassword_ReturnsNull()
    {
        // Arrange
        var email = "test@example.com";
        _context.Users.Add(new Users
        {
            FullName = "Test User",
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword("correctPassword"),
            Role = "User"
        });

        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.AuthenticateUser(email, "wrongPassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RegisterUser_NewUser_ReturnsTrue()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            FullName = "John Doe",
            Email = "john@example.com",
            Password = "securePass123",
            Role = "User"
        };

        // Act
        var result = await _userService.RegisterUser(model);

        // Assert
        Assert.True(result);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        Assert.NotNull(user);
        Assert.Equal("John Doe", user.FullName);
        Assert.Equal("User", user.Role);
    }

    [Fact]
    public async Task RegisterUser_ExistingEmail_ReturnsFalse()
    {
        // Arrange
        var model = new RegisterViewModel
        {
            FullName = "John Doe",
            Email = "john@example.com",
            Password = "securePass123",
            Role = "User"
        };

        _context.Users.Add(new Users
        {
            FullName = "John Doe",
            Email = model.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = "User"
        });

        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.RegisterUser(model);

        // Assert
        Assert.False(result);
    }
}
