using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class ModeOfTransportService : IModeOfTransportService
    {
        private readonly ScifferContext _scifferContext;
        public ModeOfTransportService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public ref_mode_of_transport Add(ref_mode_of_transport trasport)
        {
            _scifferContext.ref_mode_of_transport.Add(trasport);
            _scifferContext.SaveChanges();
            trasport.mode_of_transport_id = _scifferContext.ref_mode_of_transport.Max(x => x.mode_of_transport_id);
            return trasport;
        }

        public bool Delete(int id)
        {
            var trasport = _scifferContext.ref_mode_of_transport.FirstOrDefault(c => c.mode_of_transport_id == id);           
            _scifferContext.Entry(trasport).State = EntityState.Modified;
            _scifferContext.SaveChanges();
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

        public ref_mode_of_transport Get(int id)
        {
            return _scifferContext.ref_mode_of_transport.FirstOrDefault(c => c.mode_of_transport_id == id);
        }

        public List<ref_mode_of_transport> GetAll()
        {
            return _scifferContext.ref_mode_of_transport.ToList();
        }

        public ref_mode_of_transport Update(ref_mode_of_transport trasport)
        {
            _scifferContext.Entry(trasport).State = EntityState.Modified;
            _scifferContext.SaveChanges();
            return trasport;
        }
    }
}
