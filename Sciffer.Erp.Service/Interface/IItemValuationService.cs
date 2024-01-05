using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface IItemValuationService : IDisposable
    {
        List<REF_ITEM_VALUATION> GetAll();
        REF_ITEM_VALUATION Get(int id);
        REF_ITEM_VALUATION Create();
        bool Add(REF_ITEM_VALUATION itemvaluation);
        bool Update(REF_ITEM_VALUATION itemvaluation);
        bool Delete(int id);
    }
}
