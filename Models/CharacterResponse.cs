namespace Interview_API.Models
{
    public class CharacterResponse
    {
        public string gender { get; set; }
        public int id { get; set; }
        public LocationResponse location { get; set; }
        public string name { get; set; }
        public OriginResponse origin { get; set; }
        public string species { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }
}
