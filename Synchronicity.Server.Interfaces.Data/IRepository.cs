using System.Collections.Generic;

namespace Synchronicity.Server.Interfaces.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(T model = null);

        T Get(int id);
    }
}
