using System;
using System.Threading.Tasks;
using Application.Logic;
using FileData;
using FileData.DAOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Dtos;
using Shared.Models;

namespace ApplicationTest;
[TestClass]
public class EventLogicTest
{
    EventLogic EventLogic; 

    
    [TestInitialize]
    public void StartTests()
    {
        FileContext fileContext = new FileContext();
        EventFileDao eventFileDao = new EventFileDao(fileContext);
        UserFileDao userFileDao = new UserFileDao(fileContext);
        EventLogic = new EventLogic(eventFileDao, userFileDao);
    }
    
    [TestMethod]
    public void TitleEmpty()
    {
        // Arrange
        string username = "Jakob";
        string title = "";
        string description = "test description";
        string location = "test location";
        DateTime dateTime = DateTime.Today;

        EventCreationDto eventToCreate = new EventCreationDto(username, title, description, location, dateTime);

        // Act
        try
        {
            Task<Event> response = EventLogic.CreateAsync(eventToCreate);     
            Assert.Fail("The expected exception was not thrown.");
        }
        catch (Exception e)
        {
            // Assert
            Assert.IsTrue(true);
        } 

    }
}