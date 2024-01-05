using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface ICreditCardService :IDisposable
    {       
        List<REF_CREDIT_CARD_VM> GetAll();
        REF_CREDIT_CARD_VM Get(int id);
        bool Add(REF_CREDIT_CARD_VM BANK);
        bool Update(REF_CREDIT_CARD_VM BANK);
        bool Delete(int id);
    }
}
