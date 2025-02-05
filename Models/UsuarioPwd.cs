using System.ComponentModel.DataAnnotations;    

namespace frontendnet.Models;

public class UsarioPwd
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    [Display(Name = "Correo electronico")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [MinLength(6,ErrorMessage = "El campo {0} debe tener un minimo de {1} caracteres.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public required string Password { get; set; }

    [Required(ErrorMessage ="El campo {0} es obligatorio.")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage ="El campo {0} es obligatorio.")]
    public required string Rol { get; set; }
}