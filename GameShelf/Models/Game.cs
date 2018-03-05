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

        public void UpdateGamePeople(int[] selectedPeople, GameShelfContext db, Role role)
        {
            var selectedPeopleHS = new HashSet<int>(selectedPeople);
            var currentPeopleHS = new HashSet<int>(this.GamePersonRelationships.Where(gpr => gpr.Role == role).Select(gpr => gpr.PersonID));

            foreach (int selectedPersonID in selectedPeopleHS)
            {
                if (!currentPeopleHS.Contains(selectedPersonID))
                {
                    this.GamePersonRelationships.Add(new GamePersonRelationship { GameID = this.ID, PersonID = selectedPersonID, Role = role });
                }
            }

            foreach (var currentPersonID in currentPeopleHS)
            {
                if (!selectedPeopleHS.Contains(currentPersonID))
                {
                    var personToRemove = this.GamePersonRelationships.Where(gpr => gpr.Role == role).Single(gpr => gpr.PersonID == currentPersonID);
                    db.Remove(personToRemove);
                }
            }
        }
    }
}
