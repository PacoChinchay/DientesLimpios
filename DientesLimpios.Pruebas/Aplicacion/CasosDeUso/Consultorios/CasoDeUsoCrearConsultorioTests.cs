using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoCrearConsultorioTests
    {
        private IrepositorioConsultorios _repositorio;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CasoDeUsoCrearConsultorio _casoDeUsoCrearConsultorio;

        [TestInitialize]
        public void setup()
        {
            _repositorio = Substitute.For<IrepositorioConsultorios>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();

            _casoDeUsoCrearConsultorio = new CasoDeUsoCrearConsultorio(_repositorio, _unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
        {
            var comando = new ComandoCrearConsultorio
            {
                Nombre = "Consultorio A"
            };


            var consultorioCreado = new Consultorio("Consultorio A");
            _repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await _casoDeUsoCrearConsultorio.Handle(comando);

            await _repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await _unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            _repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();

            await Assert.ThrowsExactlyAsync<Exception>(async () =>
            {
                var resultado = await _casoDeUsoCrearConsultorio.Handle(comando);
            });

            await _unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
