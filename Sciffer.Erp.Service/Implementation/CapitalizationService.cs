using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class CapitalizationService : ICapitalizationService
    {
        private readonly ScifferContext _scifferContext;

        public CapitalizationService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public string Add(fin_ledger_capitalization_vm Capdata, List<fin_ledger_capitalization_detail_vm> DepParaArr)
        {
            try
            {

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("fin_ledger_capitalization_detail_id", typeof(int));
                dt1.Columns.Add("fin_ledger_detail_id", typeof(int));
                dt1.Columns.Add("amount_local", typeof(decimal));
                dt1.Columns.Add("gl_ledger_id", typeof(int));
                dt1.Columns.Add("sub_ledger_id", typeof(int));


                if (DepParaArr != null)
                {
                    foreach (var d in DepParaArr)
                    {
                        dt1.Rows.Add(d.fin_ledger_capitalization_detail_id == null ? 0 : d.fin_ledger_capitalization_detail_id, d.fin_ledger_detail_id, d.amount_local, d.gl_ledger_id, d.sub_ledger_id);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_fin_ledger_capitalization_details";
                t1.Value = dt1;

                DateTime dte = new DateTime(0001, 01, 01);
                DateTime dte1 = new DateTime(1990, 01, 01);
                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var fin_ledger_capitalization_id = new SqlParameter("@fin_ledger_capitalization_id", Capdata.fin_ledger_capitalization_id == null ? 0 : Capdata.fin_ledger_capitalization_id);
                var document_numbering_id = new SqlParameter("@document_numbering_id", Capdata.document_numbering_id == null ? 0 : Capdata.document_numbering_id);
                var posting_date = new SqlParameter("@posting_date", Capdata.posting_date == null ? dte1 : Capdata.posting_date == dte ? dte1 : Capdata.posting_date);
                var asset_code_id = new SqlParameter("@asset_code_id", Capdata.asset_code_id == null ? 0 : Capdata.asset_code_id);
                var capitalization_date = new SqlParameter("@capitalization_date", Capdata.capitalization_date == null ? dte1 : Capdata.capitalization_date == dte ? dte1 : Capdata.capitalization_date);
                var gl_ledger_id = new SqlParameter("@gl_ledger_id", Capdata.gl_ledger_id == null ? 0 : Capdata.gl_ledger_id);
                var created_by = new SqlParameter("@created_by", created_by1);
                var is_active = new SqlParameter("@is_active", Capdata.is_active == true ? true : false);

                var val = _scifferContext.Database.SqlQuery<string>("exec save_fin_ledger_capitalization @fin_ledger_capitalization_id ,@document_numbering_id ,@posting_date ,@asset_code_id ,@capitalization_date ,@gl_ledger_id,@created_by , @is_active,@t1 ", fin_ledger_capitalization_id, document_numbering_id, posting_date, asset_code_id, capitalization_date, gl_ledger_id, created_by, is_active, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
                    return val;
                }
                else if (val.Contains("Duplicate"))
                {
                    var sp = val.Split('~')[1];
                    return val;
                }
                else
                {
                    return "Error " + val;
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
            }
        }


        public List<fin_ledger_capitalization_vm> GetAll()
        {
            var query = (from order in _scifferContext.fin_ledger_capitalization.Where(x => x.is_active == true).OrderByDescending(x => x.fin_ledger_capitalization_id)
                         join doc in _scifferContext.ref_document_numbring on order.document_numbering_id equals doc.document_numbring_id into mach1
                         from mach2 in mach1.DefaultIfEmpty()
                         join assetcode in _scifferContext.ref_asset_master_data on order.asset_code_id equals assetcode.asset_master_data_id into assetcode1
                         from assetcode2 in assetcode1.DefaultIfEmpty()
                         join gl in _scifferContext.ref_general_ledger on order.gl_ledger_id equals gl.gl_ledger_id into gl1
                         from gl2 in gl1.DefaultIfEmpty()

                         select new fin_ledger_capitalization_vm()
                         {
                             fin_ledger_capitalization_id = order.fin_ledger_capitalization_id,
                             category = mach2.category,
                             document_no = order.document_no,
                             posting_date = order.posting_date,
                             asset_code_id = order.asset_code_id,
                             asset_code = assetcode2.asset_master_data_code,
                             capitalization_date = order.capitalization_date,
                             gl_ledger_code = gl2.gl_ledger_code + "/" + gl2.gl_ledger_name,

                         }).OrderByDescending(x => x.fin_ledger_capitalization_id).ToList();
            return query;
        }

        public fin_ledger_capitalization_vm Get(int id)
        {

            try
            {
                var val = _scifferContext.Database.SqlQuery<fin_ledger_capitalization_detail_vm>("exec GetCapitalizationeDetailById").Where(a => a.fin_ledger_capitalization_id == id).ToList();
                fin_ledger_capitalization po = _scifferContext.fin_ledger_capitalization.FirstOrDefault(c => c.fin_ledger_capitalization_id == id && c.is_active == true);
                Mapper.CreateMap<fin_ledger_capitalization, fin_ledger_capitalization_vm>();
                fin_ledger_capitalization_vm mmv = Mapper.Map<fin_ledger_capitalization, fin_ledger_capitalization_vm>(po);
                mmv.fin_ledger_capitalization_detail_vm = val;
                return mmv;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string Delete(int id, string cancellation_remarks)
        {
            try
            {
                int create_user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var fin_ledger_capitalization_id = new SqlParameter("@fin_ledger_capitalization_id", id);
                var remarks = new SqlParameter("@cancellation_remarks", cancellation_remarks == null ? "" : cancellation_remarks);
                var created_by = new SqlParameter("@created_by", create_user);
                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec cancel_fin_ledger_capitalization @fin_ledger_capitalization_id ,@cancellation_remarks ,@created_by", fin_ledger_capitalization_id, remarks, created_by).FirstOrDefault();
                return val;
            }
            catch (Exception ex)
            {
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
            }
        }

    }
}
