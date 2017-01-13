using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Disbott.Models.Objects;

namespace Disbott.Controllers
{
    public static class GiphyController
    {
        /// <summary>
        /// Calls the Gipy api and the returns a JSON string
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static async Task<string> GetRandomGif(string search)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://api.giphy.com/v1/gifs/random");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string result = null;
            HttpResponseMessage response = await client.GetAsync($"?api_key=dc6zaTOxFJmzC&tag={search}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            Gif gif = JsonConvert.DeserializeObject<Gif>(result);

            return gif.data["image_url"];
        }
    }
}
