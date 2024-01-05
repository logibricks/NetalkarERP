using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class StorageLocation : IStorageLocation
    {
        private readonly ScifferContext _scifferContext;

        public StorageLocation(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public storage_vm Add(storage_vm storage)
        {
            try
            {
                int i = 0;
                var entity = new SqlParameter("@entity", "save");
                var storage_location_id = new SqlParameter("@storage_location_id", storage.storage_location_id);
                var storage_location_name = new SqlParameter("@storage_location_name", storage.storage_location_name);
                var description = new SqlParameter("@description", storage.description);
                var plant_id = new SqlParameter("@plant_id", storage.plant_id);
                var is_blocked = new SqlParameter("@is_blocked", storage.is_blocked);
                var modify_user = new SqlParameter("@modify_user", i);
                int val = _scifferContext.Database.SqlQuery<int>(
                    "exec save_storage_location @entity,@storage_location_id,@storage_location_name,@description,@plant_id,@is_blocked,@modify_user", entity, storage_location_id,
                    storage_location_name, description, plant_id, is_blocked, modify_user).FirstOrDefault();
                if (val != 0)
                {
                    storage.storage_location_id = val;
                    storage.plant_name = _scifferContext.REF_PLANT.Where(x => x.PLANT_ID == storage.plant_id).FirstOrDefault().PLANT_NAME;
                    return storage;
                }
                else
                {
                    return storage;
                }

            }
            catch (Exception ex)
            {
                return storage;
            }
            return storage;
        }

        public bool Delete(int id)
        {
            try
            {
                var storage = _scifferContext.REF_STORAGE_LOCATION.FirstOrDefault(c => c.storage_location_id == id);
                storage.is_active = false;
                _scifferContext.Entry(storage).State = EntityState.Modified;
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

        public REF_STORAGE_LOCATION Get(int id)
        {
            var storage = _scifferContext.REF_STORAGE_LOCATION.FirstOrDefault(c => c.storage_location_id == id);
            return storage;
        }

        public List<REF_STORAGE_LOCATION> GetAll()
        {
            return _scifferContext.REF_STORAGE_LOCATION.Where(x => x.is_active == true).ToList();
        }

        public storage_vm Update(storage_vm storage)
        {
            try
            {
                REF_STORAGE_LOCATION re = new REF_STORAGE_LOCATION();
                re.is_active = true;
                re.is_blocked = storage.is_blocked;
                re.description = storage.description;
                re.storage_location_name = storage.storage_location_name;
                re.plant_id = storage.plant_id;
                re.storage_location_id = storage.storage_location_id;
                _scifferContext.Entry(re).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                storage.plant_name = _scifferContext.REF_PLANT.Where(x => x.PLANT_ID == storage.plant_id).FirstOrDefault().PLANT_NAME;
            }
            catch (Exception)
            {
                return storage;
            }
            return storage;
        }

        public List<storage_vm> getstoragelist()
        {
            var query = (from s in _scifferContext.REF_STORAGE_LOCATION.Where(x => x.is_active == true)
                         join p in _scifferContext.REF_PLANT on s.plant_id equals p.PLANT_ID
                         select new storage_vm
                         {
                             storage_location_id = s.storage_location_id,
                             storage_location_name = s.storage_location_name,
                             plant_name = p.PLANT_CODE + " - " + p.PLANT_NAME,
                             description = s.description,
                             plant_id = s.plant_id,
                             is_blocked = s.is_blocked,
                         }).OrderByDescending(a => a.storage_location_id).ToList();
            return query;
        }

        public List<storage_vm> getstoragelistUsingDocumentId(int docId)
        {
            var plantId = _scifferContext.ref_document_numbring.Where(x => x.module_form_id == 76 && x.is_blocked == false && x.document_numbring_id == docId).OrderByDescending(x => x.set_default).OrderByDescending(x => x.set_default).FirstOrDefault().plant_id;


            var query = (from s in _scifferContext.REF_STORAGE_LOCATION.Where(x => x.is_active == true && x.plant_id == plantId)
                         join p in _scifferContext.REF_PLANT on s.plant_id equals p.PLANT_ID
                         select new storage_vm
                         {
                             storage_location_id = s.storage_location_id,
                             storage_location_name = s.storage_location_name,
                             plant_name = p.PLANT_CODE + " - " + p.PLANT_NAME,
                             description = s.description,
                             plant_id = s.plant_id,
                             is_blocked = s.is_blocked,
                         }).OrderByDescending(a => a.storage_location_id).ToList();
            return query;
        }
        public int GetID(string st)
        {
            var X = _scifferContext.REF_STORAGE_LOCATION.Where(x => x.storage_location_name == st).FirstOrDefault();
            var id = X == null ? "0" : X.storage_location_id.ToString();
            return int.Parse(id);
        }
    }
}
