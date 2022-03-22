using Microsoft.EntityFrameworkCore;

namespace FishesBackend.Models
{
    public class Breed
    {
        public int id { get; set; }
        public string? name { get; set; }
    }

    class BreedDb : DbContext
    {
        public BreedDb(DbContextOptions options) : base(options) { }
        public DbSet<Breed> Breeds { get; set; }
    }
}
