using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IFinInternalReconcileService
    {
        List<fin_internal_reconcile_vm> GetAll();
        fin_internal_reconcile_vm Get(int id);
        string Add(fin_internal_reconcile_vm gender);
        string Delete(int id, string cancellation_remarks, int cancellation_reason_id);
        List<fin_internal_reconcile_detail_vm> forInternalReconcileDetail(int entity_type_id, int entity_id, DateTime from_date);
    }
}
