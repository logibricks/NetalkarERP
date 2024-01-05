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
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class StockSheetService : IStockSheetService
    {
        private readonly ScifferContext _scifferContext;

        public StockSheetService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public string Add(create_stock_sheet_vm vm)
        {
            try
            {
                var create_stock_sheet_id = new SqlParameter("@create_stock_sheet_id", vm.create_stock_sheet_id == 0 ? -1 : vm.create_stock_sheet_id);
                var category_id = new SqlParameter("@category_id", vm.category_id);
                var document_date = new SqlParameter("@document_date", vm.document_date);
                var plant_id = new SqlParameter("@plant_id", vm.plant_id);
                var sloc_id = new SqlParameter("@sloc_id", vm.sloc_id);
                var bucket_id = new SqlParameter("@bucket_id", vm.bucket_id);
                var status_id = new SqlParameter("@status_id", vm.status_id == null ? 0 : vm.status_id);
                var ref_1 = new SqlParameter("@ref_1", vm.ref_1 == null ? "" : vm.ref_1);
                int id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var created_by = new SqlParameter("@created_by", id);
                DateTime created_ts1 = DateTime.Now;
                var created_ts = new SqlParameter("@created_ts", created_ts1);
                var is_active = new SqlParameter("@is_active", true);
                var item_category_id = new SqlParameter("@item_category_id", vm.item_category_id);

                var val = _scifferContext.Database.SqlQuery<string>("exec save_create_stock_sheet @create_stock_sheet_id,@category_id, @document_date, @ref_1, @plant_id, @sloc_id, @bucket_id,@is_active,@created_by,@created_ts,@status_id, @item_category_id",
                    create_stock_sheet_id, category_id, document_date, ref_1, plant_id, sloc_id, bucket_id, is_active, created_by, created_ts, status_id, item_category_id).FirstOrDefault();

                return val;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public create_stock_sheet_vm Get(int? id)
        {
            try
            {
                create_stock_sheet stock = _scifferContext.create_stock_sheet.FirstOrDefault(c => c.create_stock_sheet_id == id && c.is_active == true);
                Mapper.CreateMap<create_stock_sheet, create_stock_sheet_vm>();
                create_stock_sheet_vm mmv = Mapper.Map<create_stock_sheet, create_stock_sheet_vm>(stock);
                return mmv;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public List<create_stock_sheet_vm> getall()
        {
            try
            {
                var query = (from css in _scifferContext.create_stock_sheet
                             join rdn in _scifferContext.ref_document_numbring on css.category_id equals rdn.document_numbring_id into rdn1
                             from rdn2 in rdn1.DefaultIfEmpty()
                             join rp in _scifferContext.REF_PLANT on css.plant_id equals rp.PLANT_ID into rp1
                             from rp2 in rp1.DefaultIfEmpty()
                             join rs in _scifferContext.REF_STORAGE_LOCATION on css.sloc_id equals rs.storage_location_id into rs1
                             from rs2 in rs1.DefaultIfEmpty()
                             join rb in _scifferContext.ref_bucket on css.bucket_id equals rb.bucket_id into rb1
                             from rb2 in rb1.DefaultIfEmpty()
                             join rst in _scifferContext.ref_status on css.status_id equals rst.status_id into rst1
                             from rst2 in rst1.DefaultIfEmpty()
                             select new create_stock_sheet_vm
                             {
                                 create_stock_sheet_id = css.create_stock_sheet_id,
                                 category_id = css.category_id,
                                 document_no = css.document_no,
                                 document_date1 = css.document_date.ToString(),
                                 ref_1 = css.ref_1,
                                 plant_id = css.plant_id,
                                 plant_name = rp2.PLANT_NAME,
                                 sloc_id = css.sloc_id,
                                 sloc_name = rs2.storage_location_name,
                                 bucket_id = css.bucket_id,
                                 bucket_name = rb2.bucket_name,
                                 status_id = css.status_id,
                                 status_name = rst2.status_name,
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<create_stock_sheet_vm> GetStockSheet()
        {
            try
            {
                var query = (from p in _scifferContext.create_stock_sheet.Where(x => x.is_active == true)
                             select new create_stock_sheet_vm
                             {
                                 create_stock_sheet_id = p.create_stock_sheet_id,
                                 document_no = p.document_no,
                             }).ToList();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<create_stock_sheet_vm> StockQuantity_Graterthan_Zero(int id, string form)
        {
            try
            {
                if (form == "CRT_STOCK")
                {
                    var list = (from stock_detail in _scifferContext.create_stock_sheet_detail.Where(x => x.create_stock_sheet_id == id && x.stock_qty > 0)
                                join stock in _scifferContext.create_stock_sheet on stock_detail.create_stock_sheet_id equals stock.create_stock_sheet_id into stock1
                                from stock2 in stock1.DefaultIfEmpty()
                                join ri in _scifferContext.REF_ITEM on stock_detail.item_id equals ri.ITEM_ID into ri1
                                from ri2 in ri1.DefaultIfEmpty()
                                join ru in _scifferContext.REF_UOM on stock_detail.uom_id equals ru.UOM_ID into ru1
                                from ru2 in ru1.DefaultIfEmpty()
                                select new create_stock_sheet_vm
                                {
                                    create_stock_sheet_detail_id = stock_detail.create_stock_sheet_detail_id,
                                    item_code = ri2.ITEM_CODE + "/" + ri2.ITEM_NAME,
                                    UOM = ru2.UOM_NAME,
                                    batch_number = stock_detail.batch_number == null ? "NULL" : stock_detail.batch_number,
                                    actual_qty = stock_detail.actual_qty,
                                    stock_qty = stock_detail.stock_qty,
                                    diff_qty = stock_detail.diff_qty,
                                    rate = stock_detail.rate,
                                    value = stock_detail.value,
                                    item_id = stock_detail.item_id,
                                    uom_id = stock_detail.uom_id,
                                }).ToList();

                    return list;
                }
                else
                {
                    var list = (from stock_detail in _scifferContext.create_stock_sheet_detail.Where(x => x.update_stock_count_id == id && x.stock_qty > 0)
                                join stock in _scifferContext.create_stock_sheet on stock_detail.create_stock_sheet_id equals stock.create_stock_sheet_id into stock1
                                from stock2 in stock1.DefaultIfEmpty()
                                join ri in _scifferContext.REF_ITEM on stock_detail.item_id equals ri.ITEM_ID into ri1
                                from ri2 in ri1.DefaultIfEmpty()
                                join ru in _scifferContext.REF_UOM on stock_detail.uom_id equals ru.UOM_ID into ru1
                                from ru2 in ru1.DefaultIfEmpty()
                                    //where stock_detail.create_stock_sheet_id == create_stock_sheet_id && stock_detail.actual_qty > 0
                                select new create_stock_sheet_vm
                                {
                                    create_stock_sheet_detail_id = stock_detail.create_stock_sheet_detail_id,
                                    item_code = ri2.ITEM_CODE + "/" + ri2.ITEM_NAME,
                                    UOM = ru2.UOM_NAME,
                                    batch_number = stock_detail.batch_number == null ? "NULL" : stock_detail.batch_number,
                                    actual_qty = stock_detail.actual_qty,
                                    stock_qty = stock_detail.stock_qty,
                                    diff_qty = stock_detail.diff_qty,
                                    rate = stock_detail.rate,
                                    value = stock_detail.value,
                                    item_id = stock_detail.item_id,
                                    uom_id = stock_detail.uom_id,
                                }).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<create_stock_sheet_vm> StockQuantity_Lessthan_Zero(int id, string form)
        {
            try
            {
                if (form == "CRT_STOCK")
                {
                    var list = (from stock_detail in _scifferContext.create_stock_sheet_detail.Where(x => x.create_stock_sheet_id == id && x.stock_qty == null)
                                join stock in _scifferContext.create_stock_sheet on stock_detail.create_stock_sheet_id equals stock.create_stock_sheet_id into stock1
                                from stock2 in stock1.DefaultIfEmpty()
                                join ri in _scifferContext.REF_ITEM on stock_detail.item_id equals ri.ITEM_ID into ri1
                                from ri2 in ri1.DefaultIfEmpty()
                                join ru in _scifferContext.REF_UOM on stock_detail.uom_id equals ru.UOM_ID into ru1
                                from ru2 in ru1.DefaultIfEmpty()
                                select new create_stock_sheet_vm
                                {
                                    create_stock_sheet_detail_id = stock_detail.create_stock_sheet_detail_id,
                                    item_code = ri2.ITEM_CODE + "/" + ri2.ITEM_NAME,
                                    UOM = ru2.UOM_NAME,
                                    batch_number = stock_detail.batch_number == null ? "NULL" : stock_detail.batch_number,
                                    actual_qty = stock_detail.actual_qty,
                                    stock_qty = stock_detail.stock_qty,
                                    diff_qty = stock_detail.diff_qty,
                                    rate = stock_detail.rate,
                                    value = stock_detail.value,
                                    item_id = stock_detail.item_id,
                                    uom_id = stock_detail.uom_id,
                                }).ToList();

                    return list;
                }
                else
                {
                    var list = (from stock_detail in _scifferContext.create_stock_sheet_detail.Where(x => x.update_stock_count_id == id && x.stock_qty == null)
                                join stock in _scifferContext.create_stock_sheet on stock_detail.create_stock_sheet_id equals stock.create_stock_sheet_id into stock1
                                from stock2 in stock1.DefaultIfEmpty()
                                join ri in _scifferContext.REF_ITEM on stock_detail.item_id equals ri.ITEM_ID into ri1
                                from ri2 in ri1.DefaultIfEmpty()
                                join ru in _scifferContext.REF_UOM on stock_detail.uom_id equals ru.UOM_ID into ru1
                                from ru2 in ru1.DefaultIfEmpty()
                                    //where stock_detail.create_stock_sheet_id == create_stock_sheet_id && stock_detail.actual_qty == null
                                select new create_stock_sheet_vm
                                {
                                    create_stock_sheet_detail_id = stock_detail.create_stock_sheet_detail_id,
                                    item_code = ri2.ITEM_CODE + "/" + ri2.ITEM_NAME,
                                    UOM = ru2.UOM_NAME,
                                    batch_number = stock_detail.batch_number == null ? "NULL" : stock_detail.batch_number,
                                    actual_qty = stock_detail.actual_qty,
                                    stock_qty = stock_detail.stock_qty,
                                    diff_qty = stock_detail.diff_qty,
                                    rate = stock_detail.rate,
                                    value = stock_detail.value,
                                    item_id = stock_detail.item_id,
                                    uom_id = stock_detail.uom_id,
                                }).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<create_stock_sheet_vm> StockQuantity_Equal_Zero(int id, string form)
        {
            try
            {
                if(form == "CRT_STOCK")
                {
                    var list = (from stock_detail in _scifferContext.create_stock_sheet_detail.Where(x => x.create_stock_sheet_id == id && x.stock_qty == 0)
                                join stock in _scifferContext.create_stock_sheet on stock_detail.create_stock_sheet_id equals stock.create_stock_sheet_id into stock1
                                from stock2 in stock1.DefaultIfEmpty()
                                join ri in _scifferContext.REF_ITEM on stock_detail.item_id equals ri.ITEM_ID into ri1
                                from ri2 in ri1.DefaultIfEmpty()
                                join ru in _scifferContext.REF_UOM on stock_detail.uom_id equals ru.UOM_ID into ru1
                                from ru2 in ru1.DefaultIfEmpty()
                                    //where stock_detail.create_stock_sheet_id == create_stock_sheet_id && stock_detail.actual_qty == 0
                                select new create_stock_sheet_vm
                                {
                                    create_stock_sheet_detail_id = stock_detail.create_stock_sheet_detail_id,
                                    item_code = ri2.ITEM_CODE + "/" + ri2.ITEM_NAME,
                                    UOM = ru2.UOM_NAME,
                                    batch_number = stock_detail.batch_number == null ? "NULL" : stock_detail.batch_number,
                                    actual_qty = stock_detail.actual_qty,
                                    stock_qty = stock_detail.stock_qty,
                                    diff_qty = stock_detail.diff_qty,
                                    rate = stock_detail.rate,
                                    value = stock_detail.value,
                                    item_id = stock_detail.item_id,
                                    uom_id = stock_detail.uom_id,
                                }).ToList();

                    return list;
                }
                else
                {
                    var list = (from stock_detail in _scifferContext.create_stock_sheet_detail.Where(x => x.update_stock_count_id == id && x.stock_qty == 0)
                                join stock in _scifferContext.create_stock_sheet on stock_detail.create_stock_sheet_id equals stock.create_stock_sheet_id into stock1
                                from stock2 in stock1.DefaultIfEmpty()
                                join ri in _scifferContext.REF_ITEM on stock_detail.item_id equals ri.ITEM_ID into ri1
                                from ri2 in ri1.DefaultIfEmpty()
                                join ru in _scifferContext.REF_UOM on stock_detail.uom_id equals ru.UOM_ID into ru1
                                from ru2 in ru1.DefaultIfEmpty()
                                select new create_stock_sheet_vm
                                {
                                    create_stock_sheet_detail_id = stock_detail.create_stock_sheet_detail_id,
                                    item_code = ri2.ITEM_CODE + "/" + ri2.ITEM_NAME,
                                    UOM = ru2.UOM_NAME,
                                    batch_number = stock_detail.batch_number == null ? "NULL" : stock_detail.batch_number,
                                    actual_qty = stock_detail.actual_qty,
                                    stock_qty = stock_detail.stock_qty,
                                    diff_qty = stock_detail.diff_qty,
                                    rate = stock_detail.rate,
                                    value = stock_detail.value,
                                    item_id = stock_detail.item_id,
                                    uom_id = stock_detail.uom_id,
                                }).ToList();

                    return list;
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<create_stock_sheet_vm> create_stock_sheet(int id)
        {
            try
            {
                var list = (from stock in _scifferContext.create_stock_sheet
                            join rp in _scifferContext.REF_PLANT on stock.plant_id equals rp.PLANT_ID into rp1
                            from rp2 in rp1.DefaultIfEmpty()
                            join rb in _scifferContext.ref_bucket on stock.bucket_id equals rb.bucket_id into rb1
                            from rb2 in rb1.DefaultIfEmpty()
                            join rsl in _scifferContext.REF_STORAGE_LOCATION on stock.sloc_id equals rsl.storage_location_id into rsl1
                            from rsl2 in rsl1.DefaultIfEmpty()
                            where stock.create_stock_sheet_id == id
                            select new create_stock_sheet_vm
                            {
                                plant_name = rp2.PLANT_NAME,
                                sloc_name = rsl2.storage_location_name,
                                bucket_name = rb2.bucket_name,
                                document_date1 = stock.document_date.ToString(),
                                document_no = stock.document_no,
                            }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
