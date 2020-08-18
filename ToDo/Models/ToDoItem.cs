using System;

namespace ToDo.Models
{
    // ToDo Structure
    public class ToDoItem
    {
        public long Id { get; set; }
        public DateTime DateTimeExp { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long PercentComplete { get; set; }
        public bool IsComplete { get; set; }
    }
}
