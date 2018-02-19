using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShelf.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShelf.Models.ViewModels
{
    public class GameEditViewModel
    {
        public GameWithPersonInfo GameWithPersonInfo { get; set; }
        public SelectList PlayTimeSelect { get; set; }

        public GameEditViewModel(GameShelfContext _context, int id)
        {
            var playTimeQuery = _context.Playtimes.OrderBy(pt => pt.ID);
            PlayTimeSelect = new SelectList(playTimeQuery, "PlayTimeCategory", "PlayTimeCategory");

            GameWithPersonInfo = new GameWithPersonInfo( _context, id);
        }
    }
}
