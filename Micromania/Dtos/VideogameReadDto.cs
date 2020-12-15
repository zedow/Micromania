using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micromania.Dtos
{
    public class VideogameReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}
