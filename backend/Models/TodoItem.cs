using System.ComponentModel.DataAnnotations;
using AutoMapper;
using backend.Dto;

namespace todo_app.Models
{
    [AutoMap(typeof(TodoItemDto))]
    [AutoMap(typeof(CreateTodoItemDto))]
    public class TodoItem
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}