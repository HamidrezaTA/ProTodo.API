using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace ProTodo.Api.Entities
{
    [Collection("TodoItems")]
    public class TodoItem
    {
        public ObjectId Id { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }
        public DateTime DueDate { get; set; }
        public TodoState State { get; set; }
        public bool IsDeleted { get; set; }
    }
}