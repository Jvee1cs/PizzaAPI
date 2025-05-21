using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Models;
using PizzaAPI.Services;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToppingsController : ControllerBase
    {
        private readonly IToppingService _service;

        public ToppingsController(IToppingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllToppingsAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Topping topping)
        {
            if (await _service.ToppingNameExistsAsync(topping.Name))
                return BadRequest("A topping with this name already exists.");

            var created = await _service.CreateToppingAsync(topping);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Topping topping)
        {
            if (await _service.ToppingNameExistsAsync(topping.Name, id))
                return BadRequest("A topping with this name already exists.");

            var updated = await _service.UpdateToppingAsync(id, topping);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await _service.DeleteToppingAsync(id) ? NoContent() : NotFound();
    }
}
