using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class StateService : IStateService
    {
        private readonly ScifferContext _scifferContext;

        public StateService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public state_vm Add(state_vm state)
        {
            try
            {
                REF_STATE rs = new REF_STATE();
                rs.COUNTRY_ID = state.Country_id;
                rs.STATE_ID = state.state_id;
                rs.STATE_NAME = state.state_name;
                rs.is_active = true;
                rs.is_blocked = state.is_blocked;
                rs.state_ut_code = state.state_ut_code;
                _scifferContext.REF_STATE.Add(rs);
                _scifferContext.SaveChanges();
                state.state_id = _scifferContext.REF_STATE.Max(x => x.STATE_ID);
                state.country_name = _scifferContext.REF_COUNTRY.Where(x => x.COUNTRY_ID == state.Country_id).FirstOrDefault().COUNTRY_NAME;
            }
            catch (Exception)
            {
                return state;
            }
            return state;
        }

        public bool Delete(int id)
        {
            try
            {
                var state = _scifferContext.REF_STATE.FirstOrDefault(c => c.STATE_ID == id);
                state.is_active = false;
                _scifferContext.Entry(state).State = EntityState.Modified;
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

        public REF_STATE Get(int id)
        {
            var state = _scifferContext.REF_STATE.FirstOrDefault(c => c.STATE_ID == id);
            return state;
        }

        public List<REF_STATE> GetAll()
        {

            return _scifferContext.REF_STATE.ToList().Where(x => x.is_active == true).ToList();
        }

        public state_vm Update(state_vm state)
        {
            try
            {
                REF_STATE rs = new REF_STATE();
                rs.COUNTRY_ID = state.Country_id;
                rs.STATE_ID = state.state_id;
                rs.STATE_NAME = state.state_name;
                rs.is_active = true;
                rs.is_blocked = state.is_blocked;
                rs.state_ut_code = state.state_ut_code;
                _scifferContext.Entry(rs).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                state.country_name = _scifferContext.REF_COUNTRY.Where(x => x.COUNTRY_ID == state.Country_id).FirstOrDefault().COUNTRY_NAME;
            }
            catch (Exception)
            {
                return state;
            }
            return state;
        }

        public List<state_vm> GetStateList()
        {
            var query = (from s in _scifferContext.REF_STATE.Where(x=>x.is_active==true)
                         join c in _scifferContext.REF_COUNTRY on s.COUNTRY_ID equals c.COUNTRY_ID
                         select new state_vm
                         {
                             state_id = s.STATE_ID,
                             state_name=s.STATE_NAME,
                             country_name=c.COUNTRY_NAME,
                             Country_id = s.COUNTRY_ID,
                             is_blocked=s.is_blocked,
                             state_ut_code=s.state_ut_code,
                         }).OrderByDescending(a => a.state_id).ToList();
            return query;
        }

        public state_vm Add(REF_STATE state)
        {
            throw new NotImplementedException();
        }

      
    }
}
