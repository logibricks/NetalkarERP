using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _GenericService;
        public int user = 1;
        public NotificationService(IGenericService GenericService, ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
            _GenericService = GenericService;
            //try
            //{
            //    user = int.Parse(System.Web.HttpContext.Current.Session["User_Id"].ToString());
            //}
            //catch
            //{
            //}
        }

        public string Add(ref_pm_notification country)
        {
            try
            {
                country.attended_by_id = country.attended_by;
                country.attended_by = "";
                ref_status status = new ref_status();
                if (country.status_id==null)
                {
                    country.status_id = 0;
                }
                if(country.notification_id==0)
                {
                    status = _scifferContext.ref_status.Where(x => x.status_name == "Open" && x.form == "MALNOTIFICATION").FirstOrDefault();
                    country.status_id = status == null ? 0 : status.status_id;
                }                   
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                country.created_by = user;
                country.created_ts = DateTime.UtcNow;
                            
                ref_pm_notification notify = new ref_pm_notification();
             
                notify.under_taken_by_id = country.under_taken_by_id;
                notify.attending_date = country.attending_date;
                notify.attending_time = country.attending_time;
                notify.plant_id = country.plant_id;
                notify.malfunction_closure_date = country.malfunction_closure_date;
                notify.malfunction_closure_time = country.malfunction_closure_time;
                notify.operator_id = country.operator_id;
                notify.reviewed_by_id = country.reviewed_by_id;
                /*For Auto increment Doc number*/
                string current_no = "";
                string prefix_suffix = "";
                string doc_no = "";
                int ps = 0;
                if (country.notification_id == 0)
                {
                    ref_document_numbring d = _scifferContext.ref_document_numbring.Where(x => x.document_numbring_id == country.category_id).FirstOrDefault();
                  //  ref_document_numbring d = _scifferContext.ref_document_numbring.Where(x => x.document_numbring_id == country.category_id && x.plant_id == country.plant_id).FirstOrDefault();

                    current_no = d.current_number;
                    if (d.current_number == null)
                    {

                        ps = d.current_number == null ? (0 + 1) : (int.Parse(d.current_number) + 1);
                        d.current_number = ps.ToString();
                    }
                    else
                    {
                        ps = d.current_number == null ? (0 + 1) : (int.Parse(d.current_number) + 1);
                        d.current_number = ps.ToString();
                    }                 
                    if (country.FileUpload != null)
                    {
                        country.attachment = _GenericService.GetFilePath("Notification", country.FileUpload);
                    }
                    else
                    {
                        country.attachment = "No File";
                    }
                    if (country.FileUpload != null)
                    {
                        country.FileUpload.SaveAs(country.attachment);
                    }
                    country.open_close = false;
                    country.doc_number = _GenericService.GetDocumentNumbering(country.category_id);
                    _scifferContext.Entry(d).State = EntityState.Modified;
                    _scifferContext.ref_pm_notification.Add(country);
                }
                else
                {
                    _scifferContext.Entry(country).State = EntityState.Modified;
                }
                _scifferContext.SaveChanges();
                country.notification_id = _scifferContext.ref_pm_notification.Max(x => x.notification_id);
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(country.attachment);
                return "Error";
            }
            return country.doc_number;

        }

        public bool Delete(int id)
        {
            try
            {
                var country = _scifferContext.ref_pm_notification.FirstOrDefault(c => c.notification_id == id);

                _scifferContext.Entry(country).State = EntityState.Modified;
                _scifferContext.SaveChanges();
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

        public ref_pm_notification Get(int id)
        {
            var country = _scifferContext.ref_pm_notification.FirstOrDefault(c => c.notification_id == id);
            var status = _scifferContext.ref_status.Where(x => x.status_id == country.status_id).FirstOrDefault();

            if(status!=null)
            {
                if(status.status_name== "Open")
                {
                    status = _scifferContext.ref_status.Where(x => x.status_name == "Under Service" && x.form == "MALNOTIFICATION").FirstOrDefault();
                    country.status_id = status == null ? 0 : status.status_id; 
                }
              else  if (status.status_name == "Under Service")
                {
                    status = _scifferContext.ref_status.Where(x => x.status_name == "Malfunction Close" && x.form == "MALNOTIFICATION").FirstOrDefault();
                    country.status_id = status == null ? 0 : status.status_id;
                }
                else if (status.status_name == "Malfunction Close")
                {
                    status = _scifferContext.ref_status.Where(x => x.status_name == "Close" && x.form == "MALNOTIFICATION").FirstOrDefault();
                    country.status_id = status == null ? 0 : status.status_id;
                }
            } 
            return country;
        }

        public List<ref_pm_notification_vm> GetAll(int? machine_id)
        {
            List<ref_pm_notification_vm> query;
            if (machine_id == 0)
            {

                 query = _scifferContext.Database.SqlQuery<ref_pm_notification_vm>("exec get_notification_index_list ").ToList();
                
                //query = (from n in _scifferContext.ref_pm_notification
                //         join p in _scifferContext.REF_PLANT on n.plant_id equals p.PLANT_ID
                //         join nt in _scifferContext.REF_NOTIFICATION_TYPE on n.notification_type equals nt.NOTIFICATION_ID
                //         join d in _scifferContext.ref_document_numbring on n.category_id equals d.document_numbring_id
                //         join m in _scifferContext.ref_machine on n.machine_id equals m.machine_id into j1
                //         from mm in j1.DefaultIfEmpty()
                //         join ee in _scifferContext.REF_EMPLOYEE on n.employee_id equals ee.employee_id into j2
                //         from em in j2.DefaultIfEmpty()
                //         select new ref_pm_notification_vm
                //         {
                //             category_name = d.category,
                //             doc_number = n.doc_number,
                //             employee_name = em.employee_name,
                //             notification_type = nt.NOTIFICATION_TYPE,
                //             notification_date = n.notification_date,
                //             notification_description = n.notification_description,
                //             plant_name = p.cst_number,
                //             start_date = n.start_date,
                //             end_date = n.end_date,
                //             start_time = n.start_time,
                //             end_time = n.end_time,
                //             detail_problem = n.detail_problem,
                //             problem_attachment = n.problem_attachment,
                //             detail_solution = n.detail_solution,
                //             attended_by = n.attended_by,
                //             reviewed_by = n.reviewed_by,
                //             breakdown_start_date = n.breakdown_start_date,
                //             breakdown_end_date = n.breakdown_end_date,
                //             breakdown_start_time = n.breakdown_start_time,
                //             notification_id = n.notification_id,
                //             machine_code = mm.machine_code
                //         }).OrderByDescending(x => x.notification_id).ToList();
            }
            else
            {
                query = (from n in _scifferContext.ref_pm_notification.Where(x => x.machine_id == machine_id)
                         join p in _scifferContext.REF_PLANT on n.plant_id equals p.PLANT_ID
                         join nt in _scifferContext.REF_NOTIFICATION_TYPE on n.notification_type equals nt.NOTIFICATION_ID
                         join d in _scifferContext.ref_document_numbring on n.category_id equals d.document_numbring_id
                         join m in _scifferContext.ref_machine on n.machine_id equals m.machine_id into j1
                         from mm in j1.DefaultIfEmpty()
                         join ee in _scifferContext.REF_EMPLOYEE on n.employee_id equals ee.employee_id into j2
                         from em in j2.DefaultIfEmpty()
                         select new ref_pm_notification_vm
                         {
                             category_name = d.category,
                             doc_number = n.doc_number,
                             employee_name = em.employee_name,
                             notification_type = nt.NOTIFICATION_TYPE,
                             notification_date = n.notification_date,
                             notification_description = n.notification_description,
                             plant_name = p.cst_number,
                             start_date = n.start_date,
                             end_date = n.end_date,
                             start_time = n.start_time,
                             end_time = n.end_time,
                             detail_problem = n.detail_problem,
                             problem_attachment = n.problem_attachment,
                             detail_solution = n.detail_solution,
                             attended_by = n.attended_by,
                             reviewed_by = n.reviewed_by,
                             breakdown_start_date = n.breakdown_start_date,
                             breakdown_end_date = n.breakdown_end_date,
                             breakdown_start_time = n.breakdown_start_time,
                             notification_id = n.notification_id,
                             machine_code = mm.machine_code
                         }).OrderByDescending(x => x.notification_id).ToList();
            }
            return query;
        }


        public List<ref_pm_notification_vm> get_mal_notification(int user_id)
        {
            try
            {
                user_id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                var created_by = new SqlParameter("@created_by", user_id);

                var val = _scifferContext.Database.SqlQuery<ref_pm_notification_vm>("exec get_mal_notification @created_by", created_by).ToList();

                return val;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Update(ref_pm_notification country)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                country.created_by = user;
                country.created_ts = DateTime.Now;
                _scifferContext.Entry(country).State = EntityState.Modified;
                _scifferContext.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string UpdateNotificationtStatus(int[] notifications_ids, int status)
        {
            try
            {
                foreach (var id in notifications_ids)
                {
                    ref_pm_notification rpn = _scifferContext.ref_pm_notification.Where(x => x.notification_id == id).FirstOrDefault();
                    rpn.open_close = true;
                    rpn.closed_by = user;
                    rpn.closed_ts = DateTime.Now;
                    _scifferContext.Entry(rpn).State = System.Data.Entity.EntityState.Modified;

                }
                _scifferContext.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
