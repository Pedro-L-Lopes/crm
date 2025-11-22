
using CRM.Application.UseCases.Client.UpdateClient;
using CRM.Communication.Requests.Client;
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Repositories.User;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;

namespace CRM.Application.UseCases.Client.UpdateAgent;

public class UpdateClientAgentUseCase : IUpdateClientAgentUseCase
{
    private readonly IClientReadOnlyRepository _clientReadOnlyRepository;
    private readonly IClientUpdateOnlyRepository _clientUpdateOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateClientAgentUseCase(
        IClientUpdateOnlyRepository clientUpdateOnlyRepository,
        IClientReadOnlyRepository clientReadOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnityOfWork unityOfWork,
        ILoggedUser loggedUser)
    {
        _clientReadOnlyRepository = clientReadOnlyRepository;
        _clientUpdateOnlyRepository = clientUpdateOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestClientUpdateAgentJson request, string clientId)
    {
        Validate(request);

        var agentId = Guid.Parse(request.AgentId);

        var loggedUser = await _loggedUser.User();

        var user = await _userReadOnlyRepository.GetUserByIdentifier(agentId);
        if (user is null)
            throw new NotFoundException(ResourceMessageException.USER_NOT_FOUND);

        var client = await _clientUpdateOnlyRepository.GetClient(loggedUser, Guid.Parse(clientId));
        if (client is null)
            throw new NotFoundException(ResourceMessageException.CLIENT_NOT_FOUND);

        _clientUpdateOnlyRepository.UpdateAgent(client, agentId);
        await _unityOfWork.Commit();
    }



    private static void Validate(RequestClientUpdateAgentJson request)
    {
        var result = new UpdateClientAgentUseValidator().Validate(request);

        if (result.IsValid == false)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
    }

   
}
