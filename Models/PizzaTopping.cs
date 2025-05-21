using PizzaAPI.Models;

namespace PizzaAPI.Models
{
    public class PizzaTopping
{
    public int PizzaId { get; set; }
    public Pizza? Pizza { get; set; }  // add '?' to make nullable

    public int ToppingId { get; set; }
    public Topping? Topping { get; set; }  // add '?'
}

}
