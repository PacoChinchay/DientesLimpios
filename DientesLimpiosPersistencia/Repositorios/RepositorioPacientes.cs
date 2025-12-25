using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Persistencia.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class RepositorioPacientes : Repositorio<Paciente>, IRepositorioPacientes
    {
        private readonly DientesLimpiosDbContext _context;
        public RepositorioPacientes(DientesLimpiosDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro)
        {
            return await _context.Pacientes.OrderBy(x => x.Nombre).Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync();
        }
    }
}
