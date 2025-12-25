namespace DientesLimpios.API.Utilidades
{
    public static class HttpContextExtensions
    {
        public static void InsertarPaginacionEnCabecera(this HttpContext httpContext, int cantidadTotalregistros)
        {
            httpContext.Response.Headers.Append("cantidad-total-registros", cantidadTotalregistros.ToString());
        }
    }
}
