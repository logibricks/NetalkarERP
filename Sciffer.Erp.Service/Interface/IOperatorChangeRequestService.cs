using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IOperatorChangeRequestService
    {
        string Add(operator_change_req_vm vm);
        operator_change_req_vm Get(int? id);
        List<operator_change_req_vm> GetAll(string entity, int? id);
        List<operator_change_req_vm> ChangeApprovedStatus(string entity, int? detail_id, int approval_status_id, string approval_comment);
    }
}
