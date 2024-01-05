using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalQuotationService:IDisposable
    {
        List<SAL_QUOTATION_VM> GetAll();
        List<SAL_QUOTATION_VM> GetQuotationDeatilForSo();
        List<SAL_QUOTATION_VM> GetQuotationDeatilForSo(int id);
        List<SAL_QUOTATION_VM> getall();
        SAL_QUOTATION_VM Get(int id);
        SAL_QUOTATION_VM GetQuotationForSO(int id);
        string Add(SAL_QUOTATION_VM quotation);        
        bool Delete(int id);
        List<sal_quotation_detail_vm> GetQuotationProductDetail(string code);
        sal_quotation_report_vm GetQuotationDetailForReport(int id);
        List<sal_quotation_detail_vm> GetQuotationProductForReport(int id);
    }
}
