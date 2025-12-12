using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Excepciones
{
    public class ExcepcionDeValidacion: Exception
    {
        public List<string> ErroresDeValidacion { get; set; } = [];

        public ExcepcionDeValidacion(ValidationResult validationException)
        {
            foreach (var erroresDeValidacion in validationException.Errors)
            {
                ErroresDeValidacion.Add(erroresDeValidacion.ErrorMessage);
            }
        }
    }
}
