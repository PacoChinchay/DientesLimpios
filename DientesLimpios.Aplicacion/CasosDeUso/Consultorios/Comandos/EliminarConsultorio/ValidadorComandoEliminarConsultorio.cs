using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.EliminarConsultorio
{
    public class ValidadorComandoEliminarConsultorio: AbstractValidator<ComandoEliminarConsultorio>
    {
        public ValidadorComandoEliminarConsultorio()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
        }
    }
}
