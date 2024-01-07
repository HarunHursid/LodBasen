using LodBasen.Models;
using LodBasen.Pages.Gruppe;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace LodbasenXUnitTests
{
    public class GetGruppeUnitTest
    {
        private readonly Mock<IGruppeService> mockRepo;
        private readonly GetGruppeModel getGruppeModel;

        public GetGruppeUnitTest()
        {
            mockRepo= new Mock<IGruppeService>();
            getGruppeModel = new GetGruppeModel(mockRepo.Object);
        }
        
        [Fact]
        public void OnGet_ReturnsIActionResult_WithAListOfGrupper()
        {
            //Arrange 
            mockRepo.Setup(mockrepo => mockrepo.GetGrupper()).Returns(GetTestGrupper());

            //Act
            var result = getGruppeModel.OnGet();
            List<Gruppe> myList = (List<Gruppe>)getGruppeModel.Grupper;

            //Assert
            Assert.IsAssignableFrom<IActionResult>(result);
            var viewResult = Assert.IsType<PageResult>(result);
            var actualMessages = Assert.IsType<List<Gruppe>>(myList);
            Assert.Equal(2, myList.Count);
            Assert.Equal("Test 1", myList[0].GruppeNavn);
            Assert.Equal("Test 2", myList[1].GruppeNavn);
        }

        private List<Gruppe> GetTestGrupper()
        {
            var grupper = new List<Gruppe>();
            grupper.Add(new Gruppe()
            {
                GruppeId = 1,
                GruppeNavn = "Test 1",
            });
            grupper.Add(new Gruppe()
            {
                GruppeId = 2,
                GruppeNavn = "Test 2",
            });
            return grupper;
        }
    }
}