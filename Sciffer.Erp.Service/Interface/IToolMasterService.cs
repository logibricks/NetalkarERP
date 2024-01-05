using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IToolMasterService
    {
        List<ref_tool_VM> GetAll();
        ref_tool Get(int id);
        ref_tool_VM Add(ref_tool_VM tool);
        ref_tool_VM Update(ref_tool_VM tool);
        bool Delete(int id);
    }
}
