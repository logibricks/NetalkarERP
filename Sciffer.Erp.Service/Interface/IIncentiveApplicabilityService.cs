using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface IIncentiveApplicabilityService
    {
        bool UpdateRecords(ref_mfg_operator_incentive_appl_vm vm);

    }
}
