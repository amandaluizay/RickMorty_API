namespace Interview_API.Models
{
    public class LogModel : Entity
    {

        public string FilePath { get; set; }

        public DateTime CreatedTimeStamp { get; set; } = DateTime.Now;

    }
}
