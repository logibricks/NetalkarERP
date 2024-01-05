using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPlantNotificationService : IDisposable
    {
        List<ref_plant_notification_vm> GetAll();
        ref_plant_notification Get(int id);
        string Add(ref_plant_notification country);
        bool Update(ref_plant_notification country);
        bool Delete(int id);
    }
}
