using System;
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
        public void AddGame_Code201()
        {
            var result = this._gamesController.AddVideogame(new VideogameCreateDto {Description = "testing",Name = "testing",Price = 0f});
            Assert.IsInstanceOfType(result.Value, typeof(VideogameReadDto));
        }
    }
}
