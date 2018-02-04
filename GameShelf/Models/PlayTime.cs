using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameShelf.Models
{
    public class PlayTime
    {
        public int ID { get; set; }

        [Required]
        public string PlayTimeCategory { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
