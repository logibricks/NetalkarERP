using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface IIncentiveBenchmarkService
    {
        ref_mfg_incentive_benchmark_vm Add(ref_mfg_incentive_benchmark_vm incben);
        ref_mfg_incentive_benchmark_vm Get(int id);
        bool Delete(int id);
    }
}
