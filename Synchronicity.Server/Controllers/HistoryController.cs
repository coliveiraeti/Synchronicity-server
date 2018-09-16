using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace Synchronicity.Server.Controllers
{
    public class HistoryController : ApiController
    {
        readonly IHistoryRepository historyRepository;

        public HistoryController(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        public IEnumerable<History> Get(int virtualMachineId, string description = null)
        {
            var model = new History
            {
                VirtualMachineId = virtualMachineId,
                Description = description
            };
            return historyRepository.Get(model);
        }

    }
}
