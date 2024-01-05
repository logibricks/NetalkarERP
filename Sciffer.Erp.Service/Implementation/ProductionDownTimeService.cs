using AutoMapper;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class ProductionDownTimeService : IProductionDownTimeService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _GenericService;

        public ProductionDownTimeService(ScifferContext scifferContext, IGenericService genericservice)
        {
            _scifferContext = scifferContext;
            _GenericService = genericservice;
        }


        public string AddExcel(HttpPostedFileBase excelFile)
        {
            try
            {
                DataSet ds = new DataSet();
                string error_msg = "";
                var ImageAttachment = "";
                if (excelFile != null)
                {
                    ImageAttachment = _GenericService.GetFilePathForImage("ProductionPlan", excelFile);
                    string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ImageAttachment + ";Extended Properties=Excel 12.0;";
                    using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    {
                        conn.Open();
                        using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                        {
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            string query = "SELECT * FROM [" + sheetName + "]";
                            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                            adapter.Fill(ds, "Items");
                            if (ds.Tables.Count != 0)
                            {

                                if (!ds.Tables["Items"].Columns.Contains("Date"))
                                {
                                    error_msg = "format Date";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("Machine Code"))
                                {
                                    error_msg = "format Machine Code";
                                }

                                else if (!ds.Tables["Items"].Columns.Contains("Item Code"))
                                {
                                    error_msg = "format Item Code";
                                }

                                else if (!ds.Tables["Items"].Columns.Contains("Shift Code"))
                                {
                                    error_msg = "format Operator Code";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("M/C BREAKDOWN"))
                                {
                                    error_msg = "format M/C BREAKDOWN";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("PM"))
                                {
                                    error_msg = "format PM";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("NO POWER"))
                                {
                                    error_msg = "format NO POWER";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("NO OPERATOR"))
                                {
                                    error_msg = "format NO OPERATOR";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("NO LOAD"))
                                {
                                    error_msg = "format NO LOAD";

                                }
                                else if (!ds.Tables["Items"].Columns.Contains("SETUP"))
                                {
                                    error_msg = "format SETUP";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("RESTART"))
                                {
                                    error_msg = "format RESTART";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("TOOL CHANGE"))
                                {
                                    error_msg = "format TOOL CHANGE";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("QUALITY CHECK"))
                                {
                                    error_msg = "format QUALITY CHECK";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("NO PLAN"))
                                {
                                    error_msg = "format NO PLAN";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("TRAINING"))
                                {
                                    error_msg = "format TRAINING";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("JH"))
                                {
                                    error_msg = "format JH";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("REMARKS"))
                                {
                                    error_msg = "format REMARKS";
                                }


                                else
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            var Date = ds.Tables[0].Rows[i]["Date"].ToString().ToLower();
                                            string pd = DateTime.Parse((Date.ToString())).ToShortDateString();
                                            if (Date == "")
                                            {
                                                error_msg = "blank Date";
                                                break;
                                            }
                                            var mac = ds.Tables[0].Rows[i]["Machine Code"].ToString().ToLower();
                                            if (mac == "")
                                            {
                                                error_msg = "blank Machine Code";
                                                break;
                                            }

                                            var item = ds.Tables[0].Rows[i]["Item Code"].ToString();
                                            if (item == "")
                                            {
                                                error_msg = "blank Item Code";
                                                break;
                                            }

                                            var sh = ds.Tables[0].Rows[i]["Shift Code"].ToString();
                                            if (sh == "")
                                            {
                                                error_msg = "blank Shift Code";
                                                break;
                                            }

                                            try
                                            {
                                                ds.Tables["Items"].PrimaryKey = new DataColumn[] { ds.Tables["Items"].Columns["Date"], ds.Tables["Items"].Columns["Machine Code"], ds.Tables["Items"].Columns["Item Code"], ds.Tables["Items"].Columns["Shift Code"] };

                                            }
                                            catch (Exception ex)
                                            {
                                                error_msg = pd + "," + item + "," + mac + "," + sh + " already exists.";
                                            }

                                        }
                                    }

                                    else
                                    {
                                        error_msg = "norows";
                                        return error_msg;
                                    }

                                    if (error_msg == "")
                                    {
                                        var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                                        t1.TypeName = "dbo.temp_prod_downtime_excel";
                                        t1.Value = ds.Tables[0];
                                        int createdby = int.Parse(HttpContext.Current.Session["user_id"].ToString());
                                        var created_by = new SqlParameter("@created_by", createdby);

                                        var val = _scifferContext.Database.SqlQuery<string>("exec Save_ProductionDownTime_excel @t1,@created_by ", t1, created_by).FirstOrDefault();
                                        error_msg = val;

                                    }
                                }
                            }
                        }
                    }

                }
                return error_msg;
            }
            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
            }
        }
     

        public string Add(List<prod_downtime_vm> DepParaArr)
         {
            try
            {

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("prod_downtime_id", typeof(int));
                dt1.Columns.Add("prod_plan_detail_id", typeof(int));
                dt1.Columns.Add("mac_breakdown", typeof(decimal));
                dt1.Columns.Add("pm", typeof(decimal));
                dt1.Columns.Add("no_power", typeof(decimal));
                dt1.Columns.Add("no_operator", typeof(decimal));
                dt1.Columns.Add("no_load", typeof(decimal));
                dt1.Columns.Add("setup", typeof(decimal));
                dt1.Columns.Add("restart", typeof(decimal));
                dt1.Columns.Add("tool_change", typeof(decimal));
                dt1.Columns.Add("quality_check", typeof(decimal));
                dt1.Columns.Add("no_plan", typeof(decimal));
                dt1.Columns.Add("training", typeof(decimal));
                dt1.Columns.Add("jh", typeof(decimal));
                dt1.Columns.Add("remarks", typeof(decimal));
               


                if (DepParaArr != null)
                {
                    foreach (var d in DepParaArr)
                    {
                        dt1.Rows.Add(d.prod_downtime_id,d.prod_plan_detail_id, d.mac_breakdown == null ? 0 : d.mac_breakdown, d.pm == null ? 0 : d.pm, d.no_power == null ? 0 : d.no_power,
                            d.no_operator == null ? 0 : d.no_operator, d.no_load == null ? 0 : d.no_load, d.setup == null ? 0 : d.setup, d.restart == null ? 0 : d.restart, d.tool_change == null ? 0 : d.tool_change, 
                            d.quality_check == null ? 0 : d.quality_check, d.no_plan == null ? 0 : d.no_plan, d.training == null ? 0 : d.training, d.jh == null ? 0 : d.jh, d.remarks == null ? 0 : d.remarks);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_prod_downtime_detail";
                t1.Value = dt1;
                int createdby = int.Parse(HttpContext.Current.Session["user_id"].ToString());
                var created_by = new SqlParameter("@created_by", createdby);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_prod_downtime @t1,@created_by ", t1, created_by).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
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


        public List<prod_downtime_vm> Get(DateTime prod_date)
        {
            var prod_date1 = new SqlParameter("@prod_date", prod_date);

            var val = _scifferContext.Database.SqlQuery<prod_downtime_vm>(
            "exec GetProductionDownTimeDetail @prod_date", prod_date1).ToList();
            return val;


        }

        public List<prod_downtime_vm> GetAll()

        {
            var query = _scifferContext.Database.SqlQuery<prod_downtime_vm>(
                "exec get_prod_downtime_index_dates").ToList();
            return query;
        }
    }
       
}
