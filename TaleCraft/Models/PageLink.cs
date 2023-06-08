namespace TaleCraft.Models;

public class PageLink
{
    public Guid SourcePageId { get; set; }
    public Page SourcePage { get; set; }

    public Guid TargetPageId { get; set; }
    public Page TargetPage { get; set; }
}