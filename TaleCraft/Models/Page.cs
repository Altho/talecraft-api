using System.ComponentModel.DataAnnotations;

namespace TaleCraft.Models;

public class Page
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Content { get; set; }
    public Guid StoryId { get; set; }
    public Story Story { get; set; }
    public ICollection<PageLink> PageLinks { get; set; } = new List<PageLink>();
}