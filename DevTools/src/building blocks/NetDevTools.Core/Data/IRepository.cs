using Microsoft.EntityFrameworkCore.Infrastructure;
using NetDevTools.Core.DomainObjects;

namespace NetDevTools.Core.Data
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        DatabaseFacade Database { get; }
    }
}
