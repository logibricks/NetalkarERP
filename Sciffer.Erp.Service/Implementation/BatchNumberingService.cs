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
    public class BatchNumberingService : IBatchNumberingService
    {
        private readonly ScifferContext _scifferContext;
        public BatchNumberingService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public ref_batch_numbering GetCategory(int item_category, int plant_id)
        {
            return _scifferContext.ref_batch_numbering.Where(x => x.item_category_id == item_category && x.is_active == true && x.plant_id == plant_id).FirstOrDefault();

        }
        public batch_numbering_VM Add(batch_numbering_VM value)
        {
            try
            {
                ref_batch_numbering bt = new ref_batch_numbering();
                bt.batch_no_id = value.batch_no_id;
                bt.from_number = value.from_number;
                bt.to_number = value.to_number;
                bt.is_active = true;
                bt.is_blocked = value.is_blocked;
                bt.item_category_id = value.item_category_id;
                bt.last_used = value.last_used;
                bt.plant_id = value.plant_id;
                bt.prefix_sufix = value.prefix_sufix;
                bt.prefix_sufix_id = value.prefix_sufix_id;
                bt.financial_year_id = value.financial_year_id;
                _scifferContext.Entry(bt).State = System.Data.Entity.EntityState.Added;
                _scifferContext.SaveChanges();
                value.batch_no_id = _scifferContext.ref_batch_numbering.Max(x => x.batch_no_id);
                var item_cat = _scifferContext.REF_ITEM_CATEGORY.Where(x => x.ITEM_CATEGORY_ID == value.item_category_id).FirstOrDefault();
                value.item_category_name = item_cat.ITEM_CATEGORY_NAME + "-" + item_cat.ITEM_CATEGORY_DESCRIPTION;
                var plant = _scifferContext.REF_PLANT.Where(x => x.PLANT_ID == value.plant_id).FirstOrDefault();
                value.plant_name = plant.PLANT_CODE + "-" + plant.PLANT_NAME;
                var financial_year = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == value.financial_year_id).FirstOrDefault();
                value.financial_year_name = financial_year.FINANCIAL_YEAR_NAME;
                value.prefix_sufix_name = bt.prefix_sufix_id == 1 ? "Prefix" : "Suffix";
            }
            catch (Exception ex)
            {
                return value;
            }
            return value;
        }

        public bool Delete(int key)
        {
            var s = _scifferContext.ref_batch_numbering.Where(x => x.batch_no_id == key).FirstOrDefault();
            s.is_active = false;
            _scifferContext.Entry(s).State = System.Data.Entity.EntityState.Modified;
            _scifferContext.SaveChanges();
            return true;
        }

        public ref_batch_numbering Get(int id)
        {
            return _scifferContext.ref_batch_numbering.Where(x => x.batch_no_id == id && x.is_active == true).FirstOrDefault();

        }

        public List<batch_numbering_VM> GetAll()
        {
            var list = (from ed in _scifferContext.ref_batch_numbering.Where(x => x.is_active == true)
                        join r in _scifferContext.REF_PLANT on ed.plant_id equals r.PLANT_ID
                        join t in _scifferContext.REF_ITEM_CATEGORY on ed.item_category_id equals t.ITEM_CATEGORY_ID
                        select new batch_numbering_VM
                        {
                            batch_no_id = ed.batch_no_id,
                            is_blocked = ed.is_blocked,
                            from_number = ed.from_number,
                            to_number = ed.to_number,
                            item_category_id = ed.item_category_id,
                            item_category_name = t.ITEM_CATEGORY_NAME + "-" + t.ITEM_CATEGORY_DESCRIPTION,
                            plant_name = r.PLANT_CODE+ "-" + r.PLANT_NAME,
                            last_used = ed.last_used,
                            plant_id = ed.plant_id,
                            prefix_sufix = ed.prefix_sufix,
                            prefix_sufix_id = ed.prefix_sufix_id,
                             prefix_sufix_name = ed.prefix_sufix_id == 1 ? "Prefix" : "Suffix",
                            financial_year_id = ed.financial_year_id,
                            financial_year_name = ed.REF_FINANCIAL_YEAR.FINANCIAL_YEAR_NAME,
                        }).OrderByDescending(a => a.batch_no_id).ToList();
            return list;
        }

        public batch_numbering_VM Update(batch_numbering_VM value)
        {
            ref_batch_numbering bt = new ref_batch_numbering();
            bt.batch_no_id = value.batch_no_id;
            bt.from_number = value.from_number;
            bt.to_number = value.to_number;
            bt.is_active = true;
            bt.is_blocked = value.is_blocked;
            bt.item_category_id = value.item_category_id;
            bt.last_used = value.last_used;
            bt.plant_id = value.plant_id;
            bt.prefix_sufix = value.prefix_sufix;
            bt.prefix_sufix_id = value.prefix_sufix_id;
            bt.financial_year_id = value.financial_year_id;
            _scifferContext.Entry(bt).State = System.Data.Entity.EntityState.Modified;
            _scifferContext.SaveChanges();
            var item_cat = _scifferContext.REF_ITEM_CATEGORY.Where(x => x.ITEM_CATEGORY_ID == value.item_category_id).FirstOrDefault();
            value.item_category_name = item_cat.ITEM_CATEGORY_NAME + "-" + item_cat.ITEM_CATEGORY_DESCRIPTION;
            var plant = _scifferContext.REF_PLANT.Where(x => x.PLANT_ID == value.plant_id).FirstOrDefault();
            value.plant_name = plant.PLANT_CODE + "-" + plant.PLANT_NAME;
            var financial_year = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_ID == value.financial_year_id).FirstOrDefault();
            value.financial_year_name = financial_year.FINANCIAL_YEAR_NAME;
            value.prefix_sufix_name = bt.prefix_sufix_id == 1 ? "Prefix" : "Suffix";
            return value;
        }
    }
}
