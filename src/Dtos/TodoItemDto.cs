using ProTodo.Api.Entities;

namespace src.Dtos
{
    public class TodoItemDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime DueDate { get; set; }
        public TodoState State { get; set; }
        public bool IsDeleted { get; set; }
    }
}