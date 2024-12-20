using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace KerazaWebApplication.Models
{
    public class Sermon
    {
        public int Id { get; set; }
        //Title
        [Required]
        public string? Title_ar { get; set; }
        public string? Title_en { get; set; }
        public string? Caption { get; set; }
        [Required]
        public DateOnly Date { get; set; }


        public string? ImageURL { get; set; }
        public string? PdfDriveId{ get; set; }
        public string? VideoDriveId { get; set; }
        public string? AudioDriveId { get; set; }


        public string? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }

        //Nav Props
        [Required]
        [ForeignKey("Speaker")]
        public int SpeakerId { get; set; }
        public Speaker? Speaker { get; set; }

        [Required]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        [Required]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        public Language? Language { get; set; }

    }
}
