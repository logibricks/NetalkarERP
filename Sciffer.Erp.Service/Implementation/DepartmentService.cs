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
    public class DepartmentService : IDepartmentService
    {
        private readonly ScifferContext _scifferContext;
        public DepartmentService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }


        public bool Add(REF_DEPARTMENT dept)
        {
            try
            {
                _scifferContext.REF_DEPARTMENT.Add(dept);
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

        public REF_DEPARTMENT Get(int id)
        {
            return _scifferContext.REF_DEPARTMENT.FirstOrDefault(c => c.DEPARTMENT_ID == id);
        }

        public List<REF_DEPARTMENT> GetAll()
        {
            return _scifferContext.REF_DEPARTMENT.OrderByDescending(a => a.DEPARTMENT_ID).ToList();
        }

        public bool Update(REF_DEPARTMENT dept)
        {
            try
            {
                _scifferContext.Entry(dept).State = EntityState.Modified;
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
