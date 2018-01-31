using System;
using System.Collections.Generic;
using System.Linq;
using GameShelf.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Models
{
    public class GameIndexViewModel
    {
        public List<GameWithPersonInfo> GameList { get; set; }
        public string TitleFilter { get; set; }

        public GameIndexViewModel (GameShelfContext db)
        {
            var gamesIEnum = db.Games
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpr => gpr.Person)
                .Select(g => new GameWithPersonInfo
                {
                    ID = g.ID,
                    Title = g.Title,
                    PublicationYear = g.PublicationYear,
                    Owners = g.GamePersonRelationships.Where(gpr => gpr.Role == "Owner").Select(gpr => gpr.Person).ToList(),
                    Designers = g.GamePersonRelationships.Where(gpr => gpr.Role == "Designer").Select(gpr => gpr.Person).ToList()
                });
            GameList = gamesIEnum.ToList();
        }
        //    if (!String.IsNullOrEmpty(titleFiler))
        //    {
        //        gamesIEnum = gamesIEnum.Where(g => g.Title.Contains(titleFiler));
        //    }
    }
}
