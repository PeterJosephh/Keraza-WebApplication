using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KerazaWebApplication.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? ImageURL { get; set; }

        public string? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }

        public List<Sermon>? Sermons { get; set; }


    }
}
