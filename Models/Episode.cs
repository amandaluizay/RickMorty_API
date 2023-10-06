namespace Interview_API.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Air_date { get; set; }
        public string _Episode { get; set; }
        public List<Character> Characters { get; set; }
    }
}