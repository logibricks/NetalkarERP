using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class GSTTdsCodeService : IGSTTdsCodeService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        public GSTTdsCodeService(ScifferContext ScifferContext)
        {
            _scifferContext = ScifferContext;
        }
        public string Add(ref_gst_tds_code_vm TDSCode)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("tds_code_detail_id", typeof(int));
                dt.Columns.Add("effective_from", typeof(DateTime));
                dt.Columns.Add("rate", typeof(double));
                if (TDSCode.tds_code_detail_id != null)
                {
                    for (var i = 0; i < TDSCode.tds_code_detail_id.Count; i++)
                    {
                        dt.Rows.Add(int.Parse(TDSCode.tds_code_detail_id[i]), DateTime.Parse(TDSCode.effective_from[i]), double.Parse(TDSCode.rate[i]));
                    }
                }
              
                int modifyuser = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var entity = new SqlParameter("@entity", "save");
                var tds_code_id = new SqlParameter("@gst_tds_code_id", TDSCode.gst_tds_code_id);
                var tds_code = new SqlParameter("@gst_tds_code", TDSCode.gst_tds_code);
                var tds_code_description = new SqlParameter("@gst_tds_code_description", TDSCode.gst_tds_code_description);
                var creditor_gl = new SqlParameter("@creditor_gl", TDSCode.creditor_gl);
                var debtor_gl = new SqlParameter("@debtor_gl", TDSCode.debtor_gl);
                var is_blocked = new SqlParameter("@is_blocked", TDSCode.is_blocked==null?false:TDSCode.is_blocked);
                var modify_user = new SqlParameter("@modify_user", modifyuser);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_tds_code_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>
                ("exec save_gst_tds_code @entity,@gst_tds_code_id ,@gst_tds_code ,@gst_tds_code_description ,@creditor_gl ,@debtor_gl ,@is_blocked,@modify_user,@t1 ",
                entity, tds_code_id, tds_code, tds_code_description, creditor_gl, debtor_gl, is_blocked, modify_user, t1).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "error";
            }
        }

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
        public ref_gst_tds_code_vm Get(int id)
        {
            try
            {
                ref_gst_tds_code JR = _scifferContext.ref_gst_tds_code.FirstOrDefault(c => c.gst_tds_code_id == id);
                Mapper.CreateMap<ref_gst_tds_code, ref_gst_tds_code_vm>();
                ref_gst_tds_code_vm JRVM = Mapper.Map<ref_gst_tds_code, ref_gst_tds_code_vm>(JR);
                if(JRVM.ref_gst_tds_code_detail!=null)
                {
                    JRVM.ref_gst_tds_code_detail = JRVM.ref_gst_tds_code_detail.Where(c => c.is_active == true).ToList();
                }
                return JRVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ref_gst_tds_code_vm> GetAll()
        {
            var query = (from tx in _scifferContext.ref_gst_tds_code
                         join cr in _scifferContext.ref_general_ledger on tx.creditor_gl equals cr.gl_ledger_id
                         join dr in _scifferContext.ref_general_ledger on tx.debtor_gl equals dr.gl_ledger_id
                         select new ref_gst_tds_code_vm()
                         {
                             gst_tds_code_id = tx.gst_tds_code_id,
                             gst_tds_code = tx.gst_tds_code,
                             gst_tds_code_description = tx.gst_tds_code_description,
                             creditor_gl_name = cr.gl_ledger_code + "-" + cr.gl_ledger_name,
                             debtor_gl_name = dr.gl_ledger_code + "-" + dr.gl_ledger_name,
                             is_blocked = (bool)tx.is_blocked,
                         }).OrderByDescending(a => a.gst_tds_code_id).ToList();
            return query;
        }
    }
}
