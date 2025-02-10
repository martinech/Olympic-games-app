using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Obligatorio2.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NuevoMVC.Controllers
{
    public class AtletasController : Controller
    {
        private readonly HttpClient _httpClient;

        public AtletasController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:7219"); // Reemplaza con tu URL base
        }

        public async Task<IActionResult> Index(int? id)
        {
            AtletasViewModel viewM = new AtletasViewModel();
            viewM.Atletas = new List<Atleta>();
            var response = await _httpClient.GetAsync($"/atletas?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var atletas = await response.Content.ReadAsStringAsync();
                AtletasViewModel viewModel = new AtletasViewModel();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                    WriteIndented = true                                // write pretty json
                };

                viewModel.Atletas = JsonSerializer.Deserialize<List<Atleta>>(atletas, options);
                return View(viewModel);
            }
            else
            {
                ViewBag.Message = response.StatusCode;
            }
            return View(viewM);
        }

        [HttpPost]
        public async Task<IActionResult> GetAtletasPorDisciplina(int id)
        {
            return RedirectToAction("Index", new { id });
        }
    }
}
