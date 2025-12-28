using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.BorrarPaciente
{
    public class CasoDeUsoBorrarPaciente : IRequestHandler<ComandoBorrarPaciente>
    {
        private readonly IRepositorioPacientes _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CasoDeUsoBorrarPaciente(IRepositorioPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task Handle(ComandoBorrarPaciente request)
        {
            var paciente = await _repositorio.ObtenerPorId(request.Id);

            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                await _repositorio.Borrar(paciente);
                await _unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
