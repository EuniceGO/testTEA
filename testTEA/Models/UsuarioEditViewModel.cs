using System.ComponentModel.DataAnnotations;

public class UsuarioEditViewModel
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }

    [DataType(DataType.Password)]
    public string? NuevaContrasena { get; set; }
}
