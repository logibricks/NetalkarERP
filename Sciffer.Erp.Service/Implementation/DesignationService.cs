using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Implementation
{
    public class DesignationService: IDesignationService
    {
        private readonly ScifferContext _scifferContext;

        public DesignationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(REF_DESIGNATION desig)
        {
            try
            {
                _scifferContext.REF_DESIGNATION.Add(desig);
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var desig = _scifferContext.REF_DESIGNATION.FirstOrDefault(c => c.designation_id == id);
                _scifferContext.Entry(desig).State = EntityState.Deleted;
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
        public REF_DESIGNATION Get(int id)
        {
            var desig = _scifferContext.REF_DESIGNATION.FirstOrDefault(c => c.designation_id == id);
            return desig;
        }

        public List<REF_DESIGNATION> GetAll()
        {
            return _scifferContext.REF_DESIGNATION.ToList();
        }

        public bool Update(REF_DESIGNATION desig)
        {
            try
            {
                _scifferContext.Entry(desig).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
