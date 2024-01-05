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
    public class ReasonDeterminationService : IReasonDeterminationService
    {
        private readonly ScifferContext _scifferContext;

        public ReasonDeterminationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public reasonvm Add(reasonvm reason)
        {
            try
            {
                REF_REASON_DETERMINATION rs = new REF_REASON_DETERMINATION();
                rs.REASON_DETERMINATION_ID = reason.REASON_DETERMINATION_ID;
                rs.REASON_DETERMINATION_NAME = reason.REASON_DETERMINATION_NAME;
                rs.REASON_DETERMINATION_TYPE = reason.REASON_DETERMINATION_TYPE;
                rs.is_blocked = reason.is_blocked;
                rs.is_active = true;
                _scifferContext.REF_REASON_DETERMINATION.Add(rs);
                _scifferContext.SaveChanges();
                reason.REASON_DETERMINATION_ID = _scifferContext.REF_REASON_DETERMINATION.Max(x => x.REASON_DETERMINATION_ID);
                reason.REASON_DETERMINATION_TYPE_NAME = reason.REASON_DETERMINATION_TYPE == 1 ? "Stock Increase" : "Stock Decrease";
            }
            catch (Exception ex)
            {
                return reason;
            }
            return reason;
        }

        public bool Delete(int id)
        {
            try
            {
                var reason = _scifferContext.REF_REASON_DETERMINATION.FirstOrDefault(c => c.REASON_DETERMINATION_ID == id);
                reason.is_active = false;
                _scifferContext.Entry(reason).State = EntityState.Modified;
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

        public REF_REASON_DETERMINATION Get(int id)
        {
            var reason = _scifferContext.REF_REASON_DETERMINATION.FirstOrDefault(c => c.REASON_DETERMINATION_ID == id);
            return reason;
        }

        public List<REF_REASON_DETERMINATION> GetAll()
        {
            return _scifferContext.REF_REASON_DETERMINATION.Where(x=>x.is_active==true).ToList();
        }

        public reasonvm Update(reasonvm reason)
        {
            try
            {
                REF_REASON_DETERMINATION rs = new REF_REASON_DETERMINATION();
                rs.REASON_DETERMINATION_ID = reason.REASON_DETERMINATION_ID;
                rs.REASON_DETERMINATION_NAME = reason.REASON_DETERMINATION_NAME;
                rs.REASON_DETERMINATION_TYPE = reason.REASON_DETERMINATION_TYPE;
                rs.is_blocked = reason.is_blocked;
                rs.is_active = true;
                _scifferContext.Entry(rs).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                reason.REASON_DETERMINATION_TYPE_NAME = reason.REASON_DETERMINATION_TYPE == 1 ? "Stock Increase" : "Stock Decrease";
            }
            catch (Exception ex)
            {
                return reason;
            }
            return reason;
        }
        public List<REF_REASON_DETERMINATION> GetReasonByCode(string code)
        {
            return _scifferContext.REF_REASON_DETERMINATION.Where(x => x.transaction_type_code == code && x.is_active == true).ToList();
            
        }

        public List<reasonvm> GetReasonList(int id)
        {
            List<reasonvm> query=new List<reasonvm>();
            if(id==0)
            {
                query = (from r in _scifferContext.REF_REASON_DETERMINATION.Where(x => x.is_active == true)
                         select new reasonvm
                         {
                             REASON_DETERMINATION_ID = r.REASON_DETERMINATION_ID,
                             REASON_DETERMINATION_NAME = r.REASON_DETERMINATION_NAME,
                             REASON_DETERMINATION_TYPE_NAME = r.REASON_DETERMINATION_TYPE == 1 ? "Stock Increase" : "Stock Decrease",
                             is_blocked = r.is_blocked,
                             REASON_DETERMINATION_TYPE = r.REASON_DETERMINATION_TYPE,
                             //r.REASON_DETERMINATION_TYPE == 2  ? "Goods Receipt" : "-"                                          
                         }).OrderByDescending(a => a.REASON_DETERMINATION_ID).ToList();
            }
          else
            {
                query = (from r in _scifferContext.REF_REASON_DETERMINATION.Where(x => x.is_active == true && x.REASON_DETERMINATION_TYPE==id)
                         select new reasonvm
                         {
                             REASON_DETERMINATION_ID = r.REASON_DETERMINATION_ID,
                             REASON_DETERMINATION_NAME = r.REASON_DETERMINATION_NAME,
                             REASON_DETERMINATION_TYPE_NAME = r.REASON_DETERMINATION_TYPE == 1 ? "Stock Increase" : "Stock Decrease",
                             is_blocked = r.is_blocked,
                             REASON_DETERMINATION_TYPE = r.REASON_DETERMINATION_TYPE,
                             //r.REASON_DETERMINATION_TYPE == 2  ? "Goods Receipt" : "-"                                          
                         }).ToList();
            }
            return query;
        }

        public List<reasonvm> GetReasonListByCode(string code)
        {
            var query = (from r in _scifferContext.REF_REASON_DETERMINATION.Where(x => x.is_active == true && x.transaction_type_code==code)
                         select new reasonvm
                         {
                             REASON_DETERMINATION_ID = r.REASON_DETERMINATION_ID,
                             REASON_DETERMINATION_NAME = r.REASON_DETERMINATION_NAME,
                         }).ToList();
           return query;
        }                       
    }
}
