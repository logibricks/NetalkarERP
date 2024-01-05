using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPurchaseInvoiceService
    {
        List<pur_pi_VM> GetAll();
        pur_pi_VM Get(int? id);
        string Add(pur_pi_VM Purchase);
        bool Update(pur_pi_VM Purchase);
        List<pur_po_detail_vm> GetPOList(int id);
        string Delete(int id, string cancellation_remarks, int reason_id);
        List<pi_detail_vm> GetPIProductDetailForPI(int id);
        pur_pi_VM GetPIDetailForReport(int id);
        List<pur_pi_vm_detail> GetGRNListForPI(int id);
        pur_pi_report_vm GetPIForReport(int id);
        List<pur_pi_detail_report_vm> GetPIDetailsForReport(int id);
    }
}
