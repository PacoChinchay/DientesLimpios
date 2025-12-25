using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    public class FiltroPacienteDTO
    {
        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; } = 10;
    }
}
