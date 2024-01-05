using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IProcessSequence
    {
        List<process_sequence_vm> GetAll();
        List<REF_ITEM> GetItemCode();
        List<ref_mfg_process> GetProcessCode();
        List<ref_machine> GetMachineCode();
        List<ref_machine> GetMachinebyProcess(int process_id);
        bool SaveProcessSequence(process_sequence_vm vm);
        bool UpdateProcessSequence(process_sequence_vm vm);
        process_sequence_vm Get(int? id);
    }
}
