using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameShelf.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        public ICollection<GamePersonRelationship> RelatedGames { get; set; }
    }
}
