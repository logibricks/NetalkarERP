using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPlantService:IDisposable
    {
        List<plant_vm> GetPlantList();
        List<REF_PLANT_VM> GetAll();
        REF_PLANT_VM Get(int id);
        bool Add(REF_PLANT_VM plant);
        bool Update(REF_PLANT_VM plant);
        bool Delete(int id);
        List<REF_PLANT_VM> GetPlant();
    }
}
