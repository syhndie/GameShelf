﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShelf.Data;

namespace GameShelf.Models.ViewModels
{
    public class PersonCreateViewModel
    {
        public Person Person { get; set; }
        public int GameID { get; set; }
        public string Origin { get; set; }


        public PersonCreateViewModel(int gameID, string origin)
        {
            GameID = gameID;
            Origin = origin;
        }

        public PersonCreateViewModel()
        {

        }
    }
}
