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
    public class BranchService : IBranchService
    {
        private readonly ScifferContext _scifferContext;
        public BranchService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

        public REF_BRANCH Add(REF_BRANCH BRANCH)
        {
            try
            {
                BRANCH.is_active = true;
                _scifferContext.REF_BRANCH.Add(BRANCH);
                _scifferContext.SaveChanges();
                BRANCH.BRANCH_ID = _scifferContext.REF_BRANCH.Max(x => x.BRANCH_ID);
            }
            catch (Exception ex)
            {
                return BRANCH;
            }
            return BRANCH;
        }

        public bool Delete(int id)
        {
            try
            {
                var batch = _scifferContext.REF_BRANCH.FirstOrDefault(c => c.BRANCH_ID == id);
                _scifferContext.Entry(batch).State = EntityState.Modified;
                batch.is_active = false;
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


        public REF_BRANCH Get(int id)
        {

            var BRANCH = _scifferContext.REF_BRANCH.FirstOrDefault(c => c.BRANCH_ID == id);
            return BRANCH;
        }

        public List<REF_BRANCH> GetAll()
        {
            return _scifferContext.REF_BRANCH.Where(x => x.is_active == true).ToList();
        }

        public REF_BRANCH Update(REF_BRANCH BRANCH)
        {
            try
            {
                BRANCH.is_active = true;
                _scifferContext.Entry(BRANCH).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return BRANCH;
            }
            return BRANCH;
        }
    }
}
