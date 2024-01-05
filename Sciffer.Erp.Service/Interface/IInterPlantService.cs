using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IInterPlantService
    {
        List<InterPlaTransferVM> GetAll();
        List<InterPlaTransferVM> getall();
        InterPlaTransferVM Get(int id);
        string Add(InterPlaTransferVM plant);
        bool Delete(int id);
        List<InterPlaTransferVM> InterPlantTransfer(int id);
        List<inter_plant_detail_vm> GetInterPlantTransferDetail(int id);
    }
}
