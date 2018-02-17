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

        public GameEditViewModel(GameShelfContext db)
        {
            var playTimeQuery = from pt in db.Playtimes
                                orderby pt.ID
                                select pt;
            PlayTimeSelect = new SelectList(playTimeQuery, "PlayTimeCategory", "PlayTimeCategory");
        }
    }
}
