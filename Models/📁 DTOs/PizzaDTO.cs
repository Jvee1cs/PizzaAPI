
public class PizzaDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; // non-nullable string
    public List<ToppingDTO> Toppings { get; set; } = new List<ToppingDTO>();
}
