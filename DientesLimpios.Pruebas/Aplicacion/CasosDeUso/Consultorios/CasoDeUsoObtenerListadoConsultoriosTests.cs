using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerListadoConsultoriosTests
    {
        private IRepositorioConsultorios _repositorio;
        private CasoDeUsoObtenerListadoConsultorios _casoDeUso;

        [TestInitialize]
        public void Setup()
        {
            _repositorio = Substitute.For<IRepositorioConsultorios>();
            _casoDeUso = new CasoDeUsoObtenerListadoConsultorios(_repositorio);
        }

        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultoriosListadoDTO()
        {
            var consultorios = new List<Consultorio>
                {
                    new Consultorio("Consultorio A"),
                };

            _repositorio.ObtenerTodos().Returns(consultorios);

            var esperado = consultorios.Select(c => new ConsultorioListadoDTO { Id = c.Id, Nombre = c.Nombre }).ToList();
            var resultado = await _casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.AreEqual(esperado.Count, resultado.Count);

            for (int i = 0; i < esperado.Count; i++)
            {
                Assert.AreEqual(esperado[i].Id, resultado[i].Id);
                Assert.AreEqual(esperado[i].Nombre, resultado[i].Nombre);
            }
        }

        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia()
        {
            _repositorio.ObtenerTodos().Returns(new List<Consultorio>());

            var resultado = await _casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
