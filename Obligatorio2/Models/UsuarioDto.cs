using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Obligatorio2.Models
{
    public class UsuarioDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
        public string token { get; set; }

        public UsuarioDto() { }
    }
}
