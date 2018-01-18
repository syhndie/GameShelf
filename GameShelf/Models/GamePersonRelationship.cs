using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShelf.Models
{
    public class GamePersonRelationship
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int PersonID { get; set; }
        public string Role { get; set; }
    }
}
