using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micromania.Dtos
{
    public class VideogameCreateDto
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}
