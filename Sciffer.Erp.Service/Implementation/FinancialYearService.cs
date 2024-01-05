using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class FinancialYearService : IFinancialYearService
    {
        private readonly ScifferContext _scifferContext;
        public FinancialYearService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public REF_FINANCIAL_YEAR Add(REF_FINANCIAL_YEAR finance)
        {
            try
            {
                 finance.is_active = true;
                _scifferContext.REF_FINANCIAL_YEAR.Add(finance);
                _scifferContext.SaveChanges();
                finance.FINANCIAL_YEAR_ID = _scifferContext.REF_FINANCIAL_YEAR.Max(x => x.FINANCIAL_YEAR_ID);
            }
            catch (Exception)
            {
                return finance;
            }
            return finance;
        }

        public bool Delete(int id)
        {
            try
            {
                var finance = _scifferContext.REF_FINANCIAL_YEAR.FirstOrDefault(c => c.FINANCIAL_YEAR_ID == id);
                finance.is_active = false;
                _scifferContext.Entry(finance).State = EntityState.Modified;
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

        public REF_FINANCIAL_YEAR Get(int id)
        {
            var finance = _scifferContext.REF_FINANCIAL_YEAR.FirstOrDefault(c => c.FINANCIAL_YEAR_ID == id);
            return finance;
        }

        public List<REF_FINANCIAL_YEAR> GetAll()
        {
            return _scifferContext.REF_FINANCIAL_YEAR.Where(x=>x.is_active ==true).OrderByDescending(a => a.FINANCIAL_YEAR_ID).ToList();
        }

        public REF_FINANCIAL_YEAR Update(REF_FINANCIAL_YEAR finance)
        {
            try
            {
                finance.is_active = true;
                _scifferContext.Entry(finance).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return finance;
            }
            return finance;
        }
    }
}
