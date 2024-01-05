using AutoMapper;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class PlanMaintenanceService : IPlanMaintenanceService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public PlanMaintenanceService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_plan_maintenance_VM main)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("maintenance_detail_id", typeof(int));
                dt.Columns.Add("parameter_sr_no", typeof(int));
                dt.Columns.Add("paramter_id", typeof(int));
                dt.Columns.Add("paramter_range", typeof(string));
                if (main.maintenance_parameter_id != null)
                {
                    for (var i = 0; i < main.maintenance_parameter_id.Count; i++)
                    {
                        dt.Rows.Add(main.maintenance_parameter_id[i] == "0" ? -1 : int.Parse(main.maintenance_parameter_id[i]),
                            main.parameter_sr_no[i],
                            main.parameter_code_id[i],
                            main.range[i]);
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_maintenance_detail";
                t1.Value = dt;

                DataTable cp = new DataTable();
                cp.Columns.Add("plan_maintenance_component_id", typeof(int));
                cp.Columns.Add("component_sr_no", typeof(int));
                cp.Columns.Add("item_id", typeof(int));
                cp.Columns.Add("uom_id", typeof(int));
                cp.Columns.Add("component_parameter_id", typeof(int));
                cp.Columns.Add("quantity", typeof(double));

                if (main.component_detail_id != null)
                {
                    for (var i = 0; i < main.component_detail_id.Count; i++)
                    {
                        cp.Rows.Add(main.component_detail_id[i] == "0" ? -1 : int.Parse(main.component_detail_id[i]),
                            main.component_sr_no[i], main.item_id[i], main.uom_id1[i], main.component_parameter[i]=="" ? 0 : int.Parse(main.component_parameter[i]),
                            main.quantity[i]);
                    }
                }
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_plan_maintenance_component";
                t2.Value = cp;
                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_ts1 = DateTime.Now;
                var plan_maintenance_id = new SqlParameter("@plan_maintenance_id", main.plan_maintenance_id == 0 ? -1 : main.plan_maintenance_id);
                var machine_id = new SqlParameter("@machine_id", main.machine_id);               
                var machine_category_id = new SqlParameter("@machine_category_id", main.machine_category_id);
                var maintenance_type_id = new SqlParameter("@maintenance_type_id", main.maintenance_type_id);
                var plan_start_date = new SqlParameter("@plan_start_date", main.plan_start_date/*== null ? DateTime.Now : DateTime.Parse(main.plan_start_dates)*/);
                var plan_end_date = new SqlParameter("@plan_end_date", main.plan_end_date/*DateTime.Parse(main.plan_end_dates)*/);
                var cycle_start_date = new SqlParameter("@cycle_start_date", main.cycle_start_date/*s == null ? DateTime.Now : DateTime.Parse(main.cycle_start_dates)*/);
                var frequency = new SqlParameter("@frequency", main.frequency);
                var frequency_type = new SqlParameter("@frequency_type", main.frequency_type);
                var create_order_days = new SqlParameter("@create_order_days", main.create_order_days);
                var create_order_dtype = new SqlParameter("@create_order_dtype", main.create_order_dtype == null ? "" : main.create_order_dtype);
                var maintenance_period = new SqlParameter("@maintenance_period", main.maintenance_period);
                var maintenance_period_type = new SqlParameter("@maintenance_period_type", main.maintenance_period_type == null ? "" : main.maintenance_period_type);
                var allowed_delay = new SqlParameter("@allowed_delay", main.allowed_delay);
                var allowed_delay_type = new SqlParameter("@allowed_delay_type", main.allowed_delay_type == null ? "" : main.allowed_delay_type);
                var allowed_early_completion = new SqlParameter("@allowed_early_completion", main.allowed_early_completion);
                var allowed_early_comlpletion_type = new SqlParameter("@allowed_early_comlpletion_type", main.allowed_early_comlpletion_type == null ? "" : main.allowed_early_comlpletion_type);
                var counter_frequency = new SqlParameter("@counter_frequency", main.counter_frequency == null ? "" : main.counter_frequency);
                var remarks = new SqlParameter("@remarks", main.remarks == null ? string.Empty : main.remarks);
                var attachement = new SqlParameter("@attachement", main.attachement == null ? "No File" : main.attachement);
                var is_active = new SqlParameter("@is_active", 1);                
                var days_before = new SqlParameter("@days_before", main.days_before == null ? 0 : main.days_before);
                var create_order_yes = new SqlParameter("@create_order_yes", main.create_order_yes == null ? 0 : main.create_order_yes);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);
                var model_no = new SqlParameter("@model_no", main.model_no == null ? "" : main.model_no);
                var manufacturer_part_no = new SqlParameter("@manufacturer_part_no", main.manufacturer_part_no == null ? "" : main.manufacturer_part_no);
                var manufacturing_serial_number = new SqlParameter("@manufacturing_serial_number", main.manufacturing_serial_number == null ? "" : main.manufacturing_serial_number);
                var asset_code_id = new SqlParameter("@asset_code_id", main.asset_code_id == null ? "" : main.asset_code_id);
                var asset_tag_no = new SqlParameter("@asset_tag_no", main.asset_tag_no == null ? "" : main.asset_tag_no);
                var manufacturer = new SqlParameter("@manufacturer", main.manufacturer == null ? "" : main.manufacturer);
                var employee_id = new SqlParameter("@employee_id", main.employee_id==null? 0 : main.employee_id);
                var deleteids = new SqlParameter("@deleteids", main.deleteids == null ? "" : main.deleteids);
                var deleteids1 = new SqlParameter("@deleteids1", main.deleteids1 == null ? "" : main.deleteids1);
                var category_id = new SqlParameter("@category_id", main.category_id);
                var posting_date = new SqlParameter("@posting_date", main.posting_date);
                var created_by = new SqlParameter("@created_by", created_by1);
                var created_ts = new SqlParameter("@created_ts", created_ts1);
                var is_blocked = new SqlParameter("@is_blocked", main.is_blocked);
                var doc_number = new SqlParameter("@doc_number", main.doc_number == null ? "" : main.doc_number);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_plant_maintenance @plan_maintenance_id ,@machine_id ,@machine_category_id ,@maintenance_type_id ,@plan_start_date ,@plan_end_date ,@cycle_start_date ,@frequency ,@frequency_type ,@create_order_days ,@create_order_dtype ,@maintenance_period ,@maintenance_period_type ,@allowed_delay ,@allowed_delay_type ,@allowed_early_completion ,@allowed_early_comlpletion_type ,@counter_frequency ,@remarks ,@attachement ,@is_active ,@days_before ,@create_order_yes ,@plant_id ,@model_no ,@manufacturer_part_no ,@manufacturing_serial_number ,@asset_code_id ,@asset_tag_no ,@manufacturer,@employee_id,@t1,@t2,@deleteids,@deleteids1,@category_id,@posting_date,@created_by,@created_ts,@is_blocked,@doc_number",
                    plan_maintenance_id, machine_id, machine_category_id, maintenance_type_id, plan_start_date, plan_end_date, cycle_start_date, frequency, frequency_type,
                    create_order_days, create_order_dtype, maintenance_period, maintenance_period_type, allowed_delay, allowed_delay_type, allowed_early_completion, allowed_early_comlpletion_type,
                    counter_frequency, remarks, attachement, is_active, days_before, create_order_yes, plant_id, model_no, manufacturer_part_no, manufacturing_serial_number, asset_code_id,
                    asset_tag_no, manufacturer, employee_id, t1, t2, deleteids, deleteids1, category_id, posting_date, created_by, created_ts, is_blocked, doc_number).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    return val.Split('~')[1];
                }
                else
                {
                    return val;
                }
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[ref_plan_maintenance] set [IS_ACTIVE] = 0 where plan_maintenance_id = " + id);
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

        public ref_plan_maintenance_VM Get(int id)
        {
            ref_plan_maintenance pm = _scifferContext.ref_plan_maintenance.FirstOrDefault(c => c.plan_maintenance_id == id);
            Mapper.CreateMap<ref_plan_maintenance, ref_plan_maintenance_VM>();
            ref_plan_maintenance_VM pmv = Mapper.Map<ref_plan_maintenance, ref_plan_maintenance_VM>(pm);
            //pmv.ref_plan_maintenance_detail = pmv.ref_plan_maintenance_detail.Where(c => c.is_active == true).ToList();
            //pmv.ref_plan_maintenance_component = pmv.ref_plan_maintenance_component.Where(c => c.is_active == true).ToList();
            pmv.plan_start_dates = pmv.plan_start_date.ToString();
            pmv.plan_end_dates = pmv.plan_end_date.ToString();
            pmv.ref_plan_maintenance_schedule = pmv.ref_plan_maintenance_schedule.Where(x=>x.is_active==true).ToList();
            pmv.ref_plan_maintenance_schedules = pmv.ref_plan_maintenance_schedule.Select(a => new
            {
                plan_maintenance_schedule_id = a.plan_maintenance_schedule_id,
                plan_maintenance_id = a.plan_maintenance_id,
                order_date = a.order_date,
                schedule_date = a.schedule_date,
                completion_date = a.completion_date,
                status_id = a.status_id,
                status = a.ref_status.status_name,
                order_no = a.order_no,
            }).ToList().Select(a => new
            ref_plan_maintenance_schedules
            {
                plan_maintenance_schedule_id = a.plan_maintenance_schedule_id,
                plan_maintenance_id = a.plan_maintenance_id,
                order_date = a.order_date,
                schedule_date = a.schedule_date,
                completion_date = a.completion_date,
                status_id = a.status_id,
                status = a.status,
                order_no = a.order_no,
            }).ToList();

            pmv.ref_plan_maintenance_details = pmv.ref_plan_maintenance_detail.Where(a => a.is_active == true).Select(a => new
            {
                maintenance_detail_id = a.maintenance_detail_id,
                plan_maintenance_id = a.plan_maintenance_id,
                parameter_range = a.parameter_range,
                sr_no = a.sr_no,
                parameter_id = a.parameter_id,
                parameter_code = a.ref_parameter_list.parameter_code,
                parameter_desc = a.ref_parameter_list.parameter_desc,
            }).ToList().Select(a => new
            ref_plan_maintenance_details
            {
                maintenance_detail_id = a.maintenance_detail_id,
                plan_maintenance_id = a.plan_maintenance_id,
                parameter_range = a.parameter_range,
                sr_no = a.sr_no,
                parameter_id = a.parameter_id,
                parameter_code = a.parameter_code + " - " + a.parameter_desc,
            }).ToList();

            pmv.ref_plan_maintenance_components = pmv.ref_plan_maintenance_component.Where(a => a.is_active == true).Select(a => new
            {
                item_id = a.item_id,
                item_code = a.REF_ITEM.ITEM_CODE,
                item_desc = a.REF_ITEM.ITEM_NAME,
                uom_name = a.REF_UOM.UOM_NAME,
                uom_code = a.REF_UOM.UOM_DESCRIPTION,
                quantity = a.quantity,
                sr_no = a.sr_no,
                uom_id = a.uom_id,
                parameter_id = a.parameter_id,
                parameter_code = a.ref_parameter_list==null ? "" : a.ref_parameter_list.parameter_code,
                parameter_desc = a.ref_parameter_list == null ? "" : a.ref_parameter_list.parameter_desc,
                plan_maintenance_id = a.plan_maintenance_id,
                plan_maintenance_component_id = a.plan_maintenance_component_id,
            }).ToList().Select(a => new
            ref_plan_maintenance_components
            {
                item_id = a.item_id,
                item_code = a.item_code + " - " + a.item_desc,
                uom_id = a.uom_id,
                uom_code = a.uom_code + " - " + a.uom_name,
                quantity = a.quantity,
                sr_no = a.sr_no,
                parameter_id = a.parameter_id,
                parameter_code = a.parameter_code + " - " + a.parameter_desc,
                plan_maintenance_id = a.plan_maintenance_id,
                plan_maintenance_component_id = a.plan_maintenance_component_id
                //quantity = string.Format("{0:0.00}", a.quantity),
            }).ToList();

            return pmv;
        }


        //public List<ref_machine_master_VM> GetAll()
        //{
        //    Mapper.CreateMap<ref_machine, ref_machine_master_VM>().ForMember(dest => dest.attachement, opt => opt.Ignore());
        //    return _scifferContext.ref_machine.Project().To<ref_machine_master_VM>().Where(c => c.is_active == true).ToList();

        //}

        public List<ref_plan_maintenance_VM> GetAll()
        {
            //var Q = _scifferContext.ref_plan_maintenance.ToList();
            var query = (from main in _scifferContext.ref_plan_maintenance.Where(x => x.is_active == true).OrderByDescending(x => x.plan_maintenance_id)
                         join mach in _scifferContext.ref_machine on main.machine_id equals mach.machine_id into mach1
                         from mach2 in mach1.DefaultIfEmpty()
                         join mcat in _scifferContext.ref_machine_category on main.machine_category_id equals mcat.machine_category_id into mcat1
                         from mcat2 in mcat1.DefaultIfEmpty()
                         join mtype in _scifferContext.ref_maintenance_type on main.maintenance_type_id equals mtype.maintenance_type_id into mtype1
                         from mtype2 in mtype1.DefaultIfEmpty()
                         join plantname in _scifferContext.REF_PLANT on main.plant_id equals plantname.PLANT_ID into plantname1
                         from plantname2 in plantname1.DefaultIfEmpty()
                         join emp in _scifferContext.REF_EMPLOYEE on main.employee_id equals emp.employee_id into emp1
                         from emp2 in emp1.DefaultIfEmpty()
                         join cat in _scifferContext.ref_document_numbring on main.category_id equals cat.document_numbring_id
                         select new ref_plan_maintenance_VM()
                         {
                             plan_maintenance_id = main.plan_maintenance_id,
                             machine_id = main.machine_id,
                             machine_category_id = main.machine_category_id,
                             machine_category_name = mcat2.machine_category_code + " - " + mcat2.machine_category_description,
                             frequency = main.frequency,
                             frequency_type = main.frequency_type,
                             days_before = main.days_before,
                             create_order_days = main.create_order_days,
                             create_order_dtype = main.create_order_dtype,
                             create_order_yes = main.create_order_yes,
                             maintenance_period = main.maintenance_period,
                             maintenance_period_type = main.maintenance_period_type,
                             allowed_delay = main.allowed_delay,
                             allowed_delay_type = main.allowed_delay_type,
                             allowed_early_completion = main.allowed_early_completion,
                             allowed_early_comlpletion_type = main.allowed_early_comlpletion_type,
                             counter_frequency = main.counter_frequency,
                             plan_start_date = main.plan_start_date,
                             plan_end_date = main.plan_end_date,
                             cycle_start_date = main.cycle_start_date,
                             remarks = main.remarks,
                             attachement = main.attachement,
                             manufacturer = mach2.manufacturer,
                             model_no = mach2.model_no,
                             manufacturer_part_no = main.manufacturer_part_no,
                             manufacturing_serial_number = main.manufacturing_serial_number,
                             asset_code_id = main.asset_code_id,
                             asset_tag_no = main.asset_tag_no,
                             plant_id = mach2.plant_id,
                             machine_name = mach2 == null ? "" : mach2.machine_name,
                             plant_name = plantname2 == null ? "" : plantname2.PLANT_NAME,
                             maintenance_type_name = mtype2 == null ? "" : mtype2.maintenance_name,
                             employee_name = emp2.employee_code + " - " + emp2.employee_name,
                             doc_number = main.doc_number,
                             posting_date = main.posting_date,
                             category_id = main.category_id,
                             category_name =  cat.category,
                         }).OrderByDescending(x => x.plan_maintenance_id).ToList();
            return query;
        }

        public bool Update(ref_plan_maintenance_VM main)
        {
            try
            {
                ref_plan_maintenance pm = new ref_plan_maintenance();
                pm.machine_id = main.machine_id;
                pm.machine_category_id = main.machine_category_id;
                pm.maintenance_type_id = main.maintenance_type_id;
                pm.plan_start_date = main.plan_start_date;
                pm.plan_end_date = main.plan_end_date;
                pm.plant_id = main.plant_id;
                pm.model_no = main.model_no;
                pm.manufacturer_part_no = main.manufacturer_part_no;
                pm.manufacturing_serial_number = main.manufacturing_serial_number;
                pm.asset_code_id = main.asset_code_id;
                pm.asset_tag_no = main.asset_tag_no;
                pm.manufacturer = main.manufacturer;
                pm.cycle_start_date = main.cycle_start_date;
                pm.frequency = main.frequency;
                pm.frequency_type = main.frequency_type;
                pm.days_before = main.days_before;
                pm.create_order_yes = main.create_order_yes;
                pm.create_order_days = main.create_order_days;
                pm.create_order_dtype = main.create_order_dtype;
                pm.maintenance_period = main.maintenance_period;
                pm.maintenance_period_type = main.maintenance_period_type;
                pm.allowed_delay = main.allowed_delay;
                pm.allowed_delay_type = main.allowed_delay_type;
                pm.allowed_early_completion = main.allowed_early_completion;
                pm.allowed_early_comlpletion_type = main.allowed_early_comlpletion_type;
                pm.counter_frequency = main.counter_frequency;
                pm.remarks = main.remarks;
                pm.attachement = main.attachement;
                pm.plan_maintenance_id = main.plan_maintenance_id;
                pm.is_active = true;

                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = main.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int maintenance_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        maintenance_detail_id = int.Parse(deleteStringArray[i]);
                        var maintenance_detail = _scifferContext.ref_plan_maintenance_detail.Find(maintenance_detail_id);
                        // int a = 0;
                        _scifferContext.Entry(maintenance_detail).State = EntityState.Modified;
                        maintenance_detail.is_active = false;
                    }
                }
                List<ref_plan_maintenance_detail> pmd = new List<ref_plan_maintenance_detail>();

                foreach (var I in main.ref_plan_maintenance_detail)
                {

                    ref_plan_maintenance_detail rd = new ref_plan_maintenance_detail();
                    rd.maintenance_detail_id = I.maintenance_detail_id;
                    rd.plan_maintenance_id = main.plan_maintenance_id;
                    rd.parameter_id = I.parameter_id;
                    rd.parameter_range = I.parameter_range;
                    rd.sr_no = I.sr_no;
                    rd.is_active = true;
                    pmd.Add(rd);
                }


                pm.ref_plan_maintenance_detail = pmd;
                foreach (var i in pm.ref_plan_maintenance_detail)
                {
                    if (i.maintenance_detail_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }

                int plan_maintenance_component_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        plan_maintenance_component_id = int.Parse(deleteStringArray[i]);
                        var maintenance_components = _scifferContext.ref_plan_maintenance_component.Find(plan_maintenance_component_id);
                        // int a = 0;
                        _scifferContext.Entry(maintenance_components).State = EntityState.Modified;
                        maintenance_components.is_active = false;
                    }
                }
                List<ref_plan_maintenance_component> pmc = new List<ref_plan_maintenance_component>();

                foreach (var I in main.ref_plan_maintenance_component)
                {
                    ref_plan_maintenance_component rc = new ref_plan_maintenance_component();

                    rc.plan_maintenance_component_id = I.plan_maintenance_component_id;
                    rc.parameter_id = I.parameter_id;
                    rc.item_id = I.item_id;
                    rc.quantity = I.quantity;
                    rc.sr_no = I.sr_no;
                    rc.plan_maintenance_id = main.plan_maintenance_id;
                    rc.is_active = true;
                    pmc.Add(rc);

                }
                pm.ref_plan_maintenance_component = pmc;
                foreach (var i in pm.ref_plan_maintenance_component)
                {
                    if (i.plan_maintenance_component_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }


                int plan_maintenance_schedule_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        plan_maintenance_schedule_id = int.Parse(deleteStringArray[i]);
                        var maintenance_schedule = _scifferContext.ref_plan_maintenance_schedule.Find(plan_maintenance_schedule_id);

                        _scifferContext.Entry(maintenance_schedule).State = EntityState.Modified;
                        maintenance_schedule.is_active = false;
                    }
                }
                List<ref_plan_maintenance_schedule> pms = new List<ref_plan_maintenance_schedule>();

                foreach (var I in main.ref_plan_maintenance_schedule)
                {

                    ref_plan_maintenance_schedule rs = new ref_plan_maintenance_schedule();
                    rs.plan_maintenance_schedule_id = I.plan_maintenance_schedule_id;
                    rs.order_no = I.order_no;
                    //rs.document_numbering_id = I.document_numbering_id;
                    rs.order_date = I.order_date;
                    rs.schedule_date = I.schedule_date;
                    rs.completion_date = I.completion_date;
                    rs.status_id = I.status_id;
                    rs.plan_maintenance_id = main.plan_maintenance_id;
                    rs.is_active = true;
                    pms.Add(rs);
                }


                pm.ref_plan_maintenance_schedule = pms;
                foreach (var i in pm.ref_plan_maintenance_schedule)
                {
                    if (i.plan_maintenance_schedule_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }

                _scifferContext.Entry(pm).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
