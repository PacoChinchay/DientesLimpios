using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    public class CasoDeUsoObtenerListadoConsultorios : IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>
    {
        private readonly IRepositorioConsultorios _repositorio;

        public CasoDeUsoObtenerListadoConsultorios(IRepositorioConsultorios repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ConsultorioListadoDTO>> Handle(ConsultaObtenerListadoConsultorios request)
        {
            var consultorios = await _repositorio.ObtenerTodos();
            var consultoriosDTO = consultorios.Select(c => c.ADto()).ToList();
            return consultoriosDTO;
        }
    }
}
