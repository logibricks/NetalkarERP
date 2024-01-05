using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IOperatorOperationMappingService
    {
        List<ref_user_management_VM> GetOperatorList();
        List<ref_mfg_process> GetProcessList();
        string UpdateOperatorOperationMapping(int operator_id, string operation_id);
        List<map_operator_operation_vm> GetOperatorOperationMapList();
        string GetMachineRole(int user_id);
    }
}
