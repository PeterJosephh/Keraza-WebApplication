using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic.FileIO;

namespace KerazaWebApplication.Models
{
    public class MediaFile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; }

        public string? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }



        [ForeignKey("Sermon")]
        public int SermonId { get; set; }
        public Sermon? Sermon { get; set; }



    }
}
