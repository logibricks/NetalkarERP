using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMachineEntryService
    {

        double GetProductionCount(string machine_id, int item_id, int status);
        double GetProductionCount1(string machine_id, int item_id, int status);
        //List<ref_machine> GetMachineList(string[] machine_id);
        List<int> CheckMacAddress(string macaddress);
        string GetOperatorPhoto(int user_id);
        string GetMachineName(int machine_id);
        string GetOperatorName(int user_id);
        List<mfg_machine_task_qc_qc_VM> GetMachineTaskUnderQc(int machine_id, int status);
        List<mfg_machine_task_VM> GetMachineTask(int machine_id, int item_id, int status, string searchtag);
        string UpdateItemStatus(int status_id, int mach_task_id, int in_item_id, int mach_id, string[] parametervalue, int[] parameterid, string heatcode, string runcode, int process_id, string tag_no_two, int supervisor_id, string supervisor_remarks);
        string GetMachineSequence(int prod_order_id);
        List<ref_mfg_machine_task_status> GetAllStatus();
        List<mfg_op_qc_parameter_list> GetAllParameters(int item_id, int machine_id);
        bool CheckMachineBlocked(int machine_id);
        string GetLastTagUsed(int machine_task_id);
        string GetMachineStatusOkay(int machine_task_id);

        void UpdateShiftCount(int machine_id, int item_id);
        bool SaveOperatorShift(int machine_id, int shift_id);
        List<ref_user_management_VM> GetSupervisorList();
        List<ref_machine_master_VM> GetMachineListByProcess(int process_id);
        List<ref_mfg_process> GetProcessListByOperator(int user_id);
        List<prod_plan_detail_vm> GetTargetQtyCount(int machine_id, int item_id, int shift_id);
        int GetOperatorProductionCountPerShift(string entity, int machine_id, int item_id);

        //--------Not implemented--------
        string GenerateNewTagNumber(string tag_number, string item_id, string machine_list);
        //--------------------------------

        string get_machine_entry_mac_level_mapping(string entity, int machine_id, int process_id, int shift_id);

        int GetPlantId(int shift_id);
        List<mfg_rejection_detail_vm> GetAllParametersAssign(int id);
        int? GetCycleTime(int item_id, int process_id, int machine_id);

    }
}
