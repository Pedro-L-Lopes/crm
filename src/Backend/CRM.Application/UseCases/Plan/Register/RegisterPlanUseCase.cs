using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Repositories.Plan;
using CRM.Domain.Repositories;
using CRM.Exceptions.ExceptionsBase;
using CRM.Exceptions;
using Mapster;

namespace CRM.Application.UseCases.Plan.Register;
public class RegisterPlanUseCase : IRegisterPlanUseCase
{
    private readonly IPlanWriteOnlyRepository _writeOnlyRepository;
    private readonly IPlanReadOnlyRepository _readOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;

    public RegisterPlanUseCase(
        IPlanWriteOnlyRepository writeOnlyRepository,
        IPlanReadOnlyRepository readOnlyRepository,
        IUnityOfWork unityOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResponseRegisterPlanJson> Execute(RequestRegisterPlanJson request)
    {
        await Validate(request);

        var plan = request.Adapt<Domain.Entities.Plan>();
        plan.Id = Guid.NewGuid();
        plan.IsActive = true;

        await _writeOnlyRepository.Add(plan);
        await _unityOfWork.Commit();

        return plan.Adapt<ResponseRegisterPlanJson>();
    }

    private async Task Validate(RequestRegisterPlanJson request)
    {
        var validator = new RegisterPlanValidator();
        var result = validator.Validate(request);

        //var planNameExists = await _readOnlyRepository.ExistsWithNameAsync(request.Name);
        //if (planNameExists)
        //{
        //    result.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.Name), ResourceMessageException.PLAN_NAME_ALREADY_EXISTS));
        //}

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}