using System.Collections.Generic;

namespace Synchronicity.Common.Model
{
    public class VirtualMachine : BaseModel
    {
        public string Name { get; set; }

        public string Hostname { get; set; }

        public IEnumerable<History> History { get; set; }
    }
}
