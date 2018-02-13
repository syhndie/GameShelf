using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameShelf.Models
{
    public class Game
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public int? PublicationYear { get; set; }

        [Display(Name ="Minimum Players")]
        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public ICollection<GamePersonRelationship> GamePersonRelationships { get; set; }

        public PlayTime PlayTime { get; set; }
    }
}
