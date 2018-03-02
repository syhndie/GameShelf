using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GameShelf.Data;

namespace GameShelf.Models
{
    public class Game
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name ="Minimum Players")]
        public int MinPlayers { get; set; }

        [Display(Name ="Maximum Players")]
        public int MaxPlayers { get; set; }

        public ICollection<GamePersonRelationship> GamePersonRelationships { get; set; }

        public int PlayTimeID { get; set; }
        public PlayTime PlayTime { get; set; }

        public void UpdateGameOwners(int[] selectedOwners, GameShelfContext db)
        {
            var selectedOwnersHS = new HashSet<int>(selectedOwners);
            var currentOwnersHS = new HashSet<int>(this.GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Select(gpr => gpr.PersonID));

            foreach (var selectedOwnerID in selectedOwners)
            {
                if (!currentOwnersHS.Contains(selectedOwnerID))
                {
                    this.GamePersonRelationships.Add(new GamePersonRelationship { GameID = this.ID, PersonID = selectedOwnerID, Role = Role.Owner });
                }
            }

            foreach (var currentOwnerID in currentOwnersHS)
            {
                if (!selectedOwnersHS.Contains(currentOwnerID))
                {
                    var ownerToRemove = this.GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Single(gpr => gpr.PersonID == currentOwnerID);
                    db.Remove(ownerToRemove);
                }
            }
        }
    }
}
