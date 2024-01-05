using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPaymentTypeService:IDisposable
    {

        List<REF_PAYMENT_TYPE> GetAll();
        REF_PAYMENT_TYPE Get(int id);
        REF_PAYMENT_TYPE Add(REF_PAYMENT_TYPE paymentType);
        REF_PAYMENT_TYPE Update(REF_PAYMENT_TYPE paymentType);
        bool Delete(int id);
    }
}
