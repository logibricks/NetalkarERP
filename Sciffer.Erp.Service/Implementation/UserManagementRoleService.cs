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
    public class UserManagementRoleService : IUserManagementRoleService
    {
        private readonly ScifferContext _scifferContext;

        public UserManagementRoleService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_user_management_role Add(ref_user_management_role mach)
        {

            try
            {
                ref_user_management_role rmc = new ref_user_management_role();
                rmc.role_code = mach.role_code;
                rmc.role_name = mach.role_name;
                rmc.is_block = false;
                _scifferContext.ref_user_management_role.Add(rmc);

                List<ref_module_form> list = _scifferContext.ref_module_form.ToList();
                foreach (var rights in list)
                {
                    ref_user_role_rights ru = new ref_user_role_rights();
                    ru.module_form_id = rights.module_form_id;
                    ru.view_rights = false;
                    ru.create_rights = false;
                    ru.create_rights = false;
                    _scifferContext.ref_user_role_rights.Add(ru);
                }

                _scifferContext.SaveChanges();
                rmc.role_id = _scifferContext.ref_user_management_role.Max(x => x.role_id);
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
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_user_management_role] set [IS_ACTIVE] = 0 where role_id = " + id);
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

        public ref_user_management_role Get(int id)
        {
            var mach = _scifferContext.ref_user_management_role.FirstOrDefault(c => c.role_id == id);
            return mach;
        }

        public List<ref_user_management_role> GetAll()
        {
            return _scifferContext.ref_user_management_role.ToList();
        }

        public ref_user_management_role Update(ref_user_management_role mach)
        {
            try
            {
               // mach.is_block = false;
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

