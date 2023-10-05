using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interview_API.Models;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection.Metadata;
using Interview_API.Intefaces;
using Interview_API.Interface;

namespace UploadCSVToAzureBlobStorage.Controllers
{
    [ApiController]
    [Route("home")]
    public class UploadController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IModelRepository modelRepository;
        private readonly IDeserializeService deserializeService;

        public UploadController(IModelRepository m, IDeserializeService d)
        {
            _httpClient = new HttpClient();
            modelRepository = m;
            deserializeService = d;
        }

        [HttpPost]
        [Route("read")]
        public async Task<List<Episode>> readEpisode(IFormFile file)
        {
            List<Episode> episodes = new List<Episode>();

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            int episodeId = int.Parse(parts[0]);
                            var ep = await deserializeService.DeserializeEpisode(episodeId);
                            episodes.Add(ep);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            var log = new LogModel()
            {
                FilePath = file.FileName,
                Id = new Guid(),
            };
            await modelRepository.Adicionar(log);
            return episodes;
        }

        

        [HttpGet]
        [Route("Log")]
        public async Task<IActionResult> Log()
        {
            var log = await modelRepository.ObterTodos();
            return Ok(log);
        }
    }
}
