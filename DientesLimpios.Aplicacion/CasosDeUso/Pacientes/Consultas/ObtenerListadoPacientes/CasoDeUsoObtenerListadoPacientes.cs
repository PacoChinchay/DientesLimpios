using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes, PaginadoDTO<PacienteListadoDTO>>
    {
        private readonly IRepositorioPacientes _repositorio;
        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<PaginadoDTO<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            var pacientes = await _repositorio.ObtenerFiltrado(request);
            var totalPacientes = await _repositorio.ObtenerCantidadTotalRegistros();
            var pacientesDto = pacientes.Select(p => p.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<PacienteListadoDTO>
            {
                Elementos = pacientesDto,
                Total = totalPacientes,
            };

            return paginadoDTO;
        }
    }
}
