
using Interview_API.Models;

namespace Interview_API.Intefaces
{
    public interface IModelRepository : IRepository<LogModel>
    {
        Task<Episode> GetEpisodeDetails(int episodeId);
    }
}