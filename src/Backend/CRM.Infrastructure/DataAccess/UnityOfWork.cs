using CRM.Domain.Repositories;
using CRM.Infrastructure.DataAccess.Repositories;

namespace CRM.Infrastructure.DataAccess
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly CRMDbContext _dbContext;
        public UnityOfWork(CRMDbContext dbContext) => _dbContext = dbContext;

        public async Task commit() => await _dbContext.SaveChangesAsync();
    }
}
