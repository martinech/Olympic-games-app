using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Obligatorio2.Models;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Obligatorio2.Controllers
{
    public class DisciplinasController : Controller
    {
        private readonly HttpClient _httpClient;

        public DisciplinasController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:7219/"); // Reemplaza con tu URL base
        }

        public async Task<IActionResult> Index(string nombre, int? id)
        {
            string token = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            DisciplinasViewModel viewModel = new DisciplinasViewModel();
            ViewBag.Message = null;
            if (id == null)
            {
                var response = await _httpClient.GetAsync($"/disciplinas?name={nombre}");
                var status = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var disciplinas = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                        WriteIndented = true                                // write pretty json
                    };

                    viewModel.Disciplinas = JsonSerializer.Deserialize<List<Disciplina>>(disciplinas, options);
                    return View(viewModel);
                }
            }
            else if (nombre == null)
            {
                var response = await _httpClient.GetAsync($"/disciplinas/BuscarPorId?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var disciplinaJson = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Configuración para camelCase
                        WriteIndented = true                               // Formato "pretty" para el JSON
                    };

                    Disciplina disciplina = JsonSerializer.Deserialize<Disciplina>(disciplinaJson, options);
                    viewModel.Disciplinas = new List<Disciplina> { disciplina };
                    return View(viewModel);
                }
                else
                {
                    ViewBag.Mensaje = response.StatusCode;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByName(string nombre)
        {
            return RedirectToAction("Index", new { nombre });
        }

        [HttpPost]
        public async Task<IActionResult> FilterById(int id)
        {
            return RedirectToAction("Index", new { id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Disciplina());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string token = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"/disciplinas/{id}");
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,       
                    WriteIndented = true                               
                };
                Disciplina disciplina = JsonSerializer.Deserialize<Disciplina>(respuesta, options);
                return View(disciplina);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"/disciplinas/BuscarPorId?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,       
                    WriteIndented = true                                
                };
                Disciplina disciplina = JsonSerializer.Deserialize<Disciplina>(respuesta, options);
                return View(disciplina);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Disciplina disciplina)
        {
            string token = HttpContext.Session.GetString("token");
            string emailUsuario = HttpContext.Session.GetString("emailLogueado");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(disciplina), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/disciplinas", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Error " + response.StatusCode.ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Disciplina disciplina)
        {
            string token = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(disciplina), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/disciplinas/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Error " + response.StatusCode.ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Disciplina disciplina)
        {
            string token = HttpContext.Session.GetString("token");
            Console.WriteLine($"Token: {token}");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"/disciplinas/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Error " + response.StatusCode.ToString();
            return View("Error");
        }
    }
}

