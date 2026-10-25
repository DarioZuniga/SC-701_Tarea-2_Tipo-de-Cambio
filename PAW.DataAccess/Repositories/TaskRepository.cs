using Task = PAW.Models.Task;

namespace PAW.DataAccess.Repositories;

public class TaskRepository
{
    // Lista en memoria (sin EF Core)
    private readonly List<Task> _tasks = new()
    {
        new Task { Id = 1, Name = "Aprender Minimal API", Status = "Pendiente" },
        new Task { Id = 2, Name = "Terminar Tarea 3", Status = "Completada" }
    };
    private int _nextId = 3;

    public IEnumerable<Task> GetAll() => _tasks;

    public Task? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public Task Add(Task task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        return task;
    }

    public bool Update(Task task)
    {
        var existing = GetById(task.Id);
        if (existing == null) return false;

        existing.Name = task.Name;
        existing.Status = task.Status;
        return true;
    }

    public bool Delete(int id)
    {
        var task = GetById(id);
        return task != null && _tasks.Remove(task);
    }
}