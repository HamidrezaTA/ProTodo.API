using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using ProTodo.Api;
using ProTodo.Api.Entities;
using src.Dtos;

namespace src.Services
{
    public interface ITodoItemService
    {
        Task CreateTodoItemAsync(TodoItemCreateAndUpdateDto todoItemDto);
        Task<List<TodoItemDto>> GetAllTodoItemsAsync();
        Task<TodoItemDto> GetTodoItemAsync(string id);
        Task UpdateTodoItemAsync(string id, TodoItemCreateAndUpdateDto todoItemDto);
        Task DeleteTodoItemAsync(string id);
    }

    public class TodoItemService : ITodoItemService
    {
        private readonly ProTodoDbContext _proTodoDbContext;
        public TodoItemService(ProTodoDbContext proTodoDbContext)
        {
            _proTodoDbContext = proTodoDbContext;
        }

        public async Task CreateTodoItemAsync(TodoItemCreateAndUpdateDto todoItemDto)
        {
            var _todoItem = new TodoItem()
            {
                Title = todoItemDto.Title,
                Content = todoItemDto.Content,
                DueDate = todoItemDto.DueDate,
                State = TodoState.Initiated,
                IsDeleted = false
            };

            await _proTodoDbContext.TodoItems.AddAsync(_todoItem);

            await _proTodoDbContext.SaveChangesAsync();
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            var objectId = new ObjectId(id);

            var _todoItem = await _proTodoDbContext.TodoItems.FindAsync(objectId);
            if (_todoItem is null)
                throw new Exception($"TodoItem with id {id} not exist");

            _todoItem.IsDeleted = true;

            _proTodoDbContext.TodoItems.Update(_todoItem);

            await _proTodoDbContext.SaveChangesAsync();
        }

        public async Task<List<TodoItemDto>> GetAllTodoItemsAsync()
        {
            var todoItemsList = await _proTodoDbContext.TodoItems.Where(ti => ti.IsDeleted == false).ToListAsync();

            var todoItemsDtoList = new List<TodoItemDto>();

            todoItemsList.ForEach(_todoItem =>
            {
                todoItemsDtoList.Add(new TodoItemDto()
                {
                    Id = _todoItem.Id.ToString(),
                    Title = _todoItem.Title,
                    Content = _todoItem.Content,
                    DueDate = _todoItem.DueDate,
                    State = _todoItem.State,
                    IsDeleted = _todoItem.IsDeleted
                });
            });

            return todoItemsDtoList;
        }

        public async Task<TodoItemDto> GetTodoItemAsync(string id)
        {
            var objectId = new ObjectId(id);

            var _todoItem = await _proTodoDbContext.TodoItems.FindAsync(objectId);
            if (_todoItem is null)
                throw new Exception($"TodoItem with id {id} not exist");

            return new TodoItemDto()
            {
                Id = _todoItem.Id.ToString(),
                Title = _todoItem.Title,
                Content = _todoItem.Content,
                DueDate = _todoItem.DueDate,
                State = _todoItem.State,
                IsDeleted = _todoItem.IsDeleted
            };
        }

        public async Task UpdateTodoItemAsync(string id, TodoItemCreateAndUpdateDto todoItemDto)
        {
            var objectId = new ObjectId(id);

            var _todoItem = await _proTodoDbContext.TodoItems.FindAsync(objectId);
            if (_todoItem is null)
                throw new Exception($"TodoItem with id {id} not exist");

            _todoItem.Content = todoItemDto.Content;
            _todoItem.DueDate = todoItemDto.DueDate;
            _todoItem.State = todoItemDto.State;
            _todoItem.Title = todoItemDto.Title;

            _proTodoDbContext.TodoItems.Update(_todoItem);

            await _proTodoDbContext.SaveChangesAsync();
        }
    }
}