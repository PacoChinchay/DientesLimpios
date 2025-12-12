using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            Assert.ThrowsExactly<ExcepcionDeReglaDeNegocio>(() => new Email(null!));
        }

        [TestMethod]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            Assert.ThrowsExactly<ExcepcionDeReglaDeNegocio>(() => new Email("paco.com"));
        }
    }
}
