using PizzaAPI.Models;
using PizzaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace PizzaAPI.Services
{
    public class ToppingService : IToppingService
    {
        private readonly AppDbContext _context;

        public ToppingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Topping>> GetAllToppingsAsync()
{
    return await _context.Toppings.ToListAsync();
}

        public async Task<Topping?> GetToppingByIdAsync(int id) =>
            await _context.Toppings.FindAsync(id);

       public async Task<Topping> CreateToppingAsync(Topping topping)
{
    _context.Toppings.Add(topping);
    await _context.SaveChangesAsync();
    return topping;  // non-nullable
}


        public async Task<Topping?> UpdateToppingAsync(int id, Topping topping)
        {
            var existing = await _context.Toppings.FindAsync(id);
            if (existing == null) return null;

            existing.Name = topping.Name;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteToppingAsync(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null) return false;

            _context.Toppings.Remove(topping);
            await _context.SaveChangesAsync();
            return true;
        }
         public async Task<bool> ToppingNameExistsAsync(string name, int? excludeId = null)
    {
        return await _context.Toppings.AnyAsync(t =>
            t.Name.ToLower() == name.ToLower() &&
            (!excludeId.HasValue || t.Id != excludeId.Value));
    }
    }
}
