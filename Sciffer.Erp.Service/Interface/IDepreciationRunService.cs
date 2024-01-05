using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface IDepreciationRunService
    {
        string Add(ref_depreciation_run_vm deprun);
    }
}
