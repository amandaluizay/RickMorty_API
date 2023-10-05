namespace Interview_API.Models
{
    public class Root
    {
        public List<Episode> episodes { get; set; }
        public string totalFemaleCharacters { get; set; }
        public string totalGenderlessCharacters { get; set; }
        public string totalGenderUnknownCharacters { get; set; }
        public string totalLocations { get; set; }
        public string totalMaleCharacters { get; set; }
        public string uploadeFilePath { get; set; }
    }
}