using System.ComponentModel.DataAnnotations;

namespace SHADotNetCore.ToDoList.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool DeleteFlag { get; set; }
    }
}
