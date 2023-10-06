
using Azure.Storage.Blobs;
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
        private readonly IConfiguration _configuration;
        public ModelRepository(MeuDbContext context, IDeserializeService deserializeService, IConfiguration configuration) : base(context) {
        
            _deserializeService = deserializeService;
            _configuration = configuration;
        }

        public int CountTotalLocation(List<Episode> episodes)
        {
            var locations = episodes
            .SelectMany(episode => episode.Characters.Select(character => character.Location))
            .Distinct()
            .ToList();

            return locations.Count();
        }

        public int CountTotalFemaleCharacter(List<Episode> episodes)
        {
            int total = episodes
            .SelectMany(episode => episode.Characters)
            .Count(character => character.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase));

            return total;
        }

        public int CountTotalMaleCharacter(List<Episode> episodes)
        {
            int total = episodes
            .SelectMany(episode => episode.Characters)
            .Count(character => character.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase));

            return total;
        }

        public int CountTotalGenderlessCharacter(List<Episode> episodes)
        {
            int total = episodes
             .SelectMany(episode => episode.Characters)
             .Count(character => string.Equals(character.Gender, "unknown", StringComparison.OrdinalIgnoreCase));

            return total;
        }

        public int CountTotalUnknowCharacter(List<Episode> episodes)
        {
            int total = episodes
            .SelectMany(episode => episode.Characters)
            .Count(character => string.Equals(character.Name, "unknown", StringComparison.OrdinalIgnoreCase));

            return total;

        }

        public string Upload(IFormFile file)
        {
            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
            var blobClient = new BlobClient(connectionString, "arquivos", file.FileName + Guid.NewGuid());
            using (var stream = new MemoryStream())
            {

                file.CopyTo(stream);
                stream.Position = 0;
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}