using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class ToolRenewTypeService : IToolRenewTypeService
    {
        private readonly ScifferContext _scifferContext;

        public ToolRenewTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public ref_tool_renew_type Add(ref_tool_renew_type tooltype)
        {
            try
            {
                if (!_scifferContext.ref_tool_renew_type.Any(x => x.tool_renew_type_name == tooltype.tool_renew_type_name && x.is_active != false))
                {
                    int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    ref_tool_renew_type mt = new ref_tool_renew_type();
                    mt.tool_renew_type_id = tooltype.tool_renew_type_id;
                    mt.tool_renew_type_name = tooltype.tool_renew_type_name;
                    mt.is_blocked = tooltype.is_blocked;
                    mt.is_active = true;
                    mt.created_on = DateTime.Now;
                    mt.created_by = user;

                    _scifferContext.ref_tool_renew_type.Add(mt);
                    _scifferContext.SaveChanges();
                    tooltype.tool_renew_type_id = _scifferContext.ref_tool_renew_type.Max(x => x.tool_renew_type_id);
                }
                else
                {
                    tooltype.tool_renew_type_id = -1;
                    return tooltype;
                }

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
                var tool = _scifferContext.ref_tool_renew_type.FirstOrDefault(c => c.tool_renew_type_id == id);
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

        public ref_tool_renew_type Get(int id)
        {
            var tool = _scifferContext.ref_tool_renew_type.FirstOrDefault(c => c.tool_renew_type_id == id);
            return tool;
        }

        public List<ref_tool_renew_type> GetAll()
        {
            var query = _scifferContext.ref_tool_renew_type.Where(x => x.is_active == true).ToList();
            return query;
        }

        public ref_tool_renew_type Update(ref_tool_renew_type tooltype)
        {
            try
            {
                ref_tool_renew_type mt = _scifferContext.ref_tool_renew_type.Where(x => x.tool_renew_type_id == tooltype.tool_renew_type_id).FirstOrDefault();
                mt.tool_renew_type_id = tooltype.tool_renew_type_id;
                mt.tool_renew_type_name = tooltype.tool_renew_type_name;
                mt.is_blocked = tooltype.is_blocked;
                mt.is_active = true;

                _scifferContext.Entry(mt).State = EntityState.Modified;
                _scifferContext.SaveChanges();

                tooltype.tool_renew_type_name = _scifferContext.ref_tool_renew_type.Where(x => x.tool_renew_type_id == tooltype.tool_renew_type_id).FirstOrDefault().tool_renew_type_name;

            }
            catch (Exception e)
            {
                return tooltype;
            }
            return tooltype;
        }
    }
}
