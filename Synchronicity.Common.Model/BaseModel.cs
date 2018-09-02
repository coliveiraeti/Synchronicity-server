using System;

namespace Synchronicity.Common.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreationUser { get; set; }
    }
}
