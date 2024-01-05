using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IProcessMasterService: IDisposable
    {
        List<ref_mfg_process> GetAll();
        ref_mfg_process_vm Get(int id);
        ref_mfg_process_vm Add(ref_mfg_process_vm process);
        ref_mfg_process_vm Update(ref_mfg_process_vm process);
        bool Delete(int id);
    }
}
