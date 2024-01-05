using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Implementation
{
    public class GradeService: IGradeService
    {
        private readonly ScifferContext _scifferContext;

        public GradeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(REF_GRADE grade)
        {
            try
            {
                _scifferContext.REF_GRADE.Add(grade);
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
                var grade = _scifferContext.REF_GRADE.FirstOrDefault(c => c.grade_id == id);
                _scifferContext.Entry(grade).State = EntityState.Deleted;
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
        public REF_GRADE Get(int id)
        {
            var grade = _scifferContext.REF_GRADE.FirstOrDefault(c => c.grade_id == id);
            return grade;
        }

        public List<REF_GRADE> GetAll()
        {
            return _scifferContext.REF_GRADE.ToList();
        }
        public bool Update(REF_GRADE grade)
        {
            try
            {
                _scifferContext.Entry(grade).State = EntityState.Modified;
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
