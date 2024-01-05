using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IProcessMachineMappingService
    {
        //List<ref_machine> GetAllMachines();
        //bool SaveProcessMapping(process_machine_mapping_VM vm);
        //List<process_machine_mapping_VM> GetAll();

        List<ref_mfg_process> GetAllProcess();
        List<ref_mfg_process_VM> GetProcessDetails();
        List<ref_machine> GetAllMachinesForProcess(int process_id);
        string UpdateProcessMapping(int processid, string machineids);
    }
}
