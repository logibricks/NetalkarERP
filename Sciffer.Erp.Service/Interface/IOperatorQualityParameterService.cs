using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IOperatorQualityParameterService
    {
        string Add(mfg_op_qc_parameter_VM vm);
        string Update(mfg_op_qc_parameter_VM vm);
        bool Delete(int id);
        List<mfg_op_qc_parameter_VM> GetAll();
        mfg_op_qc_parameter_VM Get(int? id,int? id1);
    }
}
