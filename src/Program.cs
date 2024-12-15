using Microsoft.EntityFrameworkCore;
using ProTodo.Api;
using src.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProTodoDbContext>(options =>
    options.UseMongoDB("mongodb://admin:123qwe%21%40%23QWE@localhost:27017", "ProTodoDb"));

builder.Services.AddScoped<ITodoItemService, TodoItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(option => option.SwaggerEndpoint("/openapi/v1.json", "ProTodo"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
