using GameShelf.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Data
{
    public class GameShelfContext : DbContext
    {
        public GameShelfContext(DbContextOptions<GameShelfContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<GamePersonRelationship> Relationships { get; set; }
    }
}
