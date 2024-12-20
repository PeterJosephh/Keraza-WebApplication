using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KerazaWebApplication.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public List<Event>? Events { get; set; }

        public string? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
    }
}
