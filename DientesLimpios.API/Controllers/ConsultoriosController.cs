using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConsultoriosController(IMediator mediador)
        {
            _mediator = mediador;
        }

        [HttpGet]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id)
        {
            var consulta = new ConsultaObtenerDetalleConsultorio{ Id = id };
            var resultado = await _mediator.Send(consulta);
            return resultado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDto crearConsultorioDto)
        {
            var comando = new ComandoCrearConsultorio { Nombre = crearConsultorioDto.Nombre };
            await _mediator.Send(comando);
            return Ok();
        }
    }
}
