using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SHADotNetCore.ConsoleApp3
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client;
        private readonly string _endpoint = "https://jsonplaceholder.typicode.com/posts";
        public HttpClientExample()
        {
            _client = new HttpClient();
        }
        public async Task Read()
        {
            var response = await _client.GetAsync(_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            var response = await _client.GetAsync($"{_endpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found");
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Create(int userId, string title, string body)
        {
            PostModel requestModel = new PostModel()
            {
                userId = userId,
                title = title,
                body = body
            };
            var jsonRequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(_endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }
        public async Task Update(int id, int userId, string title, string body)
        {
            PostModel requestModel = new PostModel()
            {
                id = id,
                userId = userId,
                title = title,
                body = body
            };
            var jsonRequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_endpoint}/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }
        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync($"{_endpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found");
            }
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data Deleted Successfully");
            }
        }

    }


    public class PostModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}