﻿using System;
using System.Security.AccessControl;
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
        userLogic = new UserLogic(new UserClientMock());
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
            Role = "User"
        };
        
        //Act
        userLogic.CreateAsync(user);
        //Assert
        Assert.Pass();
    }

    [Test]
    public void EmptyUsernameThrowsException()
    {
        //Arrange
        UserCreationDto user = new UserCreationDto
        {
            Email = "mail@via.dk",
            Username = "",
            Password = "password",
            Role = "User"
        };
        
        //Act
        userLogic.CreateAsync(user);
        //Assert
        Assert.Throws<AggregateException>(() =>
        {
            var result = userLogic.CreateAsync(user).Result;
        });
    }
    
    [Test]
    public void UsernameLargerThan16CharThrowsException()
    {
        //Arrange
        UserCreationDto user = new UserCreationDto
        {
            Email = "mail@via.dk",
            Username = "01234567891234567",
            Password = "password",
            Role = "User"
        };
        
        //Act
        userLogic.CreateAsync(user);
        //Assert
        Assert.Throws<AggregateException>(() =>
        {
            var result = userLogic.CreateAsync(user).Result;
        });
    }
    
    [Test]
    public void GetByUsernameTest()
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
    
    [Test]
    public void PasswordLessThan8CharThrowsException()
    {
        //Arrange
        UserCreationDto user = new UserCreationDto
        {
            Email = "mail@via.dk",
            Username = "username",
            Password = "1234567",
            Role = "User"
        };
        
        //Act
        userLogic.CreateAsync(user);
        //Assert
        Assert.Throws<AggregateException>(() =>
        {
            var result = userLogic.CreateAsync(user).Result;
        });
    }
    
    [Test]
    public void EmailWithoutAtsignThrowsException()
    {
        //Arrange
        UserCreationDto user = new UserCreationDto
        {
            Email = "mail.via.dk",
            Username = "username",
            Password = "password",
            Role = "User"
        };
        
        //Act
        userLogic.CreateAsync(user);
        //Assert
        Assert.Throws<AggregateException>(() =>
        {
            var result = userLogic.CreateAsync(user).Result;
        });
    }
    
    //For testing purposes there exists a user with:
    //username: username
    //password: password
    
    [Test]
    public void ValidateUserWithCorrectPasswordReturnsUser()
    {
        //Arrange
        //Act
        //Assert
        Assert.AreEqual("username", userLogic.ValidateUser("username", "password").Result.Username);
    }
    
    [Test]
    public void ValidateUserWrongPasswordThrowsException()
    {
        //Arrange
        //Act
        //Assert
        Assert.Throws<AggregateException>(() =>
        {
            var result = userLogic.ValidateUser("username", "wrongpassword").Result;
        });
    }
    
}