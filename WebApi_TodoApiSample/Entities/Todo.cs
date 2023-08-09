using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_TodoApiSample.Entities
{
    [Table("todos")]
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Description2 { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DatabaseContext( DbContextOptions options) : base(options)
        {

        }

        public DbSet<Todo> Todos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (optionsBuilder.IsConfigured == false)
        //    {
        //        optionsBuilder.UseSqlServer();
        //        //optionsBuilder.UseLazyLoadingProxies();
        //    }
        //}
    }
}
