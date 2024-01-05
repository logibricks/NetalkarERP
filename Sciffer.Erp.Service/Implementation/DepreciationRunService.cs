using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
  public  class DepreciationRunService : IDepreciationRunService
    {

        private readonly ScifferContext _scifferContext;

        public DepreciationRunService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public string Add(ref_depreciation_run_vm deprun)
        {
            try
            {

                DateTime dte = new DateTime(0001, 01, 01);
                DateTime dte1 = new DateTime(1990, 01, 01);
                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var depreciation_run_id = new SqlParameter("@depreciation_run_id", deprun.depreciation_run_id == null ? 0 : deprun.depreciation_run_id);
                var document_numbering_id = new SqlParameter("@document_numbering_id", deprun.document_numbering_id == null ? 0 : deprun.document_numbering_id);
                var dep_area_id = new SqlParameter("@dep_area_id", deprun.dep_area_id == null ? 0 : deprun.dep_area_id);               
                var dep_financial_year_id = new SqlParameter("@dep_financial_year_id", deprun.dep_financial_year_id == null ? 0 : deprun.dep_financial_year_id);                
                var current_dep_till_date = new SqlParameter("@current_dep_till_date", deprun.current_dep_till_date == null ? dte1 :  deprun.current_dep_till_date == dte ? dte1 : deprun.current_dep_till_date);
                var asset_class_id = new SqlParameter("@asset_class_id", deprun.asset_class_id == null ? 0 : deprun.asset_class_id);
                var created_by = new SqlParameter("@created_by", created_by1);
                var is_active = new SqlParameter("@is_active", deprun.is_active == false ? true : false);

                var val = _scifferContext.Database.SqlQuery<string>("exec save_depreciation_run @depreciation_run_id ,@document_numbering_id ,@asset_class_id ,@dep_area_id ,@dep_financial_year_id ,@current_dep_till_date,@created_by ,@is_active ", depreciation_run_id, document_numbering_id, asset_class_id, dep_area_id, dep_financial_year_id, current_dep_till_date, created_by, is_active).FirstOrDefault();

                if (val.Contains("Saved"))
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
                return ex.InnerException.InnerException.Message;
            }
        }
    }
}
