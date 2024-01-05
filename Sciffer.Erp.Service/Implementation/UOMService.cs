using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;


namespace Sciffer.Erp.Service.Implementation
{
    public class UOMService : IUOMService
    {
        private readonly ScifferContext _scifferContext;

        public UOMService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public REF_UOM Add(REF_UOM UOM)
        {
            try
            {
                UOM.is_active = true;
                _scifferContext.REF_UOM.Add(UOM);
                _scifferContext.SaveChanges();
                UOM.UOM_ID = _scifferContext.REF_UOM.Max(x => x.UOM_ID);
            }
            catch (Exception)
            {
                return UOM;
            }
            return UOM;
        }

        public bool Delete(int id)
        {
            try
            {
                var UOM = _scifferContext.REF_UOM.FirstOrDefault(c => c.UOM_ID == id);
                UOM.is_active = false;
                _scifferContext.Entry(UOM).State = EntityState.Modified;
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

        public REF_UOM Get(int id)
        {
            var UOM = _scifferContext.REF_UOM.FirstOrDefault(c => c.UOM_ID == id);
            return UOM;
        }

        public List<REF_UOM> GetAll()
        {
            return _scifferContext.REF_UOM.Where(x => x.is_active == true).OrderByDescending(a => a.UOM_ID).ToList();
        }

        public REF_UOM Update(REF_UOM UOM)
        {
            try
            {
                UOM.is_active = true;
                _scifferContext.Entry(UOM).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return UOM;
            }
            return UOM;
        }
        public int GetID(string st)
        {
            var X = _scifferContext.REF_UOM.Where(x => x.UOM_NAME == st).FirstOrDefault();
            var id = X == null ? "0" : X.UOM_ID.ToString();
            return int.Parse(id);
        }
    }
}