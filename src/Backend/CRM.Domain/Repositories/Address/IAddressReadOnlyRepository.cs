namespace CRM.Domain.Repositories.Address;

public interface IAddressReadOnlyRepository
{
    public Task<Entities.Address?> GetAddress(Entities.User loggedUser, Guid addressId);
}
