using CRM.Domain.Entities;
using CRM.Domain.Repositories.Address;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CRM.Infrastructure.DataAccess.Repositories;

public class AddressRepository : IAddressReadOnlyRepository, IAddressUpdateOnlyRepository, IAddressWriteOnlyRepository
{
    private readonly CRMDbContext _dbContext;

    public AddressRepository(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Address address)
    {
        await _dbContext.Addresses.AddAsync(address);
    }

    public async Task<Address?> GetAddress(User loggedUser, Guid addressId)
    {
        return await _dbContext.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(address => address.Id == addressId && address.TenantId == loggedUser.TenantId);
    }

    public void Update(Address address) => _dbContext.Addresses.Update(address);
}
