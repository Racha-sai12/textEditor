using System.ComponentModel.DataAnnotations;

namespace TextEditor.Models
{
    public class Chapter
    {
        [Key ] 
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjetId { get; set; }
        public Project Project { get; set; }
        public string Path { get; set; }
    }
}
