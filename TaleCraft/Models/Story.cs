using System;
using System.ComponentModel.DataAnnotations;

namespace TaleCraft.Models
{
    public class Story
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public bool Published { get; set; }
        public string? Description { get; set; }
        public Guid? FirstPageId { get; set; }
        public Page? FirstPage { get; set; }
        public ICollection<Page> Pages { get; set; } = new List<Page>();
    }
}