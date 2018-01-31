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
        public string OwnerFilter { get; set; }

        public GameIndexViewModel (GameShelfContext db, string titleFilter, string ownerFilter)
        {
            TitleFilter = titleFilter;
            OwnerFilter = ownerFilter;

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

            if (!String.IsNullOrEmpty(titleFilter))
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.Title.ToLower().Contains(titleFilter.ToLower()));
            } 

            if (!String.IsNullOrEmpty(ownerFilter))
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.Owners.Any(o => o.FullName.ToLower().Contains(ownerFilter.ToLower())));
            }

            GameList = gamesIEnum.ToList();
        }
    }
}
