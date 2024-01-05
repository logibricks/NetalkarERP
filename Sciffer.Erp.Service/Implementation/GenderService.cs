using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using AutoMapper;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class GenderService : IGenderService
    {
        private readonly ScifferContext _scifferContext;
        public GenderService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }


        public bool Add(REF_GENDER gender)
        {
            try
            { 
            _scifferContext.REF_GENDER.Add(gender);
            _scifferContext.SaveChanges();
            }

            catch(Exception e)
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

        public REF_GENDER Get(int id)
        {
            return _scifferContext.REF_GENDER.FirstOrDefault(c => c.GENDER_ID == id);
        }

        public List<REF_GENDER> GetAll()
        {
            return _scifferContext.REF_GENDER.ToList();
        }

        public bool Update(REF_GENDER gender)
        {
            try
            { 
            _scifferContext.Entry(gender).State = EntityState.Modified;
            _scifferContext.SaveChanges();
            }
            catch(Exception  e)
            {
                return false;
            }
            return true;
        }
    }
}
