using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class OrgTypeService : IOrgTypeService
    {
        private readonly ScifferContext _scifferContext;

        public OrgTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_ORG_TYPE Add(REF_ORG_TYPE organization)
        {
            try
            {

                organization.is_active = true;
                _scifferContext.REF_ORG_TYPE.Add(organization);
                _scifferContext.SaveChanges();
                organization.ORG_TYPE_ID = _scifferContext.REF_ORG_TYPE.Max(x => x.ORG_TYPE_ID);
                organization.ORG_TYPE_NAME = _scifferContext.REF_ORG_TYPE.Where(x => x.ORG_TYPE_ID == organization.ORG_TYPE_ID).FirstOrDefault().ORG_TYPE_NAME;

            }
            catch (Exception)
            {
                return organization;
            }
            return organization;
        }

        public bool Delete(int id)
        {
            try
            {
                var organization = _scifferContext.REF_ORG_TYPE.FirstOrDefault(c => c.ORG_TYPE_ID == id);
                organization.is_active = false;
                _scifferContext.Entry(organization).State = EntityState.Modified;
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

        public REF_ORG_TYPE Get(int id)
        {
            var country = _scifferContext.REF_ORG_TYPE.FirstOrDefault(c => c.ORG_TYPE_ID == id);
            return country;
        }

        public List<REF_ORG_TYPE> GetAll()
        {
            return _scifferContext.REF_ORG_TYPE.Where(x => x.is_active == true).OrderByDescending(a => a.ORG_TYPE_ID).ToList();
        }

        public REF_ORG_TYPE Update(REF_ORG_TYPE organization)
        {
            try
            {

                organization.is_active = true;
                _scifferContext.Entry(organization).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return organization;
            }
            return organization;
        }

        public int Check(string st)
        {
            var query = _scifferContext.REF_ORG_TYPE.Count(x => x.ORG_TYPE_NAME == st);
            return query;
        }

        
    }
}
