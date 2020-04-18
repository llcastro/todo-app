using AutoMapper;
using todo_app.Models;

namespace backend.Dto
{
    [AutoMap(typeof(TodoItem))]
    public class TodoItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}