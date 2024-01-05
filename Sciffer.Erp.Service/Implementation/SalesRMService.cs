using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class SalesRMService : ISalesRMService
    {
        private readonly ScifferContext _scifferContext; 
        public SalesRMService (ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public bool Add(ref_sales_rm vm)
        {
            try
            {
                vm.is_active = true;
                _scifferContext.ref_sales_rm.Add(vm);
                _scifferContext.SaveChanges();
                

            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var sales= _scifferContext.ref_sales_rm.Where(x => x.sales_rm_id == id).FirstOrDefault();
                sales.is_active = false;
                _scifferContext.Entry(sales).State = System.Data.Entity.EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public ref_sales_rm Get(int id)
        {
            var sale = _scifferContext.ref_sales_rm.FirstOrDefault(c => c.sales_rm_id == id);
            return sale;
        }

        public List<Sales_RM_VM> GetAll()
        {
            var list = (from ed in _scifferContext.ref_sales_rm.Where(x => x.is_active == true)
                        join e in _scifferContext.REF_EMPLOYEE on ed.employee_id equals e.employee_id
                        select new Sales_RM_VM
                        {
                            sales_rm_id = ed.sales_rm_id,
                            employee_id = ed.employee_id,
                            employee_code = e.employee_code,
                            employee_name = e.employee_name,
                            is_blocked = ed.is_blocked,

                        }).ToList();
            return list;
        }

        public bool Update(ref_sales_rm vm)
        {
            try
            {
                vm.is_active = true;
                _scifferContext.Entry(vm).State = System.Data.Entity.EntityState.Modified;
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
    }
}
