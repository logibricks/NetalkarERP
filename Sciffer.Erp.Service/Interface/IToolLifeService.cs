using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IToolLifeService
    {
        List<ref_tool_life_VM> GetAll();
        ref_tool_life_VM Get(int id);
        ref_tool_life_VM Add(ref_tool_life_VM tooltype);
        ref_tool_life_VM Update(ref_tool_life_VM tooltype);
        bool Delete(int id);
        List<ref_tool_life_VM> Tool_Life_Report(string entity, string tool_id, string tool_renew_type_id, string item_id, string machine_id, DateTime? fromDate, DateTime? toDate);
    }
}
