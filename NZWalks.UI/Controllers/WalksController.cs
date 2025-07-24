using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class WalksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WalksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<WalkDto> response = new List<WalkDto>();
            try
            {
                //Get all Regions from Web Api
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7214/api/Walks");
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<WalkDto>>());


            }
            catch (Exception ex)
            {


            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWalkViewModel addWalkViewModel)
        {
            var client = httpClientFactory.CreateClient();

            
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7214/api/Walks"),
                Content = new StringContent(JsonSerializer.Serialize(addWalkViewModel), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<WalkDto>();


            if (response != null)
            {
                return RedirectToAction("Index", "Walks");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<UpdateWalkDto>($"https://localhost:7214/api/Walks/{id.ToString()}");

            if (response != null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateWalkDto walkDto)
        {

            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7214/api/Walks/{walkDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(walkDto), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(request);
            httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<UpdateWalkDto>();

            if (response != null)
            {
                return RedirectToAction("Edit", "Walks");

            }

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Delete(WalkDto walkDto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7214/api/Walks/{walkDto.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Walks");
            }
            catch (Exception ex)
            {


            }

            return View("Edit");
        }
    }
}
