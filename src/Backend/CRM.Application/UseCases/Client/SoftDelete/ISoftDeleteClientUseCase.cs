namespace CRM.Application.UseCases.Client.SoftDelete;

public interface ISoftDeleteClientUseCase
{
    Task Execute(Guid clientId);
}
