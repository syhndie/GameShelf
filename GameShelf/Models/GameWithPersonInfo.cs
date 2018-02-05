using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShelf.Data;

namespace GameShelf.Models
{
    public class GameWithPersonInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? PublicationYear { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public PlayTime PlayTime { get; set; }
        public List<Person> Owners { get; set; }
        public List<Person> Designers { get; set; }
    }
}
