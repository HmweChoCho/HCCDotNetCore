using HCCDotNetCore.BirdWebApi.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace HCCDotNetCore.BirdWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        private readonly string _url = "https://burma-project-ideas.vercel.app/birds";
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<BirdDataModel> birds = JsonConvert.DeserializeObject<List<BirdDataModel>>(json)!;

                //List<BirdViewModel> lst = birds.Select(x => new BirdViewModel
                //{
                //    BirdId = x.Id,
                //    BirdName = x.BirdMyanmarName,
                //    Desp = x.Description,
                //    PhotoUrl = $"https://burma-project-ideas.vercel.app/{x.ImagePath}"
                //}).ToList();
                List<BirdViewModel> lst = birds.Select(bird => Change(bird)).ToList();

                return Ok(lst);

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{_url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                BirdDataModel bird = JsonConvert.DeserializeObject<BirdDataModel>(json)!;

                var item = Change(bird);
                return Ok(item);

            }
            else
            {
                return BadRequest();
            }
        }

        private BirdViewModel Change(BirdDataModel bird)
        {
            var item = new BirdViewModel
            {
                BirdId = bird.Id,
                BirdName = bird.BirdMyanmarName,
                Desp = bird.Description,
                PhotoUrl = $"https://burma-project-ideas.vercel.app/{bird.ImagePath}"
            };
            return item;
        }
    
        
    }
}
