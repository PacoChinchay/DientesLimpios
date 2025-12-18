using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Consultorios
{
    public class CrearConsultorioDto
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
    }
}
