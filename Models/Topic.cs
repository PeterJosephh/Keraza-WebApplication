namespace KerazaWebApplication.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }

        public List<Sermon>? Sermons { get; set; }
    }
}
