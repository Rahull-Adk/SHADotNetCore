using System.ComponentModel.DataAnnotations;

namespace SHADotNetCore.ToDoList.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public byte? PriorityLevel { get; set; }
        public int? CategoryId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
