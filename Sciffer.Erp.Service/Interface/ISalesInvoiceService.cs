using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalesInvoiceService : IDisposable
    {
        List<sal_si_vm> GetAll();
        sal_si_vm Get(int id);
        string Add(sal_si_vm invoice);
        bool Delete(int id);
        sal_si_report_vm GetSIDetailForReport(int id, string ent);
        List<sal_si_detail_report_vm> GetSIProductDetailForSI(int id);
        List<sales_si_challan> GetSIDetailForReportChallan(int id, string ent);
        List<GetBatchForSalesInvoice> gettagbatchforsalesinvoice(int item_id, int plant_id, int sloc_id, int bucket_id, int customer_id);
        // sal_si_report_vm getExciseSum(int id);
    }
}
