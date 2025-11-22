using CRM.Communication.Requests.Client;
using CRM.Communication.Responses.Client;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Address;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.Client.Register;

public class RegisterClientUseCase : IRegisterClientUseCase
{
    private readonly IClientReadOnlyRepository _readOnlyRepository;
    private readonly IClientWriteOnlyRepository _writeOnlyRepository;
    private readonly IAddressWriteOnlyRepository _addressWriteOnly;
    private readonly IUnityOfWork _unityOfWork;
    private readonly ILoggedUser _loggedUser;

    public RegisterClientUseCase(
        IClientWriteOnlyRepository writeOnlyRepository,
        IClientReadOnlyRepository readOnlyRepository,
        IAddressWriteOnlyRepository addressWriteOnly,
        IUnityOfWork unityOfWork,
        ILoggedUser loggedUser)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _addressWriteOnly = addressWriteOnly;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseClientJson> Execute(RequestClientJson request)
    {
        await Validate(request);

        var client = request.Adapt<Domain.Entities.Client>();

        if (request.Address != null)
        {
            var address = request.Address.Adapt<Domain.Entities.Address>();
            address.TenantId = request.TenantId;

            await _addressWriteOnly.Add(address);

            client.AddressId = address.Id;
            client.Address = address;
        }

        await _writeOnlyRepository.Add(client);
        await _unityOfWork.Commit();

        return client.Adapt<ResponseClientJson>();
    }

    private async Task Validate(RequestClientJson request)
    {
        var validator = new RegisterClientValidator();
        var result = validator.Validate(request);

        var clientExists = await _readOnlyRepository.ExistsClientWhiteDocument(request.Document);
        if (clientExists)
        {
            throw new CRMException(ResourceMessageException.CLIENT_ALREDY_EXISTS);
        }

        if (!result.IsValid)
        {
            throw new ErrorOnValidationException(
                result.Errors
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToList()
            );
        }
    }
}
