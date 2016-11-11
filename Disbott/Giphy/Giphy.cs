﻿using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Disbott.Modules
{
    [Module]
    public class Giphy
    {
        [Command("giphy"), Description("Gets a random gif")]
        public async Task giphy(IUserMessage msg, string search = null)
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

            var value = gif.data["image_url"];

            await msg.Channel.SendMessageAsync(value);
        }
    }
}