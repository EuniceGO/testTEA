using System.ComponentModel.DataAnnotations;

namespace testTEA.Models
{
    public class respuestas
    {
        [Key]
        public int id_respuesta { get; set; }
        public int id_cuestionario { get; set; }
        public int id_pregunta { get; set; }

        public string respuesta_usuario { get; set; }

        public int riesgo_detectado { get; set; }
        
    }
}
