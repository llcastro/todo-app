using AutoMapper;

namespace backend.Dto
{
    [AutoMap(typeof(todo_app.Models.TodoItem))]
    public class TodoItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}