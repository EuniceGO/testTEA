using System.ComponentModel.DataAnnotations;

namespace testTEA.Models
{
    public class pacientes
    {
        [Key]
        public int id_paciente { get; set; }
        public int id_usuario { get; set;  }
        public string nombre { get; set; }

        public int edad_meses { get; set; }
        public string genero { get; set; }
        public DateTime fecha_nacimiento { get; set; }
    }
}
