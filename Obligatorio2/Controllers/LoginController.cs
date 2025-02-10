using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obligatorio2.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NuevoMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("https://localhost:7219"); // Reemplaza con tu URL base
        }

        [HttpGet]
        public IActionResult Login() 
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credenciales credenciales)
        {
            string jsonbody = JsonSerializer.Serialize(credenciales);
            var content = new StringContent(jsonbody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/login", content);
            var status = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                // Deserializa el JSON a un objeto si es necesario
               

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // set camelCase       
                    WriteIndented = true                                // write pretty json
                };
                
                UsuarioDto usuarioDto = JsonSerializer.Deserialize<UsuarioDto>(respuesta, options);
                HttpContext.Session.SetString("usuario", usuarioDto.email);
                HttpContext.Session.SetString("token", usuarioDto.token);
                HttpContext.Session.SetString("rol", usuarioDto.rol);
                Console.WriteLine(HttpContext.Session.GetString("usuario"));
                Console.WriteLine(HttpContext.Session.GetString("token"));
                return RedirectToAction("OpcionesUsuario");
            }
            TempData["MensajeError"] = status;
            return View();
        }

        public IActionResult OpcionesUsuario()
        {
            if (HttpContext.Session.GetString("rol") != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
