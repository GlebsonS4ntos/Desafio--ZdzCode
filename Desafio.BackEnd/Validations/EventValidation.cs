using Desafio.BackEnd.Dtos;
using FluentValidation;

namespace Desafio.BackEnd.Validations
{
    public class EventValidation : AbstractValidator<EventDto>
    {
        public EventValidation() 
        {
            RuleFor(e => e.Date)
                .NotNull().NotEmpty().WithMessage("A data do evento é obrigatória.")
                .Must(birthDate => birthDate != null && birthDate.Value.Kind == DateTimeKind.Utc).WithMessage("A data deve estar em formato válido e ser UTC.")
                .Custom((date, context) =>
                {
                    if (date.HasValue) // Verifica se o valor não é nulo
                    {
                        var dateNow = DateTime.UtcNow;
                        if (date.Value <= dateNow)
                        {
                            context.AddFailure("A data do evento deve ser maior que a data atual.");
                        }
                    }
                });
        

            RuleFor(e => e.Name)
                .NotEmpty().NotNull().WithMessage("O campo Nome é obrigatório")
                .MinimumLength(3).WithMessage("O campo Nome deve possuir no minimo 3 Caracteres.")
                .MaximumLength(100).WithMessage("O campo Nome dever possuir no maximo 100 Caracteres.");

            RuleFor(e => e.Place)
                .NotEmpty().NotNull().WithMessage("O campo Lugar é obrigatório")
                .MinimumLength(3).WithMessage("O campo Lugar deve possuir no minimo 5 Caracteres.")
                .MaximumLength(100).WithMessage("O campo Lugar dever possuir no maximo 50 Caracteres.");

            RuleFor(e => e.PanelistId)
                .NotEmpty().NotNull().WithMessage("O campo Palestrante é obrigatório");
        }
    }
}
