using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
   public class PlantNotificationService : IPlantNotificationService
    {
        private readonly ScifferContext _scifferContext;
        public PlantNotificationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_plant_notification country)
        {
            try
            {
                int user;
                user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                country.created_by = user;
                country.created_ts = DateTime.Now;
                DateTime dte = new DateTime(0001, 1, 1);
                DateTime dte1 = new DateTime(1990, 1, 1);
                TimeSpan tt = new TimeSpan(0001, 1, 1);
                TimeSpan tt1 = new TimeSpan(1990, 1, 1);
                //if (country.end_time == tt)
                //{
                //    country.end_time = tt1;
                //}
                //if (country.breakdown_start_time == tt)
                //{
                //    country.breakdown_start_time = tt1;
                //}
                //if (country.breakdown_end_time == tt)
                //{
                //    country.breakdown_end_time = tt1;
                //}
                //if (country.end_date == dte)
                //{
                //    country.end_date = dte1;
                //}
                //if (country.breakdown_end_date == dte)
                //{
                //    country.breakdown_end_date = dte1;
                //}
                if (country.a_breakdown_end_date == dte)
                {
                    country.a_breakdown_end_date = dte1;
                }
                /*For Auto increment Doc number*/
                string current_no = "";
                string prefix_suffix = "";
                string doc_no = "";
                int ps = 0;
                if (country.plant_notification_id == 0)
                {
                    ref_document_numbring d = _scifferContext.ref_document_numbring.Where(x => x.document_numbring_id == country.category_id).FirstOrDefault();
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

                    // prefix_suffix = d.prefix_sufix;
                    // ps = d.prefix_sufix_id;
                    // if (ps == 1)
                    // {
                    //     doc_no = prefix_suffix + current_no;
                    //}
                    // else
                    // {
                    //      doc_no = current_no + prefix_suffix;
                    // }
                    _scifferContext.Entry(d).State = EntityState.Modified;
                    _scifferContext.ref_plant_notification.Add(country);
                }
                else
                {
                    _scifferContext.Entry(country).State = EntityState.Modified;
                }
                _scifferContext.SaveChanges();
                country.plant_notification_id = _scifferContext.ref_pm_notification.Max(x => x.notification_id);
            }
            catch (Exception ex)
            {
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

        public ref_plant_notification Get(int id)
        {
            var country = _scifferContext.ref_plant_notification.FirstOrDefault(c => c.plant_notification_id == id);
            return country;
        }

        public List<ref_plant_notification_vm> GetAll()
        {
            var query = (from n in _scifferContext.ref_plant_notification
                         join p in _scifferContext.REF_PLANT on n.plant_id equals p.PLANT_ID
                         join nt in _scifferContext.REF_NOTIFICATION_TYPE on n.notification_type equals nt.NOTIFICATION_ID
                         join d in _scifferContext.ref_document_numbring on n.category_id equals d.document_numbring_id
                         join m in _scifferContext.ref_machine on n.machine_id equals m.machine_id into j1
                         from mm in j1.DefaultIfEmpty()
                         select new ref_plant_notification_vm
                         {
                             category_name = d.category,
                             doc_number = n.doc_number,
                             notification_type = nt.NOTIFICATION_TYPE,
                             notification_date = n.notification_date,
                             plant_name = p.cst_number,
                             pl_breakdown_start_date = n.pl_breakdown_start_date,
                             pl_breakdown_end_date = n.pl_breakdown_end_date,
                             pl_breakdown_start_time = n.pl_breakdown_start_time,
                             pl_breakdown_end_time = n.pl_breakdown_end_time,
                             a_breakdown_start_date = n.a_breakdown_start_date,
                             a_breakdown_end_date = n.a_breakdown_end_date,
                             a_breakdown_start_time = n.a_breakdown_start_time,
                             a_breakdown_end_time = n.a_breakdown_end_time,
                             plant_notification_id = n.plant_notification_id,
                             notification_for=n.notification_for,
                             machine_code = mm.machine_code
                         }).ToList();
            return query;
        }

        public bool Update(ref_plant_notification country)
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
    }
}