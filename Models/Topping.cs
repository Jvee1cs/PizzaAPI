using System.Collections.Generic;
using PizzaAPI.Models;

namespace PizzaAPI.Models
{
    public class Topping
{
    public int Id { get; set; }

    // Fix the warning by initializing
    public string Name { get; set; } = string.Empty;

    public ICollection<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
}

}
