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
    public class ParameterListService : IParameterListService
    {
        private readonly ScifferContext _scifferContext;

        public ParameterListService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_parameter_list Add(ref_parameter_list finance)
        {
            try
            {
                finance.is_active = true;
                _scifferContext.ref_parameter_list.Add(finance);
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return finance;
            }
            return finance;
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

        public bool Delete(int id)
        {
            try
            {
                var itemvaluation = _scifferContext.ref_parameter_list.FirstOrDefault(c => c.parameter_id == id);
                itemvaluation.is_active = false;
                _scifferContext.Entry(itemvaluation).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public ref_parameter_list Get(int id)
        {
            var itemvaluation = _scifferContext.ref_parameter_list.FirstOrDefault(c => c.parameter_id == id && c.is_active==true && c.is_blocked==false);
            return itemvaluation;
        }

        public List<ref_parameter_list_VM> GetAll()
        {
            //return _scifferContext.ref_parameter_list.Where(x => x.is_active == true).ToList();
            var list = (from ed in _scifferContext.ref_parameter_list.Where(x => x.is_active == true)
                        select new ref_parameter_list_VM
                        {
                            parameter_id = ed.parameter_id,
                            parameter_code = ed.parameter_code,
                            parameter_desc = ed.parameter_desc ,
                            parameter_range = ed.parameter_range,
                            is_blocked = ed.is_blocked,
                        }).ToList();
            return list;
        }

        public List<ref_parameter_list_VM> GetUnBlockedParameterList()
        {
            //return _scifferContext.ref_parameter_list.Where(x => x.is_active == true).ToList();
            var list = (from ed in _scifferContext.ref_parameter_list.Where(x => x.is_active == true && x.is_blocked == false)
                        select new ref_parameter_list_VM
                        {
                            parameter_id = ed.parameter_id,
                            parameter_code = ed.parameter_code,
                            parameter_desc = ed.parameter_code + " - " + ed.parameter_desc,
                            parameter_range = ed.parameter_range,
                        }).ToList();
            return list;
        }

        public ref_parameter_list Update(ref_parameter_list finance)
        {
            try
            {
                finance.is_active = true;
                _scifferContext.Entry(finance).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return finance;
            }
            return finance;
        }
    }
}
