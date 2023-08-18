using ArchitectCodingChallenge.Domain.Errors;
using ArchitectCodingChallenge.Domain.Models;
using FluentValidation;

namespace ArchitectCodingChallenge.Domain.Validators;

/// <summary>
/// Represents the validation rules for the class <see cref="PersonModel"/>.
/// </summary>
public class PersonModelValidator : AbstractValidator<PersonModel> {
    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="PersonModelValidator"/>.
    /// </summary>
    public PersonModelValidator() {
        RuleFor(p => p.PersonId).GreaterThanOrEqualTo(0).WithMessage(DomainErrors.Person.IdIsNegative.Message);
        RuleFor(p => p.FirstName).NotEmpty().WithMessage(DomainErrors.Person.NameIsNullEmptyOrWhiteSpace.Message);
        RuleFor(p => p.LastName).NotEmpty().WithMessage(DomainErrors.Person.LastNameIsNullEmptyOrWhiteSpace.Message);
        RuleFor(p => p.CurrentRole).NotEmpty().WithMessage(DomainErrors.Person.CurrentRoleIsNullEmptyOrWhiteSpace.Message);
        RuleFor(p => p.Country).NotEmpty().WithMessage(DomainErrors.Person.CountryIsNullEmptyOrWhiteSpace.Message);
        RuleFor(p => p.Industry).NotEmpty().WithMessage(DomainErrors.Person.IndustryIsNullEmptyOrWhiteSpace.Message);
        RuleFor(p => p.NumberOfRecommendations).GreaterThanOrEqualTo(0).WithMessage(DomainErrors.Person.NumberOfRecomendationsIsNegative.Message);
        RuleFor(p => p.NumberOfConnections).GreaterThanOrEqualTo(0).WithMessage(DomainErrors.Person.NumberOfConnectionsIsNegative.Message);
    }
    #endregion
}
