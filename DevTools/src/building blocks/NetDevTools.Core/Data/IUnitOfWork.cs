namespace NetDevTools.Core.Data
{
    public interface IUnitOfWork
    {
        string Schema { get; }
        Task<bool> Commit();
    }
}
