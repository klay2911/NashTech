namespace API.Models;

public class Work
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }

    public Work()
    {
        Id = Guid.NewGuid();
    }
    
    public Work(string? title, bool isCompleted)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = isCompleted;
    }
}