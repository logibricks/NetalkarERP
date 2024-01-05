using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface ICurrencyService : IDisposable
    {
        List<REF_CURRENCYVM> GetAll();
        REF_CURRENCY Get(int id);
        REF_CURRENCYVM Add(REF_CURRENCYVM currency);
        REF_CURRENCYVM Update(REF_CURRENCYVM currency);
        bool Delete(int id);
        List<REF_CURRENCYVM> GetCurrency1();
        List<REF_CURRENCYVM> GetCurrency2();
    }
}
