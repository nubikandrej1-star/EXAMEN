using FluentValidation;
using EXAMEN.DTOs.Requests;

namespace EXAMEN.Domain.Validators
{
    public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDtoRequest>
    {
        public CreateBookingDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .WithMessage("Ім'я клієнта є обов'язковим.")
                .MinimumLength(2)
                .WithMessage("Ім'я повинно містити щонайменше 2 символи.");

            RuleFor(x => x.CustomerPhone)
                .NotEmpty()
                .WithMessage("Номер телефону є обов'язковим.")
                .Matches(@"^\+?[0-9\s\-\(\)]{10,20}$")
                .WithMessage("Некоректний номер телефону.");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty()
                .WithMessage("Email є обов'язковим.")
                .EmailAddress()
                .WithMessage("Некоректний формат email.");

            RuleFor(x => x.PreferredDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Дата бронювання повинна бути у майбутньому.");
        }
    }
}