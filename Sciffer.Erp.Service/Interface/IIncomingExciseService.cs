using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IIncomingExciseService:IDisposable
    {
        List<pur_po_detail_vm> GetGRNProductList(int id);
        List<pur_incoming_excise_vm> GetAll();       
        pur_incoming_excise_vm Get(int id);
        string Add(pur_incoming_excise_vm excises);
        string Delete(int id, string cancellation_remarks, int reason_id);
        pur_grnVM GetGrnDetailForIEX(int id);
       // pur_incoming_excise_report_vm GetIEXForReport(int id);
        pur_incoming_excise_report_vm GetIEXForReport(int id);
        List<pur_incoming_excise_detail_vm> GetDetailIEXForReport(int id);
        List<pur_incoming_excise_tax> GetIncomingExciseTax(string entity, string tax, double amt, DateTime posting_date, int tds_code_id);
        
    }
}
