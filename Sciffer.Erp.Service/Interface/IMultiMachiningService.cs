using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface IMultiMachiningService
    {
        bool UpdateRecords(ref_mfg_multi_machining_vm vm);
        string DeleteMultiMachine(int group_id);
    }
}
