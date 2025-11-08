using CRM.Application.Services.Cryptography;
using CRM.Communication.Requests;
using CRM.Communication.Responses;
using CRM.Domain.Repositories.Tenant;
using CRM.Domain.Repositories.User;
using CRM.Domain.Security.Tokens;
using CRM.Exceptions.ExceptionsBase;

namespace CRM.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repositoryOnly;
    private readonly ITenantReadOnlyRepository _readOnlyTenantRepository;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessToken;

    public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncripter, IAccessTokenGenerator accessToken, ITenantReadOnlyRepository readOnlyTenantRepository)
    {
        _repositoryOnly = repository;
        _passwordEncripter = passwordEncripter;
        _accessToken = accessToken;
        _readOnlyTenantRepository = readOnlyTenantRepository;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        //var tenant = _readOnlyTenantRepository.GetTenantById(request);

        var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
        var user = await _repositoryOnly.GetUserByEmailAndPassword(request.Email, encriptedPassword) ?? throw new InvalidLoginException();

        var tenant = await _readOnlyTenantRepository.GetTenantById(user.TenantId);


        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Email = user.Email,
            TenantName = tenant!.Name,
            TenantType = tenant.Type,
            PlanExpiration = tenant.PlanExpiration,
            LastLogin = DateTime.UtcNow,
            Tokens = new ReponseTokenJson
            {
                AccessToken = _accessToken.Generate(user, tenant),
            }
        };
    }
}
