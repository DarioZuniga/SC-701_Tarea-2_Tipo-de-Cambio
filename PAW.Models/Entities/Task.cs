namespace PAW.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Ojo: el profe usa "Name" y "Description"
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
}