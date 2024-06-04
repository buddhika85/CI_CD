using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Data.Repositories;
using ToDoAPI.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB connection string
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ToDos"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// API end points
// get multiple
app.MapGet("api/v1/todo", async (IToDosRepository repo) => {
    return Results.Ok(await repo.GetAll());
});

// get single
app.MapGet("api/v1/todo/{id}", async (IToDosRepository repo, int id) => {
    var todo = await repo.GetById(id);
    if (todo == null)
        return Results.NotFound($"No such todo with {id}");
    return Results.Ok(todo);
});

// create
app.MapPost("api/v1/todo", async (IToDosRepository repo, ToDoItem todo) => {
    await repo.Add(todo);
    await repo.SaveChanges();
    // rest conventions - route to access new command and new object
    return Results.Created($"api/v1/todo/{todo.Id}", todo);
});

// update
app.MapPut("api/v1/todo/{id}", async (IToDosRepository repo, int id, ToDoItem todo) => {
    var commandFromDb = await repo.GetById(id);
    if (commandFromDb == null)
        return Results.NotFound($"No such command with {id}");

    // update and save changes
    await repo.Update(id, todo);

    // rest recomendation for out
    return Results.NoContent();
});

// delete
app.MapDelete("api/v1/todo", async (IToDosRepository repo, int id) => {
    var toDoToDelete = await repo.GetById(id);
    if (toDoToDelete == null)
        return Results.NotFound($"No such todo with {id}");

    await repo.Delete(id);

    // return resource which was deleted
    return Results.Ok(toDoToDelete);
});

app.Run();