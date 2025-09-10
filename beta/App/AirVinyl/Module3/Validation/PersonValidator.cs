using AirVinyContext.Entities;
using FluentValidation;

namespace App.AirVinyl.Module3.Validation;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Email).NotEmpty().EmailAddress();
        RuleFor(p => p.FirstName).NotEmpty();
        RuleFor(p => p.LastName).NotEmpty();
    }
}