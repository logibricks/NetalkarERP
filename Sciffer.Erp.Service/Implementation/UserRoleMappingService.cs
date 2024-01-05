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
    public class UserRoleMappingService : IUserRoleMappingService
    {
        private readonly ScifferContext _scifferContext;

        public UserRoleMappingService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public bool Add(ref_user_role_mapping_VM mach)
        {
            try
            {
                int []arr = mach.role_ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                for(var i = 0; i < arr.Length;i++ )
                {
                    ref_user_role_mapping rmc = new ref_user_role_mapping();
                    rmc.user_id = Convert.ToInt32(mach.user_id);
                    rmc.role_id = arr[i];
                    rmc.is_block = mach.is_block;
                    rmc.is_active = true;
                    _scifferContext.ref_user_role_mapping.Add(rmc);
                }
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        
        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_user_role_mapping] set [is_active] = 0 where role_mapping_id = " + id);
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

        public ref_user_role_mapping_VM Get(int id) 
        {
            var result = new List<ref_user_role_mapping_VM>();

            var query = (from c in _scifferContext.ref_user_role_mapping where c.user_id == id

                         select new
                         {
                             user_id = c.user_id,
                             role_id = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == c.user_id).ToList(),
                             is_block=c.is_block,
                         }).ToList()
                         .Select(x=> new ref_user_role_mapping_VM
                         {
                             user_id = x.user_id,
                             role_id = string.Join(",", x.role_id.Select(z => z.role_id).ToArray()),
                             is_block=x.is_block,
                         }).FirstOrDefault();
               
            return query;
        }

        public List<ref_user_role_mapping_VM> GetAll()
        {
            var result = new List<ref_user_role_mapping_VM>();

            var query = (from c in _scifferContext.ref_user_role_mapping
                         join user in _scifferContext.ref_user_management on c.user_id equals user.user_id into user1
                         from user2 in user1.DefaultIfEmpty()

                         join role in _scifferContext.ref_user_management_role on c.role_id equals role.role_id into role1
                         from role2 in role1.DefaultIfEmpty()
                         where user2.is_block == false

                         select new ref_user_role_mapping_VM
                         {
                             user_id = c.user_id,
                             role_id = c.role_id.ToString(),
                             role_mapping_id = c.role_mapping_id,
                             role_name = role2 == null ? "" : role2.role_name,
                             user_name = user2 == null ? "" : user2.user_code + "/" + user2.notes
                         }).ToList();

            result = query.GroupBy(i => new { i.user_id }).Select(x => new ref_user_role_mapping_VM
            {
                user_id = x.Key.user_id,
                role_id = string.Join(",", x.Select(z => z.role_id).ToArray()),
                user_name = x.Select(y => y.user_name).FirstOrDefault(),
                role_name = string.Join(",", x.Select(z => z.role_name).ToArray()),
            }).ToList();

            return result;          
        }

        public bool Update(ref_user_role_mapping_VM mach)
        {
            try
            {
                List<ref_user_role_mapping> list = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == mach.user_id).ToList();
                foreach(var item in list)
                {
                    _scifferContext.Entry(item).State = EntityState.Deleted;
                }
                int[] arr = mach.role_ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                for (var i = 0; i < arr.Length; i++)
                {
                    ref_user_role_mapping rmc = new ref_user_role_mapping();
                    rmc.user_id = Convert.ToInt32(mach.user_id);
                    rmc.role_id = arr[i];
                    rmc.is_block = mach.is_block;
                    rmc.is_active = true;
                    _scifferContext.ref_user_role_mapping.Add(rmc);
                }
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public List<ref_user_role_mapping_VM> getall()
        {
            throw new NotImplementedException();
        }
       
    }
}

