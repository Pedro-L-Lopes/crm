using CRM.Application.UseCases.Client.Register;
using CRM.Communication.Requests.Address;
using CRM.Communication.Requests.Client;
using CRM.Communication.Responses.Address;
using CRM.Communication.Responses.Client;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Address;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.Address.Register;

public class RegisterAddressUseCase : IRegisterAddressUseCase
{
    private readonly IAddressReadOnlyRepository _readOnlyRepository;
    private readonly IAddressWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly ILoggedUser _loggedUser;

    public RegisterAddressUseCase(
        IAddressWriteOnlyRepository writeOnlyRepository,
        IAddressReadOnlyRepository readOnlyRepository,
        IUnityOfWork unityOfWork,
        ILoggedUser loggedUser)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseAddressJson> Execute(RequestAddressJson request)
    {
        await Validate(request);

        var address = request.Adapt<Domain.Entities.Address>();

        await _writeOnlyRepository.Add(address);
        await _unityOfWork.Commit();

        return address.Adapt<ResponseAddressJson>();
    }

    private async Task Validate(RequestAddressJson request)
    {
        var validator = new RegisterAddressValidator();
        var result = validator.Validate(request);

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
