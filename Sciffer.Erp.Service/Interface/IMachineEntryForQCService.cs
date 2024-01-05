using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMachineEntryForQCService
    {
        List<mfg_qc_qc_parameter_list> GetAllParameters(int item_id, int machine_id);
        List<mfg_machine_task_qc_qc_VM> GetMachinetaskUnderQC(int status_id);
        List<mfg_qc_VM> GetMahineStatus();
        //mfg_qc_VM UpdateMachineStatus(mfg_qc_VM status);
        string UpdateMachineStatus(int machine_id, bool is_machine_blocked);
        string UpdateItemStatus(int status, int machine_task_qc_qc_id, int machine_id, string[] parametervalue, int[] parameterid);
        string CheckDuplicateTagNumber(int machine_id, int machine_task_qc_qc_id);
        List<ref_mfg_nc_status> GetNcStatus();
        string SaveRejectionDetail(int machine_task_qc_qc_id, int nc_status_id, string root_cause, string nc_details, string action_plan, string remarks, string[] why_why_analysis, string nc_tag_number);
        List<mfg_rejection_detail_vm> GetNonConformingTrack(int machine_task_qc_qc_id);
        List<ref_mfg_nc_status_vm> GetStatus();
    }
}
