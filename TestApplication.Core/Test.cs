using System.ComponentModel.DataAnnotations;

namespace TestApplication.Core;

public class Test
{
    public string Id { get; set; } = new Guid().ToString();
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}