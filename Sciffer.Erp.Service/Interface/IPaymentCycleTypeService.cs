using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPaymentCycleTypeService : IDisposable
    {
        List<REF_PAYMENT_CYCLE_TYPE> GetAll();
        REF_PAYMENT_CYCLE_TYPE Get(int id);
        bool Add(REF_PAYMENT_CYCLE_TYPE payment);
        bool Update(REF_PAYMENT_CYCLE_TYPE payment);
        bool Delete(int id);
    }
}
