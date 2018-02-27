using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShelf.Models.ViewModels
{
    public class AssignedPersonData
    {
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public bool AssignedOwner { get; set; }
        public bool AssignedDesigner { get; set; }
    }
}
