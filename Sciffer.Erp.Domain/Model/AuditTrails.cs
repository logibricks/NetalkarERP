using System;

namespace Sciffer.Erp.Domain.Model
{
    public abstract class AuditTrails
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public  Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}