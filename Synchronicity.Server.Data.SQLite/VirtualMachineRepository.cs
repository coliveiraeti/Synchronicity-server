using Dapper;
using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronicity.Server.Data.SQLite
{
    public class VirtualMachineRepository : Repository, IVirtualMachineRepository
    {
        public IEnumerable<VirtualMachine> Get(VirtualMachine model)
        {
           OpenConnection();
            var sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append(" FROM virtual_machine vm ");
            sb.Append(" LEFT JOIN history h ");
            sb.Append("  ON vm.id = h.virtual_machine_id AND h.logoff_date IS NULL");
            if (model != null && model.Id > 0)
            {
                sb.Append(" WHERE vm.id = @Id ");
            }
            var result = connection.Query<VirtualMachine, History, VirtualMachine>(
                sb.ToString(),
                (vm, h) =>
                {
                    vm.History = new List<History> { h };
                    return vm;
                },
                model);
            CloseConnection();
            return result;
        }

        public VirtualMachine Get(int id)
        {
            return Get(new VirtualMachine { Id = id }).FirstOrDefault();
        }
    }
}

