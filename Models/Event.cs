using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KerazaWebApplication.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Caption { get; set; }
        public DateOnly Date { get; set; }


        public string? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Sermon>? Sermons { get; set; }


    }
}
