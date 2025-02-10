using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Obligatorio2.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AnioDeIntegracion { get; set; }

        public Disciplina() { }
    }
}
