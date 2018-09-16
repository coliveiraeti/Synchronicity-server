using Dapper;
using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synchronicity.Server.Data.SQLite
{
    public class HistoryRepository : Repository, IHistoryRepository
    {
        public HistoryRepository()
        {
        }

        public History Get(int id)
        {
            return Get(new History { Id = id }).FirstOrDefault();
        }

        public IEnumerable<History> Get(History model)
        {
            OpenConnection();
            bool hasDescription = !string.IsNullOrEmpty(model.Description);

            var sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append(" FROM history ");
            sb.Append(" WHERE virtual_machine_id = @VirtualMachineId ");
            if (hasDescription)
            {
                sb.Append(" AND description like @Description ");
            }
            sb.Append(" ORDER BY creation_date DESC ");
            if (!hasDescription)
            {
                sb.Append(" LIMIT 5 ");
            }
            var result = connection.Query<History>(sb.ToString(), 
                new {
                    model.VirtualMachineId,
                    Description = $"%{model.Description ?? ""}%"
                });
            CloseConnection();
            return result;
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
