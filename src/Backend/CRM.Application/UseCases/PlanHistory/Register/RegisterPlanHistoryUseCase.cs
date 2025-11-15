using CRM.Application.UseCases.Plan.Register;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.PlanHistory;
using CRM.Exceptions.ExceptionsBase;
using Mapster;

namespace CRM.Application.UseCases.PlanHistory.Register;

public class RegisterPlanHistoryUseCase : IRegisterPlanHistoryUseCase
{
    private readonly IPlanHistoryReadOnlyRepository _readOnlyRepository;
    private readonly IPlanHistoryWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;

    public RegisterPlanHistoryUseCase(IPlanHistoryReadOnlyRepository readOnlyRepository, 
                                      IPlanHistoryWriteOnlyRepository writeOnlyRepository,
                                      IUnityOfWork unityOfWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResponsePlanHistoryJson> Execute(RequestRegisterPlanHistoryJson request)
    {
        await Validate(request);

        var planHistory = request.Adapt<Domain.Entities.PlanHistory>();
        planHistory.Id = Guid.NewGuid();
        planHistory.CreatedAt = DateTime.UtcNow;
        planHistory.UpdatedAt = DateTime.UtcNow;

        await _writeOnlyRepository.Add(planHistory);
        await _unityOfWork.Commit();

        return planHistory.Adapt<ResponsePlanHistoryJson>();
    }


    private async Task Validate(RequestRegisterPlanHistoryJson request)
    {
        var validator = new RegisterPlanHistoryValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }

}
