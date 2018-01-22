using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameShelf.Models
{
    public class GamePersonRelationship
    {
        public int GameID { get; set; }
        public int PersonID { get; set; }

        [Required]
        public string Role { get; set; }

        public Game Game { get; set; }
        public Person Person { get; set; }
    }
}
