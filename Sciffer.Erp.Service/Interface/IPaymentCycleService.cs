using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPaymentCycleService: IDisposable
    {
        List<REF_PAYMENT_CYCLE> GetAll();
        REF_PAYMENT_CYCLE Get(int id);
        bool Add(REF_PAYMENT_CYCLE payment);
        bool Update(REF_PAYMENT_CYCLE payment);
        bool Delete(int id);
    }
}
