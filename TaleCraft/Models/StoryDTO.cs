using System.ComponentModel.DataAnnotations;

namespace TaleCraft.Models;

public class StoryDTO
{
        [Required] 
        public string Title { get; set; }
 
        [Required]
        public bool Published { get; set; }
        public string Description { get; set; }
}