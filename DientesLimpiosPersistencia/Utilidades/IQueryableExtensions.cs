using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Utilidades
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, int pagina, int registrosPorPagina)
        {
            var paginaActual = pagina == 0 ? 1 : pagina;

            if (paginaActual < 1) paginaActual = 1;

            return queryable
                .Skip((paginaActual - 1) * registrosPorPagina)
                .Take(registrosPorPagina);
        }
    }
}
