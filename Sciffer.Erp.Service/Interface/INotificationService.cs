using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
   public interface INotificationService : IDisposable
    {
        List<ref_pm_notification_vm> GetAll(int? machine_id);
        ref_pm_notification Get(int id);
        string Add(ref_pm_notification country);
        bool Update(ref_pm_notification country);
        bool Delete(int id);
        List<ref_pm_notification_vm> get_mal_notification(int user_id);
        string UpdateNotificationtStatus(int[] notifications_ids, int status);


    }
}
