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
    public class ItemGroupService : IItemGroupService
    {
        private readonly ScifferContext _scifferContext;

        public ItemGroupService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public REF_ITEM_GROUP Add(REF_ITEM_GROUP group)
        {
            try
            {
                group.is_active = true;
                _scifferContext.REF_ITEM_GROUP.Add(group);
                _scifferContext.SaveChanges();
                group.ITEM_GROUP_ID = _scifferContext.REF_ITEM_GROUP.Max(X => X.ITEM_GROUP_ID);
            }
            catch (Exception)
            {
                return group;
            }
            return group;
        }

        public REF_ITEM_GROUP Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            try
            {
                var group = _scifferContext.REF_ITEM_GROUP.FirstOrDefault(c => c.ITEM_GROUP_ID == id);
                group.is_active = false;
                _scifferContext.Entry(group).State = EntityState.Modified;
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

        public REF_ITEM_GROUP Get(int id)
        {
            var group = _scifferContext.REF_ITEM_GROUP.FirstOrDefault(c => c.ITEM_GROUP_ID == id);
            return group;
        }

        public List<REF_ITEM_GROUP> GetAll()
        {
            return _scifferContext.REF_ITEM_GROUP.Where(x=>x.is_active==true).OrderByDescending(a => a.ITEM_GROUP_ID).ToList();
        }

        public REF_ITEM_GROUP Update(REF_ITEM_GROUP group)
        {
            try
            {
                group.is_active = true;
                _scifferContext.Entry(group).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return group;
            }
            return group;
        }
    }
}
