using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShelf.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Models.ViewModels
{
    public class DeletePersonViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public int GamesCount { get; set; }

        public DeletePersonViewModel (int id, GameShelfContext db)
        {
            var personWithExtras = db.People
                .Include(p => p.RelatedGames)
                .Single(p => p.ID == id);

            FullName = personWithExtras.FullName;
            ID = id;

            var gameIDs = new List<int>();
            foreach (GamePersonRelationship gpr in personWithExtras.RelatedGames)
            {
                gameIDs.Add(gpr.GameID);
            }
            GamesCount = gameIDs.Distinct().Count();
        }
    }
}
