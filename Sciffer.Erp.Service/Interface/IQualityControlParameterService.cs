using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IQualityControlParameterService
    {
        List<mfg_qc_vm> GetAll();
        List<REF_ITEM> GetItemCode();
        List<ref_machine> GetMachineCode();
        quality_parameter_vm Get(int? id);
        bool SaveQCParameter(quality_parameter_vm vm);
        bool UpdateQCParameter(quality_parameter_vm vm);
    }
}
