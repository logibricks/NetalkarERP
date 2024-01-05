using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IInventoryStockService
    {
        List<inv_Inventory_stock_vm> GetAll();
        inv_Inventory_stock_vm Get(int id);
        string Add(inv_Inventory_stock_vm Inventory);
        List<GetItemForStock> GetItemForStock(int plant_id, int sloc_id, int bucket_id);
        List<inv_Inventory_stock_detail_VM> GetItemForStockEdit(int id);


    }
}
