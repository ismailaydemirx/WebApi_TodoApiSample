using System.ComponentModel.DataAnnotations;

namespace WebApi_TodoApiSample.Models
{
    public class TodoResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Description2 { get; set; }
        public bool IsCompleted { get; set; }

        public string IsCompletedStr
        {
            get
            {
                return IsCompleted ? "Tamamlandı" : "Tamamlanmadı";
            }

        }
    }

    public class TodoCreateModel
    {
        [Required]
        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

    }

}
