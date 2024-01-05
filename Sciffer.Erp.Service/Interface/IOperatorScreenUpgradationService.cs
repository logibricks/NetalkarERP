using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IOperatorScreenUpgradationService
    {
        List<ref_machine_master_VM> GetMachineListByProcess(int process_id);
        string UploadFile(mfg_machine_item_upgradation mfg_machine_item_upgradation);
        List<mfg_machine_item_upgradation> GetFileByItemMachineId(int machine_id, int item_id);
        List<mfg_machine_item_upgradation_vm> getall();
        mfg_machine_item_upgradation_vm get(int id);
        mfg_machine_item_upgradation Update(mfg_machine_item_upgradation itemvaluation);
        bool Delete(int id);
    }
}
