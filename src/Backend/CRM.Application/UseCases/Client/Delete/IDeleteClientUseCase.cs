namespace CRM.Application.UseCases.Client.Delete;

public interface IDeleteClientUseCase
{
    Task Execute(Guid id);
}
