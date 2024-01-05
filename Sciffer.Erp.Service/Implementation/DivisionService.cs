using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class DivisionService : IDivisionService
    {
        private readonly ScifferContext _scifferContext;
        public DivisionService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }


        public bool Add(REF_DIVISION div)
        {
            try
            {
                _scifferContext.REF_DIVISION.Add(div);
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

        public REF_DIVISION Get(int id)
        {
            return _scifferContext.REF_DIVISION.FirstOrDefault(c => c.DIVISION_ID == id);
        }

        public List<REF_DIVISION> GetAll()
        {
            return _scifferContext.REF_DIVISION.ToList();
        }

        public bool Update(REF_DIVISION div)
        {
            try
            {
                _scifferContext.Entry(div).State = EntityState.Modified;
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
