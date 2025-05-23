using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testTEA.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }

        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }

      

  
        public string? numeroSello  { get; set; } = "";

        [Required]
        public string Estado { get; set; } = "Pendiente";

        public string? telefono { get; set; }


    }
}
