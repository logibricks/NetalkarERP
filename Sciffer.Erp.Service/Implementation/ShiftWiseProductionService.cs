using Sciffer.Erp.Data;
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
    public class ShiftWiseProductionService : IShiftWiseProductionService
    {

        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _GenericService;

        public ShiftWiseProductionService(ScifferContext scifferContext, IGenericService genericservice)
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
                                else if (!ds.Tables["Items"].Columns.Contains("Shift Code"))
                                {
                                    error_msg = "format Operator Code";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("Plant Code"))
                                {
                                    error_msg = "format Item Code";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("Machine Code"))
                                {
                                    error_msg = "format Machine Code";
                                }
                              
                                else if (!ds.Tables["Items"].Columns.Contains("Target Qty"))
                                {
                                    error_msg = "format target_qty";
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
                                            var sh = ds.Tables[0].Rows[i]["Shift Code"].ToString();
                                            if (sh == "")
                                            {
                                                error_msg = "blank Shift Code";
                                                break;
                                            }
                                            var plnt = ds.Tables[0].Rows[i]["Plant Code"].ToString();
                                            if (plnt == "")
                                            {
                                                error_msg = "blank Plant Code";
                                                break;
                                            }

                                            var mac = ds.Tables[0].Rows[i]["Machine Code"].ToString().ToLower();
                                            if (mac == "")
                                            {
                                                error_msg = "blank Machine Code";
                                                break;
                                            }

                                            var qty = ds.Tables[0].Rows[i]["Target Qty"].ToString().ToLower();
                                            if (qty == "")
                                            {
                                                error_msg = "blank Target Qty";
                                                break;
                                            }

                                            try
                                            {
                                                ds.Tables["Items"].PrimaryKey = new DataColumn[] { ds.Tables["Items"].Columns["Date"], ds.Tables["Items"].Columns["Shift Code"], ds.Tables["Items"].Columns["Plant Code"], ds.Tables["Items"].Columns["Machine Code"]};

                                            }
                                            catch (Exception ex)
                                            {
                                                error_msg = pd + "," + plnt + "," + mac + "," + sh + " already exists.";
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
                                        t1.TypeName = "dbo.temp_shiftwiseprod_excel";
                                        t1.Value = ds.Tables[0];
                                        int createdby = int.Parse(HttpContext.Current.Session["user_id"].ToString());
                                        var created_by = new SqlParameter("@created_by", createdby);

                                        var val = _scifferContext.Database.SqlQuery<string>("exec Save_ShiftWiseProd_excel @t1,@created_by ", t1, created_by).FirstOrDefault();
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

        public List<shift_wise_prod_detail_vm> GetAll()

        {
            var query = _scifferContext.Database.SqlQuery<shift_wise_prod_detail_vm>(
                "exec get_shift_wise_prod_index_dates").ToList();
            return query;
        }

        public List<shift_wise_prod_detail_vm> Get(DateTime prod_date)
        {
            var prod_date1 = new SqlParameter("@prod_date", prod_date);

            var val = _scifferContext.Database.SqlQuery<shift_wise_prod_detail_vm>(
            "exec GetShiftWiseProdDetail @prod_date", prod_date1).ToList();
            return val;


        }

        public string Add(List<shift_wise_prod_detail_vm> DepParaArr)
        {
            try
            {

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("shift_wise_production_detail_id", typeof(int));
                dt1.Columns.Add("prod_dates", typeof(string));
                dt1.Columns.Add("machine_id", typeof(int));
                dt1.Columns.Add("plant_id", typeof(int));
                dt1.Columns.Add("target_qty", typeof(decimal));
                dt1.Columns.Add("shift_id", typeof(int));               
                dt1.Columns.Add("is_active", typeof(bool));


                if (DepParaArr != null)
                {
                    foreach (var d in DepParaArr)
                    {
                        dt1.Rows.Add(d.shift_wise_production_detail_id, d.prod_dates, d.machine_id, d.plant_id, d.target_qty, d.shift_id, d.is_active);
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_shift_wise_prod_update_detail";
                t1.Value = dt1;

                var val = _scifferContext.Database.SqlQuery<string>("exec save_shift_wise_prod @t1 ", t1).FirstOrDefault();

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

    }
}
