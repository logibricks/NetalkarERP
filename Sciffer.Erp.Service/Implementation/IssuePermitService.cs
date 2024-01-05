using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class IssuePermitService : IIssuePermitService
    {
        private readonly ScifferContext _scifferContext;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       


        public IssuePermitService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(ref_permit_issue_VM country)
        {
            try
            {
                //DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("permit_detail_id ", typeof(int));
                dt.Columns.Add("permit_template_id", typeof(int));
                //dt.Columns.Add("permit_category", typeof(int));
                dt.Columns.Add("checkpoints  ", typeof(string));
                dt.Columns.Add("ideal_scenario ", typeof(string));
                dt.Columns.Add("check_point_id ", typeof(int));
               
                if (country.permit_detail_id != null)
                {
                    for (int i = 0; i < country.permit_detail_id.Count; i++)
                    {
                        if (country.permit_detail_id[i] != "")
                        {
                            dt.Rows.Add(country.permit_detail_id[i] == "0" ? -1 : int.Parse(country.permit_detail_id[i]),
                                 country.permit_template_id[i] == "" ? 0 : int.Parse(country.permit_template_id[i]),
                               // country.permit_category[i] == "" ? 0 : int.Parse(country.permit_category[i]),
                                country.checkpoints[i],
                                country.ideal_scenario[i] ,
                                country.check_point_id[i] == "" ? 0 : int.Parse(country.check_point_id[i])
                               
                                );
                        }
                    }
                }
                // int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var createdts = DateTime.Now;
                var permit_id = new SqlParameter("@permit_id", country.permit_id==0?-1:country.permit_id);
                var permit_no = new SqlParameter("@permit_no", country.permit_no == null ? string.Empty : country.permit_no);
                var category_id = new SqlParameter("@category_id", country.category_id);
                var permit_date = new SqlParameter("@permit_date", country.permit_date);
               
               // var permit_category = new SqlParameter("@permit_category", country.permit_category);

                var valid_from = new SqlParameter("@valid_from", country.valid_from);
                var valid_to = new SqlParameter("@valid_to", country.valid_to);
                var no_of_workers = new SqlParameter("@no_of_workers", country.no_of_workers);
                var work_description = new SqlParameter("@work_description", country.work_description);
                var work_location = new SqlParameter("@work_location", country.work_location);
                var vender_id = new SqlParameter("@vender_id", country.vender_id);
                var plant_id = new SqlParameter("@plant_id", country.plant_id);
                //var permit_template_id = new SqlParameter("@permit_template_id", country.permit_template_id == -1 ? 0: country.permit_template_id);
                var created_by = new SqlParameter("@created_by", createdby);
                var created_ts = new SqlParameter("@created_ts", createdts);
                var deleteids = new SqlParameter("@deleteids", country.deleteids == null ? "" : country.deleteids);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_ref_permit_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_issue_permit @permit_id,@permit_no ,@category_id ,@permit_date ,@valid_from ,@valid_to ,@no_of_workers ,@work_description ,@work_location ,@vender_id ,@plant_id,@deleteids ,@created_by,@created_ts,@t1 ",
                    permit_id, permit_no, category_id, permit_date,  valid_from, valid_to, no_of_workers, work_description,
                    work_location, vender_id, plant_id, deleteids,  created_by, created_ts,
                     t1).FirstOrDefault();
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
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public bool Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public ref_permit_issue_VM Get(int id)
        {
            try
            {
                ref_permit_issue JR = _scifferContext.ref_permit_issue.FirstOrDefault(c => c.permit_id == id);
                Mapper.CreateMap<ref_permit_issue, ref_permit_issue_VM>();
                ref_permit_issue_VM JRVM = Mapper.Map<ref_permit_issue, ref_permit_issue_VM>(JR);
                JRVM.ref_permit_details = JRVM.ref_permit_details.Where(x => x.is_blocked == true).ToList();
                // JRVM.gl_ledger_code = JR.ref_general_ledger.gl_ledger_code;
                // JRVM.gl_ledger_name = JR.ref_general_ledger.gl_ledger_name;
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return country;
        }

        public List<ref_permit_issue_VM> GetAll()
        {
            var vax = (from ed in _scifferContext.ref_permit_issue.Where(x => x.is_blocked == true)
                       join cat in _scifferContext.ref_document_numbring on ed.category_id equals cat.document_numbring_id
                       select new ref_permit_issue_VM
                       {
                           permit_id = ed.permit_id,
                           permit_no = ed.permit_no,
                           category_id = ed.category_id,
                           permit_date = ed.permit_date,                           
                           //permit_category =ed.permit_category,
                           valid_from =ed.valid_from,
                           valid_to=ed.valid_to,
                           work_description = ed.work_description,
                           work_location = ed.work_location,
                           vender_id=ed.vender_id,
                           plant_id=ed.plant_id,
                           no_of_workers =ed.no_of_workers,
                           category_name = cat.category,
                       }).OrderByDescending(x=>x.permit_id).ToList();
            return vax;
        }

        public string Update(ref_permit_issue_VM country)
        {
            try
            {
                //DateTime dte = new DateTime(1990, 1, 1);
                DataTable dt = new DataTable();
                dt.Columns.Add("permit_detail_id ", typeof(int));
                dt.Columns.Add("permit_template_id", typeof(int));
               // dt.Columns.Add("permit_category", typeof(int));
                dt.Columns.Add("checkpoints  ", typeof(string));
                dt.Columns.Add("ideal_scenario ", typeof(string));
                dt.Columns.Add("check_point_id ", typeof(int));

                if (country.permit_detail_id != null)
                {
                    for (int i = 0; i < country.permit_detail_id.Count; i++)
                    {
                        if (country.permit_detail_id[i] != "")
                        {
                            dt.Rows.Add(country.permit_detail_id[i] == "0" ? -1 : int.Parse(country.permit_detail_id[i]),
                               country.permit_template_id[i] == "" ? 0 : int.Parse(country.permit_template_id[i]),
                                //country.permit_category[i] == "" ? 0 : int.Parse(country.permit_category[i]),
                                country.checkpoints[i],
                                country.ideal_scenario[i],
                                country.check_point_id[i] == "" ? 0 : int.Parse(country.check_point_id[i])

                                );
                        }
                    }
                }
                // int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var createdts = DateTime.Now;
                var permit_id = new SqlParameter("@permit_id", country.permit_id == 0 ? -1 : country.permit_id);
                var permit_no = new SqlParameter("@permit_no", country.permit_no == null ? string.Empty : country.permit_no);
                var category_id = new SqlParameter("@category_id", country.category_id);
                var permit_date = new SqlParameter("@permit_date", country.permit_date);
               // var permit_category = new SqlParameter("@permit_category", country.permit_category);
                var valid_from = new SqlParameter("@valid_from", country.valid_from);
                var valid_to = new SqlParameter("@valid_to", country.valid_to);
                var no_of_workers = new SqlParameter("@no_of_workers", country.no_of_workers);
                var work_description = new SqlParameter("@work_description", country.work_description);
                var work_location = new SqlParameter("@work_location", country.work_location);
                var vender_id = new SqlParameter("@vender_id", country.vender_id);
                var plant_id = new SqlParameter("@plant_id", country.plant_id);
                //var permit_template_id = new SqlParameter("@permit_template_id", country.permit_template_id == 0 ? -1 : country.permit_template_id);
                var created_by = new SqlParameter("@created_by", createdby);
                var created_ts = new SqlParameter("@created_ts", createdts);
                var deleteids = new SqlParameter("@deleteids", country.deleteids == null ? "" : country.deleteids);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_ref_permit_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_issue_permit @permit_id,@permit_no ,@category_id ,@permit_date ,@valid_from ,@valid_to ,@no_of_workers ,@work_description ,@work_location ,@vender_id ,@plant_id, @deleteids,@created_by,@created_ts,@t1 ",
                    permit_id, permit_no, category_id, permit_date,valid_from, valid_to, no_of_workers, work_description, work_location, vender_id, plant_id, deleteids, created_by, created_ts,t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    return val;
                }
                else
                {
                    return val;
                }
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
        }


       //#region dispoable methods
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

    }
}
