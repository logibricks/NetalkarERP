using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly ScifferContext _scifferContext;

        public NotificationTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_NOTIFICATION_TYPE Add(REF_NOTIFICATION_TYPE notificationtype)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                notificationtype.is_active = true;
                notificationtype.created_by = user;
                notificationtype.created_ts = DateTime.Now;
                _scifferContext.REF_NOTIFICATION_TYPE.Add(notificationtype);
                _scifferContext.SaveChanges();
                notificationtype.NOTIFICATION_ID = _scifferContext.REF_NOTIFICATION_TYPE.Max(x => x.NOTIFICATION_ID);
            }
            catch (Exception ex)
            {
                return notificationtype;
            }
            return notificationtype;

        }

        public bool Delete(int id)
        {
            try
            {
                var notificationtype = _scifferContext.REF_NOTIFICATION_TYPE.FirstOrDefault(c => c.NOTIFICATION_ID == id);
                notificationtype.is_active = false;
                _scifferContext.Entry(notificationtype).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

        public REF_NOTIFICATION_TYPE Get(int id)
        {
            var country = _scifferContext.REF_NOTIFICATION_TYPE.FirstOrDefault(c => c.NOTIFICATION_ID == id);
            return country;
        }

        public List<REF_NOTIFICATION_TYPE> GetAll()
        {
            return _scifferContext.REF_NOTIFICATION_TYPE.ToList().Where(x => x.is_active == true).ToList();
        }

        public REF_NOTIFICATION_TYPE Update(REF_NOTIFICATION_TYPE notificationtype)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                notificationtype.is_active = true;
                notificationtype.created_by = user;
                notificationtype.created_ts = DateTime.Now;
                _scifferContext.Entry(notificationtype).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return notificationtype;
            }
            return notificationtype;
        }
    }
}
