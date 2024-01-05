using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Web;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class ToolMasterService : IToolMasterService
    {
        private readonly ScifferContext _scifferContext;

        public ToolMasterService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_tool_VM Add(ref_tool_VM tool)
        {
            try
            {
                if (!_scifferContext.ref_tool.Any(x => x.tool_name == tool.tool_name && x.is_active != false))
                {
                    int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    ref_tool mt = new ref_tool();
                    mt.tool_id = tool.tool_id;
                    mt.tool_name = tool.tool_name;
                    mt.item_id = tool.item_id;
                    mt.is_blocked = tool.is_blocked;
                    mt.is_active = true;
                    mt.created_on = DateTime.Now;
                    mt.created_by = user;

                    _scifferContext.ref_tool.Add(mt);
                    _scifferContext.SaveChanges();
                    tool.tool_id = _scifferContext.ref_tool.Max(x => x.tool_id);
                    tool.item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == tool.item_id).FirstOrDefault().ITEM_NAME;
                }
                else
                {
                    tool.tool_id = -1;
                    return tool;
                }
                
            }
            catch (Exception e)
            {
                return tool;
            }
            return tool;
        }

        public bool Delete(int id)
        {
            try
            {
                var tool = _scifferContext.ref_tool.FirstOrDefault(c => c.tool_id == id);
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

        public ref_tool Get(int id)
        {
            var tool = _scifferContext.ref_tool.FirstOrDefault(c => c.tool_id == id);
            return tool;
        }

        public List<ref_tool_VM> GetAll()
        {
            var query = (from rt in _scifferContext.ref_tool
                         join ri in _scifferContext.REF_ITEM on rt.item_id equals ri.ITEM_ID
                         where rt.is_active == true
                         select new ref_tool_VM
                         {
                             tool_id = rt.tool_id,
                             tool_name = rt.tool_name,
                             item_id = rt.item_id,
                             item_name = ri.ITEM_NAME,
                             is_blocked = rt.is_blocked,

                         }).ToList();
            return query;
        }

        public ref_tool_VM Update(ref_tool_VM tool)
        {
            try
            {
                ref_tool mt = _scifferContext.ref_tool.Where(x => x.tool_id == tool.tool_id).FirstOrDefault();
                mt.tool_id = tool.tool_id;
                mt.tool_name = tool.tool_name;
                mt.item_id = tool.item_id;
                mt.is_blocked = tool.is_blocked;
                mt.is_active = true;
                
                _scifferContext.Entry(mt).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                tool.tool_name = _scifferContext.ref_tool.Where(x => x.tool_id == tool.tool_id).FirstOrDefault().tool_name;
                tool.item_name = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == tool.item_id).FirstOrDefault().ITEM_NAME;
            }
            catch (Exception e)
            {
                return tool;
            }
            return tool;
        }
    }
}
