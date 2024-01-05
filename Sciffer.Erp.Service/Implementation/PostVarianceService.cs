using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.SqlClient;
using Sciffer.Erp.Data;
using System.Data;
using Sciffer.Erp.Domain.Model;
using AutoMapper;

namespace Sciffer.Erp.Service.Implementation
{
    public class PostVarianceService : IPostVarianceService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _generic;
        public PostVarianceService(ScifferContext scifferContext, IGenericService generic)
        {
            _scifferContext = scifferContext;
            _generic = generic;
        }

        public string Add(post_variance_vm vm)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("post_variances_detail_id ", typeof(int));
                dt.Columns.Add("item_id ", typeof(int));
                dt.Columns.Add("uom_id ", typeof(int));
                dt.Columns.Add("batch_number ", typeof(string));
                dt.Columns.Add("stock_qty ", typeof(double));
                dt.Columns.Add("actual_qty ", typeof(double));
                dt.Columns.Add("diff_qty ", typeof(double));
                dt.Columns.Add("rate ", typeof(double));
                dt.Columns.Add("value ", typeof(double));
                dt.Columns.Add("create_stock_sheet_detail_id ", typeof(int));

                if (vm.post_variances_detail_id != null)
                {
                    for (int i = 0; i < vm.post_variances_detail_id.Count; i++) //Create Stock Sheet Detail Table
                    {
                        if (vm.actual_qty[i] > 0)
                        {
                            dt.Rows.Add(vm.post_variances_detail_id[i] == 0 ? -1 : vm.post_variances_detail_id[i],
                                vm.item_id[i],
                                vm.uom_id[i],
                                vm.batch_number[i],
                                vm.stock_qty[i],
                                vm.actual_qty[i],
                                vm.diff_qty[i],
                                vm.rate[i],
                                vm.value[i],
                                vm.create_stock_sheet_detail_id[i]);
                        }
                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_post_variances_detail";
                t1.Value = dt;

                var post_variances_id = new SqlParameter("@post_variances_id", vm.post_variances_id == 0 ? -1 : vm.post_variances_id);
                var category_id = new SqlParameter("@category_id", vm.category_id);
                var doc_number = new SqlParameter("@doc_number", vm.doc_number == null ? "" : vm.doc_number);
                var plant_id = new SqlParameter("@plant_id", vm.plant_id);
                var sloc_id = new SqlParameter("@sloc_id", vm.sloc_id);
                var bucket_id = new SqlParameter("@bucket_id", vm.bucket_id);
                var posting_date = new SqlParameter("@posting_date", vm.posting_date);
                var document_date = new SqlParameter("@document_date", vm.document_date);
                var ref1 = new SqlParameter("@ref1", vm.ref1 == null ? "" : vm.ref1);
                var update_stock_count_id = new SqlParameter("@update_stock_count_id", vm.update_stock_count_id);
                var create_stock_sheet_id = new SqlParameter("@create_stock_sheet_id", vm.create_stock_sheet_id);
                var is_active = new SqlParameter("@is_active", vm.is_active);
                var created_by = new SqlParameter("@created_by", vm.created_by);
                var created_ts = new SqlParameter("@created_ts", DateTime.Now);
                var modify_by = new SqlParameter("@modify_by", vm.modify_by);
                var modify_ts = new SqlParameter("@modify_ts", DateTime.Now);
                var cancelled_by = new SqlParameter("@cancelled_by", vm.cancelled_by);
                var cancelled_ts = new SqlParameter("@cancelled_ts", DateTime.Now);
                var cancellation_reason_id = new SqlParameter("@cancellation_reason_id", vm.cancellation_reason_id);
                var cancellaion_remarks = new SqlParameter("@cancellaion_remarks", vm.cancellaion_remarks == null ? "" : vm.cancellaion_remarks);
                var item_category_id = new SqlParameter("@item_category_id", vm.item_category_id);
                var val = _scifferContext.Database.SqlQuery<string>("exec save_post_variances @post_variances_id ,@category_id ,@doc_number ,@plant_id ,"+
                    "@sloc_id ,@bucket_id ,@posting_date ,@document_date ,@ref1 ,@update_stock_count_id ,@create_stock_sheet_id ,@is_active ,@created_by ,"+
                    "@created_ts ,@modify_by ,@modify_ts ,@cancelled_by ,@cancelled_ts ,@cancellation_reason_id ,@cancellaion_remarks,@t1,@item_category_id", 
                    post_variances_id, category_id, doc_number, plant_id, sloc_id, bucket_id, posting_date, document_date, ref1, update_stock_count_id,
                    create_stock_sheet_id, is_active, created_by, created_ts, modify_by, modify_ts, cancelled_by, cancelled_ts, cancellation_reason_id,
                    cancellaion_remarks, t1, item_category_id).FirstOrDefault();

                return val;               

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public post_variance_vm Get(int id)
        {
            try
            {
                post_variances stock = _scifferContext.post_variances.FirstOrDefault(c => c.post_variances_id == id && c.is_active == true);
                Mapper.CreateMap<post_variances, post_variance_vm>();
                post_variance_vm mmv = Mapper.Map<post_variances, post_variance_vm>(stock);
                return mmv;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<post_variance_vm> GetAll()
        {
            try
            {
                var query = (from uss in _scifferContext.post_variances
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
                             select new post_variance_vm
                             {
                                 post_variances_id = uss.post_variances_id,
                                 create_stock_sheet_id = uss.create_stock_sheet_id,
                                 category_id = uss.category_id,
                                 doc_number = uss.doc_number,
                                 document_date1 = uss.document_date.ToString(),
                                 posting_date1 = uss.posting_date.ToString(),
                                 ref1 = uss.ref1,
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
