using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.EliminarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoEliminarConsultorioTests
    {
        private IRepositorioConsultorios _repositorio;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CasoDeUsoEliminarConsultorio _casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            _repositorio = Substitute.For<IRepositorioConsultorios>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _casoDeUso = new CasoDeUsoEliminarConsultorio(_repositorio, _unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorrarConsultorioYPersiste()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoEliminarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            _repositorio.ObtenerPorId(id).Returns(consultorio);

            await _casoDeUso.Handle(comando);
            await _repositorio.Received(1).Borrar(consultorio);
            await _unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        public async Task Handle_CuandoCosnultorioNoExiste_lanzaExcepcionNoEncontrado()
        {
            var comando = new ComandoEliminarConsultorio { Id = Guid.NewGuid() };
            _repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            await Assert.ThrowsExactlyAsync<ExcepcionNoEncontrado>(async () => {
                await _casoDeUso.Handle(comando);
            });
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_LlamaReversarYLanzaExcepcion()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoEliminarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            _repositorio.ObtenerPorId(id).Returns(consultorio);

            _repositorio.Borrar(consultorio).Throws(new InvalidOperationException("fallo al eliminar"));

            await Assert.ThrowsExactlyAsync<InvalidOperationException>(() => _casoDeUso.Handle(comando));
            await _unidadDeTrabajo.Received(1).Reversar();
        } 
    }
}
