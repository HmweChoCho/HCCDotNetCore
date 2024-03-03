using HCCDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HCCDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        private readonly string _apiUrl = "https://localhost:7025/api/Blog";
        public async void Run()
        {
            await Read();
            await Edit(4);
            await Create("test title", "test author", "test content");
            await Update(5, "test title 6", "test author 6", "test content 6");
            await Delete(4);
        }
        public async Task Read()
        {
            RestRequest request = new RestRequest(_apiUrl, Method.Get);
            RestClient client = new RestClient();
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content!;
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(json)!;
                foreach (BlogModel item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Edit(int id)
        {
            string url = $"{_apiUrl}/{id}";
            RestRequest request = new RestRequest(url, Method.Get);
            RestClient client = new RestClient();
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content!;
                BlogModel? item = JsonConvert.DeserializeObject<BlogModel>(json)!;

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Create(string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestRequest request = new RestRequest(_apiUrl, Method.Post);
            request.AddJsonBody(blog);
            RestClient client = new RestClient();
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Update(int id, string title, string author, string content)
        {
            string url = $"{_apiUrl}/{id}";

            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestRequest request = new RestRequest(url, Method.Put);
            request.AddJsonBody(blog);
            RestClient client = new RestClient();
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Delete(int id)    
        {
            string url = $"{_apiUrl}/{id}";
            RestRequest request = new RestRequest(url, Method.Put);
            RestClient client = new RestClient();
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }
    }
}
