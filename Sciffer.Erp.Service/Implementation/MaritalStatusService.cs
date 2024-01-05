using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Implementation
{
    public class MaritalStatusService: IMaritalStatusService
    {
        private readonly ScifferContext _scifferContext;
        public MaritalStatusService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

        public bool Add(REF_MARITAL_STATUS MSTATUS)
        {
            try
            {
                _scifferContext.REF_MARITAL_STATUS.Add(MSTATUS);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int? id)
        {
            try
            {
                _scifferContext.REF_MARITAL_STATUS.Remove(_scifferContext.REF_MARITAL_STATUS.Find(id));
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

        public REF_MARITAL_STATUS Get(int? id)
        {
            try
            {
                return _scifferContext.REF_MARITAL_STATUS.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<REF_MARITAL_STATUS> GetAll()
        {
            try
            {
                return _scifferContext.REF_MARITAL_STATUS.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(REF_MARITAL_STATUS MSTATUS)
        {
            try
            {
                _scifferContext.Entry(MSTATUS).State = System.Data.Entity.EntityState.Modified;
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
