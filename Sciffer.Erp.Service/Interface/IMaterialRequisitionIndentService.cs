using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMaterialRequisitionIndentService
    {
        List<ref_tool_category> GetToolCategoryList();
        List<ref_tool_usage_type> GetToolUsageTypeList();
        List<ref_mfg_process> GetOperationList(int operator_id);
        List<ref_tool_operation_map_vm> GetToolOperationMappedList(int crankshaft_id,int tool_usage_type_id, int tool_category_id,int process_id);

        List<material_requision_note_vm> GetAll();
        material_requision_note_vm Get(int id);
        string Add(material_requision_note_vm material);
        string Update(material_requision_note_vm material);
        bool Delete(int id);

        int GetApprovedMRICount(int user);

        string materialRequisionIndentupdatestatusseen();


    }
}
