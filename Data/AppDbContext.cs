using Microsoft.EntityFrameworkCore;
using PizzaAPI.Models;

namespace PizzaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<PizzaTopping>()
        .HasKey(pt => new { pt.PizzaId, pt.ToppingId });

    modelBuilder.Entity<PizzaTopping>()
        .HasOne(pt => pt.Pizza)
        .WithMany(p => p.PizzaToppings)
        .HasForeignKey(pt => pt.PizzaId);

    modelBuilder.Entity<PizzaTopping>()
        .HasOne(pt => pt.Topping)
        .WithMany(t => t.PizzaToppings)
        .HasForeignKey(pt => pt.ToppingId);
         modelBuilder.Entity<Pizza>()
        .HasIndex(p => p.Name)
        .IsUnique();
}

    }
}
