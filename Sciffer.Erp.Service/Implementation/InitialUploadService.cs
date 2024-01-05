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
    public class InitialUploadService : IInitialUploadService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _GenericService;

        public InitialUploadService(ScifferContext scifferContext, IGenericService genericservice)
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
                    ImageAttachment = _GenericService.GetFilePathForImage("InitialUpload", excelFile);
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
                                if (!ds.Tables["Items"].Columns.Contains("Asset Code"))
                                {
                                    error_msg = "format Asset Code";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("Dep Area"))
                                {
                                    error_msg = "format Dep Area";
                                }

                                else if (!ds.Tables["Items"].Columns.Contains("Capitalization Date"))
                                {
                                    error_msg = "format Capitalization Date";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("Original Cost"))
                                {
                                    error_msg = "format Original Cost";
                                }
                                else if (!ds.Tables["Items"].Columns.Contains("Accumlated Depreciation"))
                                {
                                    error_msg = "format Accumlated Depreciation";
                                }

                                else if (!ds.Tables["Items"].Columns.Contains("Net value"))
                                {
                                    error_msg = "format Net value";
                                }

                                else
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            //var Asset_Code = ds.Tables[0].Rows[i]["Asset Code"].ToString().ToLower();
                                            //if (Asset_Code == "")
                                            //{
                                            //    error_msg = "blank Asset Code";
                                            //    break;
                                            //}
                                            //if (ds.Tables[0].Rows[i]["Dep Area"].ToString() == "")
                                            //{
                                            //    error_msg = "blank Dep Area";
                                            //    break;
                                            //}

                                            //var capdate = ds.Tables[0].Rows[i]["Capitalization Date"].ToString();                                          
                                            //if (capdate == "")
                                            //{
                                            //    error_msg = "blank Capitalization Date";
                                            //    break;
                                            //}


                                            //var amount = ds.Tables[0].Rows[i]["Original Cost"].ToString().ToLower();
                                            //if (amount == "")
                                            //{
                                            //    error_msg = "blank Original Cost";
                                            //    break;
                                            //}
                                            //var doc_currency = ds.Tables[0].Rows[i]["Accumlated Depreciation"].ToString().ToLower();
                                            //if (doc_currency == "")
                                            //{
                                            //    error_msg = "blank Accumlated Depreciation)";
                                            //    break;
                                            //}
                                            //var amount_local = ds.Tables[0].Rows[i]["Net value"].ToString().ToLower();
                                            //if (amount_local == "")
                                            //{
                                            //    error_msg = "blank Net value";
                                            //    break;
                                            //}                                         
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
                                        t1.TypeName = "dbo.temp_intial_upload_excel";
                                        t1.Value = ds.Tables[0];
                                        int createdby = int.Parse(HttpContext.Current.Session["user_id"].ToString());
                                        var created_by = new SqlParameter("@created_by", createdby);

                                        var val = _scifferContext.Database.SqlQuery<string>("exec Save_InitialUpload_excel1 @t1,@created_by ", t1, created_by).FirstOrDefault();
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


        public List<ref_asset_initial_data_header_vm> GetAll()
        {
            var query = (from order in _scifferContext.ref_asset_initial_data_header.Where(x => x.is_active == true).OrderByDescending(x => x.asset_initial_data_header_id)
                         join doc in _scifferContext.ref_document_numbring on order.document_category_id equals doc.document_numbring_id into doc1
                         from doc2 in doc1.DefaultIfEmpty()

                         select new ref_asset_initial_data_header_vm()
                         {
                             asset_initial_data_header_id = order.asset_initial_data_header_id,
                             category = doc2.category,
                             document_no = order.document_no,
                             posting_dates = order.posting_date.ToString()

                         }).OrderByDescending(x => x.asset_initial_data_header_id).ToList();
            return query;
        }

        public ref_asset_initial_data_header_vm Get(int id)
        {
            try
            {
                ref_asset_initial_data_header po = _scifferContext.ref_asset_initial_data_header.FirstOrDefault(c => c.asset_initial_data_header_id == id && c.is_active == true);
                Mapper.CreateMap<ref_asset_initial_data_header, ref_asset_initial_data_header_vm>();
                ref_asset_initial_data_header_vm mmv = Mapper.Map<ref_asset_initial_data_header, ref_asset_initial_data_header_vm>(po);

                var is_wdv_slm_or_block = _scifferContext.ref_asset_initial_data.Where(x => x.asset_initial_data_header_id == id && x.is_wdv_slm_or_block == true).Any();

                if (is_wdv_slm_or_block == true)
                {
                    mmv.ref_asset_initial_data_vm = (from detail in _scifferContext.ref_asset_initial_data.Where(a => a.asset_initial_data_header_id == id && a.is_active == true && a.is_wdv_slm_or_block == true)
                                                     join depm in _scifferContext.ref_asset_master_data on detail.asset_class_id equals depm.asset_master_data_id into dep12
                                                     from dep22 in dep12.DefaultIfEmpty()
                                                     join ar in _scifferContext.ref_dep_area on detail.dep_area_id equals ar.dep_area_id into ar1
                                                     from ar2 in ar1.DefaultIfEmpty()

                                                     select new ref_asset_initial_data_vm
                                                     {
                                                         asset_initial_data_header_id = detail.asset_initial_data_header_id,
                                                         asset_id = detail.asset_class_id,
                                                         asset_code = dep22.asset_master_data_code,
                                                         dep_area_id = detail.dep_area_id,
                                                         dep_area_code = ar2.dep_area_code,
                                                         historical_cost = detail.historical_cost,
                                                         acc_depriciation = detail.acc_depriciation,
                                                         net_value = detail.net_value
                                                     }
                                            ).Where(a => a.asset_initial_data_header_id == id).ToList();
                    return mmv;
                }
                else
                {
                    mmv.ref_asset_initial_data_vm = (from detail in _scifferContext.ref_asset_initial_data.Where(a => a.asset_initial_data_header_id == id && a.is_active == true && a.is_wdv_slm_or_block == false)
                                                     join depm in _scifferContext.ref_asset_class on detail.asset_class_id equals depm.asset_class_id into dep12
                                                     from dep22 in dep12.DefaultIfEmpty()
                                                         //join ar in _scifferContext.ref_dep_area on detail.dep_area_id equals ar.dep_area_id into ar1
                                                         //from ar2 in ar1.DefaultIfEmpty()

                                                     select new ref_asset_initial_data_vm
                                                     {
                                                         asset_initial_data_header_id = detail.asset_initial_data_header_id,
                                                         asset_id = detail.asset_class_id,
                                                         asset_class_des = dep22.asset_class_des,
                                                         dep_area_id = detail.dep_area_id,
                                                         //   dep_area_code = ar2.dep_area_code,
                                                         historical_cost = 0,
                                                         acc_depriciation = 0,
                                                         net_value = detail.net_value
                                                     }
                                          ).Where(a => a.asset_initial_data_header_id == id).ToList();
                    return mmv;
                }


            }
            catch (Exception ex)
            {
                ref_asset_initial_data_header_vm vm = new ref_asset_initial_data_header_vm();
                vm = null;
                return vm;
            }
        }

        public string AddExcel(List<intial_upload_excel_vm> initial_upload, bool is_based_on_wdv_slm_or_block)
        {

            try
            {

                DataTable dt1 = new DataTable();

                dt1.Columns.Add("sr_no", typeof(string));
                dt1.Columns.Add("asset_code", typeof(string));
                dt1.Columns.Add("dep_area", typeof(string));
                dt1.Columns.Add("capitalization_date", typeof(DateTime));
                dt1.Columns.Add("original_cost", typeof(decimal));
                dt1.Columns.Add("acc_depreciation", typeof(decimal));
                dt1.Columns.Add("net_value", typeof(decimal));

                dt1.Columns.Add("asset_class", typeof(string));
                dt1.Columns.Add("net_wdv_value", typeof(string));

                if (initial_upload != null)
                {
                    int row_no = 0;
                    foreach (var d in initial_upload)
                    {
                        dt1.Rows.Add(
                                      d.sr_no = row_no + 1
                                    , d.asset_code
                                    , d.dep_area
                                    , d.capitalization_date
                                    , d.original_cost
                                    , d.acc_depreciation
                                    , d.net_value
                                    , d.asset_class
                                    , d.net_wdv_value
                                  );
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_intial_upload_excel";
                t1.Value = dt1;

                var created_by1 = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_by = new SqlParameter("@created_by", created_by1);

                var is_based_on_wdv_slm_or_block_val = new SqlParameter("@is_based_on_wdv_slm_or_block", is_based_on_wdv_slm_or_block);

                var val = _scifferContext.Database.SqlQuery<string>("exec Save_InitialUpload_excel @t1, @is_based_on_wdv_slm_or_block, @created_by", t1, is_based_on_wdv_slm_or_block_val, created_by).FirstOrDefault();

                return val;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
