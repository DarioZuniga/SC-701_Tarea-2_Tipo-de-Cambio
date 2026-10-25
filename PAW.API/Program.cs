using Task = PAW.Models.Task;
using PAW.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Inyección de Dependencias (Requerido por la tarea)
builder.Services.AddSingleton<TaskRepository>();

// 2. CORS (Para que la Web pueda conectarse)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 3. Endpoints de la Minimal API 
app.MapGet("/api/tasks", (TaskRepository repo) =>
    Results.Ok(repo.GetAll()));

app.MapGet("/api/tasks/{id}", (int id, TaskRepository repo) =>
{
    var task = repo.GetById(id);
    return task is not null ? Results.Ok(task) : Results.NotFound();
});

app.MapPost("/api/tasks", (Task task, TaskRepository repo) =>
{
    var created = repo.Add(task);
    return Results.Created($"/api/tasks/{created.Id}", created);
});

app.MapPut("/api/tasks/{id}", (int id, Task task, TaskRepository repo) =>
{
    task.Id = id;
    return repo.Update(task) ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/api/tasks/{id}", (int id, TaskRepository repo) =>
{
    return repo.Delete(id) ? Results.NoContent() : Results.NotFound();
});

app.Run();