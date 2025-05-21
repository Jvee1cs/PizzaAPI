namespace PizzaAPI.DTOs
{
    public class PizzaCreateDTO
    {
        public string Name { get; set; } = null!;
        public List<int> ToppingIds { get; set; } = new();
    }
}
