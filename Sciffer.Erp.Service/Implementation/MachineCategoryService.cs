using AutoMapper;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class MachineCategoryService : IMachineCategoryService
    {
        private readonly ScifferContext _scifferContext;

        public MachineCategoryService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_machine_category Add(ref_machine_category mach)
        {
            try
            {
                ref_machine_category rmc = new ref_machine_category();

                //rmc.machine_category_id = mach.machine_category_id;
                rmc.machine_category_code = mach.machine_category_code;
                rmc.machine_category_description = mach.machine_category_description;
                rmc.is_active = true;
                rmc.is_blocked = false;
                _scifferContext.ref_machine_category.Add(rmc);
                _scifferContext.SaveChanges();
                rmc.machine_category_id = _scifferContext.ref_machine_category.Max(x => x.machine_category_id);
                return rmc;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_machine_category] set [IS_ACTIVE] = 0 where machine_category_id = " + id);
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

        public ref_machine_category Get(int id)
        {
            var mach = _scifferContext.ref_machine_category.FirstOrDefault(c => c.machine_category_id == id);
            return mach;
        }

        public List<ref_machine_category> GetAll()
        {
            return _scifferContext.ref_machine_category.Where(x => x.is_active == true).OrderByDescending(a => a.machine_category_id).ToList();
        }

        public ref_machine_category Update(ref_machine_category mach)
        {
            try
            {
                mach.is_active = true;
                mach.is_blocked = false;
                _scifferContext.Entry(mach).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return mach;
            }
            return mach;
        }
    }
}

