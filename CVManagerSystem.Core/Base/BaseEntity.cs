using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManagerSystem.Core.Base
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? CreatedBy { get; set; }

        public BaseEntity()
        {
            IsActive = true;
            CreatedOn = DateTime.Now;
        }
    }
}
