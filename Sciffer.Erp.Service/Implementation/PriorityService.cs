using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Linq;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PriorityService : IPriorityService
    {
        private readonly ScifferContext _scifferContext;

        public PriorityService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_priority_vm Add(ref_priority_vm priority)
        {
            try
            {
                REF_PRIORITY rp = new REF_PRIORITY();
                rp.form_id = priority.form_id;
                rp.PRIORITY_ID = priority.PRIORITY_ID;
                rp.PRIORITY_NAME = priority.PRIORITY_NAME;
                rp.is_active = true;
                rp.is_blocked = priority.is_blocked;
                _scifferContext.REF_PRIORITY.Add(rp);
                _scifferContext.SaveChanges();
                 priority.PRIORITY_ID = _scifferContext.REF_PRIORITY.Max(x => x.PRIORITY_ID);
                priority.form_name = priority.form_id == 1 ? "Customer Master" : priority.form_id == 2 ? "Vendor Master" : priority.form_id == 3 ? "Item Master" : "Machine Master";
            }
            catch (Exception)
            {
                return priority;
            }
            return priority;
        }

        public bool Delete(int id)
        {
            try
            {
                var state = _scifferContext.REF_PRIORITY.FirstOrDefault(c => c.PRIORITY_ID == id);
                state.is_active = false;
                _scifferContext.Entry(state).State = EntityState.Modified;
               // state.is_active = false;
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

        public REF_PRIORITY Get(int id)
        {
            var priority = _scifferContext.REF_PRIORITY.FirstOrDefault(c => c.PRIORITY_ID == id);
            return priority;
        }

        public List<ref_priority_vm> GetAll()
        {
            var query = (from p in _scifferContext.REF_PRIORITY.Where(x => x.is_active == true)
                         select new ref_priority_vm
                         {
                             form_id = p.form_id,
                              form_name=p.form_id==1 ? "Customer Master": p.form_id == 2 ? "Vendor Master" : p.form_id == 3 ? "Item Master" : "Machine Master",
                               is_active=p.is_active,
                                PRIORITY_ID=p.PRIORITY_ID,
                                 PRIORITY_NAME=p.PRIORITY_NAME,
                                 is_blocked=p.is_blocked,
                         }).ToList();
            return query;
        }

        public ref_priority_vm Update(ref_priority_vm priority)
        {
            try
            {
                REF_PRIORITY rp = new REF_PRIORITY();
                rp.form_id = priority.form_id;
                rp.PRIORITY_ID = priority.PRIORITY_ID;
                rp.PRIORITY_NAME = priority.PRIORITY_NAME;
                rp.is_active = true;
                rp.is_blocked = priority.is_blocked;
                _scifferContext.Entry(rp).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                priority.form_name = priority.form_id == 1 ? "Customer Master" : priority.form_id == 2 ? "Vendor Master" : priority.form_id == 3 ? "Item Master" : "Machine Master";

            }
            catch (Exception)
            {
                return priority;
            }
            return priority;
        }
    }
}
