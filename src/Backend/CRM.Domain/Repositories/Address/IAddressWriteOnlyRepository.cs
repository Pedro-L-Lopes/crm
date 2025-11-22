namespace CRM.Domain.Repositories.Address;

public interface IAddressWriteOnlyRepository
{
    public Task Add(Entities.Address address);
}
