using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IItemService : IDisposable
    {
        List<REF_ITEM> GetAll();
        Ref_item_VM Get(int id);
        REF_ITEM Create();
        List<ItemVM> GetItemList();
        List<ItemVM> GetTagManagedItemList();
        string Add(Ref_item_VM item);
        bool Update(Ref_item_VM item);
        bool Delete(int id);
        List<item_list> GetItems();
        int GetID(string st);
        Ref_item_VM GetItemsDetail(int id);
        List<ref_item_plant_vm> GetItemPlantValuation(int id);
        string AddExcel(List<item_list> Item, List<Uom_Excel> uom, List<item_category_gl_Excel> gl, List<parameter_Excel> param, List<ref_item_plant_vm> stanCost);
    }
}
