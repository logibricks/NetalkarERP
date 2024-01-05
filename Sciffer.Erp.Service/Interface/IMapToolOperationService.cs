using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IMapToolOperationService
    {
        string Add(ref_tool_operation_map_vm vm);
        string Update(ref_tool_operation_map_vm vm);
        bool Delete(int id);
        List<ref_tool_operation_map_vm> GetAll();
        ref_tool_operation_map_vm Get(int? id);
        List<ref_tool_operation_map_vm> GetItemCrankshaftList();
        List<ref_tool_operation_map_vm> GetItemToolList();
        List<ref_tool_operation_map_vm> GetToolUsagetypeList();
        List<ref_tool_operation_map_vm> GetToolCatagoryList();
    }
}
