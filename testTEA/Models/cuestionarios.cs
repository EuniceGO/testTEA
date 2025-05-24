using System.ComponentModel.DataAnnotations;

namespace testTEA.Models
{
    public class cuestionarios
    {
        [Key]
        public int id_cuestionario { get; set; }

        public int id_paciente { get; set; }
        public int id_test { get; set; }
        public DateTime fecha_realizacion { get; set; } = DateTime.Now;
        public string etapa { get; set; } = "Principal";
        public int total_puntaje { get; set; } = 0;
        public string resultado { get; set; } = "Sin Resultado";

    }
}
