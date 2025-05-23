using System.ComponentModel.DataAnnotations;

namespace testTEA.Models
{
    public class preguntas
    {
        [Key]
        public int id_pregunta { get; set; }

        public int id_test { get; set; }    
        public int numero_pregunta { get; set; }
        public string texto { get; set; }
    }
}
