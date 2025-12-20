using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.EliminarConsultorio
{
    public class CasoDeUsoEliminarConsultorio : IRequestHandler<ComandoEliminarConsultorio>
    {

        private readonly IRepositorioConsultorios _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public CasoDeUsoEliminarConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(ComandoEliminarConsultorio request)
        {
            var consultorio = await _repositorio.ObtenerPorId(request.Id);

            if(consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                await _repositorio.Borrar(consultorio);
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
