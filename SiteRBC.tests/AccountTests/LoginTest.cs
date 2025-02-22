using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using SiteRBC.Services;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LoginTest
{
    private readonly SiteRBCContext _context;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly AccountService _service;

    public LoginTest()
    {
        var options = new DbContextOptionsBuilder<SiteRBCContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new SiteRBCContext(options);
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        _service = new AccountService(_context, _httpContextAccessorMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldFail_WhenEmailDoesNotExist()
    {
        // Arrange
        var model = new LoginViewModel
        {
            Email = "notfound@example.com",
            Password = "Test1234!"
        };

        // Act
        var result = await _service.LoginAsync(model);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid email or password.", result.Message);
    }

    [Fact]
    public async Task LoginAsync_ShouldFail_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new Users
        {
            FullName = "John Doe",
            Email = "johndoe@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("Test1234!"),
            Role = "User"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var model = new LoginViewModel
        {
            Email = "johndoe@example.com",
            Password = "WrongPassword!"
        };

        // Act
        var result = await _service.LoginAsync(model);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid email or password.", result.Message);
    }
}
