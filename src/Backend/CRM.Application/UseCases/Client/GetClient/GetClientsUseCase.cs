using CRM.Communication.Responses;
using CRM.Communication.Responses.Client;
using CRM.Domain.Repositories.Client;
using CRM.Domain.Services.LoggedUser;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace CRM.Application.UseCases.Client.GetClient;

public class GetClientsUseCase : IGetClientsUseCase
{
    private readonly IClientReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetClientsUseCase(IClientReadOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseClientJson> Execute(Guid id)
    {
        var client = await Validate(id);

        return _mapper.Map<ResponseClientJson>(client);
    }


    private async Task<Domain.Entities.Client> Validate(Guid id)
    {
        var loggedUser = await _loggedUser.User();

        var client = await _repository.GetClient(loggedUser, id);

        if (client is null)
        {
            throw new CRMException(ResourceMessageException.CLIENT_NOT_FOUND);
        }

        return client;
    }
}
