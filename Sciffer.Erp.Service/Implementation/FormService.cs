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
    public class FormService : IFormService
    {
        private readonly ScifferContext _scifferContext;

        public FormService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public REF_FORM Add(REF_FORM frm)
        {
            try
            {
                _scifferContext.REF_FORM.Add(frm);
                _scifferContext.SaveChanges();
                frm.FORM_ID = _scifferContext.REF_FORM.Max(x => x.FORM_ID);
            }
            catch (Exception)
            {
                return frm;
            }
            return frm;
        }

        public bool Delete(int id)
        {
            try
            {
                var frm = _scifferContext.REF_FORM.FirstOrDefault(c => c.FORM_ID == id);
                _scifferContext.Entry(frm).State = EntityState.Deleted;
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

        public REF_FORM Get(int id)
        {
            var frm = _scifferContext.REF_FORM.FirstOrDefault(c => c.FORM_ID == id);
            return frm;
        }

        public List<REF_FORM> GetAll()
        {
            return _scifferContext.REF_FORM.ToList();
        }

        public REF_FORM Update(REF_FORM frm)
        {
            try
            {
                _scifferContext.Entry(frm).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return frm;
            }
            return frm;
        }
    }
}
