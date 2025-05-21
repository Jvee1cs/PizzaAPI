using System.Collections.Generic;
using PizzaAPI.Models;

public class Pizza
{
    public int Id { get; set; }

    // Fix nullability warning here by initializing or making required
    public string Name { get; set; } = string.Empty;

    // Use the join table navigation property
    public ICollection<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
}
