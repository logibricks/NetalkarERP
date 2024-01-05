using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPaymentTermsDueDateService:IDisposable
    {
        List<REF_PAYMENT_TERMS_DUE_DATE> GetAll();
        REF_PAYMENT_TERMS_DUE_DATE Get(int id);
        bool Add(REF_PAYMENT_TERMS_DUE_DATE R);
        bool Update(REF_PAYMENT_TERMS_DUE_DATE R);
        bool Delete(int id);
    }
}
