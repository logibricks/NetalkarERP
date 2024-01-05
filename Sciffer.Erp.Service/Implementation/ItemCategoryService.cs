using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public ItemCategoryService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }
        public bool Add(REF_ITEM_CATEGORYVM category)
        {
            try
            {
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = category.ledgeraccounttype.Split(new string[] { "~" }, StringSplitOptions.None);
                }
                catch
                {

                }

                DataTable dt = new DataTable();
                dt.Columns.Add("entity_type_id", typeof(int));
                dt.Columns.Add("gl_ledger_id", typeof(int));
                dt.Columns.Add("ledger_account_type_id", typeof(int));
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {

                    dt.Rows.Add(4, int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1] == string.Empty ? "0" : emptyStringArray[i].Split(new char[] { ',' })[1]), int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0] == string.Empty ? "0" : emptyStringArray[i].Split(new char[] { ',' })[0]));
                }
                int modifyuser = 0;
                var entity = new SqlParameter("@entity", "save");
                var item_category_id = new SqlParameter("@item_category_id", category.ITEM_CATEGORY_ID);
                var item_category_name = new SqlParameter("@item_category_name", category.ITEM_CATEGORY_NAME);
                var item_category_description = new SqlParameter("@item_category_description", category.ITEM_CATEGORY_DESCRIPTION);
                var is_blocked = new SqlParameter("@is_blocked", category.is_blocked);
                var item_type_id = new SqlParameter("@item_type_id", category.item_type_id);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                var modify_user = new SqlParameter("@modify_user", modifyuser);
                var prefix_sufix_id = new SqlParameter("@prefix_sufix_id", category.prefix_sufix_id==null ? 0: category.prefix_sufix_id);
                var prefix_sufix = new SqlParameter("@prefix_sufix", category.prefix_sufix==null ? "" : category.prefix_sufix);
                var from_number = new SqlParameter("@from_number", category.from_number== null ? "": category.from_number);

                t1.TypeName = "dbo.temp_sub_ledger";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_item_category @entity,@item_category_id,@item_category_name,@item_category_description,@is_blocked,@modify_user,@item_type_id,@prefix_sufix_id,@prefix_sufix, @from_number,@t1", entity, item_category_id, item_category_name,
                    item_category_description, is_blocked, modify_user, item_type_id, prefix_sufix_id, prefix_sufix, from_number, t1).FirstOrDefault();
                if (val == "Saved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            // return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var category = _scifferContext.REF_ITEM_CATEGORY.FirstOrDefault(c => c.ITEM_CATEGORY_ID == id);
                category.is_active = false;
                _scifferContext.Entry(category).State = EntityState.Modified;
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

        public REF_ITEM_CATEGORYVM Get(int id)
        {

            REF_ITEM_CATEGORY so = _scifferContext.REF_ITEM_CATEGORY.FirstOrDefault(c => c.ITEM_CATEGORY_ID == id);
            Mapper.CreateMap<REF_ITEM_CATEGORY, REF_ITEM_CATEGORYVM>();
            REF_ITEM_CATEGORYVM sovm = Mapper.Map<REF_ITEM_CATEGORY, REF_ITEM_CATEGORYVM>(so);
            return sovm;
        }

        public List<REF_ITEM_CATEGORYVM> GetAll()
        {
            var query = (from ic in _scifferContext.REF_ITEM_CATEGORY.Where(x => x.is_active == true)
                         join it in _scifferContext.ref_item_type on ic.item_type_id equals it.item_type_id
                         select new REF_ITEM_CATEGORYVM
                         {
                             is_active = ic.is_active,
                             is_blocked = ic.is_blocked,
                             ITEM_CATEGORY_DESCRIPTION = ic.ITEM_CATEGORY_DESCRIPTION,
                             ITEM_CATEGORY_ID = ic.ITEM_CATEGORY_ID,
                             ITEM_CATEGORY_NAME = ic.ITEM_CATEGORY_NAME,
                             item_type_name = it.item_type_name,
                             prefix_sufix_id =ic.prefix_sufix_id==null ? 0 : ic.prefix_sufix_id,
                             prefix_sufix = ic.prefix_sufix==null ? "" :ic.prefix_sufix,
                         }).OrderByDescending(a => a.ITEM_CATEGORY_ID).ToList();
            return query;
        }

        public List<REF_ITEM_CATEGORYVM> GetItemCategoryByItemType(int id)
        {
            var query = (from ic in _scifferContext.REF_ITEM_CATEGORY.Where(x => x.item_type_id == id)
                         select new REF_ITEM_CATEGORYVM
                         {
                             ITEM_CATEGORY_ID = ic.ITEM_CATEGORY_ID,
                             ITEM_CATEGORY_NAME = ic.ITEM_CATEGORY_NAME,
                             ITEM_CATEGORY_DESCRIPTION = ic.ITEM_CATEGORY_DESCRIPTION
                         }).ToList();
            return query;
        }
    }
}
