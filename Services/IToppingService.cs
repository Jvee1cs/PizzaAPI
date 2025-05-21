using PizzaAPI.Models;

namespace PizzaAPI.Services
{
   public interface IToppingService
{
    Task<List<Topping>> GetAllToppingsAsync();
    Task<Topping> CreateToppingAsync(Topping topping);
    Task<Topping?> UpdateToppingAsync(int id, Topping topping);
    Task<bool> DeleteToppingAsync(int id);

    // Add this:
    Task<bool> ToppingNameExistsAsync(string name, int? excludeId = null);
}

}
