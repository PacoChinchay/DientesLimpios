using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorio: IRequestHandler<ComandoCrearConsultorio, Guid>
    {
        private readonly IrepositorioConsultorios _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IValidator<ComandoCrearConsultorio> _validador;
        public CasoDeUsoCrearConsultorio(IrepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo, IValidator<ComandoCrearConsultorio> validador)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _validador = validador;
        }

        public async Task<Guid> Handle(ComandoCrearConsultorio comando)
        {
            var resultadoValidacion = await _validador.ValidateAsync(comando);
            if (!resultadoValidacion.IsValid)
            {
                throw new ExcepcionDeValidacion(resultadoValidacion);
            }

            var consultorio = new Consultorio(comando.Nombre);
            try
            {
                var respuesta = await _repositorio.Agregar(consultorio);
                await _unidadDeTrabajo.Persistir();
                return respuesta.Id;
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
