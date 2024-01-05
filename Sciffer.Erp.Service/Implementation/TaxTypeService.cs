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
    public class TaxTypeService : ITaxTypeService
    {
        private readonly ScifferContext _scifferContext;
        public TaxTypeService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public ref_tax_type Add(ref_tax_type Tax)
        {
            try
            {
                Tax.is_active = true;
                _scifferContext.ref_tax_type.Add(Tax);
                _scifferContext.SaveChanges();
                Tax.tax_type_id = _scifferContext.ref_tax_type.Max(a => a.tax_type_id);
            }
            catch (Exception ex)
            {
                return Tax;
            }
            return Tax;
        }

        public bool Delete(int? id)
        {
            try
            {
               var tax= _scifferContext.ref_tax_type.Find(id);
                tax.is_active = false;
                _scifferContext.Entry(tax).State = System.Data.Entity.EntityState.Modified;
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

        public ref_tax_type Get(int? id)
        {
            try
            {
                return _scifferContext.ref_tax_type.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_tax_type> GetAll()
        {
            try
            {
                return _scifferContext.ref_tax_type.Where(a=>a.is_active==true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ref_tax_type Update(ref_tax_type Tax)
        {
            try
            {
                Tax.is_active =true;
                _scifferContext.Entry(Tax).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Tax;
            }
            return Tax;
        }
    }
}
