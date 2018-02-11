using GameShelf.Models;
using System;
using System.Linq;

namespace GameShelf.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GameShelfContext context)
        {
            context.Database.EnsureCreated();

            if (context.Games.Any())
            {
                return;
            }

            var playTimes = new PlayTime[]
            {
                new PlayTime {PlayTimeCategory="Less Than an Hour"},
                new PlayTime {PlayTimeCategory="About an Hour"},
                new PlayTime {PlayTimeCategory="About 2 Hours"},
                new PlayTime {PlayTimeCategory="More Than 2 Hours"}
            };
            foreach (PlayTime pt in playTimes)
            {
                context.Playtimes.Add(pt);
            }
            context.SaveChanges();


            var games = new Game[]
            {
                new Game{Title="Smallworld", PublicationYear=2009, MinPlayers=2, MaxPlayers=5, PlayTime=playTimes[1] }, 
                new Game{Title="Scotland Yard", PublicationYear=1983, MinPlayers=3, MaxPlayers=6, PlayTime=playTimes[2]},
                new Game{Title="Robbits", PublicationYear=2015, MinPlayers=4, MaxPlayers=4, PlayTime=playTimes[1]}, 
                new Game{Title="Dungeon Petz", PublicationYear=2011, MinPlayers=2, MaxPlayers=4, PlayTime=playTimes[3]}, 
                new Game{Title="Champions of Midgard", PublicationYear=2015, MinPlayers=2, MaxPlayers=4, PlayTime=playTimes[2]}, 
                new Game{Title="King of Tokyo", PublicationYear=2011, MinPlayers=2, MaxPlayers=6, PlayTime=playTimes[1]}, 
                new Game{Title="Scythe", PublicationYear=2016, MinPlayers=1, MaxPlayers=5, PlayTime=playTimes[2]} 
            };
            foreach (Game g in games)
            {
                context.Games.Add(g);
            }
            context.SaveChanges();

            var people = new Person[]
            {
                new Person{FirstName="Katherine",LastName="Conrad"},
                new Person{FirstName="Tom",LastName="Conrad"},
                new Person{FirstName="Cindy",LastName="Conrad"},
                new Person{FirstName="Jamison",LastName="Muller"},
                new Person{FirstName="Matt",LastName="Conrad" },
                new Person{FirstName="Philippe",LastName="Keyaerts"},
                new Person{FirstName="Manfred",LastName="Burggraf"},
                new Person{FirstName="Dorothy",LastName="Garrels"},
                new Person{FirstName="Vlaada",LastName="Chvàtil"},
                new Person{FirstName="Ole",LastName="Steiness"},
                new Person{FirstName="Richard",LastName="Garfield"},
                new Person{FirstName="Jamey",LastName="Stegmaier"}
            };
            foreach (Person p in people)
            {
                context.People.Add(p);
            }
            context.SaveChanges();

            var gamePersonRelationships = new GamePersonRelationship[]
            {
                new GamePersonRelationship{GameID=1,PersonID=5,Role=Role.Owner},
                new GamePersonRelationship{GameID=1,PersonID=6,Role=Role.Designer},
                new GamePersonRelationship{GameID=2,PersonID=4,Role=Role.Owner},
                new GamePersonRelationship{GameID=2,PersonID=7,Role=Role.Designer},
                new GamePersonRelationship{GameID=2,PersonID=8,Role=Role.Designer},
                new GamePersonRelationship{GameID=3,PersonID=1,Role=Role.Owner},
                new GamePersonRelationship{GameID=3,PersonID=2,Role=Role.Owner},
                new GamePersonRelationship{GameID=3,PersonID=1,Role=Role.Designer},
                new GamePersonRelationship{GameID=3,PersonID=2,Role=Role.Designer},
                new GamePersonRelationship{GameID=4,PersonID=1,Role=Role.Owner},
                new GamePersonRelationship{GameID=4,PersonID=9,Role=Role.Designer},
                new GamePersonRelationship{GameID=5,PersonID=2,Role=Role.Owner},
                new GamePersonRelationship{GameID=5,PersonID=10,Role=Role.Designer},
                new GamePersonRelationship{GameID=6,PersonID=3,Role=Role.Owner},
                new GamePersonRelationship{GameID=6,PersonID=11,Role=Role.Designer},
                new GamePersonRelationship{GameID=7,PersonID=5,Role=Role.Owner},
                new GamePersonRelationship{GameID=7,PersonID=12,Role=Role.Designer}
            };
            foreach (GamePersonRelationship gpr in gamePersonRelationships)
            {
                context.Relationships.Add(gpr);
            }
            context.SaveChanges();
        }
    }
}
