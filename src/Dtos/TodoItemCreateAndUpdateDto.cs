using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProTodo.Api.Entities;

namespace src.Dtos
{
    public class TodoItemCreateAndUpdateDto
    {
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime DueDate { get; set; }
        public TodoState State { get; set; }
        public bool IsDeleted { get; set; }
    }
}