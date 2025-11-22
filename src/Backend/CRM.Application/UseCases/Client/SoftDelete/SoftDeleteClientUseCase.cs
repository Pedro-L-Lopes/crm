using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;

namespace CRM.Application.UseCases.Client.SoftDelete;

public class SoftDeleteClientUseCase : ISoftDeleteClientUseCase
{
    private readonly IClientUpdateOnlyRepository _updateRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnityOfWork _unityOfWork;

    public SoftDeleteClientUseCase(
        IClientUpdateOnlyRepository repository,
        ILoggedUser loggedUser,
        IUnityOfWork unityOfWork)
    {
        _updateRepository = repository;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid clientId)
    {
        var user = await _loggedUser.User();

        var client = await _updateRepository.GetClient(user, clientId);

        if (client is null)
            throw new NotFoundException(ResourceMessageException.CLIENT_NOT_FOUND);

        await _updateRepository.SoftDelete(clientId);
        await _unityOfWork.Commit();
    }
}

