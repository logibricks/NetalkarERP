using System;

namespace Sciffer.Erp.Domain.ViewModel
{
    public abstract class AudiTrailViewModel
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}