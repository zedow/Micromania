using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Micromania.Models;

namespace Micromania.Dtos
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<VideogameCreateDto, Videogame>();
            CreateMap<Videogame, VideogameReadDto>();
            CreateMap<VideogameUpdateDto, Videogame>();
            CreateMap<Videogame, VideogameUpdateDto> ();
        }
    }
}
