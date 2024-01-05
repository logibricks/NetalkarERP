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
    public class SalutationService: ISalutationService
    {

        private readonly ScifferContext _scifferContext;

        public SalutationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_SALUTATION salutation)
        {
            try
            {
                _scifferContext.REF_SALUTATION.Add(salutation);
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
                var salutation = _scifferContext.REF_SALUTATION.FirstOrDefault(c => c.salutation_id == id);
                _scifferContext.Entry(salutation).State = EntityState.Deleted;
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

        public REF_SALUTATION Get(int id)
        {
            var salutation = _scifferContext.REF_SALUTATION.FirstOrDefault(c => c.salutation_id == id);            
            return salutation;
        }

        public List<REF_SALUTATION> GetAll()
        {
            return _scifferContext.REF_SALUTATION.ToList();
        }

        public bool Update(REF_SALUTATION salutation)
        {
            try
            {
                _scifferContext.Entry(salutation).State = EntityState.Modified;
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
