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
using static System.Net.Mime.MediaTypeNames;

namespace NuevoMVC.Controllers
{
    public class EventosController : Controller
    {
        private readonly HttpClient _httpClient;

        public EventosController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:7219"); // Reemplaza con tu URL base
        }

        public async Task<IActionResult> Index(int? id, string nombre, DateTime fecha1, DateTime fecha2, int? puntaje1, int? puntaje2)
        {
            string token = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            EventosViewModel viewM = new EventosViewModel();
            viewM.Eventos = new List<Evento>();
            if (id == null && fecha1 == DateTime.MinValue && fecha2 == DateTime.MinValue && puntaje1 == null && puntaje2 == null)
            {
                var respuesta = await _httpClient.GetAsync($"/eventos/buscarpornombre?nombre={nombre}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var eventos = await respuesta.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                        WriteIndented = true                                // write pretty json
                    };

                    viewM.Eventos = JsonSerializer.Deserialize<List<Evento>>(eventos, options);
                    return View(viewM);
                }
                else
                {
                    ViewBag.Message = respuesta.StatusCode;
                    return View(viewM);
                }
            } else if (id != null)
            {
                var respuesta = await _httpClient.GetAsync($"/eventos/BuscarPorIdDisciplina?id={id}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var eventos = await respuesta.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                        WriteIndented = true                                // write pretty json
                    };

                    viewM.Eventos = JsonSerializer.Deserialize<List<Evento>>(eventos, options);
                    return View(viewM);
                }
                else
                {
                    ViewBag.Message = respuesta.StatusCode;
                    return View(viewM);
                }
            } else if (fecha1 != DateTime.MinValue || fecha2 != DateTime.MinValue)
            {
                if (fecha1 == DateTime.MinValue || fecha2 == DateTime.MinValue)
                {
                    ViewBag.Message = "BadRequest";
                    return View(viewM);
                }
                var respuesta = await _httpClient.GetAsync($"/eventos/buscarporfechas?fechaInicio={fecha1}&fechaFin={fecha2}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var eventos = await respuesta.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                        WriteIndented = true                                // write pretty json
                    };
                    viewM.Eventos = JsonSerializer.Deserialize<List<Evento>>(eventos, options);
                    return View(viewM);
                }
                else
                {
                    ViewBag.Message = respuesta.StatusCode;
                    return View(viewM);
                }
            } else
            {
                if (puntaje1 == null || puntaje2 == null)
                {
                    ViewBag.Message = "BadRequest";
                    return View(viewM);
                }
                var respuesta = await _httpClient.GetAsync($"/eventos/buscarporpuntajes?puntaje1={puntaje1}&puntaje2={puntaje2}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var eventos = await respuesta.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                        WriteIndented = true                                // write pretty json
                    };
                    viewM.Eventos = JsonSerializer.Deserialize<List<Evento>>(eventos, options);
                    return View(viewM);
                }
                else
                {
                    ViewBag.Message = respuesta.StatusCode;
                    return View(viewM);
                }
            }         
            return View(viewM);
        }

        [HttpPost]
        public async Task<IActionResult> GetEventosPorDisciplina(int id)
        {
            return RedirectToAction("Index", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> GetEventosPorNombre(string nombre)
        {
            return RedirectToAction("Index", new { nombre });
        }

        public async Task<IActionResult> GetEventosPorFechas(DateTime fecha1, DateTime fecha2)
        {
            return RedirectToAction("Index", new { fecha1, fecha2 });
        }

        public async Task<IActionResult> GetEventosPorPuntajes(int puntaje1, int puntaje2)
        {
            return RedirectToAction("Index", new { puntaje1, puntaje2 });
        }
    }
}
