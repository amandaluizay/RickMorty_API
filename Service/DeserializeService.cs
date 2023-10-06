using Interview_API.Interface;
using Interview_API.Models;
using Interview_API.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Interview_API.Service
{
    public class DeserializeService : IDeserializeService
    {
        private readonly HttpClient _httpClient;

        public DeserializeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Location>DeserializeLocation(string url)
        {
            var conteudo = await Deserialize(url);
            var location = JsonConvert.DeserializeObject<Location>(conteudo);
            return location;
        }
        public async Task<Origin> DeserializeOrigin(string url)
        {
            var conteudo = await Deserialize(url);
            var origin = JsonConvert.DeserializeObject<Origin>(conteudo);
            return origin;
        }
        public async Task<string> Deserialize(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<Character>> DeserializeCharacter(List<string> characters)
        {
            var lista = new List<Character>();
            var location = new Location();
            var origin = new Origin();
            try
            {
                foreach (var item in characters)
                {
                    string url = item;
                    var conteudo = await Deserialize(url);
                    var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(conteudo);
                    
                    location = !string.IsNullOrEmpty(characterResponse.Location.url) ? await DeserializeLocation(characterResponse.Location.url) : new Location();
                    origin = !string.IsNullOrEmpty(characterResponse.Origin.Url) ? await DeserializeOrigin(characterResponse.Origin.Url) : new Origin();

                    var character = new Character()
                    {
                        Gender = characterResponse.Gender,
                        Id = characterResponse.Id,
                        Name = characterResponse.Name,
                        Species = characterResponse.Species,
                        Status = characterResponse.Status,
                        Type = characterResponse.Type,
                        Location = new Location()
                        {
                            Dimension = location.Dimension,
                            Id = location.Id,
                            Name = location.Name,
                            Type = location.Type
                        },
                        Origin = new Origin()
                        {
                            Dimension = origin.Dimension,
                            Id = origin.Id,
                            Name = origin.Name,
                            Type = origin.Type,
                        }


                    };


                    lista.Add(character);
                }

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Episode> DeserializeEpisode(int episodeId)
        {
            try
            {
                string apiUrl = $"https://rickandmortyapi.com/api/episode/{episodeId}";


                var content = await Deserialize(apiUrl);

                var episodeInfo = JsonConvert.DeserializeObject<EpisodeResponse>(content);
                var caracters = await DeserializeCharacter(episodeInfo.Characters);
                var episode = new Episode()
                {
                    Air_date = episodeInfo.Air_date,
                    Name = episodeInfo.Name,
                    Characters = caracters,
                    _Episode = episodeInfo.episode,
                    Id = episodeInfo.Id
                };
                return episode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
