using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;

namespace Sciffer.Erp.Service.Implementation
{
    public class ItemWipValuationService : IItemWipValuationService
    {

        private readonly ScifferContext _scifferContext;

        public ItemWipValuationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public VM_ref_item_wip_valuation Add(VM_ref_item_wip_valuation item)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                ref_item_wip_valuation items = new ref_item_wip_valuation();
                items.item_id = item.ITEM_ID;
                items.machine_id = item.machine_id;
                items.value = item.value;

                items.is_active = true;
                items.created_on = DateTime.Now;
                items.created_by = user;
                items.modified_on = DateTime.Now;
                items.modified_by = user;

                _scifferContext.ref_item_wip_valuation.Add(items);
                _scifferContext.SaveChanges();

                item.item_wip_valuation_id = _scifferContext.ref_item_wip_valuation.Max(x => x.item_wip_valuation_id);
                item.machine_name = _scifferContext.ref_machine.Where(x => x.machine_id == item.machine_id).FirstOrDefault().machine_name;
                item.item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item.ITEM_ID).FirstOrDefault().ITEM_NAME;

            }
            catch (Exception e)
            {
                return item;
            }
            return item;
        }

        public bool Delete(int id)
        {
            try
            {
                var tool = _scifferContext.ref_item_wip_valuation.FirstOrDefault(c => c.item_wip_valuation_id == id);
                tool.is_active = false;
                _scifferContext.Entry(tool).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public VM_ref_item_wip_valuation Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<VM_ref_item_wip_valuation> GetAll()
        {
            try
            {
                var query = (from iwv in _scifferContext.ref_item_wip_valuation
                             join ri in _scifferContext.REF_ITEM on iwv.item_id equals ri.ITEM_ID
                             join rm in _scifferContext.ref_machine on iwv.machine_id equals rm.machine_id
                             where iwv.is_active == true
                             select new
                             {
                                 item_wip_valuation_id = iwv.item_wip_valuation_id,
                                 machine_id = iwv.machine_id,
                                 machine_name = rm.machine_name,
                                 ITEM_ID = iwv.item_id,
                                 ITEM_NAME = ri.ITEM_NAME,
                                 value = iwv.value
                             }).ToList()
                             .Select(x => new VM_ref_item_wip_valuation
                             {
                                 item_wip_valuation_id = x.item_wip_valuation_id,
                                 machine_id = x.machine_id,
                                 machine_name = x.machine_name,
                                 ITEM_ID = x.ITEM_ID,
                                 item_name = x.ITEM_NAME,
                                 value = x.value
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VM_ref_item_wip_valuation Update(VM_ref_item_wip_valuation item)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                ref_item_wip_valuation items = _scifferContext.ref_item_wip_valuation.Where(x => x.item_wip_valuation_id == item.item_wip_valuation_id).FirstOrDefault();

                items.item_id = item.ITEM_ID;
                items.machine_id = item.machine_id;
                items.value = item.value;

                items.is_active = true;
                items.modified_on = DateTime.Now;
                items.modified_by = user;

                _scifferContext.Entry(items).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                item.item_wip_valuation_id = _scifferContext.ref_item_wip_valuation.Max(x => x.item_wip_valuation_id);
                item.machine_id = _scifferContext.ref_machine.Where(x => x.machine_id == item.machine_id).FirstOrDefault().machine_id;
                item.machine_name = _scifferContext.ref_machine.Where(x => x.machine_id == item.machine_id).FirstOrDefault().machine_name;
                item.ITEM_ID = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item.ITEM_ID).FirstOrDefault().ITEM_ID;
                item.item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item.ITEM_ID).FirstOrDefault().ITEM_NAME;
                item.value = _scifferContext.ref_item_wip_valuation.Where(x => x.item_wip_valuation_id == item.item_wip_valuation_id).FirstOrDefault().value;
            }
            catch (Exception e)
            {
                return item;
            }
            return item;
        }
    }
}
