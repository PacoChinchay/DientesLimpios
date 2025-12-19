using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Consultorios
{
    public class ActualizarConsultorioDto
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
    }
}
