using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using System.Data.Entity;
using Sciffer.Erp.Domain.ViewModel;
using AutoMapper;
using System.Data.SqlClient;
using System.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class ItemService : IItemService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public ItemService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _scifferContext = scifferContext;
            _genericService = GenericService;
        }
        public string Add(Ref_item_VM item)
        {
            try
            {
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = item.ledgeraccounttype.Split(new string[] { "~" }, StringSplitOptions.None);
                }
                catch
                {

                }


                DataTable dt = new DataTable();
                dt.Columns.Add("entity_type_id", typeof(int));
                dt.Columns.Add("gl_ledger_id", typeof(int));
                dt.Columns.Add("ledger_account_type_id", typeof(int));
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    dt.Rows.Add(4, int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]), int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]));
                }

                DataTable t11 = new DataTable();
                t11.Columns.Add("item_category_id", typeof(int));
                t11.Columns.Add("rate", typeof(double));
                if (item.ref_item_alternate_UOM != null)
                {
                    foreach (var i in item.ref_item_alternate_UOM)
                    {
                        t11.Rows.Add(i.uom_id, i.conversion_rate);
                    }
                }

                DataTable dtp = new DataTable();
                dtp.Columns.Add("parameter_id", typeof(string));
                dtp.Columns.Add("parameter_name", typeof(string));
                dtp.Columns.Add("parameter_value", typeof(string));
                if (item.ref_item_parameter != null)
                {
                    foreach (var i in item.ref_item_parameter)
                    {
                        dtp.Rows.Add(i.parameter_id, i.parameter_name, i.parameter_range);
                    }
                }
                DataTable dtp1 = new DataTable();
                dtp1.Columns.Add("plant_id", typeof(int));
                dtp1.Columns.Add("item_value", typeof(double));
                for (var i = 0; i < item.plant_id.Count; i++)
                {
                    if (item.plant_id[i] != "")
                    {
                        dtp1.Rows.Add(Convert.ToInt32(item.plant_id[i]), Convert.ToDouble(item.item_value[i]));
                    }

                }


                DataTable dt5 = new DataTable();
                //Detail Table
                dt5.Columns.Add("child_item_id ", typeof(int));

                if(item.child_item_list_id != null)
                {
                    var chil_itemlist = item.child_item_list_id.Split(',');
                    if (chil_itemlist.Length > 0)
                    {
                        for (int i = 0; i < chil_itemlist.Length; i++) //Detail Table
                        {
                            if (chil_itemlist[i] != "")
                            {
                                dt5.Rows.Add(chil_itemlist[i]);
                            }

                        }
                    }
                }
              
                var t5 = new SqlParameter("@t5", SqlDbType.Structured);
                t5.TypeName = "dbo.temp_child_item_id_list";
                t5.Value = dt5;

                var entity = new SqlParameter("@entity", "save");
                var item_id = new SqlParameter("@item_id", item.ITEM_ID == null ? 0 : item.ITEM_ID);
                var item_name = new SqlParameter("@item_name", item.ITEM_NAME);
                var item_code = new SqlParameter("@item_code", item.ITEM_CODE==null ? "" : item.ITEM_CODE);
                var item_category_id = new SqlParameter("@item_category_id", item.ITEM_CATEGORY_ID);
                var item_group_id = new SqlParameter("@item_group_id", item.ITEM_GROUP_ID);
                var brand_id = new SqlParameter("@brand_id", item.BRAND_ID == null ? 0 : item.BRAND_ID);
                var uom_id = new SqlParameter("@uom_id", item.UOM_ID);
                var item_length = new SqlParameter("@item_length", item.ITEM_LENGTH == null ? 0 : item.ITEM_LENGTH);
                var item_lenght_uom_id = new SqlParameter("@item_lenght_uom_id", item.ITEM_LENGHT_UOM_ID == null ? 0 : item.ITEM_LENGHT_UOM_ID);
                var item_width = new SqlParameter("@item_width", item.ITEM_WIDTH == null ? 0 : item.ITEM_WIDTH);
                var item_width_uom_id = new SqlParameter("@item_width_uom_id", item.ITEM_WIDTH_UOM_ID == null ? 0 : item.ITEM_WIDTH_UOM_ID);
                var item_height = new SqlParameter("@item_height", item.ITEM_HEIGHT == null ? 0 : item.ITEM_HEIGHT);
                var item_height_uom_id = new SqlParameter("@item_height_uom_id", item.ITEM_HEIGHT_UOM_ID == null ? 0 : item.ITEM_HEIGHT_UOM_ID);
                var item_volume = new SqlParameter("@item_volume", item.ITEM_VOLUME == null ? 0 : item.ITEM_VOLUME);
                var item_volume_uom_id = new SqlParameter("@item_volume_uom_id", item.ITEM_VOLUME_UOM_ID == null ? 0 : item.ITEM_VOLUME_UOM_ID);
                var item_weight = new SqlParameter("@item_weight", item.ITEM_WEIGHT == null ? 0 : item.ITEM_WEIGHT);
                var item_weight_uom_id = new SqlParameter("@item_weight_uom_id", item.ITEM_WEIGHT_UOM_ID == null ? 0 : item.ITEM_WEIGHT_UOM_ID);
                var priority_id = new SqlParameter("@priority_id", item.PRIORITY_ID == null ? 0 : item.PRIORITY_ID);
                var is_blocked = new SqlParameter("@is_blocked", item.IS_BLOCKED);
                var created_on = new SqlParameter("@created_on", item.CREATED_ON);
                var created_by = new SqlParameter("@created_by", 1);
                var quality_managed = new SqlParameter("@quality_managed", item.QUALITY_MANAGED);
                var batch_managed = new SqlParameter("@batch_managed", item.BATCH_MANAGED);
                var shelf_life = new SqlParameter("@shelf_life", item.SHELF_LIFE == null ? 0 : item.SHELF_LIFE);
                var shelf_life_uom_id = new SqlParameter("@shelf_life_uom_id", item.SHELF_LIFE_UOM_ID == null ? 0 : item.SHELF_LIFE_UOM_ID);
                var preferred_vendor_id = new SqlParameter("@preferred_vendor_id", item.PREFERRED_VENDOR_ID == null ? 0 : item.PREFERRED_VENDOR_ID);
                var vendor_part_number = new SqlParameter("@vendor_part_number", item.VENDOR_PART_NUMBER == null ? "" : item.VENDOR_PART_NUMBER);
                var mrp = new SqlParameter("@mrp", item.MRP);
                var minimum_level = new SqlParameter("@minimum_level", item.MINIMUM_LEVEL == null ? 0 : item.MAXIMUM_LEVEL);
                var maximum_level = new SqlParameter("@maximum_level", item.MAXIMUM_LEVEL == null ? 0 : item.MAXIMUM_LEVEL);
                var reorder_level = new SqlParameter("@reorder_level", item.REORDER_LEVEL == null ? 0 : item.REORDER_LEVEL);
                var reorder_quantity = new SqlParameter("@reorder_quantity", item.REORDER_QUANTITY == null ? 0 : item.REORDER_QUANTITY);
                var item_valuation_id = new SqlParameter("@item_valuation_id", item.ITEM_VALUATION_ID);
                var item_accounting_id = new SqlParameter("@item_accounting_id", item.ITEM_ACCOUNTING_ID);
                var excise_category_id = new SqlParameter("@excise_category_id", item.EXCISE_CATEGORY_ID == null ? 0 : item.EXCISE_CATEGORY_ID);
                var excise_chapter_no = new SqlParameter("@excise_chapter_no", item.EXCISE_CHAPTER_NO == null ? 0 : item.EXCISE_CHAPTER_NO);
                var additional_information = new SqlParameter("@additional_information", item.ADDITIONAL_INFORMATION == null ? "" : item.ADDITIONAL_INFORMATION);
                var item_type_id = new SqlParameter("@item_type_id", item.item_type_id);
                var auto_batch = new SqlParameter("@auto_batch", item.auto_batch == null ? 0 : item.auto_batch);
                var standard_cost_value = new SqlParameter("@standard_cost_value", item.standard_cost_value == null ? 0 : item.standard_cost_value);
                var deleteparameter = new SqlParameter("@deleteparameter", item.deleteparameter == null ? string.Empty : item.deleteparameter);
                var tag_managed = new SqlParameter("@tag_managed", item.tag_managed);
                var user_description = new SqlParameter("@user_description", item.user_description == null ? string.Empty : item.user_description);
                var gst_applicability_id = new SqlParameter("@gst_applicability_id", item.gst_applicability_id == null ? 0 : item.gst_applicability_id);
                var sac_id = new SqlParameter("@sac_id", item.sac_id == null ? 0 : item.sac_id);
                var rack_no = new SqlParameter("@rack_no", item.rack_no == null ? "" : item.rack_no);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                if (item.FileUpload != null)
                {
                    item.Attachment = _genericService.GetFilePath("ITEM", item.FileUpload);
                }
                else
                {
                    item.Attachment = "No File";
                }
                var attachment = new SqlParameter("@attachment", item.Attachment);


                t1.TypeName = "dbo.temp_cus_ven_item_category";
                t1.Value = t11;
                var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                t2.TypeName = "dbo.temp_sub_ledger";
                t2.Value = dt;

                var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                t3.TypeName = "dbo.temp_item_parameter";
                t3.Value = dtp;

                var t4 = new SqlParameter("@t4", SqlDbType.Structured);
                t4.TypeName = "dbo.temp_item_plant_valuation";
                t4.Value = dtp1;

                var val = _scifferContext.Database.SqlQuery<string>(
                  "exec save_item @entity,@item_id,@item_name ,@item_code ,@item_category_id ,@item_group_id ,@brand_id ,@uom_id ,@item_length ,@item_lenght_uom_id ,@item_width,@item_width_uom_id ,@item_height ,@item_height_uom_id ,@item_volume ,@item_volume_uom_id ,@item_weight ,@item_weight_uom_id ,@priority_id ,@is_blocked ,@created_on ,@created_by ,@quality_managed ,@batch_managed ,@shelf_life ,@shelf_life_uom_id ,@preferred_vendor_id ,@vendor_part_number ,@mrp ,@minimum_level ,@maximum_level,@reorder_level, @reorder_quantity, @item_valuation_id, @item_accounting_id, @excise_category_id,@excise_chapter_no ,@additional_information ,@item_type_id,@auto_batch,@standard_cost_value,@deleteparameter,@tag_managed,@user_description,@gst_applicability_id,@sac_id,@rack_no, @t1,@t2,@t3,@t4,@t5", entity, item_id,
                   item_name, item_code, item_category_id, item_group_id, brand_id, uom_id, item_length, item_lenght_uom_id, item_width, item_width_uom_id, item_height,
                  item_height_uom_id, item_volume, item_volume_uom_id, item_weight, item_weight_uom_id, priority_id, is_blocked, created_on, created_by, quality_managed, batch_managed, shelf_life, shelf_life_uom_id, preferred_vendor_id, vendor_part_number, mrp, minimum_level, maximum_level, reorder_level,
                   reorder_quantity, item_valuation_id, item_accounting_id, excise_category_id, excise_chapter_no, additional_information, item_type_id, auto_batch, standard_cost_value, deleteparameter, tag_managed, user_description, gst_applicability_id, sac_id, rack_no, t1, t2, t3, t4,t5).FirstOrDefault();
                if (val == "Saved")
                {
                    return val;
                }
                else
                {
                    return val;
                }

            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
            //return true;
        }

        public REF_ITEM Create()
        {
            throw new NotImplementedException();
        }
        public List<ref_item_plant_vm> GetItemPlantValuation(int id)
        {
            var query = (from p in _scifferContext.REF_PLANT
                         join iv in _scifferContext.ref_item_plant_valuation.Where(x => x.item_id == id) on p.PLANT_ID equals iv.plant_id into j1
                         from i in j1.DefaultIfEmpty()
                         select new ref_item_plant_vm
                         {
                             // item_id=i==null?0:i.item_id,
                             plant_id = p.PLANT_ID,
                             plant_name = p.PLANT_CODE + "-" + p.PLANT_NAME,
                             item_value = i == null ? 0 : i.item_value,
                         }).ToList();
            return query;
        }
        public bool Delete(int id)
        {
            try
            {
                List<ref_item_alternate_UOM> taskd = _scifferContext.ref_item_alternate_UOM.Where(x => x.item_id == id).ToList();
                foreach (ref_item_alternate_UOM t in taskd)
                {
                    _scifferContext.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                }

                List<ref_item_parameter> par = _scifferContext.ref_item_parameter.Where(x => x.item_id == id).ToList();
                foreach (ref_item_parameter e in par)
                {
                    _scifferContext.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }

                var item = _scifferContext.REF_ITEM.FirstOrDefault(c => c.ITEM_ID == id);
                item.is_active = false;
                _scifferContext.Entry(item).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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

        public Ref_item_VM Get(int id)
        {
            REF_ITEM JR = _scifferContext.REF_ITEM.FirstOrDefault(c => c.ITEM_ID == id);

            var ent = new SqlParameter("@entity", "getchilditem");           
            var item_id = new SqlParameter("@item_id",  id);
           
            Mapper.CreateMap<REF_ITEM, Ref_item_VM>();
            Ref_item_VM JRVM = Mapper.Map<REF_ITEM, Ref_item_VM>(JR);
            JRVM.child_item_list_id = _scifferContext.Database.SqlQuery<string>("exec get_edit_detail_for_itemmaster @entity,@item_id", ent, item_id).FirstOrDefault();
            JRVM.ref_item_alternate_UOM = JRVM.ref_item_alternate_UOM.ToList();
            JRVM.ref_item_parameter = JRVM.ref_item_parameter.Where(x => x.is_active == true).ToList();
            
            return JRVM;
        }

        public List<REF_ITEM> GetAll()
        {
            return _scifferContext.REF_ITEM.ToList();
        }

        public List<item_list> GetItems()
        {
            var List = (from ed in _scifferContext.REF_ITEM.Where(x => x.is_active == true)
                        join q in _scifferContext.REF_ITEM_CATEGORY on ed.ITEM_CATEGORY_ID equals q.ITEM_CATEGORY_ID
                        join w in _scifferContext.REF_ITEM_GROUP on ed.ITEM_GROUP_ID equals w.ITEM_GROUP_ID
                        join cp in _scifferContext.REF_BRAND on ed.BRAND_ID equals cp.BRAND_ID into j1
                        from cpr in j1.DefaultIfEmpty()
                        join v in _scifferContext.REF_VENDOR on ed.PREFERRED_VENDOR_ID equals v.VENDOR_ID into j2
                        from vendor in j2.DefaultIfEmpty()
                        join r in _scifferContext.REF_UOM on ed.UOM_ID equals r.UOM_ID
                        join t in _scifferContext.REF_PRIORITY on ed.PRIORITY_ID equals t.PRIORITY_ID into j3
                        from p in j3.DefaultIfEmpty()
                        join a in _scifferContext.REF_EXCISE_CATEGORY on ed.EXCISE_CATEGORY_ID equals a.EXCISE_CATEGORY_ID into j4
                        from a1 in j4.DefaultIfEmpty()
                        join ex in _scifferContext.ref_hsn_code on ed.EXCISE_CHAPTER_NO equals ex.hsn_code_id into j5
                        from ex1 in j5.DefaultIfEmpty()
                        join itemvaluation in _scifferContext.REF_ITEM_VALUATION on ed.ITEM_VALUATION_ID equals itemvaluation.ITEM_VALUATION_ID into itemvaluation1
                        from itemvaluation2 in itemvaluation1.DefaultIfEmpty()
                        join itemaccount in _scifferContext.REF_ITEM_ACCOUNTING on ed.ITEM_ACCOUNTING_ID equals itemaccount.ITEM_ACCOUNTING_ID into itemaccount1
                        from itemaccount2 in itemaccount1.DefaultIfEmpty()
                        join hsncategory in _scifferContext.REF_EXCISE_CATEGORY on ed.EXCISE_CATEGORY_ID equals hsncategory.EXCISE_CATEGORY_ID into hsncategory1
                        from hsncategory2 in hsncategory1.DefaultIfEmpty()
                        join hsn in _scifferContext.ref_hsn_code on ed.EXCISE_CHAPTER_NO equals hsn.hsn_code_id into hsn1
                        from hsn2 in hsn1.DefaultIfEmpty()
                        join ga in _scifferContext.ref_gst_applicability on ed.gst_applicability_id equals ga.gst_applicability_id into j6
                        from gaa in j6.DefaultIfEmpty()
                        join sa in _scifferContext.ref_sac on ed.sac_id equals sa.sac_id into j7
                        from saa in j7.DefaultIfEmpty()
                        select new item_list
                        {
                            gst_applicability_name=gaa==null?string.Empty:gaa.gst_applicability_name,
                            sac_name=saa==null?string.Empty:saa.sac_code + "/" + saa.sac_description,
                            EXCISE_CATEGORY_NAME = a1 == null ? string.Empty : a1.EXCISE_CATEGORY_NAME,
                            ITEM_GROUP_NAME = w.ITEM_GROUP_NAME ?? "",
                            ITEM_CATEGORY_NAME = q.ITEM_CATEGORY_NAME ?? "",
                            BRAND_NAME = (cpr == null ? String.Empty : cpr.BRAND_NAME),
                            UOM_NAME = r.UOM_NAME ?? "",
                            PRIORITY_NAME = p.PRIORITY_NAME == null ? "" : p.PRIORITY_NAME,
                            ITEM_ID = (int?)ed.ITEM_ID ?? 0,
                            ITEM_NAME = ed.ITEM_NAME ?? "",
                            ITEM_CODE = ed.ITEM_CODE ?? "",
                            ITEM_CATEGORY_ID = (int?)ed.ITEM_CATEGORY_ID ?? 0,
                            ITEM_GROUP_ID = (int?)ed.ITEM_GROUP_ID ?? 0,
                            BRAND_ID = (int?)ed.BRAND_ID ?? 0,
                            ITEM_LENGTH = (int?)ed.ITEM_LENGTH ?? 0,
                            ITEM_WIDTH = (int?)ed.ITEM_WIDTH ?? 0,
                            ITEM_HEIGHT = (int?)ed.ITEM_HEIGHT ?? 0,
                            ITEM_VOLUME = (int?)ed.ITEM_VOLUME ?? 0,
                            ITEM_WEIGHT = ed.ITEM_WEIGHT,
                            PRIORITY_ID = (int?)ed.PRIORITY_ID ?? 0,
                            IS_BLOCKED = (bool?)ed.IS_BLOCKED ?? true,
                            CREATED_ON = ed.CREATED_ON,
                            CREATED_BY = (int?)ed.CREATED_BY ?? 0,
                            QUALITY_MANAGED = (bool?)ed.QUALITY_MANAGED ?? true,
                            BATCH_MANAGED = (bool?)ed.BATCH_MANAGED ?? true,
                            SHELF_LIFE = (int?)ed.SHELF_LIFE ?? 0,
                            VENDOR_PART_NUMBER = ed.VENDOR_PART_NUMBER ?? "",
                            MRP = (bool?)ed.MRP ?? true,
                            MINIMUM_LEVEL = (int?)ed.MINIMUM_LEVEL ?? 0,
                            MAXIMUM_LEVEL = (int?)ed.MAXIMUM_LEVEL ?? 0,
                            REORDER_LEVEL = (int?)ed.REORDER_LEVEL ?? 0,
                            REORDER_QUANTITY = (int?)ed.REORDER_QUANTITY ?? 0,
                            EXCISE_CHAPTER_NO = ex1 == null ? string.Empty : ex1.hsn_code + "-" + ex1.hsn_code_description,
                            ADDITIONAL_INFORMATION = ed.ADDITIONAL_INFORMATION ?? "",
                            vendor_name = (vendor == null ? String.Empty : vendor.VENDOR_NAME),
                            itemtype_name = ed.item_type_id == 1 ? "Inventory" : ed.item_type_id == 2 ? "Service" : ed.item_type_id == 3 ? "Asset " : "",
                            itemvaluation_name = itemvaluation2.ITEM_VALUATION_NAME,
                            itemaccount_name = itemaccount2.ITEM_ACCOUNTING_NAME,
                            hsncategory_name = hsncategory2.EXCISE_CATEGORY_NAME,
                            hsn_name = hsn2.hsn_code,
                            user_description = ed.user_description,
                            rack_no=ed.rack_no,
                        }).OrderByDescending(a => a.ITEM_ID).ToList();
            return List;

        }

        public List<ItemVM> GetItemList()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true)
                         select new ItemVM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_CODE = i.ITEM_CODE + "-" + i.ITEM_NAME,
                             ITEM_NAME = i.ITEM_NAME,

                         }).ToList();
            return query;

        }
        //public List<ItemVM> GetTagManagedItemList()
        //{
        //    var query = (from i in _scifferContext.REF_ITEM//.Where(x => x.is_active == true && x.ITEM_GROUP_ID == 23)
        //                 join q in _scifferContext.REF_ITEM_CATEGORY.Where(a=>a.ITEM_CATEGORY_NAME == "RM") on i.ITEM_CATEGORY_ID equals q.ITEM_CATEGORY_ID
        //                 select new ItemVM
        //                 {
        //                     ITEM_ID = i.ITEM_ID,
        //                     ITEM_CODE = i.ITEM_CODE + "-" + i.ITEM_NAME,
        //                     ITEM_NAME = i.ITEM_NAME,

        //                 }).ToList();
        //    return query;

        //}

        public List<ItemVM> GetTagManagedItemList()
        {
            
            var val = _scifferContext.Database.SqlQuery<ItemVM>(
            "exec GetTagManagedItemList").ToList();
            return val;
        }
        public Ref_item_VM GetItemsDetail(int id)
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == id)
                         select new Ref_item_VM
                         {
                             BATCH_MANAGED = i.BATCH_MANAGED,
                             auto_batch = i.auto_batch,
                             QUALITY_MANAGED = i.QUALITY_MANAGED,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                             ITEM_CATEGORY_NAME = i.REF_ITEM_CATEGORY.ITEM_CATEGORY_NAME,
                             tag_managed = i.tag_managed,
                         }).FirstOrDefault();
            return query;
        }
        public bool Update(Ref_item_VM item)
        {
            try
            {

                REF_ITEM re = new REF_ITEM();
                re.ITEM_ID = item.ITEM_ID;
                re.ITEM_NAME = item.ITEM_NAME;
                re.ITEM_CODE = item.ITEM_CODE;
                re.ITEM_CATEGORY_ID = item.ITEM_CATEGORY_ID;
                re.ITEM_GROUP_ID = item.ITEM_GROUP_ID;
                re.BRAND_ID = item.BRAND_ID;
                re.UOM_ID = item.UOM_ID;
                re.ITEM_LENGTH = item.ITEM_LENGTH;
                re.ITEM_LENGHT_UOM_ID = item.ITEM_LENGHT_UOM_ID;
                re.ITEM_WIDTH = item.ITEM_WIDTH;
                re.ITEM_WIDTH_UOM_ID = item.ITEM_WIDTH_UOM_ID;
                re.ITEM_HEIGHT = item.ITEM_HEIGHT;
                re.ITEM_HEIGHT_UOM_ID = item.ITEM_HEIGHT_UOM_ID;
                re.ITEM_VOLUME = item.ITEM_VOLUME;
                re.ITEM_VOLUME_UOM_ID = item.ITEM_VOLUME_UOM_ID;
                re.ITEM_WEIGHT = item.ITEM_WEIGHT;
                re.ITEM_WEIGHT_UOM_ID = item.ITEM_WEIGHT_UOM_ID;
                re.PRIORITY_ID = item.PRIORITY_ID;
                re.IS_BLOCKED = item.IS_BLOCKED;
                re.CREATED_ON = item.CREATED_ON;
                re.CREATED_BY = item.CREATED_BY;
                re.QUALITY_MANAGED = item.QUALITY_MANAGED;
                re.BATCH_MANAGED = item.BATCH_MANAGED;
                re.SHELF_LIFE = item.SHELF_LIFE;
                re.SHELF_LIFE_UOM_ID = item.SHELF_LIFE_UOM_ID;
                re.PREFERRED_VENDOR_ID = item.PREFERRED_VENDOR_ID;
                re.VENDOR_PART_NUMBER = item.VENDOR_PART_NUMBER;
                re.MRP = item.MRP;
                re.MINIMUM_LEVEL = item.MINIMUM_LEVEL;
                re.MAXIMUM_LEVEL = item.MAXIMUM_LEVEL;
                re.REORDER_LEVEL = item.REORDER_LEVEL;
                re.REORDER_QUANTITY = item.REORDER_QUANTITY;
                re.ITEM_VALUATION_ID = item.ITEM_VALUATION_ID;
                re.ITEM_ACCOUNTING_ID = item.ITEM_ACCOUNTING_ID;
                re.EXCISE_CATEGORY_ID = item.EXCISE_CATEGORY_ID;
                re.EXCISE_CHAPTER_NO = item.EXCISE_CHAPTER_NO;
                re.ADDITIONAL_INFORMATION = item.ADDITIONAL_INFORMATION;
                re.item_type_id = item.item_type_id;
                re.is_active = true;
                string[] deleteStringArray = new string[0];
                try
                {
                    deleteStringArray = item.deleteids.Split(new char[] { '~' });
                }
                catch
                {

                }
                int pt_detail_id;
                for (int i = 0; i <= deleteStringArray.Count() - 1; i++)
                {
                    if (deleteStringArray[i] != "")
                    {
                        pt_detail_id = int.Parse(deleteStringArray[i]);
                        var pt_detail = _scifferContext.ref_item_alternate_UOM.Find(pt_detail_id);
                        _scifferContext.Entry(pt_detail).State = System.Data.Entity.EntityState.Deleted;
                        //pt_detail.journal_entry_item_is_active = false;
                    }
                }
                List<ref_item_alternate_UOM> allalternate = new List<ref_item_alternate_UOM>();
                if (item.ref_item_alternate_UOM != null)
                {
                    foreach (var alternates in item.ref_item_alternate_UOM)
                    {
                        ref_item_alternate_UOM alternate = new ref_item_alternate_UOM();
                        alternate.item_id = item.ITEM_ID;
                        if (alternates.alternate_uom_id == null)
                        {
                            alternates.alternate_uom_id = 0;
                        }
                        alternate.alternate_uom_id = alternates.alternate_uom_id;
                        //alternate.item_id = alternates.item_id;
                        alternate.uom_id = alternates.uom_id;
                        alternate.conversion_rate = alternates.conversion_rate;
                        allalternate.Add(alternate);
                    }

                }
                foreach (var i in allalternate)
                {
                    if (i.alternate_uom_id == 0)
                    {
                        _scifferContext.Entry(i).State = EntityState.Added;
                    }
                    else
                    {
                        _scifferContext.Entry(i).State = EntityState.Modified;
                    }
                }
                re.ref_item_alternate_UOM = allalternate;
                _scifferContext.Entry(re).State = EntityState.Modified;
                _scifferContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public int GetID(string st)
        {
            return _scifferContext.REF_ITEM.Where(x => x.ITEM_CODE == st && x.is_active==true).FirstOrDefault().ITEM_ID;
        }
        public string AddExcel(List<item_list> Item, List<Uom_Excel> uom, List<item_category_gl_Excel> gl, List<parameter_Excel> param, List<ref_item_plant_vm> stanCost)
        {
            try
            {
                var val = "";
                foreach (var item in Item)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("entity_type_id", typeof(int));
                    dt.Columns.Add("gl_ledger_id", typeof(int));
                    dt.Columns.Add("ledger_account_type_id", typeof(int));
                    if (gl != null)
                    {
                        foreach (var gls in gl)
                        {
                            if (item.ITEM_CODE == gls.itemCode)
                            {
                                dt.Rows.Add(4, gls.gl_ledger_id, gls.ledger_account_type_id);
                            }
                        }
                    }
                    DataTable t11 = new DataTable();
                    t11.Columns.Add("item_category_id", typeof(int));
                    t11.Columns.Add("rate", typeof(double));
                    if (uom != null)
                    {
                        foreach (var uoms in uom)
                        {
                            if (item.ITEM_CODE == uoms.itemCode)
                            {
                                t11.Rows.Add(uoms.uom_id, uoms.conversion_rate);

                            }
                        }
                    }

                    DataTable dtp = new DataTable();
                    dtp.Columns.Add("parameter_id", typeof(int));
                    dtp.Columns.Add("parameter_name", typeof(string));
                    dtp.Columns.Add("parameter_value", typeof(string));
                    if (param != null)
                    {
                        foreach (var parm in param)
                        {
                            if (item.ITEM_CODE == parm.itemCode)
                            {
                                dtp.Rows.Add(parm.parameter_id == null ? 0 : parm.parameter_id, parm.parameter_name, parm.parameter_range);
                            }
                        }
                    }
                    DataTable dtp1 = new DataTable();
                    dtp1.Columns.Add("plant_id", typeof(int));
                    dtp1.Columns.Add("item_value", typeof(double));
                    if (stanCost != null)
                    {
                        foreach (var stCost in stanCost)
                        {
                            if (item.ITEM_CODE == stCost.itemCode)
                            {
                                dtp1.Rows.Add(stCost.plant_id, stCost.item_value);
                            }

                        }
                    }
                    var entity = new SqlParameter("@entity", "save");
                    var item_id = new SqlParameter("@item_id", item.ITEM_ID == null ? 0 : item.ITEM_ID);
                    var item_name = new SqlParameter("@item_name", item.ITEM_NAME);
                    var item_code = new SqlParameter("@item_code", item.ITEM_CODE);
                    var item_category_id = new SqlParameter("@item_category_id", item.ITEM_CATEGORY_ID);
                    var item_group_id = new SqlParameter("@item_group_id", item.ITEM_GROUP_ID);
                    var brand_id = new SqlParameter("@brand_id", item.BRAND_ID == null ? 0 : item.BRAND_ID);
                    var uom_id = new SqlParameter("@uom_id", item.UOM_ID);
                    var item_length = new SqlParameter("@item_length", item.ITEM_LENGTH == null ? 0 : item.ITEM_LENGTH);
                    var item_lenght_uom_id = new SqlParameter("@item_lenght_uom_id", item.ITEM_LENGHT_UOM_ID == null ? 0 : item.ITEM_LENGHT_UOM_ID);
                    var item_width = new SqlParameter("@item_width", item.ITEM_WIDTH == null ? 0 : item.ITEM_WIDTH);
                    var item_width_uom_id = new SqlParameter("@item_width_uom_id", item.ITEM_WIDTH_UOM_ID == null ? 0 : item.ITEM_WIDTH_UOM_ID);
                    var item_height = new SqlParameter("@item_height", item.ITEM_HEIGHT == null ? 0 : item.ITEM_HEIGHT);
                    var item_height_uom_id = new SqlParameter("@item_height_uom_id", item.ITEM_HEIGHT_UOM_ID == null ? 0 : item.ITEM_HEIGHT_UOM_ID);
                    var item_volume = new SqlParameter("@item_volume", item.ITEM_VOLUME == null ? 0 : item.ITEM_VOLUME);
                    var item_volume_uom_id = new SqlParameter("@item_volume_uom_id", item.ITEM_VOLUME_UOM_ID == null ? 0 : item.ITEM_VOLUME_UOM_ID);
                    var item_weight = new SqlParameter("@item_weight", item.ITEM_WEIGHT == null ? 0 : item.ITEM_WEIGHT);
                    var item_weight_uom_id = new SqlParameter("@item_weight_uom_id", item.ITEM_WEIGHT_UOM_ID == null ? 0 : item.ITEM_WEIGHT_UOM_ID);
                    var priority_id = new SqlParameter("@priority_id", item.PRIORITY_ID == null ? 0 : item.PRIORITY_ID);
                    var is_blocked = new SqlParameter("@is_blocked", item.IS_BLOCKED);
                    var created_on = new SqlParameter("@created_on", item.CREATED_ON);
                    var created_by = new SqlParameter("@created_by", 1);
                    var quality_managed = new SqlParameter("@quality_managed", item.QUALITY_MANAGED);
                    var batch_managed = new SqlParameter("@batch_managed", item.BATCH_MANAGED);
                    var shelf_life = new SqlParameter("@shelf_life", item.SHELF_LIFE == null ? 0 : item.SHELF_LIFE);
                    var shelf_life_uom_id = new SqlParameter("@shelf_life_uom_id", item.SHELF_LIFE_UOM_ID == null ? 0 : item.SHELF_LIFE_UOM_ID);
                    var preferred_vendor_id = new SqlParameter("@preferred_vendor_id", item.PREFERRED_VENDOR_ID == null ? 0 : item.PREFERRED_VENDOR_ID);
                    var vendor_part_number = new SqlParameter("@vendor_part_number", item.VENDOR_PART_NUMBER == null ? "" : item.VENDOR_PART_NUMBER);
                    var mrp = new SqlParameter("@mrp", item.MRP);
                    var minimum_level = new SqlParameter("@minimum_level", item.MINIMUM_LEVEL == null ? 0 : item.MAXIMUM_LEVEL);
                    var maximum_level = new SqlParameter("@maximum_level", item.MAXIMUM_LEVEL == null ? 0 : item.MAXIMUM_LEVEL);
                    var reorder_level = new SqlParameter("@reorder_level", item.REORDER_LEVEL == null ? 0 : item.REORDER_LEVEL);
                    var reorder_quantity = new SqlParameter("@reorder_quantity", item.REORDER_QUANTITY == null ? 0 : item.REORDER_QUANTITY);
                    var item_valuation_id = new SqlParameter("@item_valuation_id", item.ITEM_VALUATION_ID);
                    var item_accounting_id = new SqlParameter("@item_accounting_id", item.ITEM_ACCOUNTING_ID);
                    var excise_category_id = new SqlParameter("@excise_category_id", item.EXCISE_CATEGORY_ID);
                    var excise_chapter_no = new SqlParameter("@excise_chapter_no", item.EXCISE_CHAPTER_NO);
                    var additional_information = new SqlParameter("@additional_information", item.ADDITIONAL_INFORMATION == null ? "" : item.ADDITIONAL_INFORMATION);
                    var item_type_id = new SqlParameter("@item_type_id", item.item_type_id);
                    var auto_batch = new SqlParameter("@auto_batch", item.auto_batch == null ? 0 : item.auto_batch);
                    var standard_cost_value = new SqlParameter("@standard_cost_value", item.standard_cost_value == null ? 0 : item.standard_cost_value);
                    var deleteparameter = new SqlParameter("@deleteparameter", item.deleteparameter == null ? string.Empty : item.deleteparameter);
                    var tag_managed = new SqlParameter("@tag_managed", item.tag_managed == null ? 0 : 1);
                    var user_description = new SqlParameter("@user_description", item.user_description == null ? string.Empty : item.user_description);
                    var gst_applicability_id = new SqlParameter("@gst_applicability_id", item.gst_applicability_id == null ? 0 : item.gst_applicability_id);
                    var sac_id = new SqlParameter("@sac_id", item.sac_id == null ? 0 : item.sac_id);
                    var rack_no = new SqlParameter("@rack_no", item.rack_no == null ? "" : item.rack_no);
                    var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                    t1.TypeName = "dbo.temp_cus_ven_item_category";
                    t1.Value = t11;
                    var t2 = new SqlParameter("@t2", SqlDbType.Structured);
                    t2.TypeName = "dbo.temp_sub_ledger";
                    t2.Value = dt;

                    var t3 = new SqlParameter("@t3", SqlDbType.Structured);
                    t3.TypeName = "dbo.temp_item_parameter";
                    t3.Value = dtp;

                    var t4 = new SqlParameter("@t4", SqlDbType.Structured);
                    t4.TypeName = "dbo.temp_item_plant_valuation";
                    t4.Value = dtp1;

                    val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_item @entity,@item_id,@item_name ,@item_code ,@item_category_id ,@item_group_id ,@brand_id ,@uom_id ,@item_length ,@item_lenght_uom_id ,@item_width,@item_width_uom_id ,@item_height ,@item_height_uom_id ,@item_volume ,@item_volume_uom_id ,@item_weight ,@item_weight_uom_id ,@priority_id ,@is_blocked ,@created_on ,@created_by ,@quality_managed ,@batch_managed ,@shelf_life ,@shelf_life_uom_id ,@preferred_vendor_id ,@vendor_part_number ,@mrp ,@minimum_level ,@maximum_level,@reorder_level, @reorder_quantity, @item_valuation_id, @item_accounting_id, @excise_category_id,@excise_chapter_no ,@additional_information ,@item_type_id,@auto_batch,@standard_cost_value,@deleteparameter,@tag_managed,@user_description,@gst_applicability_id,@sac_id,@rack_no,@t1,@t2,@t3,@t4", entity, item_id,
                    item_name, item_code, item_category_id, item_group_id, brand_id, uom_id, item_length, item_lenght_uom_id, item_width, item_width_uom_id, item_height,
                    item_height_uom_id, item_volume, item_volume_uom_id, item_weight, item_weight_uom_id, priority_id, is_blocked, created_on, created_by, quality_managed, batch_managed, shelf_life, shelf_life_uom_id, preferred_vendor_id, vendor_part_number, mrp, minimum_level, maximum_level, reorder_level,
                    reorder_quantity, item_valuation_id, item_accounting_id, excise_category_id, excise_chapter_no, additional_information, item_type_id, auto_batch, standard_cost_value, deleteparameter, tag_managed, user_description, gst_applicability_id, sac_id, rack_no, t1, t2, t3, t4).FirstOrDefault();

                }
                if (val == "Saved")
                {
                    return val;
                }
                else
                {
                    return val;
                }

            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return "Error : " + ex.Message;
                //return "error";
            }
        }

    }
}
