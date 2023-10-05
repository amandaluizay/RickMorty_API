
using Interview_API.Intefaces;
using Interview_API.Interface;
using Interview_API.Models;
using Interview_API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Interview_API.Data
{
    public class ModelRepository : Repository<LogModel>, IModelRepository
    {
        private readonly IDeserializeService _deserializeService;
        public ModelRepository(MeuDbContext context, IDeserializeService deserializeService) : base(context) {
        
            _deserializeService = deserializeService;
        }

        public Task<Episode> GetEpisodeDetails(int episodeId)
        {
            throw new NotImplementedException();
        }
    }
}