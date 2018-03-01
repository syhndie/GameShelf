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
            if (selectedOwners.Length == 0)
            {
                GamePersonRelationships = GamePersonRelationships.Where(gpr => gpr.Role != Role.Owner).ToList();
                return;
            }

            var selectedOwnersHS = new HashSet<int>(selectedOwners);
            var currentOwnersHS = new HashSet<int>(this.GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Select(gpr => gpr.PersonID));
            foreach (var person in db.People)
            {
                if (selectedOwnersHS.Contains(person.ID))
                {
                    if (!currentOwnersHS.Contains(person.ID))
                    {
                        GamePersonRelationships.Add(new GamePersonRelationship { GameID = ID, PersonID = person.ID, Role = Role.Owner });
                    }
                }
                else
                {
                    if (currentOwnersHS.Contains(person.ID))
                    {
                        GamePersonRelationship ownerToRemove = GamePersonRelationships.Where(gpr => gpr.Role == Role.Owner).Single(gpr => gpr.PersonID == person.ID);
                        db.Remove(ownerToRemove);
                    }
                }
            }

        }
    }

}
