using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface INotificationTypeService
    {
        List<REF_NOTIFICATION_TYPE> GetAll();
        REF_NOTIFICATION_TYPE Get(int id);
        REF_NOTIFICATION_TYPE Add(REF_NOTIFICATION_TYPE notificationtype);
        REF_NOTIFICATION_TYPE Update(REF_NOTIFICATION_TYPE notificationtype);
        bool Delete(int id);
    }
}
