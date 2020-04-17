using System.ComponentModel.DataAnnotations;

namespace todo_app.Models
{
    public class TodoItem
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}