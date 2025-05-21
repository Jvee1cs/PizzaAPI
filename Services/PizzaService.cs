using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Models;
using PizzaAPI.DTOs;

namespace PizzaAPI.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly AppDbContext _context;

        public PizzaService(AppDbContext context) => _context = context;

        public async Task<List<PizzaDTO>> GetAllPizzasAsync()
        {
            var pizzas = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .ToListAsync();

            return pizzas.Select(MapPizzaToDTO).ToList();
        }

        public async Task<PizzaDTO?> GetPizzaByIdAsync(int id)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null) return null;

            return MapPizzaToDTO(pizza);
        }

        public async Task<PizzaDTO> CreatePizzaAsync(Pizza pizza, List<int> toppingIds)
        {
            if (await _context.Pizzas.AnyAsync(p => p.Name == pizza.Name))
                throw new Exception("Pizza name already exists.");

            pizza.PizzaToppings = toppingIds.Select(id => new PizzaTopping { ToppingId = id }).ToList();

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            // Reload with toppings included
            var createdPizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(p => p.Id == pizza.Id);

            return MapPizzaToDTO(createdPizza!);
        }

        public async Task<PizzaDTO?> UpdatePizzaAsync(int id, Pizza updatedPizza)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null) return null;

            pizza.Name = updatedPizza.Name;
            await _context.SaveChangesAsync();

            // Reload with toppings included
            var updated = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(p => p.Id == id);

            return MapPizzaToDTO(updated!);
        }

        public async Task<PizzaDTO?> UpdatePizzaToppingsAsync(int id, List<int> toppingIds)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null) return null;

            pizza.PizzaToppings.Clear();
            pizza.PizzaToppings = toppingIds.Select(toppingId => new PizzaTopping
            {
                PizzaId = pizza.Id,
                ToppingId = toppingId
            }).ToList();

            await _context.SaveChangesAsync();

            // Reload with toppings included
            var updated = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(p => p.Id == id);

            return MapPizzaToDTO(updated!);
        }

        public async Task<bool> DeletePizzaAsync(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null) return false;

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return true;
        }

        private PizzaDTO MapPizzaToDTO(Pizza pizza)
        {
            return new PizzaDTO
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Toppings = pizza.PizzaToppings?.Select(pt => new ToppingDTO
                {
                    Id = pt.Topping.Id,
                    Name = pt.Topping.Name
                }).ToList() ?? new List<ToppingDTO>()
            };
        }
    }
}
