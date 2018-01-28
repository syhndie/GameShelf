﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameShelf.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Models
{
    public class GameIndexViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int?  PublicationYear { get; set; }
        public List<Person> Owners { get; set; }
        public List<Person> Designers { get; set; }
        public string TitleFilter { get; set; }

        public static List<GameIndexViewModel> GetList(GameShelfContext db, string titleFiler)
        {
            var gamesIEnum = db.Games
                 .Include(g => g.GamePersonRelationships)
                 .ThenInclude(gpr => gpr.Person)
                 .Select(g => new GameIndexViewModel
                 {
                     ID = g.ID,
                     Title = g.Title,
                     PublicationYear = g.PublicationYear,
                     Owners = g.GamePersonRelationships.Where(gpr => gpr.Role == "Owner").Select(gpr => gpr.Person).ToList(),
                     Designers = g.GamePersonRelationships.Where(gpr => gpr.Role == "Designer").Select(gpr => gpr.Person).ToList()
                 });
            if (!String.IsNullOrEmpty(titleFiler))
            {
                gamesIEnum = gamesIEnum.Where(g => g.Title.Contains(titleFiler));
            }

            return gamesIEnum.ToList();
        }
    }
}
