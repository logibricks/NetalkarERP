using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPurchaseOrderService : IDisposable
    {
        List<pur_poVM> GetAll();
        List<pur_poVM> getall();
        pur_poVM Get(int? id);
        string Add(pur_poVM Purchase);
        bool Update(pur_poVM Purchase);
        List<pur_po_report_vm> GetPOList(int id, int vendor_id);//id=0 for all po otherwise only item po        
        List<pur_po_detail_vm> GetBalancePOList(int id);
        List<pur_po_report_vm> GetPOListByItemOrService(int id, int vendor_id);
        bool Delete(int? id);
        pur_po_report_vm GetPOForReport(int id);
        List<pur_po_detail_vm> GetPOProductForGRN(string ent, int id, DateTime? posting_date);
        List<pur_po_detail_report_vm> GetPOProductForReport(int id, string ent);
        List<pur_po_detail_report_vm> GetPODeliverydetail(int id, string ent);
        List<pur_poVM> GetPendigApprovedList(int id);
        bool ChangeApprovedStatus(pur_poVM vm);
        string Delete(int id, string cancellation_remarks, int reason_id);
        ref_price_list_vendor_detail_vm GetVendorItemPrice(int vendor_id, int item_id);
        string Close(int? id, string closed_remarks);

        string PurchaseOrderupdatestatusseen();

        List<Po_History_vm> GetPoHistory(string ent, int item_id);
        List<pur_poVM> GetPending__Slab_PO_ApprovalList();

    }
}
