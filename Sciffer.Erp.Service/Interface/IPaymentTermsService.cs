using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPaymentTermsService: IDisposable
    {
        List<payment_terms_vm> GetPaymentTerms();
        List<REF_PAYMENT_TERMS> GetAll();
        REF_PAYMENT_TERMS Get(int id);
        payment_terms_vm Add(payment_terms_vm payment);
        payment_terms_vm Update(payment_terms_vm payment);
        bool Delete(int id);
    }
}
