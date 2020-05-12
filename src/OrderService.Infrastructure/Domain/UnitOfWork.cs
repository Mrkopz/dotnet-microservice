using System.Threading;
using System.Threading.Tasks;
using OrderService.Infrastructure.Database;
using OrderService.Infrastructure.Processing;
using OrderService.Infrastructure.SeedWork;

namespace OrderService.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            AppDbContext appContext, 
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            this._appContext = appContext;
            this._domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();
            return await this._appContext.SaveChangesAsync(cancellationToken);
        }
    }
}