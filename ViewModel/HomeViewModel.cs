using KerazaWebApplication.Models;

namespace KerazaWebApplication.ViewModel
{
    public class HomeViewModel
    {

        public string searchWord { get; set; }
        public List<Sermon> Sermons { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Event> Events { get; set; }
        public List<Language> Languages { get; set; }
        public List<Topic> Topics { get; set; }



        public int EventId { get; set; }
        public int TopicId { get; set; }

        public int CategoryId { get; set; }

        public int LanguageId { get; set; }

        public int SpeakerId { get; set; }


    }
}
