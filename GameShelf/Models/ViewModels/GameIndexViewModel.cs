using System;
using System.Collections.Generic;
using System.Linq;
using GameShelf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameShelf.Models;



namespace GameShelf.Models
{
    public class GameIndexViewModel
    {
        public List<GameWithPersonInfo> GameList { get; set; }
        public string TitleFilter { get; set; }
        public int PlayTimeFilter { get; set; }
        public int MinFilter { get; set; }
        public int MaxFilter { get; set; }
        public string OwnerFilter { get; set; }
        public string DesignerFilter { get; set; }
        public string Sort { get; set; }
        public SelectList PlayTimeSelect { get; set; }

        public GameIndexViewModel (
            GameShelfContext db, 
            string titleFilter, 
            int minFilter,
            int maxFilter,
            int playTimeFilter,
            string ownerFilter, 
            string designerFilter, 
            string sort)
        {
            TitleFilter = titleFilter;
            MinFilter = minFilter;
            MaxFilter = maxFilter;
            PlayTimeFilter = playTimeFilter;
            OwnerFilter = ownerFilter;
            DesignerFilter = designerFilter;
            Sort = sort;

            var playTimeQuery = from pt in db.Playtimes
                                orderby pt.ID
                                select pt;
            PlayTimeSelect = new SelectList(playTimeQuery, "ID", "PlayTimeCategory");

            var gamesIEnum = db.Games
                .Include(g => g.PlayTime)
                .Include(g => g.GamePersonRelationships)
                .ThenInclude(gpr => gpr.Person)
                .ToList()
                .Select(g => new GameWithPersonInfo(g));

            if (!String.IsNullOrEmpty(titleFilter))
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.Title.ToLower().Contains(titleFilter.ToLower()));
            } 

            if (minFilter != 0)
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.MinPlayers == minFilter);
            }

            if (maxFilter != 0)
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.MaxPlayers == maxFilter);
            }

            if (playTimeFilter != 0)
            {
                gamesIEnum = gamesIEnum.Where(gpi => gpi.PlayTimeID == playTimeFilter);
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
                GameList = gamesIEnum.OrderByDescending(gpi => gpi.Title).ToList();
            }
            else if (sort == "time-asc")
            {
                GameList = gamesIEnum.OrderBy(gpi => gpi.PlayTimeID).ThenBy(gpi => gpi.Title).ToList();
            }
            else if (sort == "time-desc")
            {
                GameList = gamesIEnum.OrderByDescending(gpi => gpi.PlayTimeID).ThenByDescending(gpi => gpi.Title).ToList();
            }
            else if (sort == "minplay-asc")
            {
                GameList = gamesIEnum.OrderBy(gpi => gpi.MinPlayers).ThenBy(gpi => gpi.Title).ToList();
            }
            else if (sort == "minplay-desc")
            {
                GameList = gamesIEnum.OrderByDescending(gpi => gpi.MinPlayers).ThenByDescending(gpi => gpi.Title).ToList();
            }
            else if (sort == "maxplay-asc")
            {
                GameList = gamesIEnum.OrderBy(gpi => gpi.MaxPlayers).ThenBy(gpi => gpi.Title).ToList();
            }
            else if (sort == "maxplay-desc")
            {
                GameList = gamesIEnum.OrderByDescending(gpi => gpi.MaxPlayers).ThenByDescending(gpi => gpi.Title).ToList();
            }
            else
            {
                GameList = gamesIEnum.OrderBy(gpi => gpi.Title).ToList();
            }
        }
    }
}
