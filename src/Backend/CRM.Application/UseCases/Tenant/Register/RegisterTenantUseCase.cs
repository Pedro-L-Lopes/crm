using CRM.Application.Services.Cryptography;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Entities;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.Tenant.Register;
public class RegisterTenantUseCase : IRegisterTenantUseCase
{
    private readonly ITenantWriteOnlyRepository _writeOnlyRepository;
    private readonly ITenantReadOnlyRepository _readOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;

    public RegisterTenantUseCase(ITenantWriteOnlyRepository writeOnlyRepository, ITenantReadOnlyRepository readOnlyRepository, IUnityOfWork unityOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResponseRegisterTenantJson> Execute(RequestRegisterTenantJson request)
    {
        await Validate(request);

        var tenant = request.Adapt<Domain.Entities.Tenant>();

        tenant.Id = Guid.NewGuid();

        await _writeOnlyRepository.Add(tenant);

        await _unityOfWork.commit();

        return new ResponseRegisterTenantJson
        {
            Name = request.Name,
        };
    }

    private async Task Validate(RequestRegisterTenantJson request)
    {
        var validator = new RegisterTenantValidator();

        var result = validator.Validate(request);

        var emailExist = await _readOnlyRepository.ExistActiveTenantWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageException.EMAIL_ALREDY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
