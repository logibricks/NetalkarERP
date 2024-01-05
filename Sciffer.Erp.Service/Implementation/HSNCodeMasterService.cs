using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Service.Interface;


namespace Sciffer.Erp.Service.Implementation
{
    public class HSNCodeMasterService : IHSNCodeMasterService
    {
        private readonly ScifferContext _scifferContext;

        public HSNCodeMasterService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_hsn_code Add(ref_hsn_code hsn)
        {
            try
            {
             //  hsn.is_blocked = hsn.is_blocked;
                _scifferContext.ref_hsn_code.Add(hsn);
                _scifferContext.SaveChanges();
                hsn.hsn_code_id = _scifferContext.ref_hsn_code.Max(x => x.hsn_code_id);
            }
            catch (Exception ex)
            {
                return hsn;
            }
            return hsn;
        }

        public bool Delete(int id)
        {
            try
            {
                var batch = _scifferContext.ref_hsn_code.FirstOrDefault(c => c.hsn_code_id == id);
                _scifferContext.Entry(batch).State = EntityState.Modified;
                batch.is_blocked = false;
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



        public ref_hsn_code Get(int id)
        {
            var hsn = _scifferContext.ref_hsn_code.FirstOrDefault(c => c.hsn_code_id == id);
            return hsn;
        }

        public List<ref_hsn_code_vm> GetAll()
        {
            var query = (from hs in _scifferContext.ref_hsn_code
                         join t1 in _scifferContext.ref_tax on hs.within_state_tax_id equals t1.tax_id into j1
                         from t11 in j1.DefaultIfEmpty()
                         join t2 in _scifferContext.ref_tax on hs.inter_state_tax_id equals t2.tax_id into j2
                         from t22 in j2.DefaultIfEmpty()
                         select new ref_hsn_code_vm
                         {
                             hsn_code = hs.hsn_code,
                             hsn_code_description = hs.hsn_code_description,
                             hsn_code_id = hs.hsn_code_id,
                             inter_state_tax_id = t22 == null ? 0 : t22.tax_id,
                             inter_state_tax_name = t22 == null ? string.Empty : t22.tax_name,
                             is_blocked = hs.is_blocked,
                             within_state_tax_id = t11 == null ? 0 : t11.tax_id,
                             within_state_tax_name = t11 == null ? string.Empty : t11.tax_name,
                         }).ToList();

            return query;
        }

        public ref_hsn_code Update(ref_hsn_code hsn)
        {
            try
            {
              //  hsn.is_blocked = true;
                _scifferContext.Entry(hsn).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return hsn;
            }
            return hsn;
        }
    }
}
