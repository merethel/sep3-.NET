using Application.Logic;
using NUnit.Framework;
using Shared.Models;

namespace UnitTest.EventLogicTest;

public class UserLogicTest
{
    private UserLogic userLogic;

    public UserLogicTest()
    {
        userLogic = new UserLogic(new UserDaoMock());
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateUser()
    {
        //Arrange
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            SecurityLevel = 1
        };
        
        //Act
        
        //Assert
        Assert.Pass();
    }
}