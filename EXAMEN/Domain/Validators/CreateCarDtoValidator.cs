using FluentValidation;
using EXAMEN.DTOs.Requests;

namespace EXAMEN.Domain.Validators
{
    public class CreateCarDtoValidator : AbstractValidator<CreateCarDtoRequest>
    {
        public CreateCarDtoValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Модель автомобіля є обов'язковою.");

            RuleFor(x => x.Year)
                .InclusiveBetween(1990, DateTime.Now.Year + 1)
                .WithMessage($"Рік повинен бути від 1990 до {DateTime.Now.Year + 1}.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Ціна повинна бути більше 0.");

            RuleFor(x => x.Mileage)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Пробіг не може бути від'ємним.");
        }
    }
}