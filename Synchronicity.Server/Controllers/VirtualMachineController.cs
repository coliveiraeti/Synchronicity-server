using Synchronicity.Common.Model;
using Synchronicity.Server.Interfaces.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace Synchronicity.Server.Controllers
{
    public class VirtualMachineController : ApiController
    {
        readonly IVirtualMachineRepository virtualMachineRepository;

        public VirtualMachineController(IVirtualMachineRepository virtualMachineRepository)
        {
            this.virtualMachineRepository = virtualMachineRepository;
        }

        // GET: api/VirtualMachine
        public IEnumerable<VirtualMachine> Get()
        {
            return virtualMachineRepository.Get();
        }
    }
}
