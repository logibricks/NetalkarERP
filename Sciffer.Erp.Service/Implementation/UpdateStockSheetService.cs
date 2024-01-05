using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using System.Data.SqlClient;
using System.Web;
using System.Data;
using System.Data.OleDb;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class UpdateStockSheetService : IUpdateStockSheetService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _generic;
        public UpdateStockSheetService(ScifferContext scifferContext, IGenericService generic)
        {
            _scifferContext = scifferContext;
            _generic = generic;
        }

        public string Add(update_stock_count_vm vm)
        {
            var ImageAttachment = "";
            try
            {
                DataSet ds = new DataSet();
                string error_msg = "";

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Item", typeof(string));
                dt.Columns.Add("UOM", typeof(string));
                dt.Columns.Add("Batch", typeof(string));
                dt.Columns.Add("Quantity", typeof(double));

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_update_stock_count_detail";

                if (vm.file != null)
                {
                    var file = vm.file.FileName.Split('.')[0];
                    ImageAttachment = _generic.GetFilePathForImage("UpdateStockSheet", vm.file, file);
                    string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ImageAttachment + ";Extended Properties=Excel 12.0;";
                    using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    {
                        conn.Open();
                        using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                        {
                            for(var i=0; i< dtExcelSchema.Rows.Count; i++)
                            {
                                string sheetName1 = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                                string query1 = "SELECT * FROM [" + sheetName1 + "]";
                               
                                if (sheetName1== "'Stock Quantity Greater than Zer$'")
                                {
                                    OleDbDataAdapter adapter1 = new OleDbDataAdapter(query1, conn);
                                    adapter1.Fill(ds, "Items1");
                                }
                                else 
                                if (sheetName1 == "'No Transaction$'")
                                {
                                    OleDbDataAdapter adapter2 = new OleDbDataAdapter(query1, conn);
                                    adapter2.Fill(ds, "Items2");
                                }
                                else
                                if (sheetName1 == "'Stock Quantity Equals to zero$'")                                
                                {
                                    OleDbDataAdapter adapter3 = new OleDbDataAdapter(query1, conn);
                                    adapter3.Fill(ds, "Items3");
                                }
                            }

                            //string sheetName1 = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
                            //string query1 = "SELECT * FROM [" + sheetName1 + "]";
                            //OleDbDataAdapter adapter1 = new OleDbDataAdapter(query1, conn);
                            //adapter1.Fill(ds, "Items1");

                            //string sheetName2 = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
                            //string query2 = "SELECT * FROM [" + sheetName2 + "]";
                            //OleDbDataAdapter adapter2 = new OleDbDataAdapter(query2, conn);
                            //adapter2.Fill(ds, "Items2");

                            //string sheetName3 = dtExcelSchema.Rows[3]["TABLE_NAME"].ToString();
                            //string query3 = "SELECT * FROM [" + sheetName3 + "]";
                            //OleDbDataAdapter adapter3 = new OleDbDataAdapter(query3, conn);
                            //adapter3.Fill(ds, "Items3");

                            //ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns[2], ds.Tables[0].Columns[3], ds.Tables[0].Columns[5], ds.Tables[0].Columns[6] };

                            if (ds.Tables.Count != 0)
                            {
                                //if (!ds.Tables["Items"].Columns.Contains("Item"))
                                //{
                                //    error_msg = "Item column in not proper format";
                                //}
                                //if (!ds.Tables["Items"].Columns.Contains("UOM"))
                                //{
                                //    error_msg = "UoM column in not proper format";
                                //}
                                //if (!ds.Tables["Items"].Columns.Contains("Batch"))
                                //{
                                //    error_msg = "Batch column in not proper format";
                                //}
                                //if (!ds.Tables["Items"].Columns.Contains("Quantity"))
                                //{
                                //    error_msg = "Quantity in not proper format";
                                //}
                                if (error_msg == "")
                                {
                                    for(var i=0;i< ds.Tables.Count; i++)
                                    {
                                        foreach (DataRow row in ds.Tables[i].Rows)
                                        {
                                            string itemcodename = row["Item"].ToString()==null?"": row["Item"].ToString();
                                            var item_code = itemcodename.Split('/')[0];
                                            var item = _scifferContext.REF_ITEM.Where(x => x.ITEM_CODE == item_code).FirstOrDefault();
                                            if (item == null)
                                            {
                                                error_msg = "Item " + itemcodename + " is not available in database !";
                                                break;
                                            }
                                            string uomcode = row["UoM"].ToString();
                                            var uom = _scifferContext.REF_UOM.Where(x => x.UOM_NAME == uomcode).FirstOrDefault();
                                            if (uom == null)
                                            {
                                                error_msg = "UoM " + uomcode + " is not available in database !";
                                                break;
                                            }
                                            decimal actual_qty = row["Quantity"].ToString() == "" ? 0 : decimal.Parse(row["Quantity"].ToString());
                                            DataRow dr = dt.NewRow();
                                            dr["ID"] = 0;
                                            dr["Item"] = item.ITEM_ID;
                                            dr["UoM"] = uom.UOM_ID;
                                            dr["Batch"] = row["Batch"];
                                            dr["Quantity"] = actual_qty;
                                            dt.Rows.Add(dr);
                                        }                                     
                                    }
                                }
                                t1.Value = dt;
                            }

                        }
                    }
                }

                if (error_msg == "")
                {                
                    if (vm.create_stock_sheet_detail_id != null)
                    {
                        for (int i = 0; i < vm.create_stock_sheet_detail_id.Count; i++) //Create Stock Sheet Detail Table
                        {
                            
                                dt.Rows.Add(vm.create_stock_sheet_detail_id[i] == "" ? 0 : int.Parse(vm.create_stock_sheet_detail_id[i]),
                                    vm.item_code[i],
                                    vm.UOM[i],
                                    vm.batch_number[i],
                                    vm.actual_qty[i] == null ? 0: vm.actual_qty[i]);
                            
                        }
                    }


                    var update_stock_count_id = new SqlParameter("@update_stock_count_id", vm.update_stock_count_id == 0 ? -1 : vm.update_stock_count_id);
                    var category_id = new SqlParameter("@category_id", vm.category_id);
                    var posting_date = new SqlParameter("@posting_date", vm.posting_date);
                    var document_date = new SqlParameter("@document_date", vm.document_date);
                    var plant_id = new SqlParameter("@plant_id", vm.plant_id);
                    var sloc_id = new SqlParameter("@sloc_id", vm.sloc_id);
                    var bucket_id = new SqlParameter("@bucket_id", vm.bucket_id);
                    var status_id = new SqlParameter("@status_id", vm.status_id == null ? 0 : vm.status_id);
                    var ref_1 = new SqlParameter("@ref_1", vm.ref_1 == null ? "" : vm.ref_1);
                    var create_stock_sheet_id = new SqlParameter("@create_stock_sheet_id", vm.create_stock_sheet_id);
                    int id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                    var created_by = new SqlParameter("@created_by", id);
                    DateTime created_ts1 = DateTime.Now;
                    var created_ts = new SqlParameter("@created_ts", created_ts1);
                    var is_active = new SqlParameter("@is_active", true);
                    var item_category_id = new SqlParameter("@item_category_id", vm.item_category_id);

                    var val = _scifferContext.Database.SqlQuery<string>("exec save_update_stock_count @update_stock_count_id,@category_id, @posting_date,@document_date, @ref_1, @create_stock_sheet_id, @plant_id, @sloc_id, @bucket_id,@created_by,@created_ts,@is_active,@t1,@item_category_id",
                        update_stock_count_id, category_id, posting_date, document_date, ref_1, create_stock_sheet_id, plant_id, sloc_id, bucket_id, created_by, created_ts, is_active, t1, item_category_id).FirstOrDefault();

                    return val;
                }
                else
                {
                    return error_msg;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<update_stock_count_vm> create_stock_sheet(int create_stock_sheet_id)
        {
            throw new NotImplementedException();
        }

        public update_stock_count_vm Get(int? id)
        {
            try
            {
                update_stock_count stock = _scifferContext.update_stock_count.FirstOrDefault(c => c.update_stock_count_id == id);
                Mapper.CreateMap<update_stock_count, update_stock_count_vm>();
                update_stock_count_vm mmv = Mapper.Map<update_stock_count, update_stock_count_vm>(stock);
                mmv.update_stock_sheet_detail_vm = stock.create_stock_sheet_detail.Select(a => new
                update_stock_sheet_detail_vm()
                {
                    item_id = a.item_id,
                    actual_qty =a.actual_qty,
                    batch_number = a.batch_number,
                    create_stock_sheet_detail_id = a.create_stock_sheet_detail_id,
                    create_stock_sheet_id = a.create_stock_sheet_id,
                    diff_qty = a.diff_qty,
                    new_batch = a.new_batch,
                    rate = a.rate,
                    stock_qty = a.stock_qty,
                    uom_id = a.uom_id,
                    update_stock_count_id = a.update_stock_count_id,
                    value = a.value,
                    item_code = a.REF_ITEM.ITEM_CODE + "/" + a.item_id,
                    UOM = a.REF_UOM.UOM_NAME,
                 
                }).ToList();
                return mmv;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<update_stock_count_vm> getall()
        {
            try
            {
                var query = (from uss in _scifferContext.update_stock_count
                             join rdn in _scifferContext.ref_document_numbring on uss.category_id equals rdn.document_numbring_id into rdn1
                             from rdn2 in rdn1.DefaultIfEmpty()
                             join rp in _scifferContext.REF_PLANT on uss.plant_id equals rp.PLANT_ID into rp1
                             from rp2 in rp1.DefaultIfEmpty()
                             join rs in _scifferContext.REF_STORAGE_LOCATION on uss.sloc_id equals rs.storage_location_id into rs1
                             from rs2 in rs1.DefaultIfEmpty()
                             join rb in _scifferContext.ref_bucket on uss.bucket_id equals rb.bucket_id into rb1
                             from rb2 in rb1.DefaultIfEmpty()
                             join rst in _scifferContext.ref_status on uss.status_id equals rst.status_id into rst1
                             from rst2 in rst1.DefaultIfEmpty()
                             select new update_stock_count_vm
                             {
                                 create_stock_sheet_id = uss.create_stock_sheet_id,
                                 category_id = uss.category_id,
                                 doc_number = uss.doc_number,
                                 document_date1 = uss.document_date.ToString(),
                                 posting_date1 = uss.posting_date.ToString(),
                                 ref_1 = uss.ref_1,
                                 plant_id = uss.plant_id,
                                 plant_name = rp2.PLANT_NAME,
                                 sloc_id = uss.sloc_id,
                                 sloc_name = rs2.storage_location_name,
                                 bucket_id = uss.bucket_id,
                                 bucket_name = rb2.bucket_name,
                                 status_id = uss.status_id,
                                 status_name = rst2.status_name,
                                 update_stock_count_id = uss.update_stock_count_id,
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }        
    }
}
