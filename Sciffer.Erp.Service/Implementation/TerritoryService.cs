using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class TerritoryService: ITerritoryService
    {
        private readonly ScifferContext _scifferContext;

        public TerritoryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public REF_TERRITORY Add(REF_TERRITORY territory)
        {
            try
            {
                territory.is_active = true;
                _scifferContext.REF_TERRITORY.Add(territory);
                _scifferContext.SaveChanges();
                territory.TERRITORY_ID = _scifferContext.REF_TERRITORY.Max(x => x.TERRITORY_ID);
            }
            catch (Exception)
            {
                return territory;
            }
            return territory;
        }

        public bool Delete(int id)
        {
            try
            {
                var state = _scifferContext.REF_TERRITORY.FirstOrDefault(c => c.TERRITORY_ID == id);
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

        public REF_TERRITORY Get(int id)
        {
            var territory = _scifferContext.REF_TERRITORY.FirstOrDefault(c => c.TERRITORY_ID == id);
            return territory;
        }

        public List<REF_TERRITORY> GetAll()
        {
            return _scifferContext.REF_TERRITORY.ToList().Where(x=>x.is_active==true).ToList();
        } 

        public REF_TERRITORY Update(REF_TERRITORY territory)
        {
            try
            {
                territory.is_active = true;
                _scifferContext.Entry(territory).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return territory;
            }
            return territory;
        }
    }
}
