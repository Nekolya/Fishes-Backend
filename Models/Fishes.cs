using Microsoft.EntityFrameworkCore;

namespace FishesBackend.Models
{
    public class Fish
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? breedId { get; set; }
    }

    class FishDb : DbContext
    {
        public FishDb(DbContextOptions options) : base(options) { }
        public DbSet<Fish> Fishes { get; set; }
    }
}
