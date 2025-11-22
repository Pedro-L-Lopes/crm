using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Filters;
using CRM.Domain.Repositories.Client;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CRM.Infrastructure.DataAccess.Repositories;

public class ClientRepository : IClientReadOnlyRepository, IClientWriteOnlyRepository, IClientUpdateOnlyRepository
{
    private readonly CRMDbContext _dbContext;

    public ClientRepository(CRMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private static readonly Dictionary<string, Expression<Func<Client, object>>> SortExpressions = new()
    {
        ["name"] = c => c.Name,
        ["phone"] = c => c.Phone,
        ["document"] = c => c.Document,
        ["type"] = c => c.Type,
        ["gender"] = c => c.Gender,
        ["createdat"] = c => c.CreatedAt
    };

    public async Task Add(Client client)
    {
        await _dbContext.Clients.AddAsync(client);
    }

    public async Task<bool> ExistsClientWhiteDocument(string document)
    {
        return await _dbContext.Clients
            .AnyAsync(client => client.Document.Equals(document) && !client.IsDeleted);
    }

    async Task<Client?> IClientReadOnlyRepository.GetClient(User loggedUser, Guid clientId)
    {
        return await _dbContext.Clients
        .AsNoTracking()
        .Include(client => client.Address)
        .FirstOrDefaultAsync(client => client.Id == clientId && !client.IsDeleted && client.TenantId == loggedUser.TenantId);
    }

    async Task<Client?> IClientUpdateOnlyRepository.GetClient(User loggedUser, Guid clientId)
    {
        return await _dbContext.Clients
        .Include(client => client.Address)
        .FirstOrDefaultAsync(client => client.Id == clientId && client.IsDeleted == false && client.TenantId == loggedUser.TenantId);
    }

    public async Task<PagedResult<Client>> GetPaged(ClientQueryParams queryParams)
    {
        var query = _dbContext.Clients
            .AsNoTracking()
            .Where(c => !c.IsDeleted);

        query = ApplyFilters(query, queryParams.Filters);


        var totalItems = await query.CountAsync();

        query = ApplySort(query, queryParams.Sort);

        var items = await query
            .Skip((queryParams.Pagination.Page - 1) * queryParams.Pagination.PageSize)
            .Take(queryParams.Pagination.PageSize)
            .ToListAsync();

        return new PagedResult<Client>(
            items: items,
            totalItems: totalItems,
            page: queryParams.Pagination.Page,
            pageSize: queryParams.Pagination.PageSize
        );
    }

    public void Update(Client client) => _dbContext.Clients.Update(client);

    public async Task SoftDelete(Guid clientId)
    {
        var client = await _dbContext.Clients.FindAsync(clientId);

        if (client is null)
            return;

        client.IsDeleted = true;
        client.UpdatedAt = DateTime.UtcNow;

        _dbContext.Clients.Update(client);
    }


    public async Task Delete(Guid id)
    {
        var client = await _dbContext.Clients.FindAsync(id);

        _dbContext.Clients.Remove(client!);
    }

    public void UpdateAgent(Client client, Guid agentId)
    {
        client.AgentId = agentId;

        _dbContext.Clients.Attach(client);
        _dbContext.Entry(client).Property(c => c.AgentId).IsModified = true;
    }


    private static IQueryable<Client> ApplyFilters(IQueryable<Client> query, ClientFilters filters)
    {
        if (filters.TenantId.HasValue)
            query = query.Where(c => c.TenantId == filters.TenantId.Value);

        if (filters.AgentId.HasValue)
            query = query.Where(c => c.AgentId == filters.AgentId.Value);

        if (!string.IsNullOrWhiteSpace(filters.Name))
            query = query.Where(c => EF.Functions.Like(c.Name, $"%{filters.Name}%"));

        if (!string.IsNullOrWhiteSpace(filters.Phone))
            query = query.Where(c => EF.Functions.Like(c.Phone, $"%{filters.Phone}%"));

        if (!string.IsNullOrWhiteSpace(filters.Document))
            query = query.Where(c => c.Document.Contains(filters.Document));

        if (!string.IsNullOrWhiteSpace(filters.Type))
            query = query.Where(c => c.Type == filters.Type);

        if (!string.IsNullOrWhiteSpace(filters.Gender))
            query = query.Where(c => c.Gender == filters.Gender);

        return query;
    }

    private static IQueryable<Client> ApplySort(IQueryable<Client> query, SortParams sort)
    {
        var orderBy = string.IsNullOrWhiteSpace(sort.OrderBy)
            ? "name"
            : sort.OrderBy.ToLowerInvariant();

        if (!SortExpressions.TryGetValue(orderBy, out var sortExpression))
        {
            sortExpression = SortExpressions["name"];
        }

        return sort.Descending
            ? query.OrderByDescending(sortExpression)
            : query.OrderBy(sortExpression);
    }
}
