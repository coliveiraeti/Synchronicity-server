using Dapper;
using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Synchronicity.Server.Data.SQLite
{
    public class HistoryRepository : Repository, IHistoryRepository
    {
        public HistoryRepository()
        {

        }

        public IEnumerable<History> Get()
        {
            return new List<History>
            {
                new History {Description = "teste"}
            };
        }

        public History Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Put(History model)
        {
            OpenConnection();
            if (model.Id == 0)
            {
                Insert(model);
            }
            else
            {
                Update(model);
            }
            CloseConnection();
        }

        void Insert(History model)
        {
            var sb = new StringBuilder();
            sb.Append("UPDATE history ");
            sb.Append(" SET logoff_date = @LogOffDate ");
            sb.Append(" WHERE virtual_machine_id = @VirtualMachineId ");
            sb.Append("   AND logoff_date IS NULL ");
            var hijacked = connection.Execute(sb.ToString(), new History { VirtualMachineId = model.VirtualMachineId, LogOffDate = DateTime.Now } );
            if (hijacked > 0)
            {
                model.Description = "[Hijacked] " + model.Description;
            }

            sb = new StringBuilder();
            sb.Append("INSERT INTO history ");
            sb.Append(" (virtual_machine_id, description, creation_date, creation_user) ");
            sb.Append(" VALUES (@VirtualMachineId, @Description, @CreationDate, @CreationUser)");
            var result = connection.Execute(sb.ToString(), model);
        }

        void Update(History model)
        {
            var sb = new StringBuilder();
            sb.Append("UPDATE history ");
            sb.Append(" SET logoff_date = @LogOffDate ");
            sb.Append(" WHERE id = @Id");
            var result = connection.Execute(sb.ToString(), model);
        }


    }
}
