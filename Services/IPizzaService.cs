using PizzaAPI.DTOs;
using PizzaAPI.Models;
public interface IPizzaService
{
    Task<List<PizzaDTO>> GetAllPizzasAsync();
    Task<PizzaDTO?> GetPizzaByIdAsync(int id);
    Task<PizzaDTO> CreatePizzaAsync(Pizza pizza, List<int> toppingIds);
    Task<PizzaDTO?> UpdatePizzaAsync(int id, Pizza updatedPizza);
    Task<PizzaDTO?> UpdatePizzaToppingsAsync(int id, List<int> toppingIds);
    Task<bool> DeletePizzaAsync(int id);
}
