using Dapper;
using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronicity.Server.Data.SQLite
{
    public class VirtualMachineRepository : Repository, IVirtualMachineRepository
    {
        public IEnumerable<VirtualMachine> Get()
        {
           OpenConnection();
            var sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append(" FROM virtual_machine vm ");
            sb.Append(" LEFT JOIN history h ");
            sb.Append("  ON vm.id = h.virtual_machine_id AND h.logoff_date IS NULL");
            var result = connection.Query<VirtualMachine, History, VirtualMachine>(
                sb.ToString(),
                (vm, h) =>
                {
                    vm.History = new List<History> { h };
                    return vm;
                });
            CloseConnection();
            return result;
        }

        public VirtualMachine Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}

