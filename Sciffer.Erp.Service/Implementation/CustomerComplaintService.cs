using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class CustomerComplaintService : ICustomerComplaintService
    {
        private readonly ScifferContext _scifferContext;
        public CustomerComplaintService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_customer_complaint cc)
        {
            try
            {
                cc.created_ts = DateTime.Now;
                cc.created_by= int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                _scifferContext.ref_customer_complaint.Add(cc);
                _scifferContext.SaveChanges();
                return "Saved";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                GC.Collect();
            }
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

        public ref_customer_complaint Get(int id)
        {
            try
            {
                return _scifferContext.ref_customer_complaint.Where(x => x.customer_complaint_id == id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ref_customer_complaint> GetAll()
        {
            return _scifferContext.ref_customer_complaint.ToList();
        }

        public string Update(ref_customer_complaint cc)
        {
            try
            {
                ref_customer_complaint dd = _scifferContext.ref_customer_complaint.Where(x => x.customer_complaint_id == cc.customer_complaint_id).FirstOrDefault();

                dd.capa_no_submission_date = cc.capa_no_submission_date;
                dd.change_note = cc.change_note;
                dd.complaint_date = cc.complaint_date;
                dd.complaint_details = cc.complaint_details;
                dd.complaint_no = cc.complaint_no;
                dd.complaint_receipt_date = cc.complaint_receipt_date;
                dd.complaint_received_from = cc.complaint_received_from;
                dd.complaint_type = cc.complaint_type;
                dd.containment_action = cc.containment_action;
                dd.corrective_action = cc.corrective_action;
                dd.cp = cc.cp;
                dd.customer_id = cc.customer_id;
                dd.fmea = cc.fmea;
                dd.item_id = cc.item_id;
                dd.lir = cc.lir;
                dd.modified_ts = DateTime.Now;
                dd.modified_id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                dd.month1 = cc.month1;
                dd.month2 = cc.month2;
                dd.month3 = cc.month3;
                dd.npt_customer_account_holder = cc.npt_customer_account_holder;
                dd.oir = cc.oir;
                dd.others_pm_gauge_poka_yoke = cc.others_pm_gauge_poka_yoke;
                dd.pfc = cc.pfc;
                dd.preventive_action = cc.preventive_action;
                dd.ptdb = cc.ptdb;
                dd.quantity_affected = cc.quantity_affected;
                dd.reported_first_repeated = cc.reported_first_repeated;
                dd.root_cause_at_occurrence = cc.root_cause_at_occurrence;
                dd.root_cause_for_detection = cc.root_cause_for_detection;
                dd.start_up_check_sheet = cc.start_up_check_sheet;
                dd.status = cc.status;
                dd.tag_no = cc.tag_no;
                dd.wi_sop = cc.wi_sop;
                _scifferContext.Entry(dd).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return "Saved";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
