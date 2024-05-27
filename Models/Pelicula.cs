using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet.Models;

public class Pelicula
{
    [Display(Name = "Id")]
    public int? PeliculaId { get; set; }

   [Required(ErrorMessage = "El campo {0} es oblicagorio.")]
   [DataType(DataType.MultilineText)]
   public string Sinopsis { get; set; } = "sin sinopsis";

   [Required(ErrorMessage = "El campo {0} es obligatorio.")]
   [Range(1950,2024, ErrorMessage = "El valor del campo {0} debeestar entre {1} y {2}.")]
   [Display(Name = "Año")]
   public int Anio { get; set; }    

   [Required(ErrorMessage = "El campo {0} es obligatorio.")]
   [Remote(action: "ValidaPoster", controller: "Peliculas", ErrorMessage = "El campo {0} debe ser una direccion URL valida o N/A")]
   public string Poster {get; set;} = "N/A";

   [Display(Name = "Eliminable")]
   public bool Portegida { get; set; }
   public ICollection<Categoria>? Categorias { get; set; }
}