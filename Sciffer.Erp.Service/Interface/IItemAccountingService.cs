using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IItemAccountingService:IDisposable
    {
        List<REF_ITEM_ACCOUNTING> GetAll();
        REF_ITEM_ACCOUNTING Get(int id);
        REF_ITEM_ACCOUNTING Create();
        bool Add(REF_ITEM_ACCOUNTING itemvaluation);
        bool Update(REF_ITEM_ACCOUNTING itemvaluation);
        bool Delete(int id);
    }
}
