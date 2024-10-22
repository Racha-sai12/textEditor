using System.ComponentModel.DataAnnotations;

namespace TextEditor.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
