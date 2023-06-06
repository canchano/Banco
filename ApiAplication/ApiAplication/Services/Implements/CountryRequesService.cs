using System;
using ApiAplication.Domain.DTO;
using ApiAplication.Domain.Request;
using ApiAplication.Services.Interfaces;
using Newtonsoft.Json;

namespace ApiAplication.Services.Implements
{
    public class CountryRequesService : ICountryRequesService
    {
        private HttpClient client;
        private readonly CountryClientConfig config;
        public CountryRequesService(HttpClient _client, CountryClientConfig _config)
        {
            client = _client;
            config = _config;
            SetClientConfiguration(client);
        }

        private void SetClientConfiguration(HttpClient _client)
        {
            _client.BaseAddress = new Uri(config.BaseAdress);
            client = _client;
        }

        private async Task<IList<Country>> GetDeserializedResponseAsync(HttpResponseMessage message)
        {
            IList<Country> response = new List<Country>();
            if (message.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    response = JsonConvert.DeserializeObject<IList<Country>>(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return response;
        }
        public async Task<IList<Country>> GetRequestAsync(string url)
        {
            return await GetDeserializedResponseAsync(await client.GetAsync(url).ConfigureAwait(false));
        }
    }
}

