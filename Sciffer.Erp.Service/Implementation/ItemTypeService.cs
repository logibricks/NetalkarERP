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
    public class ItemTypeService : IItemTypeService
    {
        private readonly ScifferContext _scifferContext;

        public ItemTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_item_type bank)
        {
            try
            {
                _scifferContext.ref_item_type.Add(bank);
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var batch = _scifferContext.ref_item_type.FirstOrDefault(c => c.item_type_id == id);
                _scifferContext.Entry(batch).State = EntityState.Deleted;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
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

        public ref_item_type Get(int id)
        {
            var item = _scifferContext.ref_item_type.FirstOrDefault(c => c.item_type_id == id);
            return item;
        }

        public List<ref_item_type> GetAll()
        {
            return _scifferContext.ref_item_type.ToList();
        }

        public bool Update(ref_item_type bank)
        {
            try
            {
                _scifferContext.Entry(bank).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
