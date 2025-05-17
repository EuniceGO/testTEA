using System.ComponentModel.DataAnnotations;

namespace testTEA.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }
    }
}
