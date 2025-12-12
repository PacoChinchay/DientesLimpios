using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class DentistaTests
    {
        [TestMethod]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("paco@ejemplo.com");
            Assert.ThrowsExactly<ExcepcionDeReglaDeNegocio>(() => new Dentista(null!, email));
        }

        [TestMethod]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Email email = null!;
            Assert.ThrowsExactly<ExcepcionDeReglaDeNegocio>(() => new Dentista("Paco", email));
        }
    }
}
