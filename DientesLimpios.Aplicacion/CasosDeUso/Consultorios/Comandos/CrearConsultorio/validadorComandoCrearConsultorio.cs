using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class validadorComandoCrearConsultorio: AbstractValidator<ComandoCrearConsultorio>
    {
        public validadorComandoCrearConsultorio()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre del consultorio es obligatorio.")
                .MaximumLength(150).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}"); 
        }
    }
}
