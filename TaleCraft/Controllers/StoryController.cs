using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> InitStory(StoryDTO story)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var storyToCreate = new Story
            {
                Title = story.Title,
                Published = story.Published,
                Description = story.Description,

            };
            var createdStory = await _storyService.AddStory(storyToCreate);
            return CreatedAtAction(nameof(GetStory), new { id = createdStory.Id }, createdStory);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Story>> GetStory(Guid id)
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