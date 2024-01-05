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
    public class IncentiveRuleService : IIncentiveRuleService
    {
        private readonly ScifferContext _scifferContext;

        public IncentiveRuleService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public VM_incentive_rule Add(VM_incentive_rule rule)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                mfg_incentive_rule items = new mfg_incentive_rule();
                items.item_id = rule.ITEM_ID;
                items.machine_id = rule.machine_id;
                items.min_prod_qty = rule.min_prod_qty;
                items.incentive_per_qty = rule.incentive_per_qty;
                items.effective_date = rule.effective_date;
                items.is_blocked = false;

                items.is_active = true;
                items.created_on = DateTime.Now;
                items.created_by = user;
                items.modified_on = DateTime.Now;
                items.modified_by = user;

                _scifferContext.mfg_incentive_rule.Add(items);
                _scifferContext.SaveChanges();

                rule.incentive_rule_id = _scifferContext.mfg_incentive_rule.Max(x => x.incentive_rule_id);
                rule.machine_name = _scifferContext.ref_machine.Where(x => x.machine_id == rule.machine_id).FirstOrDefault().machine_name;
                rule.item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == rule.ITEM_ID).FirstOrDefault().ITEM_NAME;

            }
            catch (Exception e)
            {
                return rule;
            }
            return rule;
        }

        public bool Delete(int id)
        {
            try
            {
                var tool = _scifferContext.mfg_incentive_rule.FirstOrDefault(c => c.incentive_rule_id == id);
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

        public VM_incentive_rule Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<VM_incentive_rule> GetAll()
        {
            try
            {
                var query = (from mir in _scifferContext.mfg_incentive_rule
                             join ri in _scifferContext.REF_ITEM on mir.item_id equals ri.ITEM_ID
                             join rm in _scifferContext.ref_machine on mir.machine_id equals rm.machine_id
                             where mir.is_active == true
                             select new
                             {
                                 incentive_rule_id = mir.incentive_rule_id,
                                 machine_id = mir.machine_id,
                                 machine_name = rm.machine_name,
                                 ITEM_ID = mir.item_id,
                                 ITEM_NAME = ri.ITEM_NAME,
                                 min_prod_qty = mir.min_prod_qty,
                                 incentive_per_qty = mir.incentive_per_qty,
                                 effective_date = mir.effective_date,
                                 is_blocked = mir.is_blocked
                             }).ToList()
                             .Select(x => new VM_incentive_rule
                             {
                                 incentive_rule_id = x.incentive_rule_id,
                                 machine_id = x.machine_id,
                                 machine_name = x.machine_name,
                                 ITEM_ID = x.ITEM_ID,
                                 item_name = x.ITEM_NAME,
                                 min_prod_qty = x.min_prod_qty,
                                 incentive_per_qty = x.incentive_per_qty,
                                 effective_date = x.effective_date,
                                 is_blocked = x.is_blocked
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VM_incentive_rule Update(VM_incentive_rule rule)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                mfg_incentive_rule rules = _scifferContext.mfg_incentive_rule.Where(x => x.incentive_rule_id == rule.incentive_rule_id).FirstOrDefault();
                rules.item_id = rule.ITEM_ID;
                rules.machine_id = rule.machine_id;
                rules.min_prod_qty = rule.min_prod_qty;
                rules.incentive_per_qty = rule.incentive_per_qty;
                rules.is_blocked = rule.is_blocked;

                rules.is_active = true;
                rules.modified_on = DateTime.Now;
                rules.modified_by = user;

                _scifferContext.Entry(rules).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                rule.incentive_rule_id = _scifferContext.mfg_incentive_rule.Max(x => x.incentive_rule_id);
                rule.machine_id = _scifferContext.ref_machine.Where(x => x.machine_id == rule.machine_id).FirstOrDefault().machine_id;
                rule.machine_name = _scifferContext.ref_machine.Where(x => x.machine_id == rule.machine_id).FirstOrDefault().machine_name;
                rule.ITEM_ID = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == rule.ITEM_ID).FirstOrDefault().ITEM_ID;
                rule.item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == rule.ITEM_ID).FirstOrDefault().ITEM_NAME;
                rules.min_prod_qty = _scifferContext.mfg_incentive_rule.Where(x => x.incentive_rule_id == rule.incentive_rule_id).FirstOrDefault().min_prod_qty;
                rules.incentive_per_qty = _scifferContext.mfg_incentive_rule.Where(x => x.incentive_rule_id == rule.incentive_rule_id).FirstOrDefault().incentive_per_qty;
                rules.effective_date = _scifferContext.mfg_incentive_rule.Where(x => x.incentive_rule_id == rule.incentive_rule_id).FirstOrDefault().effective_date;
                rules.is_blocked = _scifferContext.mfg_incentive_rule.Where(x => x.incentive_rule_id == rule.incentive_rule_id).FirstOrDefault().is_blocked;
            }
            catch (Exception e)
            {
                return rule;
            }
            return rule;
        }
    }
}
