using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        public void Constructor_FechaInicioPosteriorFechaFin_LanzaExcepcion()
        {
            Assert.ThrowsExactly<ExcepcionDeReglaDeNegocio>(() => new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1)));
        }

        [TestMethod]
        public void Contructor_ParametrosValidos_NoLanzaExcepcion()
        {
            new IntervaloDeTiempo(DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30));
        }
    }
}
