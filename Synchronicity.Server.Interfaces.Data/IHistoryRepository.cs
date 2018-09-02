using Synchronicity.Common.Model;

namespace Synchronicity.Server.Interfaces.Data
{
    public interface IHistoryRepository : IRepository<History>
    {
        void Put(History model);
    }
}
