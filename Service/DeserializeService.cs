using Interview_API.Interface;
using Interview_API.Models;
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
            try
            {
                foreach (var item in characters)
                {
                    string url = item;
                    var conteudo = await Deserialize(url);
                    var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(conteudo);

                    var location = await DeserializeLocation(characterResponse.location.url);
                    var origin = JsonConvert.DeserializeObject<CharacterResponse>(await Deserialize(characterResponse.origin.url));


                    var character = new Character()
                    {
                        Gender = characterResponse.gender,
                        Id = characterResponse.id,
                        Name = characterResponse.name,
                        Species = characterResponse.species,
                        Status = characterResponse.status,
                        Type = characterResponse.type,
                        Location = new Location()
                        {
                            dimension = location.dimension,
                            id = location.id,
                            name = location.name,
                            type = location.type
                        },
                        Origin = new Origin()
                        {
                            dimension = origin.dimension,
                            id = origin.id,
                            name = origin.name,
                            type = origin.type,
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
                var caracters = await DeserializeCharacter(episodeInfo.characters);
                var episode = new Episode()
                {
                    air_date = episodeInfo.air_date,
                    characters = caracters,
                    episode = episodeInfo.episode,
                    id = episodeInfo.id
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
