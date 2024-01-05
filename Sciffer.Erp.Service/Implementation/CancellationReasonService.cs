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
    public class CancellationReasonService : ICancellationReasonService
    {
        private readonly ScifferContext _scifferContext;

        public CancellationReasonService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_cancellation_reason Add(ref_cancellation_reason cancellationreason)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                cancellationreason.is_active = true;
                cancellationreason.created_by = user;
                cancellationreason.created_ts = DateTime.Now;
                _scifferContext.ref_cancellation_reason.Add(cancellationreason);
                _scifferContext.SaveChanges();
                cancellationreason.cancellation_reason_id = _scifferContext.ref_cancellation_reason.Max(x => x.cancellation_reason_id);
            }
            catch (Exception ex)
            {
                return cancellationreason;
            }
            return cancellationreason;

        }

        public bool Delete(int id)
        {
            try
            {
                var cancellationreason = _scifferContext.ref_cancellation_reason.FirstOrDefault(c => c.cancellation_reason_id == id);
                cancellationreason.is_active = false;
                _scifferContext.Entry(cancellationreason).State = EntityState.Modified;
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

        public ref_cancellation_reason Get(int id)
        {
            var country = _scifferContext.ref_cancellation_reason.FirstOrDefault(c => c.cancellation_reason_id == id);
            return country;
        }

        public List<ref_cancellation_reason_vm> GetAll()
        {
            var query = (from ed in _scifferContext.ref_cancellation_reason.Where(x => x.is_active == true)
                         join module in _scifferContext.ref_module_form on ed.module_form_id equals module.module_form_id 
                         select new ref_cancellation_reason_vm
                         {
                             module_form_name = module.module_form_name,
                             module_form_id=ed.module_form_id,
                             cancellation_reason = ed.cancellation_reason,
                             cancellation_reason_id = ed.cancellation_reason_id,
                             is_blocked =ed.is_blocked,
                         }).ToList();
            return query;
            //return _scifferContext.ref_cancellation_reason.ToList().Where(x => x.is_active == true).ToList();
        }

        public ref_cancellation_reason Update(ref_cancellation_reason cancellationreason)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                cancellationreason.is_active = true;
                cancellationreason.created_by = user;
                cancellationreason.created_ts = DateTime.Now;
                _scifferContext.Entry(cancellationreason).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return cancellationreason;
            }
            return cancellationreason;
        }
        
    }
}
