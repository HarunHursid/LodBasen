using LodBasen.Models;
using LodBasen.Pages.Gruppe;
using LodBasen.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LodbasenXUnitTests.Unit_Test
{
    public class CreateGruppeUnitTest
    {

        [Fact]
        public void CreateGruppe_Post_ReturnsARedirectAndAddsGruppe_WhenModelStateIsValid()
        {
            //Arrange

            var mockRepo = new Mock<IGruppeService>();
            mockRepo.Setup(repo => repo.AddGruppe(It.IsAny<Gruppe>())).Verifiable();

            var @gruppe = new Gruppe() { GruppeId = 1, GruppeNavn = "Test 1" };

            var createModel = new CreateGruppeModel(mockRepo.Object);
            
            createModel.Gruppe = @gruppe;

            //Act 
            var result = createModel.OnPost();

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("GetGruppe", redirectToActionResult.PageName);
            mockRepo.Verify((e) => e.AddGruppe(@gruppe), Times.Once);
        }
    }
}
