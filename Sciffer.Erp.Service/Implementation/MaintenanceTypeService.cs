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
    public class MaintenanceTypeService : IMaintenanceTypeService
    {
        private readonly ScifferContext _scifferContext;
        public MaintenanceTypeService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(ref_maintenance_type Main)
        {
            try
            {
                _scifferContext.ref_maintenance_type.Add(Main);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.ref_maintenance_type.Remove(_scifferContext.ref_maintenance_type.Find(id));
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

        public ref_maintenance_type Get(int id)
        {
            try
            {
                return _scifferContext.ref_maintenance_type.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ref_maintenance_type> GetAll()
        {
            try
            {
                return _scifferContext.ref_maintenance_type.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(ref_maintenance_type Main)
        {
            try
            {
                _scifferContext.Entry(Main).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
