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
        private IValidator<ComandoCrearConsultorio> _validator;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CasoDeUsoCrearConsultorio _casoDeUsoCrearConsultorio;

        [TestInitialize]
        public void setup()
        {
            _repositorio = Substitute.For<IrepositorioConsultorios>();
            _validator = Substitute.For<IValidator<ComandoCrearConsultorio>>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();

            _casoDeUsoCrearConsultorio = new CasoDeUsoCrearConsultorio(_repositorio, _unidadDeTrabajo, _validator);
        }

        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
        {
            var comando = new ComandoCrearConsultorio
            {
                Nombre = "Consultorio A"
            };

            _validator.ValidateAsync(comando).Returns(new ValidationResult());

            var consultorioCreado = new Consultorio("Consultorio A");
            _repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

            var resultado = await _casoDeUsoCrearConsultorio.Handle(comando);

            await _validator.Received(1).ValidateAsync(comando);
            await _repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await _unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_ComandoNoValido_LanzaExcepcion()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "" };
            var resultadoInvalido = new ValidationResult(new[] {
                new ValidationFailure("Nombre", "El nombre no puede estar vacío")
            });

            _validator.ValidateAsync(comando).Returns(resultadoInvalido);

            await Assert.ThrowsExactlyAsync<ExcepcionDeValidacion>(async () =>
            {
                await _casoDeUsoCrearConsultorio.Handle(comando);
            });

            await _repositorio.DidNotReceive().Agregar(Arg.Any<Consultorio>());
        }

        [TestMethod]
        public async Task Handle_CuandoHayError_HacemosRollback()
        {
            var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
            _repositorio.Agregar(Arg.Any<Consultorio>()).Throws<Exception>();
            _validator.ValidateAsync(comando).Returns(new ValidationResult());

            await Assert.ThrowsExactlyAsync<Exception>(async () =>
            {
                var resultado = await _casoDeUsoCrearConsultorio.Handle(comando);
            });

            await _unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
