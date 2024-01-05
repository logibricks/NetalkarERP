using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface IFinBankRecoService:IDisposable
    {
        string Add(fin_bank_reco_vm reco);
        List<fin_bank_reco_vm> GetAll();
        fin_bank_reco_vm Get(int id);
        List<fin_bank_payment_receipt_reco_vm> GetPaymentReceiptForBRS(int id, DateTime start_date, DateTime end_date);
    }
}
