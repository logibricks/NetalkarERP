using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using System.Web;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class ToolMachineUsageService : IToolMachineUsageService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public ToolMachineUsageService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public string Add(ref_tool_machine_usage_VM vm)
        {
            try
            {
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                var previous_tool = _scifferContext.ref_tool_machine_usage.Where(x => x.tool_id == vm.tool_id && x.machine_id == vm.machine_id && x.in_use == true).ToList();
                foreach(var xx in previous_tool)
                {
                    xx.in_use = false;
                    xx.modified_on = DateTime.Now;
                    xx.modified_by = createdby;
                    _scifferContext.Entry(xx).State = System.Data.Entity.EntityState.Modified;
                }

                ref_tool_machine_usage tool = new ref_tool_machine_usage();
                tool.tool_id = vm.tool_id;
                tool.tool_renew_type_id = vm.tool_renew_type_id;
                tool.machine_id = vm.machine_id;
                tool.start_date_time = vm.start_date_time;
                tool.is_active = true;
                tool.in_use = true;
                tool.created_by = createdby;
                tool.created_on = DateTime.Now;
                tool.modified_by = createdby;
                tool.modified_on = DateTime.Now;
                _scifferContext.ref_tool_machine_usage.Add(tool);

                for(int i = 0; i<vm.item_id.Count; i++)
                {
                    ref_tool_machine_item_usage item = new ref_tool_machine_item_usage();
                    item.item_id = Convert.ToInt32(vm.item_id[i]);
                    item.life_no_of_items = Convert.ToDouble(vm.life_no_of_items[i]);
                    item.no_of_items_processed = Convert.ToDouble(vm.no_of_items_processed[i]);
                    item.per_unit_life_consumption = Convert.ToDouble(vm.per_unit_life_consumption[i]);
                    item.item_life_consumption = Convert.ToDouble(vm.item_life_consumption[i]);
                    item.item_life_consumption_percentage = Convert.ToDouble(vm.item_life_consumption_percentage[i]);
                    item.ref_tool_machine_usage = tool;
                    _scifferContext.ref_tool_machine_item_usage.Add(item);
                }

                _scifferContext.SaveChanges();
                return "Saved";
            }
            catch(Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }

        public bool Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public ref_tool_machine_usage_VM Get(int? id)
        {
            ref_tool_machine_usage JR = _scifferContext.ref_tool_machine_usage.FirstOrDefault(c => c.tool_machine_usage_id == id);
            Mapper.CreateMap<ref_tool_machine_usage, ref_tool_machine_usage_VM>();
            ref_tool_machine_usage_VM JRVM = Mapper.Map<ref_tool_machine_usage, ref_tool_machine_usage_VM>(JR);
            JRVM.ref_tool_machine_item_usage = JRVM.ref_tool_machine_item_usage.ToList();
            return JRVM;
        }

        public List<ref_tool_machine_usage_VM> GetAll()
        {
            var query = (from tms in _scifferContext.ref_tool_machine_usage
                         join rm in _scifferContext.ref_machine on tms.machine_id equals rm.machine_id
                         join rt in _scifferContext.REF_ITEM on tms.tool_id equals rt.ITEM_ID
                         join trt in _scifferContext.ref_tool_renew_type on tms.tool_renew_type_id equals trt.tool_renew_type_id
                         select new ref_tool_machine_usage_VM
                         {
                             tool_machine_usage_id = tms.tool_machine_usage_id,
                             tool_id = tms.tool_id,
                             tool_name = rt.ITEM_NAME,
                             tool_renew_type_id = tms.tool_renew_type_id,
                             tool_renew_type_name = trt.tool_renew_type_name,
                             machine_id = tms.machine_id,
                             machine_name = rm.machine_name,
                             current_life_percentage = tms.current_life_percentage,
                             start_date_time = tms.start_date_time,
                             in_use = tms.in_use,
                         }).OrderByDescending(a => a.tool_machine_usage_id).ToList();
            return query;
        }

        public string Update(ref_tool_machine_usage_VM vm)
        {
            throw new NotImplementedException();
        }

        public List<ref_tool_machine_item_usage_VM> GetItemDetails(int tool_id, int tool_renew_type_id, int machine_id)
        {
            var list = new List<ref_tool_machine_item_usage_VM>();
            try
            {
                list = (from rtl in _scifferContext.ref_tool_life
                        join ri in _scifferContext.REF_ITEM on rtl.item_id equals ri.ITEM_ID
                        where rtl.tool_id == tool_id && rtl.tool_renew_type_id == tool_renew_type_id
                        && rtl.machine_id == machine_id && rtl.is_active == true
                        select new
                        {
                            item_id = rtl.item_id,
                            item_name = ri.ITEM_NAME,
                            life_no_of_items = rtl.life_no_of_items,
                            per_unit_life_consumption = rtl.per_unit_life_consumption,
                        })
                        .Select(x => new ref_tool_machine_item_usage_VM
                        {
                            item_id = x.item_id,
                            item_name = x.item_name,
                            life_no_of_items = x.life_no_of_items,
                            per_unit_life_consumption = Math.Round(x.per_unit_life_consumption,4),
                        }).ToList();
            }
            catch(Exception ex)
            {

            }
            return list;
        }
    }
}
