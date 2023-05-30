using TaleCraft.Models;
namespace TaleCraft.Interfaces;

public interface IStoryService
{
    Task<Story> AddStory(Story story);
    Task<Story> GetStory(int id);
}