using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface  IQualityParameterService
    {
        string Add(mfg_qc_qc_parameter_VM vm);
        string Update(mfg_qc_qc_parameter_VM vm);
        bool Delete(int id);
        List<mfg_qc_qc_parameter_VM> GetAll();
        mfg_qc_qc_parameter_VM Get(int? id, int? id1);
    }
}
