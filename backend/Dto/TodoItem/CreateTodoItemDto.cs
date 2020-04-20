using AutoMapper;

namespace backend.Dto
{
    [AutoMap(typeof(todo_app.Models.TodoItem))]
    public class CreateTodoItemDto
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}