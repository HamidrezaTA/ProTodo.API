using Microsoft.AspNetCore.Mvc;
using src.Services;
using ProTodo.Api.Entities;
using src.Dtos;

namespace ProTodo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemController : ControllerBase
{
    private readonly ILogger<TodoItemController> _logger;
    private readonly ITodoItemService _todoItemService;

    public TodoItemController(ILogger<TodoItemController> logger, ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
        _logger = logger;
    }

    [HttpPost]
    public async Task CreateTodoItem([FromBody] TodoItemCreateAndUpdateDto todoItemDto)
    {
        await _todoItemService.CreateTodoItemAsync(todoItemDto);
    }

    [HttpPut("{id}")]
    public async Task UpdateTodoItem([FromRoute] string id, [FromBody] TodoItemCreateAndUpdateDto todoItemDto)
    {
        await _todoItemService.UpdateTodoItemAsync(id, todoItemDto);
    }

    [HttpDelete("{id}")]
    public async Task DeleteTodoItem(string id)
    {
        await _todoItemService.DeleteTodoItemAsync(id);
    }

    [HttpGet]
    public async Task<List<TodoItemDto>> GetAllTodoItems()
    {
        return await _todoItemService.GetAllTodoItemsAsync();
    }

    [HttpGet("{id}")]
    public async Task<TodoItemDto> GetTodoItem([FromRoute] string id)
    {
        return await _todoItemService.GetTodoItemAsync(id);
    }
}
