using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ICancellationReasonService
    {
        List<ref_cancellation_reason_vm> GetAll();
        ref_cancellation_reason Get(int id);
        ref_cancellation_reason Add(ref_cancellation_reason notificationtype);
        ref_cancellation_reason Update(ref_cancellation_reason notificationtype);
        bool Delete(int id);
    }
}
