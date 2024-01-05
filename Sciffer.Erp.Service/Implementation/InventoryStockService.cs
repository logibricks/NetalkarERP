using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class InventoryStockService : IInventoryStockService
    {
       private readonly ScifferContext _scifferContext;
       private readonly IGenericService _genericService;
        public InventoryStockService(ScifferContext ScifferContext, IGenericService GenericService)
        {
            _scifferContext = ScifferContext;
            _genericService = GenericService;
        }
        public string Add(inv_Inventory_stock_vm main)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("inventory_stock_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("item_batch_id", typeof(int));
                dt.Columns.Add("batch_number", typeof(string));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("actual_qty", typeof(double));
              
                if (main.item_id != null)
                {
                    for (var i = 0; i < main.inventory_stock_detail_id.Count; i++)
                    {
                        int tag = -1;
                        if (main.inventory_stock_detail_id != null)
                        {
                            tag = main.inventory_stock_detail_id[i] == "" ? -1 : Convert.ToInt32(main.inventory_stock_detail_id[i]);
                        }
                        dt.Rows.Add(tag, main.item_id[i] == "0" ? 0 : int.Parse(main.item_id[i]), int.Parse(main.item_batch_id[i]), main.batch_number[i], int.Parse(main.uom_id[i]), main.actual_qty[i] == "" ? 0 : double.Parse(main.actual_qty[i]));

                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_inventory_stock_detail";
                t1.Value = dt;

                var inventory_stock_id = new SqlParameter("@inventory_stock_id", main.inventory_stock_id == 0 ? -1 : main.inventory_stock_id);
                var category_id = new SqlParameter("@category_id", main.@category_id);
                var number = new SqlParameter("@number", main.number);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);
                var posting_date = new SqlParameter("@posting_date", main.posting_date);
                var sloc_id = new SqlParameter("@sloc_id", main.sloc_id);
                var document_date = new SqlParameter("@document_date", main.document_date);
                var bucket_id = new SqlParameter("@bucket_id", main.bucket_id);               
                var ref1 = new SqlParameter("@ref1", main.ref1 == null ? string.Empty : main.ref1);


                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec Save_InventoryStock @inventory_stock_id ,@category_id ,@number ,@plant_id ,@posting_date,@sloc_id,@document_date ,@bucket_id ,@ref1,@t1",
                    inventory_stock_id, category_id, number, plant_id, posting_date,sloc_id, document_date, bucket_id, ref1, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
                    return sp;
                }
                else
                {
                    return "Error";
                }
               
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }


        public inv_Inventory_stock_vm Get(int id)
        {
            try
            {
                inv_Inventory_stock pla = _scifferContext.inv_Inventory_stock.FirstOrDefault(c => c.inventory_stock_id == id);
                Mapper.CreateMap<inv_Inventory_stock, inv_Inventory_stock_vm>();
                inv_Inventory_stock_vm plvm = Mapper.Map<inv_Inventory_stock, inv_Inventory_stock_vm>(pla);
                //plvm.inv_Inventory_stock_detail = plvm.inv_Inventory_stock_detail.ToList();
                var inventory_stock = new SqlParameter("@inventory_stock_id", id);
                plvm.inv_Inventory_stock_detail_VM= _scifferContext.Database.SqlQuery<inv_Inventory_stock_detail_VM>("exec GetItemforStockEdit  @inventory_stock_id", inventory_stock).ToList();
                var company = _scifferContext.REF_COMPANY.FirstOrDefault();
                plvm.company_name = company.COMPANY_DISPLAY_NAME;
                plvm.company_address = company.REGISTERED_ADDRESS + ',' + company.REGISTERED_CITY + ',' + company.REF_STATE.STATE_NAME + '-' + company.registered_pincode + ',' + company.REF_STATE.REF_COUNTRY.COUNTRY_NAME;
                return plvm;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<inv_Inventory_stock_vm> GetAll()
        {
            var query = (from mach in _scifferContext.inv_Inventory_stock

                        

                         join bucket in _scifferContext.ref_bucket on mach.bucket_id equals bucket.bucket_id into bucket1
                         from bucket2 in bucket1.DefaultIfEmpty()

                         join slocname in _scifferContext.REF_STORAGE_LOCATION on mach.sloc_id equals slocname.storage_location_id into slocname1
                         from slocname2 in slocname1.DefaultIfEmpty()

                         join plant in _scifferContext.REF_PLANT on mach.plant_id equals plant.PLANT_ID into plant1
                         from plant2 in plant1.DefaultIfEmpty()

                         join plantname in _scifferContext.REF_PLANT on mach.plant_id equals plantname.PLANT_ID into plantname1
                         from plantname2 in plantname1.DefaultIfEmpty()

                         join sloc in _scifferContext.REF_STORAGE_LOCATION on mach.sloc_id equals sloc.storage_location_id into sloc1
                         from sloc2 in sloc1.DefaultIfEmpty()

                         join bucket_name in _scifferContext.ref_bucket on mach.bucket_id equals bucket_name.bucket_id into bucket_name1
                         from bucket_name2 in bucket_name1.DefaultIfEmpty()

                         select new inv_Inventory_stock_vm()
                         {
                             inventory_stock_id = mach.inventory_stock_id,
                            bucket_id = mach.bucket_id,
                            plant_id = mach.plant_id,
                             plant_name = plantname2 == null ? "" : plantname2.PLANT_CODE + "-" + plantname2.PLANT_NAME ,
                             slocname = slocname2 == null ? "" : slocname2.storage_location_name + "-" + slocname2.description,
                             bucket_name = bucket_name2 == null ? "" : bucket_name2.bucket_name,
                             sloc_id = mach.sloc_id,
                             posting_date = mach.posting_date,
                             document_date = mach.document_date,
                             number = mach.number,
                             category_id = mach.category_id,
                             ref1 = mach.ref1
                         }).OrderByDescending(a => a.inventory_stock_id).ToList();
            return query;
        }

        public List<GetItemForStock> GetItemForStock( int plant_id, int sloc_id, int bucket_id)
        {
           
            var plant = new SqlParameter("@plant_id", plant_id);
            var sloc = new SqlParameter("@sloc_id", sloc_id);
            var bucket = new SqlParameter("@bucket_id", bucket_id);
            
            var val = _scifferContext.Database.SqlQuery<GetItemForStock>(
            "exec GetItemForStock @plant_id,@sloc_id,@bucket_id", plant, sloc, bucket).ToList();
            return val;
        }

        public List<inv_Inventory_stock_detail_VM> GetItemForStockEdit(int id)
        {
            
            var inventory_stock = new SqlParameter("@inventory_stock_id", id);
            var val = _scifferContext.Database.SqlQuery<inv_Inventory_stock_detail_VM>(
            "exec GetItemforStockEdit  @inventory_stock_id", inventory_stock).ToList();
            return val;
        }



    }
}
