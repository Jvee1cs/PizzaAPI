using Microsoft.AspNetCore.Mvc;
using PizzaAPI.DTOs;
using PizzaAPI.Models;
using PizzaAPI.Services;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaService _service;

        public PizzasController(IPizzaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllPizzasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pizza = await _service.GetPizzaByIdAsync(id);
            if (pizza == null)
                return NotFound();
            return Ok(pizza);
        }

        // For creation, accept a DTO that has the Name and ToppingIds
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PizzaCreateDTO dto)
        {
            try
            {
                // Map DTO to Pizza model
                var pizza = new Pizza
                {
                    Name = dto.Name
                };
                var created = await _service.CreatePizzaAsync(pizza, dto.ToppingIds);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // For update, accept DTO with Name only (assuming toppings updated separately)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PizzaUpdateDTO dto)
        {
            var pizza = new Pizza
            {
                Name = dto.Name
            };
            var updated = await _service.UpdatePizzaAsync(id, pizza);
            return updated != null ? Ok(updated) : NotFound();
        }

        // Update toppings separately via query params
        [HttpPut("{id}/toppings")]
        public async Task<IActionResult> UpdateToppings(int id, [FromQuery] List<int> toppingIds)
        {
            var updated = await _service.UpdatePizzaToppingsAsync(id, toppingIds);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _service.DeletePizzaAsync(id) ? NoContent() : NotFound();
    }
}
