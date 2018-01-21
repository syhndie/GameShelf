using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShelf.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }

        public ICollection<GamePersonRelationship> GamePersonRelationships { get; set; }
    }
}
