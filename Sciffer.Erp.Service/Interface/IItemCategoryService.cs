using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IItemCategoryService:IDisposable
    {
        List<REF_ITEM_CATEGORYVM> GetAll();       
        REF_ITEM_CATEGORYVM Get(int id);     
        bool Add(REF_ITEM_CATEGORYVM category);      
        bool Delete(int id);
        List<REF_ITEM_CATEGORYVM> GetItemCategoryByItemType(int id);
    }
}
