using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class StatusService : IStatusService
    {
        private readonly ScifferContext _scifferContext;
        public StatusService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(ref_status Status)
        {
            try
            {
                _scifferContext.ref_status.Add(Status);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.ref_status.Remove(_scifferContext.ref_status.Find(id));
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

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

        public ref_status Get(int id)
        {
            try
            {
                return _scifferContext.ref_status.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public List<ref_status> GetAll()
        {
            try
            {
                return _scifferContext.ref_status.OrderByDescending(a => a.status_id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public bool Update(ref_status Status)
        {
            try
            {
                _scifferContext.Entry(Status).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
