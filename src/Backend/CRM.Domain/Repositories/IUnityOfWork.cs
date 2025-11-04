namespace CRM.Domain.Repositories;

public interface IUnityOfWork
{
    public Task commit();
}
