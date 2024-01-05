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
    public class FrequencyService : IFrequencyService
    {
        private readonly ScifferContext _scifferContext;
        public FrequencyService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public bool Add(ref_frequency Frequency)
        {
            try
            {
                 _scifferContext.ref_frequency.Add(Frequency);
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int? id)
        {
            try
            {
                _scifferContext.ref_frequency.Remove(_scifferContext.ref_frequency.Find(id));
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

        public ref_frequency Get(int? id)
        {
            try
            {
                return _scifferContext.ref_frequency.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_frequency> GetAll()
        {
            try
            {
                return _scifferContext.ref_frequency.OrderByDescending(a => a.frequency_id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ref_frequency Frequency)
        {
            try
            {
                _scifferContext.Entry(Frequency).State = System.Data.Entity.EntityState.Modified;
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
