namespace TaleCraft.Models;

public class Story
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Published { get; set; }
    public string? Content { get; set; }
}