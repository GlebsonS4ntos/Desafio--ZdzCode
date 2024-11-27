using Desafio.BackEnd.Dtos;
using FluentValidation;

namespace Desafio.BackEnd.Validations
{
    public class PanelistValidation : AbstractValidator<PanelistDto>
    {
        public PanelistValidation() 
        {
            RuleFor(p => p.Name)
                .NotEmpty().NotNull().WithMessage("O campo Nome é obrigatório")
                .MinimumLength(3).WithMessage("O campo Nome deve possuir no minimo 3 Caracteres.")
                .MaximumLength(100).WithMessage("O campo Nome dever possuir no maximo 100 Caracteres.");

            RuleFor(p => p.BirthDate)
                .NotNull().NotEmpty().WithMessage("O campo Data de Aniversário é obrigatorio.")
                .Must(birthDate => birthDate != null && birthDate.Value.Kind == DateTimeKind.Utc).WithMessage("A data deve estar em formato válido e ser UTC.")
                .Custom((birthDate, context) =>
                {
                    if (birthDate.HasValue)
                    {
                        var minBirthDate = DateTime.Now.AddYears(-18);
                        if (birthDate.Value > minBirthDate)
                        {
                            context.AddFailure("O Palestrante deve possuir mais de 18 anos.");
                        }
                    }
                });

            RuleFor(p => p.Job)
                .NotNull().NotEmpty().WithMessage("O campo Trabalho é obrigatorio.")
                .MinimumLength(3).WithMessage("O campo Trabalho deve possuir no minimo 3 Caracteres.")
                .MaximumLength(70).WithMessage("O campo Trabalho dever possuir no maximo 70 Caracteres.");
        }
    }
}
