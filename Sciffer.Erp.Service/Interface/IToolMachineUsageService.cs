using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IToolMachineUsageService
    {
        string Add(ref_tool_machine_usage_VM vm);
        string Update(ref_tool_machine_usage_VM vm);
        bool Delete(int? id);
        List<ref_tool_machine_usage_VM> GetAll();
        ref_tool_machine_usage_VM Get(int? id);
        List<ref_tool_machine_item_usage_VM> GetItemDetails(int tool_id, int tool_renew_type_id, int machine_id);
    }
}
