using System.ComponentModel.DataAnnotations;

namespace testTEA.Models
{
    public class tests
    {
        [Key]
        public int id_test { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

          
    }
}
