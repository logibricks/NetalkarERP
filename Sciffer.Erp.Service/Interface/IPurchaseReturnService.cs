using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
namespace Sciffer.Erp.Service.Interface
{
    public interface IPurchaseReturnService
    {
        List<pur_pi_return_vm> GetAll();

        pur_po_report_vm GetPOForReportPReturn(int id);
        pur_pi_return_vm Get(int? id);
        string Add(pur_pi_return_vm Purchase);
        bool Update(pur_pi_return_vm Purchase);
        List<pur_po_detail_vm> GetPOList(int id);
        List<pi_detail_vm> GetPIProductDetailForPI(int id);
        pur_pi_VM GetPIDetailForReport(int id);
        List<pur_pi_return_detail_vm> GetPiListForPI_return(int id);
        double forPurchaseReturnPI(int pi_id, int item_id, int bucket_id, int storage_location_id,int plant_id);
        List<pur_pi_VM> GetPiforPiReturn(int vendor_id);
        pur_pi_VM PiforPireturn(int id);
        List<pur_pi_return_detail_vm> GetPurchasereturnDetail(string entity, int buyer_id, int plant_id, string item_id, string sloc_id, string bucket_id, int pi_id);
        string Delete(int id, string cancellation_remarks, int reason_id);
        
        pur_pi_return_vm GetPurchaseReturnheaderReport(int id);

        List<pur_pi_return_detail_vm> GetPurchaseReturnDetailsForReport(int id);


    }
}
