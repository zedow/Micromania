using System;
using System.Collections.Generic;
using AutoMapper;
using Micromania.Controllers;
using Micromania.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micromania.tests
{
    [TestClass]
    public class VideogamesControllerTests
    {
        private MapperConfiguration _configuration;
        private IMapper _mapper;
        private GamesController _gamesController;
        public VideogamesControllerTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Profiles());
            });
            _mapper = new Mapper(_configuration);
            this._gamesController = new GamesController(_mapper);
        }

        [TestMethod]
        public void AddGame_Code404_WhenParamIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>((() => this._gamesController.AddVideogame(null)));
        }

        [TestMethod]
        public void AddGame_Code201_WhenParamIsOkay()
        {
            var result = this._gamesController.AddVideogame(new VideogameCreateDto {Description = "testing",Name = "testing",Price = 0f});
            Assert.IsInstanceOfType(result,typeof(ActionResult<VideogameReadDto>));
        }

        [TestMethod]
        public void GetGames_Code200()
        {
            var result = this._gamesController.GetGames();
            Assert.IsInstanceOfType(result,typeof(ActionResult<IEnumerable<VideogameReadDto>>));
        }

        [TestMethod]
        public void GetVideogame_Code404_WrongId()
        {
            var result = this._gamesController.GetVideogame(new Guid());
            Assert.IsInstanceOfType(result.Result,typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [TestMethod]
        public void GetVideogame_Code200_GoodId()
        {
            var result = this._gamesController.GetVideogame(Guid.Parse("8c1e8960-7f45-41f9-abb8-6b2a722f8a16"));
            Assert.IsInstanceOfType(result.Result,typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [TestMethod]
        public void UpdateVideogame_Code404_WrongId()
        {
            var result = this._gamesController.UpdateVideogame(new Guid(), new VideogameUpdateDto());
            Assert.IsInstanceOfType(result.Result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [TestMethod]
        public void UpdateVideogame_Code204_GoodIdUpdated()
        {
            var result = this._gamesController.UpdateVideogame(Guid.Parse("8c1e8960-7f45-41f9-abb8-6b2a722f8a16"), new VideogameUpdateDto { Price = 10f});
            Assert.IsInstanceOfType(result.Result, typeof(Microsoft.AspNetCore.Mvc.NoContentResult));
        }

        [TestMethod]
        public void DeleteGame_Code404_WrongId()
        {
            var result = this._gamesController.DeleteGame(new Guid());
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [TestMethod]
        public void DeleteGame_Code204_GoodIdDeleted()
        {
            var result = this._gamesController.DeleteGame(Guid.Parse("8c1e8960-7f45-41f9-abb8-6b2a722f8a16"));
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NoContentResult));
        }
    }
}
