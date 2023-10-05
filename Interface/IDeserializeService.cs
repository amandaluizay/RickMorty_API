using Interview_API.Models;

namespace Interview_API.Interface
{
    public interface IDeserializeService
    {
        Task<List<Character>> DeserializeCharacter(List<string> characters);

        Task<string> Deserialize(string url);
        Task<Episode> DeserializeEpisode(int episodeId);
    }
}
