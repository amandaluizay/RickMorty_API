namespace Interview_API.Models
{
    public class Root
    {
        public List<Episode> episodes { get; set; }
        public int TotalLocations { get; set; }
        public int TotalFemaleCharacters { get; set; }
        public int TotalMaleCharacters { get; set; }
        public int TotalGenderlessCharacters { get; set; }
        public int TotalGenderUnknownCharacters { get; set; }
        public string UploadeFilePath { get; set; }
    }
}