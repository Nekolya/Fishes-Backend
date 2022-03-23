using Microsoft.EntityFrameworkCore;

namespace FishesBackend.Models
{
    public class Fish
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? breedId { get; set; }
    }

    public class Breed
    {
        public int id { get; set; }
        public string? name { get; set; }
    }

    class FishDb : DbContext
    {
        public FishDb(DbContextOptions options) : base(options) { }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Breed> Breeds { get; set; }
    }
}
