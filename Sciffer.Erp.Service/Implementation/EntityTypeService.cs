using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class EntityTypeService : IEntityTypeService
    {

        private readonly ScifferContext _scifferContext;

        public EntityTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_ENTITY_TYPE entitytype)
        {
            try
            {
                _scifferContext.REF_ENTITY_TYPE.Add(entitytype);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var country = _scifferContext.REF_ENTITY_TYPE.FirstOrDefault(c => c.ENTITY_TYPE_ID == id);
                _scifferContext.Entry(country).State = EntityState.Deleted;
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

        public REF_ENTITY_TYPE Get(int id)
        {
            var entity_type = _scifferContext.REF_ENTITY_TYPE.FirstOrDefault(c => c.ENTITY_TYPE_ID == id);
            return entity_type;
        }

        public List<REF_ENTITY_TYPE> GetAll()
        {
            return _scifferContext.REF_ENTITY_TYPE.ToList();
        }

        public bool Update(REF_ENTITY_TYPE entitytype)
        {
            try
            {
                _scifferContext.Entry(entitytype).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
