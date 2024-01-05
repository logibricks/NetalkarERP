using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class ToolLifeService : IToolLifeService
    {
        private readonly ScifferContext _scifferContext;

        public ToolLifeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_tool_life_VM Add(ref_tool_life_VM tooltype)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                
                ref_tool_life mt = new ref_tool_life();
                mt.tool_life_id = tooltype.tool_life_id;
                mt.tool_id = tooltype.tool_id;
                mt.tool_renew_type_id = tooltype.tool_renew_type_id;
                mt.machine_id = tooltype.machine_id;
                mt.item_id = tooltype.ITEM_ID;
                mt.life_no_of_items = tooltype.life_no_of_items;
                mt.per_unit_life_consumption = Math.Round((1 / tooltype.life_no_of_items),4);
                mt.is_active = true;
                mt.created_on = DateTime.Now;
                mt.created_by = user;

                _scifferContext.ref_tool_life.Add(mt);
                _scifferContext.SaveChanges();

                tooltype.tool_life_id = _scifferContext.ref_tool_life.Max(x => x.tool_life_id);
                tooltype.per_unit_life_consumption = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().per_unit_life_consumption;
                tooltype.tool_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == tooltype.tool_id).FirstOrDefault().ITEM_NAME;
                tooltype.tool_renew_type_name = _scifferContext.ref_tool_renew_type.Where(x => x.tool_renew_type_id == tooltype.tool_renew_type_id).FirstOrDefault().tool_renew_type_name;
                tooltype.machine_name = _scifferContext.ref_machine.Where(x => x.machine_id == tooltype.machine_id).FirstOrDefault().machine_name;
                tooltype.ITEM_NAME = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == tooltype.ITEM_ID).FirstOrDefault().ITEM_NAME;
                
            }
            catch (Exception e)
            {
                return tooltype;
            }
            return tooltype;
        }

        public bool Delete(int id)
        {
            try
            {
                var tool = _scifferContext.ref_tool_life.FirstOrDefault(c => c.tool_life_id == id);
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

        public ref_tool_life_VM Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ref_tool_life_VM> GetAll()
        {
            try
            {
                var query = (from tl in _scifferContext.ref_tool_life
                             join tt in _scifferContext.REF_ITEM on tl.tool_id equals tt.ITEM_ID
                             join tr in _scifferContext.ref_tool_renew_type on tl.tool_renew_type_id equals tr.tool_renew_type_id
                             join ri in _scifferContext.REF_ITEM on tl.item_id equals ri.ITEM_ID
                             join rm in _scifferContext.ref_machine on tl.machine_id equals rm.machine_id
                             where tl.is_active == true
                             select new
                             {
                                 tool_life_id = tl.tool_life_id,
                                 tool_id = tl.tool_id,
                                 tool_name = tt.ITEM_NAME,
                                 tool_renew_type_id = tl.tool_renew_type_id,
                                 tool_renew_type_name = tr.tool_renew_type_name,
                                 machine_id = tl.machine_id,
                                 machine_name = rm.machine_name,
                                 ITEM_ID = tl.item_id,
                                 ITEM_NAME = ri.ITEM_NAME,
                                 life_no_of_items = tl.life_no_of_items,
                                 per_unit_life_consumption = tl.per_unit_life_consumption,
                             }).ToList()
                             .Select(x => new ref_tool_life_VM
                             {
                                 tool_life_id = x.tool_life_id,
                                 tool_id = x.tool_id,
                                 tool_name = x.tool_name,
                                 tool_renew_type_id = x.tool_renew_type_id,
                                 tool_renew_type_name = x.tool_renew_type_name,
                                 machine_id = x.machine_id,
                                 machine_name = x.machine_name,
                                 ITEM_ID = x.ITEM_ID,
                                 ITEM_NAME = x.ITEM_NAME,
                                 life_no_of_items = x.life_no_of_items,
                                 per_unit_life_consumption = x.per_unit_life_consumption,
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ref_tool_life_VM Update(ref_tool_life_VM tooltype)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                ref_tool_life mt = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault();
                mt.tool_id = tooltype.tool_id;
                mt.tool_renew_type_id = tooltype.tool_renew_type_id;
                mt.machine_id = tooltype.machine_id;
                mt.item_id = tooltype.ITEM_ID;
                mt.life_no_of_items = tooltype.life_no_of_items;
                mt.per_unit_life_consumption = Math.Round((1/tooltype.life_no_of_items),4);
                mt.is_active = true;
                mt.created_by = user;
                mt.created_on = DateTime.Now;

                _scifferContext.Entry(mt).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                
                tooltype.per_unit_life_consumption = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().per_unit_life_consumption;
                tooltype.tool_id = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().tool_life_id;
                tooltype.tool_name = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().REF_ITEM1.ITEM_NAME;
                tooltype.tool_renew_type_id = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().tool_renew_type_id;
                tooltype.tool_renew_type_name = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().ref_tool_renew_type.tool_renew_type_name;
                tooltype.machine_id = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().machine_id;
                tooltype.machine_name = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().ref_machine.machine_name;
                tooltype.ITEM_ID = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().item_id;
                tooltype.ITEM_NAME = _scifferContext.ref_tool_life.Where(x => x.tool_life_id == tooltype.tool_life_id).FirstOrDefault().REF_ITEM.ITEM_NAME;
                
            }
            catch (Exception e)
            {
                return tooltype;
            }
            return tooltype;
        }


        public List<ref_tool_life_VM> Tool_Life_Report(string entity, string tool_id, string tool_renew_type_id, string item_id, string machine_id, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                DateTime dte1 = new DateTime(0001, 1, 1);

                var ent = new SqlParameter("@entity", entity);
                var fromdate = new SqlParameter("@from_date", fromDate == null ? dte : fromDate == dte1 ? dte : fromDate);
                var todate = new SqlParameter("@to_date", toDate == null ? dte : toDate);
                var _tool_id = new SqlParameter("@tool_id", tool_id == null ? "-1" : tool_id);
                var _tool_renew_type_id = new SqlParameter("@tool_renew_type_id", tool_renew_type_id == null ? "-1" : tool_renew_type_id);
                var _item_id = new SqlParameter("@item_id", item_id == null ? "-1" : item_id);
                var _machine_id = new SqlParameter("@machine_id", machine_id == null ? "-1" : machine_id);

                var val = _scifferContext.Database.SqlQuery<ref_tool_life_VM>(
                    "exec report_get_tool_life @entity,@from_date,@to_date,@tool_id,@tool_renew_type_id,@item_id,@machine_id",
                      ent, fromdate, todate, _tool_id, _tool_renew_type_id, _item_id, _machine_id).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
