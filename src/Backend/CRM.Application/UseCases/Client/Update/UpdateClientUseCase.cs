using CRM.Application.UseCases.Client.Register;
using CRM.Communication.Requests.Client;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Address;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.Client.UpdateClient;

public class UpdateClientUseCase : IUpdateClientUseCase
{

    private readonly IClientReadOnlyRepository _clientReadOnlyRepository;
    private readonly IClientUpdateOnlyRepository _clientUpdateOnlyRepository;
    private readonly IAddressUpdateOnlyRepository _addressUpdateOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateClientUseCase(
        IClientUpdateOnlyRepository clientUpdateOnlyRepository,
        IClientReadOnlyRepository clientReadOnlyRepository,
        IAddressUpdateOnlyRepository addressUpdateOnlyRepository,
        IUnityOfWork unityOfWork,
        ILoggedUser loggedUser)
    {
        _clientReadOnlyRepository = clientReadOnlyRepository;
        _clientUpdateOnlyRepository = clientUpdateOnlyRepository;
        _addressUpdateOnlyRepository = addressUpdateOnlyRepository;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestClientJson request, Guid clientId)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var client = await _clientUpdateOnlyRepository.GetClient(loggedUser, clientId);

        if (client is null)
            throw new CRMException(ResourceMessageException.CLIENT_NOT_FOUND);

        client.Name = request.Name;
        client.Email = request.Email;
        client.Phone = request.Phone;
        client.Document = request.Document;
        client.SecondDocument = request.SecondDocument;

        client.Type = request.Type;
        client.Notes = request.Notes;
        client.BirthDate = request.BirthDate;
        client.Occupation = request.Occupation;
        client.Income = request.Income;
        client.Gender = request.Gender;

        if (request.Address != null)
        {
            if (client.Address != null)
            {
                client.Address.ZipCode = request.Address.ZipCode;
                client.Address.Street = request.Address.Street;
                client.Address.Number = request.Address.Number;
                client.Address.Complement = request.Address.Complement;
                client.Address.Neighborhood = request.Address.Neighborhood;
                client.Address.City = request.Address.City;
                client.Address.State = request.Address.State;
                client.Address.Latitude = request.Address.Latitude;
                client.Address.Longitude = request.Address.Longitude;

                _addressUpdateOnlyRepository.Update(client.Address);
            }
            else
            {
                var newAddress = request.Address.Adapt<Domain.Entities.Address>();
                newAddress.TenantId = client.TenantId;

                client.Address = newAddress;
                client.AddressId = newAddress.Id;
            }
        }

        _clientUpdateOnlyRepository.Update(client);
        await _unityOfWork.Commit();
    }



    private static void Validate(RequestClientJson request)
    {
        var result = new UpdateClientUseValidator().Validate(request);

        if (result.IsValid == false)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
    }
}
