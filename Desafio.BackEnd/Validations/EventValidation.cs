using Desafio.BackEnd.Dtos;
using FluentValidation;

namespace Desafio.BackEnd.Validations
{
    public class EventValidation : AbstractValidator<EventDto>
    {
        public EventValidation() 
        {
            RuleFor(e => e.Date)
                .NotNull().WithMessage("A data do evento é obrigatória.")
                .NotEmpty().WithMessage("A data do evento é obrigatória.")
                .Custom((date, context) =>
                {
                    var dateNow = DateTime.Now;
                    if (date < dateNow)
                    {
                        context.AddFailure("A data do evento deve ser maior que a data Atual");
                    }
                });

            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("O campo Nome é obrigatório")
                .NotNull().WithMessage("O campo Nome é obrigatório")
                .MinimumLength(3).WithMessage("O campo Nome deve possuir no minimo 3 Caracteres.")
                .MaximumLength(100).WithMessage("O campo Nome dever possuir no maximo 100 Caracteres.");

            RuleFor(e => e.Place)
                .NotEmpty().WithMessage("O campo Lugar é obrigatório")
                .NotNull().WithMessage("O campo Lugar é obrigatório")
                .MinimumLength(3).WithMessage("O campo Lugar deve possuir no minimo 5 Caracteres.")
                .MaximumLength(100).WithMessage("O campo Lugar dever possuir no maximo 50 Caracteres.");

            RuleFor(e => e.PanelistId)
                .NotEmpty().WithMessage("O campo Palestrante é obrigatório")
                .NotNull().WithMessage("O campo Palestrante é obrigatório");
        }
    }
}
