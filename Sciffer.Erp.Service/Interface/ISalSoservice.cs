using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalSoservice : IDisposable
    {
        List<sal_so_vm> GetAll();
        List<sal_so_vm> getall();
        sal_so_vm Get(int id);
        string Add(sal_so_vm quotation);
        string Delete(int id, string cancellation_remarks, int reason_id);
        int GetSOId(string num);
        sal_so_vm GetSOForSI(int id);
        List<sal_so_vm> GetSOForSI();
        List<sal_so_detail_report_vm> GetSOProductDetail(int code);
        List<sal_so_vm> GetSOForSalesInvoice(int id);
        sal_so_report_vm GetSoReport(int id);
        List<sal_so_detail_report_vm> GetSOProductList(int id);
        string Close(int? id, string closed_remarks);
    }   
}
