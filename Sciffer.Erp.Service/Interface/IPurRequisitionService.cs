using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPurRequisitionService : IDisposable
    {
        List<pur_requisition_vm> GetPurRequistion();
        List<pur_requisition_vm> GetAll();
        pur_requisition_vm Get(int id);
        string Add(pur_requisition_vm pur);
        bool Delete(int id);
        List<pur_req_stock> GetItemStock(int id);
        List<pur_req_detail_vm> GetPurRquisitionList(int id);
        pur_req_report_vm GetPurRequisitionReport(int id);
        List<pur_req_report_detail_vm> GetPurRequisitionDetailReport(int id);
        List<pur_requisition_detail_vm> GetPRDetails(int plant_id, string entity);
        List<pur_req_detail_vm> GetPurRquisitionDetails(string entity, int id);
        List<pur_requisition_vm> GetPendigApprovedList();
        bool ChangeApprovedStatus(pur_requisition_vm vm);
        string Close(int? id, string closed_remarks);

        string PurchaseRequisitionupdatestatusseen();

    }
}
