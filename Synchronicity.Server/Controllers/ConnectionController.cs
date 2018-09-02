using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace Synchronicity.Server.Controllers
{
    public class ConnectionController : ApiController
    {
        readonly IHistoryRepository historyRepository;

        public ConnectionController(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        // GET: api/Connection
        public IEnumerable<History> Get()
        {
            return historyRepository.Get();
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Connection/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Connection
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Connection/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Connection/5
        public void Delete(int id)
        {
        }
    }
}
