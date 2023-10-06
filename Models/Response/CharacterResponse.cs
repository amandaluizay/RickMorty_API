namespace Interview_API.Models.Response
{
    public class CharacterResponse
    {
        public string Gender { get; set; }
        public int Id { get; set; }
        public LocationResponse Location { get; set; }
        public string Name { get; set; }
        public OriginResponse Origin { get; set; }
        public string Species { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
