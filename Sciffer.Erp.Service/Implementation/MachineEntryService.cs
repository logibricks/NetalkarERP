using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class MachineEntryService : IMachineEntryService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public MachineEntryService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }

        public bool CheckMachineBlocked(int machine_id)
        {
            try
            {
                return _scifferContext.mfg_qc.Where(x => x.machine_id == machine_id).FirstOrDefault().is_machine_blocked;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //Save operator shift
        public bool SaveOperatorShift(int machine_id, int shift_id)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var shift = _scifferContext.ref_shifts.Where(x => x.shift_id == shift_id).Select(x => x.shift_code).FirstOrDefault();
                DateTime date = DateTime.Now.Date;

                if (!(_scifferContext.mfg_operator_shift.Any(x => x.shift_id == shift_id && x.machine_id == machine_id && x.is_logged_in == true
                    && x.operator_shift_ts.Year == date.Year && x.operator_shift_ts.Month == date.Month && x.operator_shift_ts.Day == date.Day)))
                {
                    mfg_operator_shift mos = new mfg_operator_shift();
                    mos.operator_id = user;
                    mos.machine_id = machine_id;
                    mos.shift_id = shift_id;
                    mos.operator_shift_ts = DateTime.Now;
                    mos.is_logged_in = true;
                    _scifferContext.mfg_operator_shift.Add(mos);
                    _scifferContext.SaveChanges();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> CheckMacAddress(string macaddress)
        {
            return _scifferContext.ref_machine.Where(x => x.mac_address == macaddress).Select(x => x.machine_id).ToList();
        }

        public string GetMachineName(int machine_id)
        {
            try
            {
                string machine_name = _scifferContext.ref_machine.Where(x => x.machine_id == machine_id).FirstOrDefault().machine_name;
                string machine_code = _scifferContext.ref_machine.Where(x => x.machine_id == machine_id).FirstOrDefault().machine_code;
                return machine_code + " - " + machine_name;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string GetOperatorPhoto(int user_id)
        {
            try
            {
                var employee = _scifferContext.ref_user_management.Where(x => x.user_id == user_id).FirstOrDefault().REF_EMPLOYEE;
                return employee == null ? string.Empty : employee.employeephoto;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        public string GetOperatorName(int user_id)
        {
            try
            {
                var employee = _scifferContext.ref_user_management.Where(x => x.user_id == user_id).FirstOrDefault().REF_EMPLOYEE;
                return employee == null ? string.Empty : employee.employee_name;
            }
            catch (Exception)
            {
                return "";
            }
        }



        public List<ref_mfg_machine_task_status> GetAllStatus()
        {
            List<ref_mfg_machine_task_status> data = new List<ref_mfg_machine_task_status>();
            try
            {
                data = _scifferContext.ref_mfg_machine_task_status.ToList();
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        //update shift count
        public void UpdateShiftCount(int machine_id, int item_id)
        {
            try
            {
                var query = _scifferContext.mfg_qc.Where(x => x.machine_id == machine_id && x.item_id == item_id).FirstOrDefault();
                query.shift_count = 0;// shift count;
                query.lifetime_count = 0;//lifetime count changes to zero per shift
                _scifferContext.Entry(query).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        //get production count only
        public double GetProductionCount(string machine_id, int item_id, int status)
        {
            int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

            var status_str = "getcount";
            var entity = new SqlParameter("@entity", status_str);
            var _machine_id = new SqlParameter("@machine_id", machine_id);
            var _status = new SqlParameter("@status", status);
            var _item_id = new SqlParameter("@item_id", item_id);
            var operator_id = new SqlParameter("@operator_id", user);
            var curr_day = new SqlParameter("@current_day", DateTime.Now);
            var prev_day = new SqlParameter("@previous_day", DateTime.Now);
            var tag_no = new SqlParameter("@tag_no", "");

            var val = _scifferContext.Database.SqlQuery<mfg_machine_task_VM>(
                     "exec GetMahcineTask @entity,@machine_id,@status,@item_id,@operator_id,@current_day,@previous_day,@tag_no",
                     entity, _machine_id, _status, _item_id, operator_id, curr_day, prev_day, tag_no).ToList();
            return val.Count();
        }
        public double GetProductionCount1(string machine_id, int item_id, int status)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                var status_str = "getcount1";
                var entity = new SqlParameter("@entity", status_str);
                var _machine_id = new SqlParameter("@machine_id", machine_id);
                var _status = new SqlParameter("@status", status);
                var _item_id = new SqlParameter("@item_id", item_id);
                var operator_id = new SqlParameter("@operator_id", user);
                var curr_day = new SqlParameter("@current_day", DateTime.Now);
                var prev_day = new SqlParameter("@previous_day", DateTime.Now);
                var tag_no = new SqlParameter("@tag_no", "");

                var val = _scifferContext.Database.SqlQuery<mfg_machine_task_VM>(
                         "exec GetMahcineTask @entity,@machine_id,@status,@item_id,@operator_id,@current_day,@previous_day,@tag_no",
                         entity, _machine_id, _status, _item_id, operator_id, curr_day, prev_day, tag_no).ToList();
                return val.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //get target qty
        public List<prod_plan_detail_vm> GetTargetQtyCount(int machine_id, int item_id, int shift_id)
        {
            int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

            var _machine_id = new SqlParameter("@machine_id", machine_id);
            var _item_id = new SqlParameter("@item_id", item_id);
            var _shift_id = new SqlParameter("@shift_id", shift_id);


            var val = _scifferContext.Database.SqlQuery<prod_plan_detail_vm>(
                     "exec GetTargetQtyForMac @machine_id,@item_id,@shift_id",
                     _machine_id, _item_id, _shift_id).ToList();
            return val;
        }

        //get data on machine ok, not ok, assigned
        public List<mfg_machine_task_VM> GetMachineTask(int machine_id, int item_id, int status, string searchtag)
        {
            List<mfg_machine_task_VM> data = new List<mfg_machine_task_VM>();
            try
            {
                var status_str = "";
                var current_day = DateTime.Now;
                var previous_day = DateTime.Now.AddDays(-1);
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                if (status == 1)
                {
                    status_str = "Assigned";
                }
                else
                {
                    status_str = "OkNotOk";
                }

                var entity = new SqlParameter("@entity", status_str);
                var _machine_id = new SqlParameter("@machine_id", machine_id);
                var _status = new SqlParameter("@status", status);
                var _item_id = new SqlParameter("@item_id", item_id);
                var operator_id = new SqlParameter("@operator_id", user);
                var curr_day = new SqlParameter("@current_day", current_day);
                var prev_day = new SqlParameter("@previous_day", previous_day);
                var tag_no = new SqlParameter("@tag_no", searchtag);

                //1st Search in assigned items
                var val = _scifferContext.Database.SqlQuery<mfg_machine_task_VM>(
                         "exec GetMahcineTask @entity,@machine_id,@status,@item_id,@operator_id,@current_day,@previous_day,@tag_no",
                         entity, _machine_id, _status, _item_id, operator_id, curr_day, prev_day, tag_no).ToList();

                //if any item is not assigned on machine
                if (val.Count == 0)
                {
                    //check tag number is entered or not
                    if (searchtag == "")
                    {
                        return val;
                    }
                    else
                    {
                        //search tag is availble on machine or not
                        status_str = "GetTagNumberToCheck";
                        var entity1 = new SqlParameter("@entity", status_str);
                        var _machine_id1 = new SqlParameter("@machine_id", machine_id);
                        var _status1 = new SqlParameter("@status", status);
                        var _item_id1 = new SqlParameter("@item_id", item_id);
                        var operator_id1 = new SqlParameter("@operator_id", user);
                        var curr_day1 = new SqlParameter("@current_day", current_day);
                        var prev_day1 = new SqlParameter("@previous_day", previous_day);
                        var tag_no1 = new SqlParameter("@tag_no", searchtag);

                        var tagdata = _scifferContext.Database.SqlQuery<mfg_machine_task_VM>(
                             "exec GetMahcineTask @entity,@machine_id,@status,@item_id,@operator_id,@current_day,@previous_day,@tag_no",
                             entity1, _machine_id1, _status1, _item_id1, operator_id1, curr_day1, prev_day1, tag_no1).ToList();

                        //check tag number is ok'd on the current machine
                        if (!tagdata.Any(x => x.status_id == 2 && x.machine_id == machine_id.ToString()))
                        {
                            status_str = "GetTagNumber";
                            var entity2 = new SqlParameter("@entity", status_str);
                            var _machine_id2 = new SqlParameter("@machine_id", machine_id);
                            var _status2 = new SqlParameter("@status", status);
                            var _item_id2 = new SqlParameter("@item_id", item_id);
                            var operator_id2 = new SqlParameter("@operator_id", user);
                            var curr_day2 = new SqlParameter("@current_day", current_day);
                            var prev_day2 = new SqlParameter("@previous_day", previous_day);
                            var tag_no2 = new SqlParameter("@tag_no", searchtag);

                            var val1 = _scifferContext.Database.SqlQuery<mfg_machine_task_VM>(
                                 "exec GetMahcineTask @entity,@machine_id,@status,@item_id,@operator_id,@current_day,@previous_day,@tag_no",
                                 entity2, _machine_id2, _status2, _item_id2, operator_id2, curr_day2, prev_day2, tag_no2).ToList();

                            foreach (var item in val1)
                            {
                                var machine_name = "";
                                var machine_id_str = item.machine_id.Split('/');
                                var count = 0;
                                foreach (var zz in machine_id_str)
                                {
                                    count++;
                                    var id = int.Parse(zz);
                                    if (count == machine_id_str.Count())
                                    {
                                        machine_name = machine_name + _scifferContext.ref_machine.Where(x => x.machine_id == id).FirstOrDefault().machine_name;
                                    }
                                    else
                                    {
                                        machine_name = machine_name + _scifferContext.ref_machine.Where(x => x.machine_id == id).FirstOrDefault().machine_name + ", ";
                                    }
                                }
                                item.machine_name = machine_name;
                            }
                            return val1;
                        }
                        else
                        {
                            return val;
                        }
                    }
                }
                return val;
            }
            catch (Exception ex)
            {
                return data;
            }
        }
        //get machine data under qc
        public List<mfg_machine_task_qc_qc_VM> GetMachineTaskUnderQc(int machine_id, int status)
        {
            var data = new List<mfg_machine_task_qc_qc_VM>();
            try
            {
                data = (from qc in _scifferContext.mfg_machine_task_qc_qc
                        join invt in _scifferContext.inv_item_batch_detail_tag on qc.tag_id equals invt.tag_id
                        join mmt in _scifferContext.mfg_machine_task on qc.machine_task_id equals mmt.machine_task_id
                        join pod in _scifferContext.mfg_prod_order_detail on mmt.prod_order_detail_id equals pod.prod_order_detail_id
                        join po in _scifferContext.mfg_prod_order on pod.prod_order_id equals po.prod_order_id
                        join invb in _scifferContext.inv_item_batch on pod.batch_id equals invb.item_batch_id
                        join ri in _scifferContext.REF_ITEM on pod.in_item_id equals ri.ITEM_ID
                        where qc.machine_id == machine_id && qc.current_status_id == status
                        select new
                        {
                            machine_task_qc_qc_id = qc.machine_task_qc_qc_id,
                            machine_task_qc_qc_date = qc.machine_task_qc_qc_date,
                            machine_task_qc_qc_time = qc.machine_task_qc_qc_time,
                            machine_task_id = qc.machine_task_id,
                            mfg_qc_reason_id = qc.mfg_qc_reason_id,
                            machine_id = qc.machine_id,
                            current_status_id = qc.current_status_id,
                            is_corr_qc_triggered = qc.is_corr_qc_triggered,
                            tag_id = qc.tag_id,

                            prod_order_detail_id = pod.prod_order_detail_id,
                            prod_order_id = po.prod_order_id,
                            prod_order_no = po.prod_order_no,
                            in_item_id = pod.in_item_id,
                            item_batch_id = pod.batch_id,
                            item_batch_no = invb.batch_number,
                            tag_no = invt.tag_no,
                            status_name = qc.ref_mfg_machine_task_status.machine_task_status_name,
                            in_item_code = ri.ITEM_CODE,
                            in_item_desc = ri.ITEM_NAME,

                        }).ToList()
                        .Select(x => new mfg_machine_task_qc_qc_VM
                        {
                            machine_task_qc_qc_id = x.machine_task_qc_qc_id,
                            machine_task_qc_qc_date = x.machine_task_qc_qc_date,
                            machine_task_qc_qc_time = x.machine_task_qc_qc_time,
                            machine_task_id = x.machine_task_id,
                            mfg_qc_reason_id = x.mfg_qc_reason_id,
                            machine_id = x.machine_id,
                            current_status_id = x.current_status_id,
                            is_corr_qc_triggered = x.is_corr_qc_triggered,
                            tag_id = x.tag_id,

                            prod_order_id = x.prod_order_id,
                            prod_order_detail_id = x.prod_order_detail_id,
                            prod_order_no = x.prod_order_no,
                            in_item_id = x.in_item_id,
                            item_batch_id = x.item_batch_id,
                            item_batch_no = x.item_batch_no,
                            tag_no = x.tag_no,
                            current_status = x.status_name,
                            in_item_desc = x.in_item_desc,


                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public List<mfg_op_qc_parameter_list> GetAllParameters(int item_id, int machine_id)
        {
            var list = new List<mfg_op_qc_parameter_list>();
            try
            {
                list = (from op in _scifferContext.mfg_op_qc_parameter
                        join opd in _scifferContext.mfg_op_qc_parameter_list on op.mfg_op_qc_parameter_id equals opd.mfg_op_qc_parameter_id
                        where op.item_id == item_id && op.machine_id == machine_id && op.is_active == true && opd.is_active == true
                        select new
                        {
                            mfg_op_qc_parameter_id = op.mfg_op_qc_parameter_id,
                            mfg_op_qc_parameter_list_id = opd.mfg_op_qc_parameter_list_id,
                            parameter_name = opd.parameter_name,
                            parameter_uom = opd.parameter_uom,
                            std_range_start = opd.std_range_start,
                            std_range_end = opd.std_range_end,
                            is_numeric = opd.is_numeric,

                        }).ToList()
                        .Select(x => new mfg_op_qc_parameter_list
                        {
                            mfg_op_qc_parameter_id = x.mfg_op_qc_parameter_id,
                            mfg_op_qc_parameter_list_id = x.mfg_op_qc_parameter_list_id,
                            parameter_name = x.parameter_name,
                            parameter_uom = x.parameter_uom,
                            std_range_start = x.std_range_start,
                            std_range_end = x.std_range_end,
                            is_numeric = x.is_numeric,
                        }).ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        //update item status--main procedure call
        public string UpdateItemStatus(int status_id, int mach_task_id, int in_item_id, int mach_id, string[] parametervalue, int[] parameterid, string heatcode, string runcode, int process_id, string tag_no_two, int supervisor_id, string supervisor_remarks)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                var production_order_detail_id = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == mach_task_id).FirstOrDefault().prod_order_detail_id;

                var production_order_id = _scifferContext.mfg_prod_order_detail.Where(x => x.prod_order_detail_id == production_order_detail_id).FirstOrDefault().prod_order_id;

                var Machine_sequence = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == production_order_id).FirstOrDefault().machine_seq;
                var ProcessSequence = Machine_sequence.Split(',');
                var next_machine_in_sequecne = "";

                var machine_task = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == mach_task_id).FirstOrDefault();

                var month = DateTime.Now.Month;
                var year = DateTime.Now.Year;

                var mfg_tag_number = _scifferContext.mfg_tag_numbering.Where(x => x.item_id == in_item_id &&
                                    x.machine_id == mach_id && x.month == month && x.year == year).FirstOrDefault();

                var first_machine = ProcessSequence[0];
                if (mfg_tag_number == null && first_machine == mach_id.ToString())
                {
                    var tag_number_ref = _scifferContext.mfg_tag_numbering_ref.Where(x => x.year == year && x.month == month).FirstOrDefault();

                    mfg_tag_numbering mtn = new mfg_tag_numbering();
                    mtn.prefix = tag_number_ref.series;
                    mtn.month = month;
                    mtn.year = year;
                    mtn.item_id = in_item_id;
                    mtn.machine_id = mach_id;
                    mtn.is_active = true;
                    mtn.is_blocked = false;
                    mtn.to_number = "9999";
                    mtn.from_number = "0001";
                    mtn.current_number = null;
                    _scifferContext.mfg_tag_numbering.Add(mtn);
                    _scifferContext.SaveChanges();
                }

                //get next machine in sequence
                try
                {
                    for (var i = 0; i < ProcessSequence.Length; i++)
                    {
                        if (mach_id.ToString() == ProcessSequence[i])
                        {
                           // i++;
                            next_machine_in_sequecne = ProcessSequence[i];
                            break;
                        }
                        else
                        {
                            var machines = ProcessSequence[i].Split('/');
                            if (machines.Contains(mach_id.ToString()))
                            {
                                i++;
                                next_machine_in_sequecne = ProcessSequence[i];
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }

                DataTable t11 = new DataTable();
                t11.Columns.Add("mach_task_id", typeof(int));
                t11.Columns.Add("mfg_op_qc_parameter_id", typeof(int));
                t11.Columns.Add("parameter_value", typeof(string));

                try
                {
                    for (int i = 0; i < parameterid.Length; i++)
                    {
                        t11.Rows.Add(mach_task_id, Convert.ToInt32(parameterid[i]), parametervalue[i]);
                    }

                }
                catch (Exception ex)
                {

                }
                var machine_task_id = new SqlParameter("@machine_task_id", mach_task_id);
                var machine_id = new SqlParameter("@machine_id", mach_id);
                var machine_task_status_id = new SqlParameter("@machine_task_status_id", status_id);
                var item_id = new SqlParameter("@in_item_id", in_item_id);
                var operator_id = new SqlParameter("@operator_id", user);
                var prod_order_id = new SqlParameter("@prod_order_id", production_order_id);
                var next_machine_id = new SqlParameter("@next_machine_id", next_machine_in_sequecne);
                var _heatcode = new SqlParameter("@heatcode", heatcode);
                var _runcode = new SqlParameter("@runcode", runcode);
                var _tag_no_two = new SqlParameter("@tag_no_two", tag_no_two);
                var _supervisor_id = new SqlParameter("@supervisor_id", supervisor_id);
                var _supervisor_remarks = new SqlParameter("@supervisor_remarks", supervisor_remarks);
                var _process_id = new SqlParameter("@process_id", process_id);

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_machine_task_op_qc_detail";
                t1.Value = t11;

                var val = _scifferContext.Database.SqlQuery<string>
                            ("exec Save_MachineTask @machine_task_id, @machine_id, @machine_task_status_id, @in_item_id, @operator_id, @prod_order_id, @next_machine_id, @heatcode, @runcode, @tag_no_two, @supervisor_id, @supervisor_remarks, @process_id, @t1",
                            machine_task_id, machine_id, machine_task_status_id, item_id, operator_id, prod_order_id, next_machine_id, _heatcode, _runcode, _tag_no_two, _supervisor_id, _supervisor_remarks, _process_id, t1).FirstOrDefault();

                if (val.Contains("ACCESSDENIED"))
                {
                    return "ACCESSDENIED";
                }
                else if (val == "UNDERQC" || val.Contains("Saved") || val.Contains("ENTEREDINQC") || val == "MACHINEBLOCKED")
                {
                    try
                    {
                        var query = (from tool in _scifferContext.ref_tool_machine_usage
                                     join item in _scifferContext.ref_tool_machine_item_usage on tool.tool_machine_usage_id equals item.tool_machine_usage_id
                                     where tool.machine_id == mach_id && item.item_id == in_item_id && tool.in_use == true
                                     select item).ToList();
                        foreach (var item in query)
                        {
                            item.no_of_items_processed = item.no_of_items_processed + 1;
                            item.item_life_consumption = Math.Round((item.no_of_items_processed * item.per_unit_life_consumption), 4);
                            item.item_life_consumption_percentage = Math.Round((item.item_life_consumption * 100), 4);

                            item.ref_tool_machine_usage.current_life_percentage = Math.Round((item.ref_tool_machine_usage.current_life_percentage + ((1 * item.per_unit_life_consumption) * 100)), 4);

                            _scifferContext.Entry(item).State = EntityState.Modified;
                        }
                        _scifferContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        log4net.GlobalContext.Properties["user"] = 0;
                        log.Error("Exception Occured ", ex);
                        if (ex.Message.ToString().Contains("inner exception"))
                            log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                    }
                    return val;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }

        public string GetMachineSequence(int prod_order_id)
        {
            var machine_sequence = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == prod_order_id).FirstOrDefault().machine_seq;
            return machine_sequence;
        }

        public string GetMachineStatusOkay(int machine_task_id)
        {
            try
            {
                var list = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == machine_task_id).FirstOrDefault();
                if (list.machine_task_status_id == 2)
                {
                    return "Okay";
                }
                else
                {
                    return "Not Okay";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetLastTagUsed(int machine_task_id)
        {
            try
            {
                var tag_no = (from mmt in _scifferContext.mfg_machine_task
                              join inv in _scifferContext.inv_item_batch_detail_tag on mmt.tag_id equals inv.tag_id
                              where mmt.machine_task_id == machine_task_id
                              select new
                              {
                                  mmt.tag_id,
                                  inv.tag_no,
                              }).FirstOrDefault();

                string lastitem = tag_no.tag_no;

                return lastitem;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public List<ref_machine> GetMachineList(string[] machine_id)
        {
            var query = new ref_machine();
            var list = new List<ref_machine>();
            try
            {
                foreach (var item in machine_id)
                {
                    var mid = int.Parse(item);
                    query = (from rm in _scifferContext.ref_machine
                             where rm.machine_id == mid
                             select new
                             {
                                 machine_id = rm.machine_id,
                                 machine_code = rm.machine_code,
                                 machine_name = rm.machine_name
                             }).ToList()
                            .Select(x => new ref_machine
                            {
                                machine_id = x.machine_id,
                                machine_name = x.machine_code + " - " + x.machine_name,
                            }).FirstOrDefault();

                    list.Add(query);
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        public string GenerateNewTagNumber(string tag_number, string item_id, string machine_list)
        {
            try
            {
                int item = Convert.ToInt32(item_id);
                machine_list = machine_list.Replace(',', '/');

                var batch_data = (from iibdt in _scifferContext.inv_item_batch_detail_tag
                                  join iib in _scifferContext.inv_item_batch on iibdt.item_batch_id equals iib.item_batch_id
                                  where iib.item_id == item
                                  select new
                                  {
                                      tag_id = iibdt.tag_id,
                                      tag_no = iibdt.tag_no,
                                  }).ToList()
                                  .Select(x => new inv_item_batch_detail_tag
                                  {
                                      tag_id = x.tag_id,
                                      tag_no = x.tag_no,
                                  }).ToList();


                if (!batch_data.Any(x => x.tag_no == tag_number))
                {
                    foreach (var batch in batch_data)
                    {
                        inv_item_batch_detail_tag tag = _scifferContext.inv_item_batch_detail_tag.Where(x => x.tag_id == batch.tag_id).FirstOrDefault();
                        mfg_machine_task mmt = _scifferContext.mfg_machine_task.Where(x => x.tag_id == batch.tag_id).FirstOrDefault();
                        var pod = _scifferContext.mfg_prod_order_detail.Where(x => x.batch_id == tag.item_batch_id).FirstOrDefault();
                        var po = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == pod.prod_order_id).FirstOrDefault().machine_seq;
                        var ProcessSequence = po.Split(',');
                        var flag = false;

                        try
                        {
                            for (var i = 0; i < ProcessSequence.Length; i++)
                            {
                                if (machine_list == ProcessSequence[i])
                                {
                                    flag = true;
                                    break;
                                }
                                else
                                {
                                    var machines = ProcessSequence[i].Split('/');
                                    if (machines.Contains(machine_list.ToString()))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }

                        if (flag == false)
                        {
                            return "wrongsequence";
                        }

                        try
                        {
                            if ((tag.tag_no == "" || tag.tag_no == null) && mmt.machine_task_status_id == 1)
                            {
                                var first_machine = po.First().ToString();

                                if (mmt.machine_id == first_machine)
                                {
                                    tag.tag_no = tag_number;
                                    _scifferContext.Entry(tag).State = EntityState.Modified;

                                    mmt.machine_id = machine_list;
                                    mmt.task_time = DateTime.Now;
                                    _scifferContext.Entry(mmt).State = EntityState.Modified;

                                    _scifferContext.SaveChanges();
                                    break;
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                else
                {
                    return "failed";
                }
                return "Saved";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //get Supervisor list
        public List<ref_user_management_VM> GetSupervisorList()
        {
            var result = (from rum in _scifferContext.ref_user_management.Where(x => !x.is_block)
                          join rurm in _scifferContext.ref_user_role_mapping on rum.user_id equals rurm.user_id
                          join rumr in _scifferContext.ref_user_management_role on rurm.role_id equals rumr.role_id
                          join re in _scifferContext.REF_EMPLOYEE.Where(x => !x.is_block) on rum.employee_id equals re.employee_id
                          where rumr.role_code == "PROD_SUP"
                          select new ref_user_management_VM
                          {
                              user_id = rum.user_id,
                              user_name = rum.user_name + " - " + re.employee_name,
                          }).ToList();
            return result;
        }

        public int? GetCycleTime(int item_id, int process_id, int machine_id)
        {
            var is_allow = _scifferContext.ref_validation.FirstOrDefault()?.allow_to_check_cycle_time;
            if (is_allow == true)
            {
                var result = _scifferContext.ref_cycle_time.FirstOrDefault(x => x.item_id == item_id && x.process_id == process_id && x.machine_id == machine_id && x.is_active);
                return result == null ? 0 : result.cycle_time;
            }

            return null;

        }

        //Get machine list by operator mapped with operator and machine
        public List<ref_machine_master_VM> GetMachineListByProcess(int process_id)
        {
            var list = new List<ref_machine_master_VM>();
            try
            {
                list = (from mmpm in _scifferContext.map_mfg_process_machine
                        join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                        where mmpm.process_id == process_id
                        select new ref_machine_master_VM
                        {
                            machine_id = rm.machine_id,
                            machine_name = rm.machine_name,
                        }).ToList();

            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
            }
            return list;
        }

        //Get machine list by operator mapped with operator and process
        public List<ref_mfg_process> GetProcessListByOperator(int user_id)
        {
            var list = new List<ref_mfg_process>();
            try
            {
                var role_id = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id).Select(z => z.role_id).ToList();

                foreach (var r in role_id)
                {
                    var role_code = _scifferContext.ref_user_management_role.FirstOrDefault(x => x.role_id == r).role_code;
                    if (role_code == "IT_ADMIN" || role_code == "TOP_MGMT" || role_code == "MAIN_MGR"
                        || role_code == "QA_MGR" || role_code == "PROD_MGR" || role_code == "PROD_SUP" || role_code == "QA_OP")
                    {
                        list = _scifferContext.ref_mfg_process.ToList();
                        return list;
                    }
                    else
                    {
                        continue;
                    }
                }

                list = (from moo in _scifferContext.map_operation_operator
                        join rmp in _scifferContext.ref_mfg_process on moo.operation_id equals rmp.process_id
                        where moo.operator_id == user_id
                        select new
                        {
                            process_id = rmp.process_id,
                            process_description = rmp.process_description,
                        }).ToList().Select(x => new ref_mfg_process
                        {
                            process_id = x.process_id,
                            process_description = x.process_description,
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }

        //Get Operators production count per shift(1st Shift, 2nd Shift, 3rd Shift)
        public int GetOperatorProductionCountPerShift(string entity, int machine_id, int item_id)
        {
            int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
            var _entity = new SqlParameter("@entity", entity);
            var _machine_id = new SqlParameter("@machine_id", machine_id);
            var _status = new SqlParameter("@status", 2);
            var _item_id = new SqlParameter("@item_id", item_id);
            var operator_id = new SqlParameter("@operator_id", user);
            var curr_day = new SqlParameter("@current_day", DateTime.Now);
            var prev_day = new SqlParameter("@previous_day", DateTime.Now);
            var tag_no = new SqlParameter("@tag_no", "");

            var val = _scifferContext.Database.SqlQuery<int>(
                    "exec GetMahcineTask @entity,@machine_id,@status,@item_id,@operator_id,@current_day,@previous_day,@tag_no",
                    _entity, _machine_id, _status, _item_id, operator_id, curr_day, prev_day, tag_no).FirstOrDefault();
            return val;

        }

        public string get_machine_entry_mac_level_mapping(string entity, int machine_id, int process_id, int shift_id)
        {
            var entity1 = new SqlParameter("@entity", entity);
            var machine_id1 = new SqlParameter("@machine_id", machine_id);
            var process_id1 = new SqlParameter("@operator_id", process_id);
            var shift_id1 = new SqlParameter("@shift_id", shift_id);
            var val = _scifferContext.Database.SqlQuery<string>(
            "exec get_machine_entry_mac_level_mapping @entity,@machine_id,@operator_id,@shift_id",
            entity1, machine_id1, process_id1, shift_id1).FirstOrDefault();
            return val;
        }

        public int GetPlantId(int shift_id)
        {
            int plant_id = 0;
            try
            {
                if (shift_id != 0)
                {
                    plant_id = _scifferContext.ref_shifts.Where(x => x.shift_id == shift_id).FirstOrDefault().plant_id;
                    return plant_id;

                }
                return plant_id;

            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public List<mfg_rejection_detail_vm> GetAllParametersAssign(int id)
        {
            var list = new List<mfg_rejection_detail_vm>();
            try
            {
                list = (from de in _scifferContext.mfg_rejection_detail
                        join tak in _scifferContext.inv_item_batch_detail_tag on de.tag_id equals tak.tag_id
                        join st in _scifferContext.ref_mfg_nc_status on de.nc_status_id equals st.nc_status_id
                        join msc in _scifferContext.ref_machine on de.machine_id equals msc.machine_id
                        join uer in _scifferContext.ref_user_management on de.operator_id equals uer.user_id
                        join tag in _scifferContext.inv_item_batch_detail_tag on de.tag_id equals tag.tag_id
                        where de.tag_id == id
                        select new
                        {
                            rejection_detail_id = de.rejection_detail_id,
                            tag_id = de.tag_id,
                            Non_coforming_status = st.nc_status_desc,
                            non_conforming_desc = de.nc_details,
                            action_plan = de.action_plan,
                            root_cause_details = de.root_cause_details,
                            created_oon = de.create_ts,
                            remarks = de.remarks,
                            machine = msc.machine_name,
                            User = uer.user_name,
                            rejected_on_machine_by = uer.user_name + "" + "(" + de.create_ts + ")"


                        }).ToList()
                        .Select(x => new mfg_rejection_detail_vm
                        {
                            rejection_detail_id = x.rejection_detail_id,
                            tag_id = x.tag_id,
                            nc_status_desc = x.Non_coforming_status,
                            nc_details = x.non_conforming_desc,
                            action_plan = x.action_plan,
                            created_ts_on = string.Format("{0:dd-MMM-yyyy}", x.created_oon),
                            root_cause_details = x.root_cause_details,
                            remarks = x.remarks,
                            machine_name = x.machine,
                            create_ts_str = x.User,
                            rejected_onmachine_by = x.rejected_on_machine_by
                        }).OrderByDescending(x => x.rejection_detail_id).Take(2).ToList();
            }
            catch (Exception ex)
            { }
            return list;
        }
    }
}
