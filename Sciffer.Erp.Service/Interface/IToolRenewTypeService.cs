using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IToolRenewTypeService
    {
        List<ref_tool_renew_type> GetAll();
        ref_tool_renew_type Get(int id);
        ref_tool_renew_type Add(ref_tool_renew_type tooltype);
        ref_tool_renew_type Update(ref_tool_renew_type tooltype);
        bool Delete(int id);
    }
}
