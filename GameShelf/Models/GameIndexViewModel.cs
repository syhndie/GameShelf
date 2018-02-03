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
        public string DesignerFilter { get; set; }
        public string Sort { get; set; }

        public GameIndexViewModel (GameShelfContext db, string titleFilter, string ownerFilter, string designerFilter, string sort)
        {
            TitleFilter = titleFilter;
            OwnerFilter = ownerFilter;
            DesignerFilter = designerFilter;
            Sort = sort;

            var gamesIEnum = db.Games
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpr => gpr.Person)
                .Select(g => new GameWithPersonInfo
                {
                    ID = g.ID,
                    Title = g.Title,
                    PublicationYear = g.PublicationYear,
                    Owners = g.GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Select(gpr => gpr.Person).ToList(),
                    Designers = g.GamePersonRelationships.Where(gpr => gpr.Role == Role.Designer).Select(gpr => gpr.Person).ToList()
                });

            if (!String.IsNullOrEmpty(titleFilter))
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.Title.ToLower().Contains(titleFilter.ToLower()));
            } 

            if (!String.IsNullOrEmpty(ownerFilter))
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.Owners.Any(o => o.FullName.ToLower().Contains(ownerFilter.ToLower())));
            }

            if (!String.IsNullOrEmpty(designerFilter))
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.Designers.Any(d => d.FullName.ToLower().Contains(designerFilter.ToLower())));
            }


            if (sort == "title-desc")
            {
                GameList = gamesIEnum.OrderByDescending(gpi => gpi.Title).ThenByDescending(gpi => gpi.PublicationYear).ToList();
            }
            else if (sort == "year-asc")
            {
                GameList = gamesIEnum.OrderBy(gpi => gpi.PublicationYear).ThenBy(gpi => gpi.Title).ToList();
            }
            else if (sort == "year-desc")
            {
                GameList = gamesIEnum.OrderByDescending(gpi => gpi.PublicationYear).ThenByDescending(gpi => gpi.Title).ToList();
            }
            else
            {
                GameList = gamesIEnum.OrderBy(gpi => gpi.Title).ThenBy(gpi => gpi.PublicationYear).ToList();
            }
        }
    }
}
