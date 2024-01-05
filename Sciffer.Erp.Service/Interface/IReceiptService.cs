using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;
namespace Sciffer.Erp.Service.Interface
{
    public interface IReceiptService:IDisposable
    {
        List<fin_payment_receipt_vm> GetAll(int in_out);
        fin_ledger_paymentVM Get(int id, int in_out);
        string Add(fin_ledger_paymentVM receipt);
        string Delete(int id, string cancellation_remarks, int cancellation_reason_id);
        List<entity_transaction_detail> GetEntityTransaction(int entity_type_id, string entity_id);
        List<entity_transaction_detail> GetPaymentReceiptDetail(int id);
        fin_payment_receipt_vm GetPaymentReceiptHeader(int id);
    }
}
