using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShelf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        /// <summary>
        /// To use this constructor, the game MUST already have person relationships and persons included.
        /// </summary>
        public GameWithPersonInfo(Game game)
        {
            ID = game.ID;
            Title = game.Title;
            PublicationYear = game.PublicationYear;
            MinPlayers = game.MinPlayers;
            MaxPlayers = game.MaxPlayers;
            PlayTime = game.PlayTime;
            Owners = game.GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Select(gpr => gpr.Person).ToList();
            Designers = game.GamePersonRelationships.Where(gpr => gpr.Role == Role.Designer).Select(gpr => gpr.Person).ToList();
        }

        public GameWithPersonInfo(GameShelfContext _context, int id)
        {
            ID = id;
            var gameWithPersonInfo = _context.Games
                .Include(g => g.PlayTime)
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpi => gpi.Person)
                .Single(m => m.ID == id);
            Title = gameWithPersonInfo.Title;
            PublicationYear = gameWithPersonInfo.PublicationYear;
            MinPlayers = gameWithPersonInfo.MinPlayers;
            MaxPlayers = gameWithPersonInfo.MaxPlayers;
            PlayTime = gameWithPersonInfo.PlayTime;
            Owners = gameWithPersonInfo.GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Select(gpr => gpr.Person).ToList();
            Designers = gameWithPersonInfo.GamePersonRelationships.Where(gpr => gpr.Role == Role.Designer).Select(gpr => gpr.Person).ToList();
        }
    }
}
