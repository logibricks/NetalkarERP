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
    public class UserRoleRightsService : IUserRoleRightsService
    {
        private readonly ScifferContext _scifferContext;

        public UserRoleRightsService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public List<ref_user_role_rights_VM> GetAllFromModules(int role_id)
        {

            var query = (from m in _scifferContext.ref_module_form
                         join r in _scifferContext.ref_user_role_rights.Where(x => x.role_id == role_id) on m.module_form_id equals r.module_form_id into j1
                         from r1 in j1.DefaultIfEmpty()
                         select new ref_user_role_rights_VM
                         {
                             module_form_id = m.module_form_id,
                             module_form_name = m.module_form_name,
                             role_id = r1 == null ? 0 : r1.role_id,
                             create_rights = r1 == null ? false : r1.create_rights,
                             edit_rights = r1 == null ? false : r1.edit_rights,
                             view_rights = r1 == null ? false : r1.view_rights,
                         }).ToList();

            //var list = new List<ref_user_role_rights_VM>();

            //try
            //{

            //    list = (from r in _scifferContext.ref_user_role_rights
            //            where r.role_id == role_id
            //            join m in _scifferContext.ref_module_form on r.module_form_id equals m.module_form_id
            //            select new
            //            {
            //                role_id = r.role_id,
            //                module_form_id = r.module_form_id,
            //                module_form_name = m.module_form_name,
            //                create_rights = r.create_rights,
            //                edit_rights = r.edit_rights,
            //                view_rights = r.view_rights,


            //            }).ToList()
            //               .Select(x => new ref_user_role_rights_VM
            //               {
            //                   role_id = x.role_id,
            //                   module_form_id = x.module_form_id,
            //                   module_form_name = x.module_form_name,
            //                   create_rights = x.create_rights,
            //                   edit_rights = x.edit_rights,
            //                   view_rights = x.view_rights,
            //               }).ToList();

            //}
            //catch (Exception ex)
            //{
            //    return null;

            //}
            return query;


        }

        public bool UpdateRecords(ref_user_role_rights_VM vm)
        {
            try
            {
                List<ref_user_role_rights> list = _scifferContext.ref_user_role_rights.Where(x => x.role_id == vm.role_id).ToList();
                foreach (var item in list)
                {

                    _scifferContext.Entry(item).State = EntityState.Deleted;
                }

                for (var i = 0; i < vm.form_id_name.Count; i++)
                {
                    {
                        ref_user_role_rights rmc = new ref_user_role_rights();
                        rmc.role_id = vm.role_id;
                        rmc.module_form_id = Convert.ToInt32(vm.form_id_name[i]);
                        rmc.create_rights = Convert.ToBoolean(vm.create_rights_name[i]);
                        rmc.edit_rights = Convert.ToBoolean(vm.edit_rights_name[i]);
                        rmc.view_rights = Convert.ToBoolean(vm.view_rights_name[i]);

                        _scifferContext.ref_user_role_rights.Add(rmc);
                    }
                }
                _scifferContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;

            }
            return true;

        }
        }


}

