using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using System.Linq;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Web;
using System.Data.Entity;

namespace Sciffer.Erp.Service.Implementation
{
    public class PurRequisitionService : IPurRequisitionService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public PurRequisitionService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }

        public string Add(pur_requisition_vm pur)
        {
            try
            {
                if (pur.FileUpload != null)
                {
                    pur.attachement = _genericService.GetFilePath("PurchaseRequisition", pur.FileUpload);
                }
                else
                {
                    pur.attachement = "No File";
                }
                DataTable dt = new DataTable();
                DateTime dte = new DateTime(1990, 1, 1);
                dt.Columns.Add("pur_requisition_detail_id", typeof(int));
                dt.Columns.Add("pur_requisition_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("delivery_date", typeof(DateTime));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("info_price", typeof(double));
                dt.Columns.Add("storage_location_id", typeof(int));
                dt.Columns.Add("vendor_id", typeof(int));
                dt.Columns.Add("sac_hsn_id", typeof(int));
                for (var i = 0; i < pur.item_id1.Count; i++)
                {
                    if (pur.item_id1[i] != "")
                    {
                        if (pur.vendor_id1[i] == "null")
                        {
                            pur.vendor_id1[i] = "";
                        }
                        if (pur.info_price[i] == "null")
                        {
                            pur.info_price[i] = "0";
                        }
                        if (pur.item_service_id1[i] == "null")
                        {
                            pur.item_service_id1[i] = "0";
                        }
                      
                        var id = pur.pur_requisition_detail_id[i] == "" ? 0 : Convert.ToInt32(pur.pur_requisition_detail_id[i]);
                        var item_id = pur.item_id1[i] == "" ? 0 : Convert.ToInt32(pur.item_id1[i]);
                        var delivery_dates = pur.delivery_date1[i] == "" ? dte : DateTime.Parse(pur.delivery_date1[i]);
                        var quantity = pur.quantity[i] == "" ? 0 : Convert.ToDouble(pur.quantity[i]);
                        var uom_id = pur.uom_id[i] == "" ? 0 : Convert.ToInt32(pur.uom_id[i]);
                        var price = pur.info_price[i] == "" ? 0 : Convert.ToDouble(pur.info_price[i]);
                        var sloc_id = pur.storage_location_id1[i] == "" ? 0: pur.storage_location_id1[i] == "null" ? 0 : Convert.ToInt32(pur.storage_location_id1[i]);
                        var vendor_id = pur.vendor_id1[i] == "" ? 0 : Convert.ToInt32(pur.vendor_id1[i]);
                       
                        var hsn_id = 0;
                        dt.Rows.Add(id, 0, item_id, delivery_dates, quantity, uom_id, price, sloc_id, vendor_id, hsn_id);

                    }
                }
                var pur_requisition_id = new SqlParameter("@pur_requisition_id", pur.pur_requisition_id);
                var category_id = new SqlParameter("@category_id", pur.category_id);
                var pur_requisition_number = new SqlParameter("@pur_requisition_number", pur.pur_requisition_number == null ? "0" : pur.pur_requisition_number);
                var pur_requisition_date = new SqlParameter("@pur_requisition_date", pur.pur_requisition_date);
                var business_unit_id = new SqlParameter("@business_unit_id", pur.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", pur.plant_id);
                var delivery_date = new SqlParameter("@delivery_date", pur.delivery_date == null ? dte : pur.delivery_date);
                var source_id = new SqlParameter("@source_id", pur.source_id);
                var created_by = new SqlParameter("@created_by", pur.created_by);
                var internal_remarks = new SqlParameter("@internal_remarks", pur.internal_remarks == null ? string.Empty : pur.internal_remarks);
                var remarks = new SqlParameter("@remarks", pur.remarks == null ? string.Empty : pur.remarks);
                var attachement = new SqlParameter("@attachement", pur.attachement);
                var status_id = new SqlParameter("@status_id", pur.status_id);
                var deleteids = new SqlParameter("@deleteids", pur.deleteids == null ? string.Empty : pur.deleteids);
                var responsibility_id = new SqlParameter("@responsibility_id", pur.responsibility_id == null ? 0 : pur.responsibility_id);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_pur_requisition_detail";
                t1.Value = dt;
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_purchase_requisition @pur_requisition_id ,@category_id ,@pur_requisition_number ,@pur_requisition_date ,@business_unit_id ,@plant_id ,@delivery_date ,@source_id ,@created_by ,@internal_remarks ,@remarks ,@attachement ,@status_id,@deleteids,@responsibility_id,@t1 ",
                    pur_requisition_id, category_id, pur_requisition_number, pur_requisition_date, business_unit_id, plant_id, delivery_date, source_id, created_by, internal_remarks, remarks, attachement,
                    status_id, deleteids, responsibility_id, t1).FirstOrDefault();

                if (val.Contains("Saved"))
                {

                    return val;
                }
                else
                {
                    System.IO.File.Delete(pur.attachement);
                    return val;
                }

            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scifferContext.Dispose();
            }
        }
        #endregion

        public pur_requisition_vm Get(int id)
        {
            try
            {
                pur_requisition pr = _scifferContext.pur_requisition.FirstOrDefault(c => c.pur_requisition_id == id);
                Mapper.CreateMap<pur_requisition, pur_requisition_vm>();
                pur_requisition_vm prvm = Mapper.Map<pur_requisition, pur_requisition_vm>(pr);
                prvm.pur_requisition_detail = prvm.pur_requisition_detail.Where(c => c.is_active == true).ToList();
                var details = (from a in prvm.pur_requisition_detail
                               join item in _scifferContext.REF_ITEM on a.item_id equals item.ITEM_ID
                               join sac in _scifferContext.ref_sac on a.sac_hsn_id equals sac.sac_id into sac1
                               from sac2 in sac1.DefaultIfEmpty()
                               join hsn in _scifferContext.ref_hsn_code on a.sac_hsn_id equals hsn.hsn_code_id into hsn1
                               from hsn2 in hsn1.DefaultIfEmpty()
                               join itemtype in _scifferContext.ref_item_type on item.item_type_id equals itemtype.item_type_id into itemtype1
                               from itemtype2 in itemtype1.DefaultIfEmpty()
                               select new
                               {
                                   pur_requisition_detail_id = a.pur_requisition_detail_id,
                                   pur_requisition_id = a.pur_requisition_id,
                                   sr_no = a.sr_no,
                                   item_id = a.item_id,
                                   delivery_date = a.delivery_date,
                                   quantity = a.quantity,
                                   uom_id = a.uom_id,
                                   info_price = a.info_price,
                                   is_short_close = a.is_short_close,
                                   is_active = a.is_active,
                                   storage_location_id = a.storage_location_id,
                                   vendor_id = a.vendor_id,
                                   order_status = a.order_status,
                                   sac_hsn_id = item.item_type_id == 2 ? sac2 == null ? 0 : sac2.sac_id : hsn2 == null ? 0 : hsn2.hsn_code_id,
                                   hsn_code = item.item_type_id == 2 ? sac2 == null ? "" : sac2.sac_code + "/" + sac2.sac_description: hsn2 == null ? "" : hsn2.hsn_code + "/" + hsn2.hsn_code_description,
                                   item_code = item.ITEM_CODE + "/" + item.ITEM_NAME,
                                   uom_name = a.REF_UOM.UOM_NAME,
                                   vendor_name = a.REF_VENDOR == null ? "" : a.REF_VENDOR.VENDOR_CODE + "/" + a.REF_VENDOR.VENDOR_NAME,
                                   storage_location_name = a.REF_STORAGE_LOCATION.storage_location_name + "/" + a.REF_STORAGE_LOCATION.description,
                                   item_type_id = item.item_type_id,
                                   item_type_name = itemtype2.item_type_name

                               }).ToList().Select(a => new pur_requisition_detail_vm
                               {
                                   pur_requisition_detail_id = a.pur_requisition_detail_id,
                                   pur_requisition_id = a.pur_requisition_id,
                                   sr_no = a.sr_no,
                                   item_id = a.item_id,
                                   delivery_date = a.delivery_date,
                                   quantity = a.quantity,
                                   uom_id = a.uom_id,
                                   info_price = a.info_price,
                                   is_short_close = a.is_short_close,
                                   is_active = a.is_active,
                                   storage_location_id = a.storage_location_id,
                                   vendor_id = a.vendor_id,
                                   order_status = a.order_status,
                                   hsn_id = a.sac_hsn_id,
                                   hsn_code = a.hsn_code,
                                   item_code = a.item_code,
                                   uom_name = a.uom_name,
                                   storage_location_name = a.storage_location_name,
                                   vendor_name = a.vendor_name,
                                   item_type_id = a.item_type_id,
                                   item_type_name = a.item_type_name
                               }).OrderBy(x=> x.item_id).ToList();
                prvm.pur_requisition_detail_vm = details;
                return prvm;
            }
           catch(Exception ex)
            {
                return null;
            }
        }

        public List<pur_requisition_vm> GetAll()
        {
            var ent = new SqlParameter("@entity", "getall");
            var val = _scifferContext.Database.SqlQuery<pur_requisition_vm>(
                "exec get_pur_requisition @entity", ent).ToList();
            return val;
        }



        public List<pur_requisition_vm> GetPurRequistion()
        {
            var query = (from pur in _scifferContext.pur_requisition.Where(x=>x.approval_status==1 )
                         join st in _scifferContext.ref_status.Where(a=>a.status_name == "Open" || a.status_name == "Partially Open") on pur.status_id equals st.status_id
                         select new
                         {
                             pur_requisition_id = pur.pur_requisition_id,
                             pur_requisition_number = pur.pur_requisition_number,
                             pur_requisition_date = pur.pur_requisition_date,

                         }).ToList().Select(a => new pur_requisition_vm
                         {
                             pur_requisition_id = a.pur_requisition_id,
                             pr_date = a.pur_requisition_number + " - " + DateTime.Parse(a.pur_requisition_date.ToString()).ToString("dd/MMM/yyyy"),
                         }).ToList().OrderByDescending(a=>a.pur_requisition_id).ToList();
            return query;
        }

        public List<pur_req_stock> GetItemStock(int id)
        {
            var query = (from iv in _scifferContext.inv_item_inventory.Where(a => a.item_id == id)
                         join p in _scifferContext.REF_PLANT on iv.plant_id equals p.PLANT_ID
                         join s in _scifferContext.REF_STORAGE_LOCATION on iv.sloc_id equals s.storage_location_id
                         group iv by new { p.PLANT_NAME, s.storage_location_name } into g
                         select new pur_req_stock
                         {
                             plant_name = g.Key.PLANT_NAME,
                             sloc_name = g.Key.storage_location_name,
                             cu_stock = g.Sum(x => x.cu_stock_blocked + x.cu_stock_free + x.cu_stock_quality + x.cu_stock_transit),
                         }).ToList();
            return query;
        }
        public List<pur_req_detail_vm> GetPurRquisitionList(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpurRequisitionforPO");
            var val = _scifferContext.Database.SqlQuery<pur_req_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public pur_req_report_vm GetPurRequisitionReport(int id)
        {
            var val = (from p in _scifferContext.pur_requisition.Where(x => x.pur_requisition_id == id)
                       join d in _scifferContext.ref_document_numbring on p.category_id equals d.document_numbring_id
                       join pl in _scifferContext.REF_PLANT on p.plant_id equals pl.PLANT_ID
                       join s in _scifferContext.REF_STATE on pl.PLANT_STATE equals s.STATE_ID
                       from co in _scifferContext.REF_COMPANY
                       select new
                       {
                           companyname = co.COMPANY_DISPLAY_NAME.ToUpper(),
                           doccategory = d.category,
                           docnum = p.pur_requisition_number,
                           plantaddress = pl.PLANT_ADDRESS,//+ ',' + pl.PLANT_CITY + ',' + s.STATE_NAME + '-' + pl.pincode + ',' + s.REF_COUNTRY.COUNTRY_NAME
                           plantcity = pl.PLANT_CITY,
                           plantstate = s.STATE_NAME,
                           pincode = pl.pincode,
                           country = s.REF_COUNTRY.COUNTRY_NAME,
                           plantname = pl.PLANT_CODE,

                           posting_date = p.pur_requisition_date,
                           purreqid = p.pur_requisition_id,
                           purreqsource = p.source_id == 1 ? "Manual" : "MRP",
                           gst_no = pl.gst_number,
                           plant_mobile = pl.PLANT_TELEPHONE,
                           plant_email = pl.PLANT_EMAIL,
                           remakrs=p.internal_remarks,
                           approval_status = p.approval_status==null? 0 : p.approval_status,
                           PLANT_CODE = pl.PLANT_CODE,
                       }).ToList().Select(a => new pur_req_report_vm
                       {
                           companyname = a.companyname,
                           doccategory = a.doccategory,
                           docnum = a.docnum,
                           gst_no = a.gst_no,
                           plantname = a.plantname + ',' + a.plantaddress + ',' + a.plantcity + ','+ a.pincode + ',' + a.plantstate + '-' + a.country,
                           postingdate = DateTime.Parse(a.posting_date.ToString()).ToString("dd-MM-yyyy"),
                           purreqid = a.purreqid,
                           purreqsource = a.purreqsource,
                           plant_mobile = "Phone : " + "+91 831 " + a.plant_mobile,
                           plant_email = "Email :" + a.plant_email,
                           remarks=a.remakrs,
                           approval_status=a.approval_status,
                           PLANT_CODE = a.PLANT_CODE,
                       }).FirstOrDefault();

            string[] telephones = val.plant_mobile.Split(',');

            foreach (var phone in telephones)
            {
                if (phone.Contains("Phone"))
                    val.plant_mobile = phone;
                else
                    val.plant_mobile += ", +91 831" + phone;
            }

            return val;
        }
        public List<pur_req_report_detail_vm> GetPurRequisitionDetailReport(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "getpurreqdetailreport");
            var val = _scifferContext.Database.SqlQuery<pur_req_report_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }

         public List<pur_requisition_detail_vm> GetPRDetails(int plant_id,string entity)
        {
            var plant = new SqlParameter("@plant_id", plant_id);
            var entitys = new SqlParameter("@entity", entity);
            var list = _scifferContext.Database.SqlQuery<pur_requisition_detail_vm>(
            "exec GetPRDetails @plant_id,@entity", plant, entitys).ToList();
            return list;
        }
        
        public List<pur_req_detail_vm> GetPurRquisitionDetails(string entity,int id)
        {
            var ids = new SqlParameter("@id", id);
            var entitys= new SqlParameter("@entity", entity);
            var val = _scifferContext.Database.SqlQuery<pur_req_detail_vm>(
            "exec GetBalanceQuantity @entity,@id", entitys, ids).ToList();
            var list = (from pur in val
                        select new
                        {
                            pur_requisition_detail_id = pur.pur_requisition_detail_id,
                            pur_requisition_id = pur.pur_requisition_id,
                            uom_id = pur.uom_id,
                            uom_name = pur.uom_name,
                            unit_price = pur.unit_price,
                            info_price = pur.info_price,
                            net_value = pur.net_value,
                            storage_location_id = pur.storage_location_id,
                            storage_location_name = pur.storage_location_name,
                            vendor_id = pur.vendor_id,
                            vendor_name = pur.vendor_name,
                            vendor_code = pur.vendor_code,
                            item_id = pur.item_id,
                            item_name = pur.item_name,
                            item_names = pur.item_names,
                            item_code = pur.item_code,
                            delivery_dates = pur.delivery_dates,
                            delivery_date = pur.delivery_date,
                            quantity = pur.quantity,
                            balance_quantity = pur.balance_quantity,
                            plant_id = pur.plant_id,
                            freight_terms_id = pur.freight_terms_id,
                            business_unit_id = pur.business_unit_id,
                            plant_name = pur.plant_name,
                            business_unit_name = pur.business_unit_name,
                            freight_terms_name = pur.freight_terms_name,
                            hsn_id = pur.hsn_id,                       
                            pur_requisition_number = pur.pur_requisition_number+"/"+pur.pur_requisition_date,
                            discount=pur.discount,
                            user_description = pur.user_description,
                        }
                        ).ToList().Select(a => new pur_req_detail_vm
                        {
                            pur_requisition_detail_id = a.pur_requisition_detail_id,
                            pur_requisition_id = a.pur_requisition_id,
                            uom_id = a.uom_id,
                            uom_name = a.uom_name,
                            unit_price = a.unit_price,
                            info_price = a.info_price,
                            net_value = a.net_value,
                            storage_location_id = a.storage_location_id,
                            storage_location_name = a.storage_location_name,
                            vendor_id = a.vendor_id,
                            vendor_name = a.vendor_name,
                            vendor_code = a.vendor_code,
                            item_id = a.item_id,
                            item_name = a.item_name,
                            item_names = a.item_names,
                            item_code = a.item_code,
                            delivery_dates = a.delivery_dates,
                            delivery_date = a.delivery_date,
                            quantity = a.quantity,
                            balance_quantity = a.balance_quantity,
                            plant_id = a.plant_id,
                            freight_terms_id = a.freight_terms_id,
                            business_unit_id = a.business_unit_id,
                            plant_name = a.plant_name,
                            business_unit_name = a.business_unit_name,
                            freight_terms_name = a.freight_terms_name,
                            hsn_id = a.hsn_id,
                            pur_requisition_number = a.pur_requisition_number,
                            discount = a.discount,
                            hsn_sac = _genericService.Get_hsn_saclist(a.item_id),
                            user_description = a.user_description
                        }).ToList();
            return list;
        }
        public List<pur_requisition_vm> GetPendigApprovedList()
        {
            int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
            var quotation_id = new SqlParameter("@id", createdby);
            var entity = new SqlParameter("@entity", "getprforapproval");
            var val = _scifferContext.Database.SqlQuery<pur_requisition_vm>(
            "exec get_purchase_order @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public bool ChangeApprovedStatus(pur_requisition_vm vm)
        {
            try
            {
                var data = _scifferContext.pur_requisition.Where(x => x.pur_requisition_id == vm.pur_requisition_id).FirstOrDefault();
                var status = _scifferContext.ref_status.Where(a => a.status_name == "Cancelled" && a.form == "PO").FirstOrDefault();
                data.approval_status = vm.approval_status;
                data.approval_comments = vm.approval_comments;
                data.approved_by = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                data.approved_ts = DateTime.Now;
                if (vm.approval_status == 2)
                {
                    data.status_id = status.status_id;
                    data.order_status = true;
                }

                _scifferContext.Entry(data).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public string Close(int? id, string closed_remarks)
        {
            try
            {
                int createdby = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var status = _scifferContext.ref_status.Where(a => a.status_name == "Closed" && a.form == "PRCRQ").FirstOrDefault(); ;
                var st = "update [dbo].[pur_requisition] set [order_status] = 1 ,[status_id]=" + status.status_id + " , [closed_remarks] = '" + closed_remarks + "', [closed_by] = " + createdby + ", [closed_ts] = '" + DateTime.Now + "' where pur_requisition_id = " + id;
                //var st = "update [dbo].[pur_requisition] set [order_status] = 1 ,[status_id] = 6 , [closed_remarks] = '" + closed_remarks + "', [closed_by] = " + createdby + ", [closed_ts] = '" + DateTime.Now + "' where pur_requisition_id = " + id;

                _scifferContext.Database.ExecuteSqlCommand(st);
                _scifferContext.Database.ExecuteSqlCommand("update [dbo].[pur_requisition_detail] set [order_status] = 1 where pur_requisition_id = " + id);
                return "Closed";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public string PurchaseRequisitionupdatestatusseen()
        {
            try
            {
                int user_id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var op = _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id).FirstOrDefault();
                var role = _scifferContext.ref_user_management_role.Where(x => x.role_id == op.role_id).FirstOrDefault();
                //if(op.role_id == role.role_id)
                if (role.role_code== "STO_EXEC"|| role.role_code == "PUR_EXEC"|| role.role_code =="IT_ADMIN")

                {
                    var purchaseReqDetail = _scifferContext.pur_requisition.Where(x => x.is_seen == false).ToList();
                    foreach (var item in purchaseReqDetail)
                    {
                       item.is_seen = true;
                        _scifferContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                _scifferContext.SaveChanges();                   
                }
              
                return "true";
            }
            
            catch (Exception)
            {
                return "false";
            }
        }
    }
}
