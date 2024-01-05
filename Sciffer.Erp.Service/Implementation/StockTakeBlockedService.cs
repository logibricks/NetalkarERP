using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using System.Web;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class StockTakeBlockedService : IStockTakeBlockedService
    {
        private readonly ScifferContext _scifferContext;

        public StockTakeBlockedService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public stock_take_blocked_vm Add(stock_take_blocked_vm data)
        {
            try
            {
                Mapper.CreateMap<stock_take_blocked_vm, stock_take_blocked>();
                stock_take_blocked JRVM = Mapper.Map<stock_take_blocked_vm, stock_take_blocked>(data);
                var created_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                JRVM.created_by = created_by;
                JRVM.created_ts = DateTime.Now;
                _scifferContext.stock_take_blocked.Add(JRVM);
                _scifferContext.SaveChanges();
                data.stock_take_blocked_id = _scifferContext.REF_COUNTRY.Max(x => x.COUNTRY_ID);
            }
            catch (Exception)
            {
                return data;
            }
            return data;
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
        public stock_take_blocked Get(int id)
        {
            var stk = _scifferContext.stock_take_blocked.FirstOrDefault(c => c.stock_take_blocked_id == id);
            return stk;
        }

        public List<stock_take_blocked_vm> GetAll()
        {
            var d = (from ed in _scifferContext.stock_take_blocked
                     join buk in _scifferContext.ref_bucket on ed.bucket_id equals buk.bucket_id
                     join cat in _scifferContext.REF_ITEM_CATEGORY on ed.item_category_id equals cat.ITEM_CATEGORY_ID
                     join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
                     join sloc in _scifferContext.REF_STORAGE_LOCATION on ed.sloc_id equals sloc.storage_location_id
                     select new stock_take_blocked_vm
                     {
                         bucket_id = ed.bucket_id,
                         bucket_name = buk.bucket_name,
                         created_by = ed.created_by,
                         created_ts = ed.created_ts,
                         is_blocked = ed.is_blocked,
                         item_category_id = ed.item_category_id,
                         item_category_name = cat.ITEM_CATEGORY_NAME + "-"+cat.ITEM_CATEGORY_DESCRIPTION,
                         modify_by = ed.modify_by,
                         modify_ts = ed.modify_ts,
                         plant_id= ed.plant_id,
                         plant_name = plant.PLANT_CODE + "-" + plant.PLANT_NAME,
                         sloc_id = ed.sloc_id,
                         sloc_name = sloc.storage_location_name,
                         stock_take_blocked_id = ed.stock_take_blocked_id,
                     }
                     ).ToList();
            return d;
        }

        public stock_take_blocked_vm Update(stock_take_blocked_vm stk)
        {
            try
            {
                var data = _scifferContext.stock_take_blocked.Where(x => x.stock_take_blocked_id == stk.stock_take_blocked_id).FirstOrDefault();
                var modify_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                data.modify_by = modify_by;
                data.modify_ts = DateTime.Now;
                data.is_blocked = stk.is_blocked;
                _scifferContext.Entry(data).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                stk = (from ed in _scifferContext.stock_take_blocked.Where(x=>x.stock_take_blocked_id == stk.stock_take_blocked_id)
                         join buk in _scifferContext.ref_bucket on ed.bucket_id equals buk.bucket_id
                         join cat in _scifferContext.REF_ITEM_CATEGORY on ed.item_category_id equals cat.ITEM_CATEGORY_ID
                         join plant in _scifferContext.REF_PLANT on ed.plant_id equals plant.PLANT_ID
                         join sloc in _scifferContext.REF_STORAGE_LOCATION on ed.sloc_id equals sloc.storage_location_id
                         select new stock_take_blocked_vm
                         {
                             bucket_id = ed.bucket_id,
                             bucket_name = buk.bucket_name,
                             created_by = ed.created_by,
                             created_ts = ed.created_ts,
                             is_blocked = ed.is_blocked,
                             item_category_id = ed.item_category_id,
                             item_category_name = cat.ITEM_CATEGORY_NAME + "-" + cat.ITEM_CATEGORY_DESCRIPTION,
                             modify_by = ed.modify_by,
                             modify_ts = ed.modify_ts,
                             plant_id = ed.plant_id,
                             plant_name = plant.PLANT_CODE + "-" + plant.PLANT_NAME,
                             sloc_id = ed.sloc_id,
                             sloc_name = sloc.storage_location_name,
                             stock_take_blocked_id = ed.stock_take_blocked_id,
                         }
                     ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return stk;
            }
            return stk;
        }
    }
}
