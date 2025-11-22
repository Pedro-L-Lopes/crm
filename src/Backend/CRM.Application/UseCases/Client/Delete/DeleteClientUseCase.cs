
using CRM.Domain.Repositories;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using System.Reflection;

namespace CRM.Application.UseCases.Client.Delete;

public class DeleteClientUseCase : IDeleteClientUseCase
{
    private readonly IClientReadOnlyRepository _readOnlyRepository;
    private readonly IClientWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteClientUseCase(
        IClientWriteOnlyRepository writeOnlyRepository,
        IClientReadOnlyRepository readOnlyRepository,
        IUnityOfWork unityOfWork,
        ILoggedUser loggedUser)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unityOfWork = unityOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id)
    {
        var loggedUser = await _loggedUser.User();

        var client = await _readOnlyRepository.GetClient(loggedUser, id);

        if (client is null)
            throw new NotFoundException(ResourceMessageException.CLIENT_NOT_FOUND);

        await _writeOnlyRepository.Delete(id);

        await _unityOfWork.Commit();
    }
}
