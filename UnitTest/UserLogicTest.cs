using Application.Logic;
using NUnit.Framework;
using Shared.Dtos;
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
        UserCreationDto user = new UserCreationDto
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
        };
        
        //Act
        userLogic.CreateAsync(user);
        //Assert
        Assert.Pass();
    }
    [Test]
    public void GetByUsername()
    {
        //Arrange
        User user = new User
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        
        //Act
        User userReply = userLogic.getUser(user.Username).Result;
        //Assert
        Assert.AreEqual(user.Username, userReply.Username);
    }
}