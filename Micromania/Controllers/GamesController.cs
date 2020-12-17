using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Micromania.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Micromania.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Micromania.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private const string Path = "./database.json";
        private List<Videogame> Videogames;

        public GamesController(IMapper mapper)
        {
            _mapper = mapper;
            string readText = System.IO.File.ReadAllText(Path);
            Videogames = JsonConvert.DeserializeObject<IEnumerable<Videogame>>(readText).ToList();
        }

        public void SaveChanges()
        {
            System.IO.File.Delete(Path);
            System.IO.File.AppendAllText(Path, JsonConvert.SerializeObject(Videogames));
        }

        private void OverwriteFile(IEnumerable<Videogame> collection)
        {
            System.IO.File.Delete(Path);
            System.IO.File.AppendAllText(Path, JsonConvert.SerializeObject(collection));
        }

        /// <summary>
        /// Retourne la liste complète des jeux-vidéos 
        /// </summary>
        /// <returns>Un code 200 contenant la liste</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VideogameReadDto>> GetGames()
        {
            return Ok(_mapper.Map<IEnumerable<VideogameReadDto>>(Videogames));
        }

        /// <summary>
        /// Créer un nouveau jeu-vidéo
        /// </summary>
        /// <param name="game"></param>
        /// <returns>
        /// Un code 201 contenant l'objet créé
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<VideogameReadDto> AddVideogame(VideogameCreateDto game)
        {
            if (game == null)
            {
                throw new ArgumentNullException();
            }
            var newGame = _mapper.Map<Videogame>(game);
            Videogames.Add(newGame);
            SaveChanges();
            return StatusCode(201,_mapper.Map<VideogameReadDto>(newGame));
        }

        /// <summary>
        /// Retourne un jeu-vidéo selon l'id en paramètre
        /// </summary>
        /// <param name="id">L'id du jeu-vidéo à retourner</param>
        /// <returns>Le jeu vidéo dont l'id correspond au paramètre</returns>
        /// <response code="404">Si l'id ne correspond à aucun jeu-vidéo</response>
        /// <response code="200">Si l'id correspond à un jeu-vidéo</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VideogameReadDto> GetVideogame(Guid id)
        {
            var game = Videogames.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VideogameReadDto>(game));
        }

        /// <summary>
        /// Mettre à jour un jeu-vidéo
        /// </summary>
        /// <param name="id">L'id du jeu-vidéo à retourner</param>
        /// <returns>Aucun contenu</returns>
        /// <response code="404">Si l'id ne correspond à aucun jeu-vidéo</response>
        /// <response code="204">Si le jeu-vidéo a été mis à jour</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VideogameReadDto> UpdateVideogame(Guid id,[FromBody] VideogameUpdateDto gameUpdate)
        {
            var game = Videogames.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            _mapper.Map(gameUpdate, game);
            SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Supprime un jeu vidéo
        /// </summary>
        /// <param name="id">L'id du jeu-vidéo à retourner</param>
        /// <returns>Aucun contenu</returns>
        /// <response code="404">Si l'id ne correspond à aucun jeu-vidéo</response>
        /// <response code="204">Si le jeu-vidéo a été supprimé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteGame(Guid id)
        {
            var game = Videogames.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            Videogames.Remove(game);
            SaveChanges();
            return NoContent();
        }
    }
}
