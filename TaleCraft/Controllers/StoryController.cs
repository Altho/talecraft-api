using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaleCraft.Helpers;
using TaleCraft.Interfaces;
using TaleCraft.Models;

namespace TaleCraft.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class StoryController : ControllerBase
{
    private readonly ILogger<StoryController> _logger;
    private readonly IStoryService _storyService;

    public StoryController(ILogger<StoryController> logger, IStoryService storyService)
    {
        _logger = logger;
        _storyService = storyService;
    }

    [HttpPost]
    public async Task<IActionResult> PostStory(Story story)
    {
        var createdStory = await _storyService.AddStory(story);
        return CreatedAtAction(nameof(GetStory), new { id = createdStory.Id }, createdStory);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Story>> GetStory(int id)
    {
        var story = await _storyService.GetStory(id);
        if (story == null)
        {
            return NotFound();
        }
        return story;
    }

    // Other methods...
}