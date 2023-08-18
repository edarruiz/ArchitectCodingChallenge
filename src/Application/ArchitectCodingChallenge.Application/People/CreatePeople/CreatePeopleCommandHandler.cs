using System.Net;
using ArchitectCodingChallenge.Domain.Abstractions;
using ArchitectCodingChallenge.Domain.Entities;
using ArchitectCodingChallenge.Domain.Models;
using FluentValidation;
using MediatR;

namespace ArchitectCodingChallenge.Application.People.CreatePeople;

/// <summary>
/// Represents the mediator pattern for the <see cref="CreatePeopleCommand"/>.
/// </summary>
public sealed class CreatePeopleCommandHandler : IRequestHandler<CreatePeopleCommand, CreatePeopleResponse> {
    #region Fields
    /// <summary>
    /// Represents the validator for the class <see cref="PersonModel"/>.
    /// </summary>
    private IValidator<PersonModel> _validator;

    /// <summary>
    /// Represents the service for the people repository operations.
    /// </summary>
    private IPeopleRepository _peopleRepository;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="CreatePeopleCommandHandler"/>.
    /// </summary>
    /// <param name="peopleRepository"><see cref="IPeopleRepository"/> representing the people repository service.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="peopleRepository"/> is <c>null</c>.</exception>
    public CreatePeopleCommandHandler(
        IPeopleRepository peopleRepository,
        IValidator<PersonModel> validator
    ) {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(peopleRepository));
    }
    #endregion

    #region IRequestHandler
    /// <inheritdoc/>
    public async Task<CreatePeopleResponse> Handle(CreatePeopleCommand request, CancellationToken cancellationToken) {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var response = new CreatePeopleResponse();

        foreach (var person in request.People) {
            var validationResult = _validator.Validate(person);

            if (!validationResult.IsValid) {
                foreach (var error in validationResult.Errors) {
                    response.Results.Add(new() {
                        PersonId = person.PersonId,
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = error.ErrorMessage
                    });
                }
                continue;
            } else {
                var newPerson = Person.Create(
                    Guid.NewGuid(),
                    person.PersonId,
                    person.FirstName,
                    person.LastName,
                    person.CurrentRole,
                    person.Country,
                    person.Industry,
                    person.NumberOfRecommendations,
                    person.NumberOfConnections
                );

                if (newPerson.IsSuccess) {
                    var result = await _peopleRepository.AddPerson(newPerson.Value);
                    if (result is not null) {
                        response.Results.Add(new() {
                            PersonId = newPerson.Value.PersonId!.Value,
                            StatusCode = HttpStatusCode.Created,
                            Message = "Created successfully."
                        });
                    } else {
                        response.Results.Add(new() {
                            PersonId = newPerson.Value.PersonId!.Value,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = $"Failed: This PersonId already exists in the list."
                        });
                    }
                }

                if (newPerson.IsFailed) {
                    response.Results.Add(new() {
                        PersonId = newPerson.Value.PersonId!.Value,
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = $"Failed: {newPerson.Errors[0].Message}"
                    });
                }
            }
        }
        _peopleRepository.SaveChanges();

        return response;
    }
    #endregion
}
