using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGrnService : IDisposable
    {
        List<pur_grnVM> GetAll();
        List<pur_grnViewModel> getall();
        pur_grnVM Get(int? id);
        string Add(pur_grnVM GRN);
        //string Delete(int id, string cancellation_remarks);
        string Delete(int id, string cancellation_remarks, int reason_id);
        pur_poVM GetQuotationForGRN(int id);
        List<pur_grnVM> GetGrnListForIEX(int vendor_id);
        DateTime? GetBatchForExpiraryDate(string batch);
        pur_grnViewModel GetGrnForReport(int id);
        List<grn_report_detail> GetGrnDetailForReport(int id);
        List<pur_grnVM> GetGrnList();
    }
}
