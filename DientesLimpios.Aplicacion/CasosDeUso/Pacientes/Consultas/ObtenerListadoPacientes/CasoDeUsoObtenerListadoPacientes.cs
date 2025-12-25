using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes, List<PacienteListadoDTO>>
    {
        private readonly IRepositorioPacientes _repositorio;
        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<List<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            var pacientes = await _repositorio.ObtenerTodos();
            var pacientesDto = pacientes.Select(p => p.ADto()).ToList();
            return pacientesDto;
        }
    }
}
