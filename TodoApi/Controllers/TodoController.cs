using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Services;
using MongoDB.Bson;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public TodoController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService; 
        }

       [HttpGet]
public async Task<IActionResult> GetTodos()
{
    var todos = await _mongoDbService.GetTodosAsync();
    var result = todos.Select(todo => new
    {
        id = todo.Id.ToString(), 
        title = todo.Title,
        isComplete = todo.IsComplete
    });

    return Ok(result);
}

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("TodoItem is required.");
            }

            await _mongoDbService.CreateTodoAsync(todoItem);
            return CreatedAtAction(nameof(GetTodos), new { id = todoItem.Id }, todoItem);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(string id)
        {
            try
            {
               
                var objectId = ObjectId.Parse(id);
                var deleted = await _mongoDbService.DeleteTodoAsync(objectId);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (FormatException)
            {
                return BadRequest("Invalid ID format.");
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        
    }
}
