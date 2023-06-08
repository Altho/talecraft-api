using Microsoft.AspNetCore.Authorization;
using TaleCraft.Interfaces;
using TaleCraft.Models;

namespace TaleCraft.Services;

public class StoryService : IStoryService
{
    private readonly DataContext _context;

    public StoryService(DataContext context)
    {
        _context = context;
    }

    [Authorize]
    public async Task<Story> AddStory(Story story)
    {
        _context.Stories.Add(story);
        await _context.SaveChangesAsync();
        return story;
    }
    public async Task<Story> GetStory(Guid id)
    {
        return await _context.Stories.FindAsync(id);
    }
}