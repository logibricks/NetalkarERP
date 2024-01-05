using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPlantTransferService:IDisposable
    {

        List<pla_transferVM> GetAll();
        List<PlantTransferVM> getall();
        pla_transferVM Get(int id);
        string Add(pla_transferVM plant);
      
        bool Delete(int id);
        List<GetTagForPlaTransfer> gettagitemforplanttransfer(int item_id, int plant_id, int sloc_id, int bucket_id);
        List<GetTagForPlaTransfer> getnontagitemforplanttransfer(int item_id, int plant_id, int sloc_id, int bucket_id);
        pla_transferVM GetDetails(int id);

    }
}
