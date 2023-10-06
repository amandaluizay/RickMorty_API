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
using System.ComponentModel;

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
        [Route("Upload")]
        public async Task<Root> Upload2(IFormFile file)
        {
            var url = modelRepository.Upload(file);
            List<Episode> episodes = new List<Episode>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Faça uma solicitação HTTP GET para a URL para baixar o arquivo CSV.
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(stream))
                        {
                            reader.ReadLine(); // Descarte a primeira linha, se necessário.
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                var parts = line.Split(',');
                                if (parts.Length == 4 && !string.IsNullOrEmpty(parts[0]))
                                {
                                    int episodeId = int.Parse(parts[0]);
                                    var ep = await deserializeService.DeserializeEpisode(episodeId);
                                    episodes.Add(ep);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }


            var log = new LogModel()
            {
                FilePath = file.FileName,
                Id = new Guid(),
            };
            await modelRepository.Adicionar(log);

            var root = new Root()
            {
                episodes = episodes,
                TotalFemaleCharacters = modelRepository.CountTotalFemaleCharacter(episodes),
                TotalGenderlessCharacters = modelRepository.CountTotalGenderlessCharacter(episodes),
                TotalGenderUnknownCharacters = modelRepository.CountTotalGenderlessCharacter(episodes),
                TotalLocations = modelRepository.CountTotalLocation(episodes),
                TotalMaleCharacters = modelRepository.CountTotalMaleCharacter(episodes),
                UploadeFilePath = file.FileName,
            };

            return root;
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
