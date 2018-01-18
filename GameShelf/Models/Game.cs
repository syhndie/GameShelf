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
        public DateTime OrigPubYear { get; set; }
        public int Edition { get; set; }

        public ICollection<GamePersonRelationship> RelatedPeople { get; set; }
    }
}
