using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;

        public TaskService(ScifferContext scifferContext, IGenericService genericService)
        {
            _scifferContext = scifferContext;
            _genericService = genericService;
        }

        public string Add(ref_task_vm vm)
        {
            try
            {
                DateTime dte = new DateTime(1990, 1, 1);
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var task_id = new SqlParameter("@task_id", vm.task_id == 0 ? -1 : vm.task_id);
                var task_type_id = new SqlParameter("@task_type_id", vm.task_type_id);
                var task_doer_id = new SqlParameter("@task_doer_id", vm.task_doer_id);
                var task_reviewer_id = new SqlParameter("@task_reviewer_id", vm.task_reviewer_id);
                var due_date = new SqlParameter("@due_date", vm.due_date == null ? dte : vm.due_date);
                var task_name = new SqlParameter("@task_name", vm.task_name);
                var is_recurring = new SqlParameter("@is_recurring", vm.is_recurring);
                var task_periodicity_id = new SqlParameter("@task_periodicity_id", vm.task_periodicity_id == null ? 0 : vm.task_periodicity_id);
                var remind_before_days = new SqlParameter("@remind_before_days", vm.remind_before_days);
                var first_escalation_days = new SqlParameter("@first_escalation_days", vm.first_escalation_days);
                var second_escalation_days = new SqlParameter("@second_escalation_days", vm.second_escalation_days);
                var created_by = new SqlParameter("@created_by", create_user);
                var task_details = new SqlParameter("@task_details", vm.task_details == null ? "" : vm.task_details);

                var is_active = new SqlParameter("@is_active", true);

                if (vm.FileUpload != null)
                {
                    vm.attachment = _genericService.GetFilePath("Task", vm.FileUpload);
                }
                else
                {
                    if (vm.attachment == null)
                    {
                        vm.attachment = "No File";
                    }
                }

                if (vm.FileUpload1 != null)
                {
                    vm.new_attachment = _genericService.GetFilePath("Task", vm.FileUpload1);
                }
                else
                {
                    if (vm.new_attachment == null)
                    {
                        vm.new_attachment = "No File";
                    }
                }
                var attachment = new SqlParameter("@attachment", vm.attachment);
                var category_id = new SqlParameter("@category_id", vm.task_category_id == null ? 0 : vm.task_category_id);
                var document_no = new SqlParameter("@document_no", vm.document_no == null ? "" : vm.document_no);
                var new_attachment = new SqlParameter("@new_attachment", vm.new_attachment);
                var status_id = new SqlParameter("@status_id", vm.status_id == null ? 0 : vm.status_id);
                var new_remarks = new SqlParameter("@new_remarks", vm.new_remarks == null ? "" : vm.new_remarks);
                var old_status_id = new SqlParameter("@old_status_id", vm.old_status_id == null ? 0 : vm.old_status_id);
                var parent_task_id = new SqlParameter("@parent_task_id", vm.parent_task_id == null ? 0 : vm.parent_task_id);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_task @task_id,@task_type_id,@task_doer_id,@task_reviewer_id,@due_date,@task_name, @is_recurring, @task_periodicity_id, @remind_before_days, @first_escalation_days, @second_escalation_days,@task_details,@created_by,@is_active,@attachment,@category_id,@document_no,@status_id,@new_attachment,@new_remarks,@old_status_id,@parent_task_id",
                    task_id, task_type_id, task_doer_id, task_reviewer_id, due_date, task_name, is_recurring, task_periodicity_id, remind_before_days, first_escalation_days, second_escalation_days, task_details, created_by, is_active, attachment, category_id, document_no, status_id, new_attachment, new_remarks, old_status_id, parent_task_id).FirstOrDefault();
                if (val.Contains("Saved"))
                {

                    if (vm.FileUpload != null)
                    {
                        vm.FileUpload.SaveAs(vm.attachment);
                    }
                    if (vm.FileUpload1 != null)
                    {
                        vm.FileUpload1.SaveAs(vm.new_attachment);
                    }
                    //for mail setupe
                    try
                    {
                        string task_no = val.Split('~')[1];
                        REF_EMPLOYEE em = new REF_EMPLOYEE();
                        em = _scifferContext.REF_EMPLOYEE.Where(x => x.employee_id == vm.task_doer_id).FirstOrDefault();
                        DateTime dtt = (DateTime)vm.due_date;
                        REF_EMPLOYEE e = new REF_EMPLOYEE();
                        e = _scifferContext.REF_EMPLOYEE.Where(x => x.employee_id == vm.task_reviewer_id).FirstOrDefault();
                        ref_user_management em1 = new ref_user_management();
                        em1 = _scifferContext.ref_user_management.Where(x => x.user_id == create_user).FirstOrDefault();
                        string textBody = "";
                        if (vm.task_id == 0)
                        {
                            if (em.email_id != null || em.email_id != string.Empty)
                            {
                                textBody = "";
                                textBody = "Hello " + em.employee_name + ", <br />Task no. " + task_no + " for " + vm.task_name + " has been created . <br /><br />Details of Tasks are as follows :- <br /><br />Task Name - " + vm.task_name + "<br /><br />Task Description - ";
                                textBody += vm.task_details == null ? "" : vm.task_details + "<br /><br />Due Date  - " + dtt.ToString("dd-MMM-yyyy") + "<br /><br />Task Reviewer - " + e.employee_name + "<br /><br />You are requested to login in the ERP Portal for further details.<br /><br /><br />Thank You. ";
                                _genericService.SendMail(em.email_id, e.email_id, em1 == null ? "" : em1.email, textBody, "Task Created. Task No : " + task_no + " for " + vm.task_name);
                            }
                        }
                        else
                        {
                            ref_status status = new ref_status();
                            status = _scifferContext.ref_status.Where(x => x.status_id == vm.status_id && x.form == "MCAL").FirstOrDefault();
                            if (status.status_name == "Completed")
                            {
                                textBody = "";
                                textBody = "Hello " + em.employee_name + ", <br />Task no. " + task_no + " for " + vm.task_name + " has been Completed . <br /><br />Details of Tasks are as follows :- <br /><br />Task Name - " + vm.task_name + "<br /><br />Task Description - ";
                                textBody += vm.task_details == null ? "" : vm.task_details + "<br /><br />Due Date  - " + dtt.ToString("dd-MMM-yyyy") + "<br /><br />Task Reviewer - " + e.employee_name + "<br /><br />You are requested to login in the ERP Portal for further details.<br /><br /><br />Thank You. ";
                                _genericService.SendMail(em.email_id, e.email_id, em1 == null ? "" : em1.email, textBody, "Task Completed. Task No : " + task_no + " for " + vm.task_name);
                            }
                            else if (status.status_name == "Cancelled")
                            {
                                textBody = "";
                                textBody = "Hello " + em.employee_name + ", <br />Task no. " + task_no + " for " + vm.task_name + " has been Cancelled . <br /><br />Details of Tasks are as follows :- <br /><br />Task Name - " + vm.task_name + "<br /><br />Task Description - ";
                                textBody += vm.task_details == null ? "" : vm.task_details + "<br /><br />Due Date  - " + dtt.ToString("dd-MMM-yyyy") + "<br /><br />Task Reviewer - " + e.employee_name + "<br /><br />You are requested to login in the ERP Portal for further details.<br /><br /><br />Thank You. ";
                                _genericService.SendMail(em.email_id, e.email_id, em1 == null ? "" : em1.email, textBody, "Task Cancelled. Task No : " + task_no + " for " + vm.task_name);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }

                    return val;
                }
                else
                {
                    System.IO.File.Delete(vm.attachment);
                    System.IO.File.Delete(vm.new_attachment);
                    return val;
                }


            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public List<ref_task_vm> GetAll()
        {
            int user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

            ref_user_management RCC1 = _scifferContext.ref_user_management.Where(x => x.user_id == user).FirstOrDefault();

            ref_user_management RCC = _scifferContext.ref_user_management.Where(x => x.user_name == "Admin").FirstOrDefault();
            if (user == RCC.user_id)
            {
                var query = (from ts in _scifferContext.ref_task.Where(x => x.is_active == true)
                                 //join tsl in _scifferContext.ref_task_log on ts.task_id equals tsl.task_id into tsl1
                                 //from tsl2 in tsl1.DefaultIfEmpty()
                             join c in _scifferContext.ref_task_type on ts.task_type_id equals c.task_type_id into c1
                             from c2 in c1.DefaultIfEmpty()
                             join p in _scifferContext.ref_task_periodicity on ts.task_periodicity_id equals p.task_periodicity_id into p1
                             from p2 in p1.DefaultIfEmpty()
                             join doer in _scifferContext.REF_EMPLOYEE on ts.task_doer_id equals doer.employee_id into doer1
                             from doer2 in doer1.DefaultIfEmpty()
                             join viewer in _scifferContext.REF_EMPLOYEE on ts.task_reviewer_id equals viewer.employee_id into viewer1
                             from viewer2 in viewer1.DefaultIfEmpty()
                             join sts in _scifferContext.ref_status on ts.status_id equals sts.status_id into sts1
                             from sts2 in sts1.DefaultIfEmpty()
                             select new ref_task_vm
                             {
                                 task_id = ts.task_id,
                                 task_type = c2 == null ? string.Empty : c2.task_type,
                                 doer = doer2 == null ? string.Empty : doer2.employee_code + "/" + doer2.employee_name,
                                 reviewer = viewer2 == null ? string.Empty : viewer2.employee_code + "/" + viewer2.employee_name,
                                 due_date = ts.due_date,
                                 recurring = ts.is_recurring == true ? "Yes" : "No",
                                 periodicity_name = p2 == null ? string.Empty : p2.task_periodicity_name,
                                 remind_before_days = ts.remind_before_days,
                                 first_escalation_days = ts.first_escalation_days,
                                 second_escalation_days = ts.second_escalation_days,
                                 task_name = ts.task_name,
                                 task_details = ts.task_details,
                                 created_ts = ts.created_ts,
                                 modified_ts = sts2 == null ? null : sts2.status_name != "Completed" ? null : ts.modified_ts,
                                 status = sts2 == null ? "" : sts2.status_name,
                                 document_no = ts.document_no
                             }).OrderByDescending(a => a.task_id).ToList();
                return query;
            }
            else
            {
                var query = (from ts in _scifferContext.ref_task.Where(x => x.is_active == true && x.task_doer_id == RCC1.employee_id)
                                 //join tsl in _scifferContext.ref_task_log on ts.task_id equals tsl.task_id into tsl1
                                 //from tsl2 in tsl1.DefaultIfEmpty()
                             join c in _scifferContext.ref_task_type on ts.task_type_id equals c.task_type_id into c1
                             from c2 in c1.DefaultIfEmpty()
                             join p in _scifferContext.ref_task_periodicity on ts.task_periodicity_id equals p.task_periodicity_id into p1
                             from p2 in p1.DefaultIfEmpty()
                             join doer in _scifferContext.REF_EMPLOYEE on ts.task_doer_id equals doer.employee_id into doer1
                             from doer2 in doer1.DefaultIfEmpty()
                             join viewer in _scifferContext.REF_EMPLOYEE on ts.task_reviewer_id equals viewer.employee_id into viewer1
                             from viewer2 in viewer1.DefaultIfEmpty()
                             join sts in _scifferContext.ref_status on ts.status_id equals sts.status_id into sts1
                             from sts2 in sts1.DefaultIfEmpty()
                             select new ref_task_vm
                             {
                                 task_id = ts.task_id,
                                 task_type = c2 == null ? string.Empty : c2.task_type,
                                 doer = doer2 == null ? string.Empty : doer2.employee_code + "/" + doer2.employee_name,
                                 reviewer = viewer2 == null ? string.Empty : viewer2.employee_code + "/" + viewer2.employee_name,
                                 due_date = ts.due_date,
                                 recurring = ts.is_recurring == true ? "Yes" : "No",
                                 periodicity_name = p2 == null ? string.Empty : p2.task_periodicity_name,
                                 remind_before_days = ts.remind_before_days,
                                 first_escalation_days = ts.first_escalation_days,
                                 second_escalation_days = ts.second_escalation_days,
                                 task_name = ts.task_name,
                                 task_details = ts.task_details,
                                 created_ts = ts.created_ts,
                                 modified_ts = sts2 == null ? null : sts2.status_name != "Completed" ? null : ts.modified_ts,
                                 status = sts2 == null ? "" : sts2.status_name,
                                 document_no = ts.document_no
                             }).OrderByDescending(a => a.task_id).ToList();
                return query;
            }

        }

        public ref_task_vm Get(int id)
        {
            ref_task ts = _scifferContext.ref_task.FirstOrDefault(c => c.task_id == id);
            Mapper.CreateMap<ref_task, ref_task_vm>();
            ref_task_vm mmv = Mapper.Map<ref_task, ref_task_vm>(ts);
            ref_task_log tsl = _scifferContext.ref_task_log.Where(v => v.task_id == id).OrderByDescending(x => x.task_log_id).Take(1).SingleOrDefault();
            mmv.old_status_id = tsl.new_status_id;
            mmv.attachment = ts.oroginal_attachment;
            // mmv.new_attachment = ts.new_attachment;

            var val = _scifferContext.Database.SqlQuery<Task_Status_vm>(
           "select tl.old_status_id,tl.new_status_id, st.status_name old_status, st1.status_name new_status,format( tl.created_ts,'dd/MMM/yyyy HH:MM:ss') created_ts,iif(oroginal_attachment='No File',(SELECT top 1 item FROM dbo.SplitString(tl.new_attachment, '/') order by item),(SELECT top 1 item FROM dbo.SplitString(tl.oroginal_attachment, '/') order by item)) as oroginal_attachment,tl.new_remarks,tl.new_attachment,tl.task_log_id,tl.oroginal_attachment attachment from ref_task_log tl " +
             "left join ref_status st on tl.old_status_id = st.status_id " +
             "left join ref_status st1 on tl.new_status_id = st1.status_id " +
             "where tl.task_id =" + id).ToList();
            mmv.Status_log = val;
            //var query = (from ts in _scifferContext.ref_task.Where(x => x.is_active == true && x.task_id==id)
            //             join tsl in _scifferContext.ref_task_log on ts.task_id equals tsl.task_id

            //             select new ref_task_vm
            //             {
            //                 task_id = ts.task_id,
            //                 task_type_id = ts.task_type_id,
            //                 task_doer_id = ts.task_doer_id,
            //                 task_reviewer_id = ts.task_reviewer_id,
            //                 due_date = ts.due_date,
            //                 is_recurring = ts.is_recurring ,
            //                 task_periodicity_id = ts.task_periodicity_id,
            //                 remind_before_days = ts.remind_before_days,
            //                 first_escalation_days = ts.first_escalation_days,
            //                 second_escalation_days = ts.second_escalation_days,
            //                 task_name = ts.task_name,
            //                 task_details = ts.task_details,
            //                 created_ts = ts.created_ts,
            //                 modified_ts = ts.modified_ts,
            //                 status_id = ts.status_id
            //             });
            return mmv;
        }

        public ref_task_log Getattachment(int id)
        {
            ref_task_log tsl = _scifferContext.ref_task_log.Where(v => v.task_log_id == id).SingleOrDefault();

            return tsl;
        }
    }
}
