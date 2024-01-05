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
    public class UserManagementService : IUserManagementService
    {
        private readonly ScifferContext _scifferContext;

        public UserManagementService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_user_management_VM Add(ref_user_management_VM mach)
        {
            try
            {
                ref_user_management rmc = new ref_user_management();

                rmc.user_name = mach.user_name;
                rmc.user_code = mach.user_code;
                rmc.employee_id = mach.employee_id;
                rmc.email = mach.email;
                rmc.branch_id = mach.branch_id;
                rmc.department_id = mach.department_id;
                rmc.password = mach.password;
                rmc.is_authentication = mach.is_authentication;
                rmc.mobile_no = mach.mobile_no;
                rmc.notes = mach.notes;
                rmc.is_block = mach.is_block;
                _scifferContext.ref_user_management.Add(rmc);
                _scifferContext.SaveChanges();
                rmc.user_id = _scifferContext.ref_user_management.Max(x => x.user_id);
                return mach;
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
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_user_management] set [IS_ACTIVE] = 0 where user_id = " + id);
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

        public ref_user_management Get(int id)
        {
            var mach = _scifferContext.ref_user_management.FirstOrDefault(c => c.user_id == id);
            return mach;
        }

        public List<ref_user_management_VM> GetAll()
        {
            var query = (from c in _scifferContext.ref_user_management
                         join emp in _scifferContext.REF_EMPLOYEE on c.employee_id equals emp.employee_id into emp1
                         from emp2 in emp1.DefaultIfEmpty()
                         join branch in _scifferContext.REF_BRANCH on c.branch_id equals branch.BRANCH_ID into branch1
                         from branch2 in branch1.DefaultIfEmpty()
                         join dep in _scifferContext.REF_DEPARTMENT on c.department_id equals dep.DEPARTMENT_ID into dep1
                         from dep2 in dep1.DefaultIfEmpty()
                        

                         select new ref_user_management_VM
                         {
                             user_id = c.user_id,
                             user_name = c.user_name, //+"/"+c.notes,
                             user_code = c.user_code, //+ "/" + c.notes,
                             employee_id = (int)c.employee_id,
                             email = c.email,
                             branch_id = (int)c.branch_id,
                             department_id = (int)c.department_id,
                             password = c.password,
                              is_authentication=c.is_authentication,
                             is_block=c.is_block,
                             notes = c.notes,
                             mobile_no = c.mobile_no,
                             employee_name = emp2 == null ? "" : emp2.employee_name,
                             branch_name = branch2 == null ? "" : branch2.BRANCH_NAME,
                             department_name = dep2 == null ? "" : dep2.DEPARTMENT_NAME,
                           

                         }).ToList();
            return query;
        }
        
        public ref_user_management_VM Update(ref_user_management_VM mach)
        {
            try
            {
                ref_user_management rmc = _scifferContext.ref_user_management.FirstOrDefault(c => c.user_id == mach.user_id);
                
                rmc.user_id = mach.user_id;
                rmc.user_name = mach.user_name;
                rmc.user_code = mach.user_code;
                rmc.employee_id = mach.employee_id;
                rmc.email = mach.email;
                rmc.branch_id = mach.branch_id;
                rmc.department_id = mach.department_id;
                rmc.password = mach.password;
                rmc.is_authentication = mach.is_authentication;
                rmc.notes = mach.notes;
                rmc.mobile_no = mach.mobile_no;
                rmc.is_block = mach.is_block;
                
                _scifferContext.Entry(rmc).State = EntityState.Modified;
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


