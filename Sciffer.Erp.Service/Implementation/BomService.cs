using Sciffer.Erp.Data;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.Entity;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;

namespace Sciffer.Erp.Service.Implementation
{
    public class BomService : IBomService
    {
        private readonly ScifferContext _scifferContext;

        public BomService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }

        public string Add(ref_mfg_bom_VM vm)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("mfg_bom_detail_id", typeof(int));
                dt1.Columns.Add("in_item_group_id", typeof(int));
                dt1.Columns.Add("in_item_id", typeof(int));
                dt1.Columns.Add("in_item_qty", typeof(double));
                dt1.Columns.Add("in_uom_id", typeof(int));
                
                if (vm.in_item_group_id != null)
                {
                    for (var i = 0; i < vm.in_item_group_id.Count; i++)
                    {
                        dt1.Rows.Add(vm.mfg_bom_detail_id == null ? -1 : int.Parse(vm.mfg_bom_detail_id[i]),
                            int.Parse(vm.in_item_group_id[i]),
                            int.Parse(vm.in_item_id[i]),
                            vm.in_item_qty[i] == "" ? 0 : double.Parse(vm.in_item_qty[i]),
                            int.Parse(vm.in_uom_id[i]));
                    }
                }
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_ref_mfg_bom_detail";
                t1.Value = dt1;

                var mfg_bom_id = new SqlParameter("@mfg_bom_id", vm.mfg_bom_id == 0 ? -1 : vm.mfg_bom_id);
                var create_ts = new SqlParameter("@create_ts", vm.create_ts);
                var mfg_bom_name = new SqlParameter("@mfg_bom_name", vm.mfg_bom_name);
                var out_item_id = new SqlParameter("@out_item_id", vm.out_item_id);
                var mfg_bom_qty = new SqlParameter("@mfg_bom_qty", vm.mfg_bom_qty);
                var category_id = new SqlParameter("@category_id", vm.category_id);
                var drawing_no = new SqlParameter("@drawing_no", vm.drawing_no==null ? "" : vm.drawing_no);
                var version = new SqlParameter("@version", vm.version==null?"":vm.version);
                var remarks = new SqlParameter("@remarks", vm.remarks == null ? "" : vm.remarks);
                var is_blocked = new SqlParameter("@is_blocked", false);
               
                var val = _scifferContext.Database.SqlQuery<string>("exec Save_BillofMaterials @mfg_bom_id,@create_ts,@mfg_bom_name,@out_item_id,@mfg_bom_qty, @category_id, @drawing_no, @version, @remarks, @is_blocked, @t1",
                    mfg_bom_id, create_ts, mfg_bom_name, out_item_id, mfg_bom_qty, category_id, drawing_no, version, remarks, is_blocked, t1).FirstOrDefault();
                if (val.Contains("Saved"))
                {
                    var sp = val.Split('~')[1];
                    return sp;
                }
                else
                {
                    return "Error";
                }
                //ref_mfg_bom refBom = new ref_mfg_bom();
                //refBom.mfg_bom_name = bomVM.mfg_bom_name;
                //refBom.out_item_id = bomVM.out_item_id;
                //refBom.mfg_bom_qty = bomVM.mfg_bom_qty;
                //refBom.remarks = bomVM.remarks;
                //refBom.category_id = bomVM.category_id;
                //refBom.drawing_no = bomVM.drawing_no;
                //refBom.version = bomVM.version;
                //refBom.create_ts = bomVM.create_ts;
                //refBom.mfg_bom_no = bomVM.mfg_bom_no;
                //refBom.is_blocked = bomVM.is_blocked;
                //_scifferContext.ref_mfg_bom.Add(refBom);

                //for (int i = 0; i < bomVM.in_item_group_id.Count; i++)
                //{
                //    ref_mfg_bom_detail bomd = new ref_mfg_bom_detail();
                //    bomd.in_item_group_id = int.Parse(bomVM.in_item_group_id[i]);
                //    bomd.in_item_id = int.Parse(bomVM.in_item_id[i]);
                //    bomd.in_item_qty = int.Parse(bomVM.in_item_qty[i]);
                //    bomd.in_uom_id = int.Parse(bomVM.in_uom_id[i]);
                //    _scifferContext.ref_mfg_bom_detail.Add(bomd);
                //}

                //var doc = _scifferContext.ref_document_numbring.FirstOrDefault(x => x.document_numbring_id == refBom.category_id);
                //var current_number = refBom.mfg_bom_no;
                //doc.current_number = current_number.Replace(doc.prefix_sufix, "");
                //_scifferContext.Entry(doc).State = EntityState.Modified;

                //_scifferContext.SaveChanges();
                
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        #region dispoable methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ref_mfg_bom_VM Get(int id)
        {
            ref_mfg_bom bom = _scifferContext.ref_mfg_bom.FirstOrDefault(X => X.mfg_bom_id == id);
            Mapper.CreateMap<ref_mfg_bom, ref_mfg_bom_VM>();

            ref_mfg_bom_VM bom_vm = Mapper.Map<ref_mfg_bom, ref_mfg_bom_VM>(bom);
            bom_vm.ref_mfg_bom_detail = bom_vm.ref_mfg_bom_detail.ToList();      

            return bom_vm;
        }


        public bool Update(ref_mfg_bom_VM id)
        {
            try
            {
                var vax = _scifferContext.ref_mfg_bom.Where(x => x.mfg_bom_id == id.mfg_bom_id).FirstOrDefault();
                vax.is_blocked = id.is_blocked;
                _scifferContext.Entry(vax).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }


        public List<ref_mfg_bom_VM> getall()
        {
            var query = (from bm in _scifferContext.ref_mfg_bom
                         join cat in _scifferContext.ref_document_numbring on bm.category_id equals cat.document_numbring_id
                         join i in _scifferContext.REF_ITEM on bm.out_item_id equals i.ITEM_ID
                         select new
                         {
                             mfg_bom_id = bm.mfg_bom_id,
                             mfg_bom_name = bm.mfg_bom_name,
                             mfg_bom_no = bm.mfg_bom_no,

                             out_item_id = bm.out_item_id,
                             out_item_name=i.ITEM_NAME,

                             mfg_bom_qty = bm.mfg_bom_qty,
                             remarks = bm.remarks,

                             category_id = bm.category_id,
                             category_name = cat.category,

                             drawing_no= bm.drawing_no,
                             version=bm.version,
                             create_ts=bm.create_ts
                         }).ToList()
                         .Select(x => new ref_mfg_bom_VM
                         {
                             mfg_bom_id = x.mfg_bom_id,
                             mfg_bom_name = x.mfg_bom_name,
                             mfg_bom_no = x.mfg_bom_no,

                             out_item_id = x.out_item_id,
                             out_item_name=x.out_item_name,

                             mfg_bom_qty = x.mfg_bom_qty,
                             remarks = x.remarks,

                             category_id = x.category_id,
                             category_name = x.category_name,

                             drawing_no = x.drawing_no,
                             version = x.version,
                             create_ts = x.create_ts
                         }).OrderByDescending(a => a.mfg_bom_id).ToList();


            return query;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion
    }
}
