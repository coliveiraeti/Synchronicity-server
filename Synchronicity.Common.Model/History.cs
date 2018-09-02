using System;

namespace Synchronicity.Common.Model
{
    public class History : BaseModel
    {
        public int VirtualMachineId { get; set; }

        public string Description { get; set; }

        public DateTime? LogOffDate { get; set; }
    }
}
