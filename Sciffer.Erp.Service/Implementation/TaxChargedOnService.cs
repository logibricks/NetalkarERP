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
    public class TaxChargedOnService : ITaxChargedOnService
    {
        private readonly ScifferContext _scifferContext;

        public TaxChargedOnService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public ref_tax_charged_on Add(ref_tax_charged_on tax)
        {
            try
            {
                _scifferContext.ref_tax_charged_on.Add(tax);
                _scifferContext.SaveChanges();
                tax.tax_chargerd_on_id = _scifferContext.ref_tax_charged_on.Max(x=>x.tax_chargerd_on_id);
            }
            catch
            {
                return tax;
            }
            return tax;
        }

        public bool Delete(int id)
        {
            try
            {
                var tax = _scifferContext.ref_tax_charged_on.FirstOrDefault(c => c.tax_chargerd_on_id == id);
                _scifferContext.Entry(tax).State = EntityState.Deleted;               
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

        public ref_tax_charged_on Get(int id)
        {
            return _scifferContext.ref_tax_charged_on.FirstOrDefault(c => c.tax_chargerd_on_id == id);
            
        }

        public List<ref_tax_charged_on> GetAll()
        {
            return _scifferContext.ref_tax_charged_on.ToList();
        }

        public ref_tax_charged_on Update(ref_tax_charged_on tax)
        {
            try
            {
                _scifferContext.Entry(tax).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return tax;
            }
            return tax;
        }
    }
}
