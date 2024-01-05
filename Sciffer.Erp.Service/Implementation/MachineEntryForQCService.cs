using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class MachineEntryForQCService : IMachineEntryForQCService
    {
        private readonly ScifferContext _scifferContext;

        public MachineEntryForQCService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        //Get Under QC Records 
        public List<mfg_machine_task_qc_qc_VM> GetMachinetaskUnderQC(int status_id)
        {
            List<mfg_machine_task_qc_qc_VM> result = new List<mfg_machine_task_qc_qc_VM>();
            try
            {
                if (status_id == 6)
                {
                    var _status_id = new SqlParameter("@status_id", status_id);

                    result = _scifferContext.Database.SqlQuery<mfg_machine_task_qc_qc_VM>
                            ("exec Get_MachineTaskNCQCList @status_id",
                            _status_id).ToList();
                }
                else
                {
                    result = (from qc in _scifferContext.mfg_machine_task_qc_qc
                              join invt in _scifferContext.inv_item_batch_detail_tag on qc.tag_id equals invt.tag_id
                              join mmt in _scifferContext.mfg_machine_task on qc.machine_task_id equals mmt.machine_task_id
                              join pod in _scifferContext.mfg_prod_order_detail on mmt.prod_order_detail_id equals pod.prod_order_detail_id
                              join po in _scifferContext.mfg_prod_order on pod.prod_order_id equals po.prod_order_id
                              join inv in _scifferContext.inv_item_batch on pod.batch_id equals inv.item_batch_id
                              join rm in _scifferContext.ref_machine on qc.machine_id equals rm.machine_id
                              join ri in _scifferContext.REF_ITEM on pod.in_item_id equals ri.ITEM_ID
                              where qc.current_status_id == status_id && mmt.machine_task_status_id != 9
                              orderby qc.modify_ts descending
                              select new
                              {
                                  machine_task_qc_qc_id = qc.machine_task_qc_qc_id,
                                  tag_id = (int)qc.tag_id,
                                  tag_no = invt.tag_no,
                                  machine_id = qc.machine_id,
                                  machine_task_id = qc.machine_task_id,
                                  current_status_id = qc.current_status_id,
                                  current_status = qc.ref_mfg_machine_task_status.machine_task_status_name,

                                  prod_order_detail_id = pod.prod_order_detail_id,
                                  in_item_id = pod.in_item_id,
                                  prod_order_id = po.prod_order_id,
                                  prod_order_no = po.prod_order_no,
                                  batch_id = pod.batch_id,
                                  batch_number = inv.batch_number,

                                  machine_code = rm.machine_code,
                                  machine_name = rm.machine_name,

                                  in_item_code = ri.ITEM_CODE,
                                  in_item_desc = ri.ITEM_NAME,
                                  mfg_qc_reason_id = qc.mfg_qc_reason_id,

                              }).ToList()
                           .Select(x => new mfg_machine_task_qc_qc_VM
                           {
                               machine_task_qc_qc_id = x.machine_task_qc_qc_id,
                               tag_id = x.tag_id,
                               tag_no = x.tag_no,
                               machine_id = x.machine_id,
                               machine_task_id = x.machine_task_id,
                               current_status_id = x.current_status_id,
                               current_status = x.current_status,

                               prod_order_detail_id = x.prod_order_detail_id,
                               in_item_id = x.in_item_id,
                               prod_order_id = x.prod_order_id,
                               prod_order_no = x.prod_order_no,
                               item_batch_id = x.batch_id,
                               item_batch_no = x.batch_number,

                               in_item_desc = x.in_item_desc,
                               machine_desc = x.machine_name,
                               mfg_qc_reason_id = x.mfg_qc_reason_id,
                           }).Take(1500).ToList();
                }

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        //Get QC Parameters
        public List<mfg_qc_qc_parameter_list> GetAllParameters(int item_id, int machine_id)
        {
            var list = new List<mfg_qc_qc_parameter_list>();
            try
            {
                list = (from qc in _scifferContext.mfg_qc_qc_parameter
                        join qcd in _scifferContext.mfg_qc_qc_parameter_list on qc.mfg_qc_qc_parameter_id equals qcd.mfg_qc_qc_parameter_id
                        where qc.item_id == item_id && qc.machine_id == machine_id && qc.is_active == true && qcd.is_active == true
                        select new
                        {
                            mfg_qc_qc_parameter_id = qc.mfg_qc_qc_parameter_id,
                            mfg_qc_qc_parameter_list_id = qcd.mfg_qc_qc_parameter_list_id,
                            parameter_name = qcd.parameter_name,
                            parameter_uom = qcd.parameter_uom,
                            std_range_start = qcd.std_range_start,
                            std_range_end = qcd.std_range_end,
                            is_numeric = qcd.is_numeric,

                        }).ToList()
                        .Select(x => new mfg_qc_qc_parameter_list
                        {
                            mfg_qc_qc_parameter_id = x.mfg_qc_qc_parameter_id,
                            mfg_qc_qc_parameter_list_id = x.mfg_qc_qc_parameter_list_id,
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


        //Update Tag number status
        public string UpdateItemStatus(int status, int machine_task_qc_qc_id, int machine_id, string[] parametervalue, int[] parameterid)
        {
            try
            {
                mfg_machine_task_qc_qc mmtqc = _scifferContext.mfg_machine_task_qc_qc.Where(x => x.machine_task_qc_qc_id == machine_task_qc_qc_id).FirstOrDefault();

                mfg_machine_task mmt = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == mmtqc.machine_task_id).FirstOrDefault();

                var production_order_id = _scifferContext.mfg_prod_order_detail.Where(x => x.prod_order_detail_id == mmt.prod_order_detail_id).FirstOrDefault().prod_order_id;

                var Machine_sequence = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == production_order_id).FirstOrDefault().machine_seq;
                var ProcessSequence = Machine_sequence.Split(',');
                var next_machine_in_sequecne = "";

                //get next machine in sequence
                try
                {
                    for (var i = 0; i < ProcessSequence.Length; i++)
                    {
                        if (machine_id.ToString() == ProcessSequence[i])
                        {
                            i++;
                            next_machine_in_sequecne = ProcessSequence[i];
                            break;
                        }
                        else
                        {
                            var machines = ProcessSequence[i].Split('/');
                            if (machines.Contains(machine_id.ToString()))
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
                t11.Columns.Add("machine_task_qc_qc_id", typeof(int));
                t11.Columns.Add("mfg_qc_qc_parameter_id", typeof(int));
                t11.Columns.Add("parameter_value", typeof(string));

                try
                {
                    for (int i = 0; i < parameterid.Length; i++)
                    {
                        t11.Rows.Add(machine_task_qc_qc_id, Convert.ToInt32(parameterid[i]), parametervalue[i]);
                    }
                }
                catch (Exception ex)
                {

                }
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var _machine_task_qc_qc_id = new SqlParameter("@machine_task_qc_qc_id", machine_task_qc_qc_id);
                var machine_task_status_id = new SqlParameter("@machine_task_status_id", status);
                var operator_id = new SqlParameter("@operator_id", user);
                var next_machine_id = new SqlParameter("@next_machine_id", next_machine_in_sequecne);
                var prod_order_detail_id = new SqlParameter("@prod_order_detail_id", mmt.prod_order_detail_id);

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_mfg_machine_task_qc_qc_detail";
                t1.Value = t11;

                var val = _scifferContext.Database.SqlQuery<string>
                            ("exec Save_MachineTaskForQc @machine_task_qc_qc_id, @machine_task_status_id, @operator_id, @next_machine_id, @prod_order_detail_id, @t1",
                            _machine_task_qc_qc_id, machine_task_status_id, operator_id, next_machine_id, prod_order_detail_id, t1).FirstOrDefault();
                if (val == "Saved")
                {
                    return val;
                }
                else
                {
                    return "Error";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Get Machine blocked or not
        public List<mfg_qc_VM> GetMahineStatus()
        {
            List<mfg_qc_VM> result = new List<mfg_qc_VM>();
            try
            {
                result = (from mq in _scifferContext.mfg_qc.Where(x => x.is_machine_blocked == true)
                          join rm in _scifferContext.ref_machine on mq.machine_id equals rm.machine_id
                          select new
                          {
                              mfg_qc_id = mq.mfg_qc_id,
                              machine_id = mq.machine_id,
                              machine_code = rm.machine_code,
                              machine_name = rm.machine_name,
                              is_machine_blocked = mq.is_machine_blocked,
                          }).ToList()
                          .Select(x => new mfg_qc_VM
                          {
                              mfg_qc_id = x.mfg_qc_id,
                              machine_id = x.machine_id,
                              machine_code = x.machine_code,
                              machine_desc = x.machine_name,
                              is_machine_blocked = x.is_machine_blocked,
                          }).ToList();

                result = result.GroupBy(z => z.machine_id).Select(x => new mfg_qc_VM
                {
                    mfg_qc_id = x.Select(c => c.mfg_qc_id).FirstOrDefault(),
                    machine_id = x.Select(c => c.machine_id).FirstOrDefault(),
                    machine_code = x.Select(c => c.machine_code).FirstOrDefault(),
                    machine_desc = x.Select(c => c.machine_desc).FirstOrDefault(),
                    is_machine_blocked = x.Select(c => c.is_machine_blocked).FirstOrDefault(),
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        //Remove machine block
        public string UpdateMachineStatus(int machine_id, bool is_machine_blocked)
        {
            try
            {
                List<mfg_qc> qc = _scifferContext.mfg_qc.Where(x => x.machine_id == machine_id).ToList();
                foreach (var item in qc)
                {
                    item.is_machine_blocked = is_machine_blocked;
                    _scifferContext.Entry(item).State = EntityState.Modified;
                }
                _scifferContext.SaveChanges();

                //status.machine_id = _scifferContext.mfg_qc.Where(x => x.mfg_qc_id == status.mfg_qc_id).FirstOrDefault().machine_id;

                //status.machine_desc = _scifferContext.mfg_qc.Where(x => x.mfg_qc_id == status.mfg_qc_id).FirstOrDefault().ref_machine.machine_code.ToString()
                //    + "-" + _scifferContext.mfg_qc.Where(x => x.mfg_qc_id == status.mfg_qc_id).FirstOrDefault().ref_machine.machine_name.ToString();

                //status.is_machine_blocked = _scifferContext.mfg_qc.Where(x => x.mfg_qc_id == status.mfg_qc_id).FirstOrDefault().is_machine_blocked;
                return "Saved";
            }
            catch (Exception e)
            {
                return "Error";
            }

        }

        //Check duplicate tag number from same machine
        public string CheckDuplicateTagNumber(int machine_id, int machine_task_qc_qc_id)
        {
            try
            {
                //get machine_task_qc row
                var machine_task_qc = _scifferContext.mfg_machine_task_qc_qc.Where(x => x.machine_task_qc_qc_id == machine_task_qc_qc_id && x.machine_id == machine_id).FirstOrDefault();
                //get machine_task row
                var machine_task = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == machine_task_qc.machine_task_id).FirstOrDefault();
                if (machine_task.machine_task_status_id == 2 && machine_task_qc.current_status_id == 5)
                {
                    return "Approved";
                }
                else
                {
                    return "NotApproved";
                }
            }
            catch (Exception ex)
            {
                return "Approved";
            }
        }

        //Get all non-comforming status
        public List<ref_mfg_nc_status> GetNcStatus()
        {
            return _scifferContext.ref_mfg_nc_status.ToList();
        }

        //Save Rejection complete work flow
        public string SaveRejectionDetail(int machine_task_qc_qc_id, int nc_status_id, string root_cause, string nc_details, string action_plan, string remarks, string[] why_why_analysis, string nc_tag_number)
        {
            try
            {
                int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                mfg_machine_task_qc_qc qc = _scifferContext.mfg_machine_task_qc_qc.Where(x => x.machine_task_qc_qc_id == machine_task_qc_qc_id).FirstOrDefault();
                //qc.current_status_id = 9;
                //qc.modify_user = user;
                //qc.modify_ts = DateTime.Now;
                //_scifferContext.Entry(qc).State = EntityState.Modified;
                mfg_rejection_detail mrd = new mfg_rejection_detail();
                mrd.tag_id = qc.tag_id;
                mrd.machine_id = qc.machine_id;
                mrd.operator_id = user;
                mrd.root_cause_details = root_cause;
                mrd.nc_status_id = nc_status_id;
                mrd.nc_details = nc_details;
                mrd.action_plan = action_plan;
                mrd.create_ts = DateTime.Now;
                mrd.machine_task_qc_qc_id = machine_task_qc_qc_id;
                mrd.remarks = remarks;
                mrd.why1 = why_why_analysis[0];
                mrd.why2 = why_why_analysis[1];
                mrd.why3 = why_why_analysis[2];
                mrd.why4 = why_why_analysis[3];
                mrd.why5 = why_why_analysis[4];
                mrd.nc_tag_number = nc_tag_number;

                _scifferContext.mfg_rejection_detail.Add(mrd);

                mfg_machine_task machine_task = _scifferContext.mfg_machine_task.Where(x => x.machine_task_id == qc.machine_task_id).FirstOrDefault();
                machine_task.machine_task_status_id = 3;
                _scifferContext.Entry(machine_task).State = EntityState.Modified;

                var nc_status_code = _scifferContext.ref_mfg_nc_status.Where(x => x.nc_status_id == nc_status_id).FirstOrDefault().nc_status_code;

                if (nc_status_code == "QCAPRV")
                {
                    machine_task.machine_task_status_id = 2;
                    _scifferContext.Entry(machine_task).State = EntityState.Modified;

                    qc.current_status_id = 5;
                    _scifferContext.Entry(qc).State = EntityState.Modified;

                    //Get production order
                    var production_order_id = _scifferContext.mfg_prod_order_detail.Where(x => x.prod_order_detail_id == machine_task.prod_order_detail_id).FirstOrDefault().prod_order_id;
                    //Get machine sequence
                    var Machine_sequence = _scifferContext.mfg_prod_order.Where(x => x.prod_order_id == production_order_id).FirstOrDefault().machine_seq;
                    var ProcessSequence = Machine_sequence.Split(',');
                    var next_machine_in_sequecne = "";

                    //get next machine in sequence
                    try
                    {
                        for (var i = 0; i < ProcessSequence.Length; i++)
                        {
                            if (machine_task.machine_id.ToString() == ProcessSequence[i])
                            {
                                i++;
                                next_machine_in_sequecne = ProcessSequence[i];
                                break;
                            }
                            else
                            {
                                var machines = ProcessSequence[i].Split('/');
                                if (machines.Contains(machine_task.machine_id.ToString()))
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
                    //Insert into mfg_machine_task for rework
                    mfg_machine_task mmt = new mfg_machine_task();
                    mmt.prod_order_detail_id = machine_task.prod_order_detail_id;
                    mmt.tag_id = machine_task.tag_id;
                    mmt.machine_id = next_machine_in_sequecne;
                    mmt.machine_task_status_id = 1;
                    _scifferContext.mfg_machine_task.Add(mmt);
                }

                _scifferContext.SaveChanges();
                return "Saved";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //Get Rejection work flow track
        public List<mfg_rejection_detail_vm> GetNonConformingTrack(int machine_task_qc_qc_id)
        {
            List<mfg_rejection_detail_vm> result = new List<mfg_rejection_detail_vm>();
            try
            {
                result = (from mrd in _scifferContext.mfg_rejection_detail
                          join qc in _scifferContext.mfg_machine_task_qc_qc on mrd.machine_task_qc_qc_id equals qc.machine_task_qc_qc_id
                          join rm in _scifferContext.ref_machine on mrd.machine_id equals rm.machine_id
                          join ru in _scifferContext.ref_user_management on mrd.operator_id equals ru.user_id
                          join re in _scifferContext.REF_EMPLOYEE on ru.employee_id equals re.employee_id into subpet1
                          from sub in subpet1.DefaultIfEmpty()
                          join nc in _scifferContext.ref_mfg_nc_status on mrd.nc_status_id equals nc.nc_status_id
                          join ru2 in _scifferContext.ref_user_management on qc.create_user equals ru2.user_id
                          join re2 in _scifferContext.REF_EMPLOYEE on ru2.employee_id equals re2.employee_id into subpet2
                          from sub2 in subpet2.DefaultIfEmpty()
                          where mrd.machine_task_qc_qc_id == machine_task_qc_qc_id
                          select new mfg_rejection_detail_vm
                          {
                              tag_id = mrd.tag_id,
                              machine_id = mrd.machine_id,
                              machine_name = rm.machine_name,
                              operator_id = mrd.operator_id,
                              operator_name = sub.employee_name,
                              root_cause_details = mrd.root_cause_details,
                              nc_status_id = mrd.nc_status_id,
                              nc_status_desc = nc.nc_status_desc,
                              nc_details = mrd.nc_details,
                              action_plan = mrd.action_plan,
                              create_ts_str = mrd.create_ts.ToString(),
                              remarks = mrd.remarks,
                              rejected_onmachine_by = sub2.employee_name + " (" + qc.create_ts.ToString() + ")"
                          }).ToList();

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<ref_mfg_nc_status_vm> GetStatus()
        {
            var data = new List<ref_mfg_nc_status_vm>();

            data = (from nc in _scifferContext.ref_mfg_nc_status
                    select new ref_mfg_nc_status_vm
                    {
                        nc_status_id = nc.nc_status_id,
                        nc_status_code = nc.nc_status_code,
                        nc_status_desc = nc.nc_status_desc
                    }

                  ).ToList();

            return data;
            // return _scifferContext.ref_mfg_nc_status.ToList();
        }
    }
}
