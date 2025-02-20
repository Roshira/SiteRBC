using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SiteRBC.Controllers;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;
using Xunit;

public class LibreryMoqTest
{
    public interface IDatabase
    {
        IEnumerable<string> GetData();
        void SaveData(IEnumerable<string> data);

    }

    [Fact]
    public void TestDatabaseCopy()
    {
        // Arrange
        var mockDatabase = new Mock<IDatabase>();
        var originalData = new List<string> { "Data1", "Data2", "Data3" };

        // Налаштування мок-об'єкта для повернення тестових даних
        mockDatabase.Setup(db => db.GetData()).Returns(originalData);

        // Act
        var copiedData = mockDatabase.Object.GetData().ToList();

        // Assert
        mockDatabase.Verify(db => db.GetData(), Times.Once); // Перевірка, що метод GetData був викликаний один раз
        Assert.Equal(originalData, copiedData); // Перевірка, що дані скопійовані правильно
    }
}