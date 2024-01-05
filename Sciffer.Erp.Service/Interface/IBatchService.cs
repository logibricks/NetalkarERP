using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBatchService:IDisposable
    {
        List<inv_item_batch_VM> GetAll();
        inv_item_batch_VM Get(int id);
        bool Add(inv_item_batch_VM batch);
        bool Update(inv_item_batch_VM batch);
        bool Delete(int id);
        //List<BATCH> GetBatchUsingPlantItem(int plant_id);
        List<inv_item_batch_VM> GetBatchNumberUsingPlant(int item_id, int plant_id);
        List<inv_item_batch_VM> GetBatchNumber(int item_id);
    }
}
