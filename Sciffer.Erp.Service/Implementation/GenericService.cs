using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Syncfusion.XlsIO;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Net.Mail;

namespace Sciffer.Erp.Service.Implementation
{
    public class GenericService : IGenericService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IBudgetMasterService _budgetService;
        private readonly IStateService _stateService;
        public GenericService(ScifferContext scifferContext, IBudgetMasterService budget, IStateService state)
        {
            _scifferContext = scifferContext;
            _budgetService = budget;
            _stateService = state;
        }
        public List<REF_PAYMENT_CYCLE_TYPE> GetPaymentCycleTypeList()
        {
            return _scifferContext.REF_PAYMENT_CYCLE_TYPE.ToList();
        }
        public List<REF_UOM> GetUOMList()
        {
            return _scifferContext.REF_UOM.Where(x => x.is_active == true && x.is_blocked == false).ToList();
        }
        public void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path));
            if (!folderExists)
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
            }
        }
        public string GetFilePath(string controller, HttpPostedFileBase files)
        {
            CreateIfMissing("~/Files/" + controller + "/ ");
            string extension = Path.GetExtension(files.FileName);
            string A = DateTime.Now.ToString("F");
            var B = A.Replace(',', ' ');
            var filename = B.Replace(':', ' ');
            string fileLocation = string.Format("{0}/{1}{2}", System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller + "/ "), filename, extension);
            return fileLocation;
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
        public List<ref_module_form> GetModuleForm(int id)
        {
            return _scifferContext.ref_module_form.Where(x => x.module_id == id && x.is_active == true && x.doc_numbering_flag == true).ToList();
        }
        public List<REF_STORAGE_LOCATION> GetAllStorageLocation(int id)
        {
            return _scifferContext.REF_STORAGE_LOCATION.Where(s => s.plant_id == id).ToList();
        }
        public List<ref_easy_hr_data> GetEasyHRData()
        {
            return _scifferContext.ref_easy_hr_data.ToList();
        }
        public List<REF_PAYMENT_CYCLE> GetPaymentCycle(int id)
        {
            if (id != 0)
            {
                return _scifferContext.REF_PAYMENT_CYCLE.Where(c => c.PAYMENT_CYCLE_TYPE_ID == id).ToList();
            }
            else
            {
                return _scifferContext.REF_PAYMENT_CYCLE.ToList();
            }
        }
        public List<REF_STATE> GetState(int id)
        {
            if (id != 0)
            {
                return _scifferContext.REF_STATE.Where(s => s.COUNTRY_ID == id && s.is_active == true && s.is_blocked == false).ToList();
            }
            else
            {
                return _scifferContext.REF_STATE.Where(s => s.is_active == true && s.is_blocked == false).ToList();
            }
        }
        public int? plantby_doc_no(int category_id)
        {
            return _scifferContext.ref_document_numbring.Where(a => a.document_numbring_id == category_id).FirstOrDefault().plant_id;
        }
        public List<ref_document_numbring> GetCategoryListByPlant(int id, int plant_id)
        {
            return _scifferContext.ref_document_numbring.Where(x => x.module_form_id == id && x.is_blocked == false && x.plant_id == plant_id).OrderByDescending(x => x.set_default).OrderByDescending(x => x.category).ToList();
        }
        public List<ref_document_numbring> GetCategoryListByPlant1(int id)
        {
            return _scifferContext.ref_document_numbring.Where(x => x.module_form_id == id && x.is_blocked == false).OrderByDescending(x => x.document_numbring_id).Take(2).ToList();
        }
        public List<ref_cost_center> GetCostCenter()
        {
            var costcenter = _scifferContext.ref_cost_center.ToList().Select(a => new { a.cost_center_code, a.cost_center_id, a.cost_center_description }).ToList().Select(a => new ref_cost_center { cost_center_id = a.cost_center_id, cost_center_code = a.cost_center_code + "/" + a.cost_center_description }).ToList();
            return costcenter;
        }
        public List<REF_PLANT> GetPlantCode()
        {
            var plant = _scifferContext.REF_PLANT.ToList().Select(a => new { a.PLANT_ID, a.PLANT_CODE, a.PLANT_NAME }).Select(a => new REF_PLANT { PLANT_ID = a.PLANT_ID, PLANT_CODE = a.PLANT_CODE + "/" + a.PLANT_NAME }).ToList();
            return plant;
        }
        public int GetItemType_id(int item_id)
        {
            var item_type_id = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item_id).FirstOrDefault().item_type_id;
            return (int)item_type_id;
        }
        public int GetReOrderLevelBy_id(int item_id)
        {
            var reorderlevel = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == item_id).FirstOrDefault().REORDER_LEVEL;
            return (int)reorderlevel;
        }

        public int GetReOrderCount()
        {
            var entity = new SqlParameter("@entity", "notificationCount");
            var val = _scifferContext.Database.SqlQuery<int>(
                    "exec Get_ReorderCount @entity", entity).FirstOrDefault();
            return val;
        }
        public List<REF_EMPLOYEE> GetEmployeeCode()
        {
            var emp = _scifferContext.REF_EMPLOYEE.ToList().Select(a => new { a.employee_id, a.employee_name, a.employee_code }).ToList().Select(a => new REF_EMPLOYEE { employee_id = a.employee_id, employee_code = a.employee_code + "/" + a.employee_name }).ToList();
            return emp;
        }
        public List<ref_cancellation_reason_vm> GetCANCELLATIONList(int module_form_id)
        {
            var val = (from r in _scifferContext.ref_cancellation_reason.Where(x => x.module_form_id == module_form_id)
                       select new ref_cancellation_reason_vm
                       {
                           cancellation_reason_id = r.cancellation_reason_id,
                           cancellation_reason = r.cancellation_reason,
                       }).ToList();
            return val;
        }
        public List<ref_cancellation_reason_vm> GetCANCELLATIONReason(string module_form_code)
        {
            var val = (from r in _scifferContext.ref_cancellation_reason
                       join f in _scifferContext.ref_module_form.Where(x => x.module_form_code == module_form_code) on r.module_form_id equals f.module_form_id
                       select new ref_cancellation_reason_vm
                       {
                           cancellation_reason_id = r.cancellation_reason_id,
                           cancellation_reason = r.cancellation_reason,
                       }).ToList();
            return val;
        }

        public List<REF_ENTITY_TYPE> GetEntityList()
        {
            return _scifferContext.REF_ENTITY_TYPE.Where(x => x.IS_ACTIVE == true).ToList();
        }
        public int? GetGSTCustomerTypeforGRN(int id)
        {
            return _scifferContext.REF_VENDOR.Where(a => a.VENDOR_ID == id).FirstOrDefault().gst_vendor_type_id;
        }
        public int CheckDuplicate(string st, string st1, string name1, string frm, int id)
        {
            int query = 0;

            if (frm == "AssetMasterData_Dublicate_Machine")
            {
                //  var machine_id = _scifferContext.ref_asset_master_data.Where(y => y.asset_master_data_code + "" + y.asset_master_data_desc == st).Select(y => y.machine_id).FirstOrDefault();
                if (id == 0)
                {

                    query = _scifferContext.ref_asset_master_data.Count(x => x.machine_id == Convert.ToInt32(st) && x.is_active == true);
                }
                else
                {
                    var machine_id = Convert.ToInt32(st);
                    query = _scifferContext.ref_asset_master_data.Count(x => x.machine_id == machine_id && x.is_active == true);
                }
            }

            if (frm == "AssetMasterData")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_asset_master_data.Count(x => x.asset_master_data_code == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.ref_asset_master_data.Count(x => x.asset_master_data_code == st && x.asset_master_data_id != id && x.is_active == true);
                }
            }
            if (frm == "AssetClass")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_asset_class.Count(x => x.asset_class_code == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.ref_asset_class.Count(x => x.asset_class_code == st && x.asset_class_id != id && x.is_active == true);
                }
            }
            if (frm == "AssetGroup")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_asset_group.Count(x => x.asset_group_code == st && x.is_blocked == false);
                }
                else
                {
                    query = _scifferContext.ref_asset_group.Count(x => x.asset_group_code == st && x.asset_group_id != id && x.is_blocked == true);
                }
            }
            if (frm == "DepreciationArea")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_dep_area.Count(x => x.dep_area_code == st && x.is_blocked == false);
                }
                else
                {
                    query = _scifferContext.ref_dep_area.Count(x => x.dep_area_code == st && x.dep_area_id != id && x.is_blocked == true);
                }
            }
            if (frm == "OrganizationType")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_ORG_TYPE.Count(x => x.ORG_TYPE_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_ORG_TYPE.Count(x => x.ORG_TYPE_NAME == st && x.ORG_TYPE_ID != id && x.is_active == true);
                }
            }
            if (frm == "SalesRM")
            {
                int emp = int.Parse(st);
                if (id == 0)
                {
                    query = _scifferContext.ref_sales_rm.Count(x => x.employee_id == emp && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.ref_sales_rm.Count(x => x.employee_id == emp && x.sales_rm_id != id && x.is_active == true);
                }
            }
            else if (frm == "CustomerMaster")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_CUSTOMER.Count(x => x.CUSTOMER_CODE == st);
                }
                else
                {
                    query = _scifferContext.REF_CUSTOMER.Count(x => x.CUSTOMER_CODE == st && x.CUSTOMER_ID != id);
                }

            }
            else if (frm == "PriceListCustomer")
            {
                int customer_id = int.Parse(st);
                if (id == 0)
                {
                    query = _scifferContext.ref_price_list_customer.Count(x => x.customer_id == customer_id);
                }
                else
                {
                    query = _scifferContext.ref_price_list_customer.Count(x => x.customer_id == customer_id && x.price_list_id != id);
                }
            }
            else if (frm == "PriceList")
            {
                int vendor_id = int.Parse(st);
                if (id == 0)
                {
                    query = _scifferContext.ref_price_list_vendor.Count(x => x.vendor_id == vendor_id);
                }
                else
                {
                    query = _scifferContext.ref_price_list_vendor.Count(x => x.vendor_id == vendor_id && x.price_list_id != id);
                }
            }
            else if (frm == "CustomerParent")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_CUSTOMER_PARENT.Count(x => x.customer_code == st);
                }
                else
                {
                    query = _scifferContext.REF_CUSTOMER_PARENT.Count(x => x.customer_code == st && x.CUSTOMER_PARENT_ID != id);
                }
            }
            else if (frm == "VendorParent")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_VENDOR_PARENT.Count(x => x.vendor_code == st);
                }
                else
                {
                    query = _scifferContext.REF_VENDOR_PARENT.Count(x => x.vendor_code == st && x.VENDOR_PARENT_ID != id);
                }
            }
            else if (frm == "taxtype")
            {

                if (id == 0)
                {
                    query = _scifferContext.ref_tax_type.Count(x => x.tax_type_name == st);
                }
                else
                {
                    query = _scifferContext.ref_tax_type.Count(x => x.tax_type_name == st && x.tax_type_id != id);
                }
            }
            else if (frm == "WithinPlantTransfer")
            {
                if (id == 0)
                {
                    query = _scifferContext.pla_transfer.Count(x => x.pla_transfer_number == st);
                }
                else
                {
                    query = _scifferContext.pla_transfer.Count(x => x.pla_transfer_number == st && x.pla_transfer_id != id);
                }
            }
            else if (frm == "CompanyDetails")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_COMPANY.Count(x => x.COMPANY_NAME == st);
                }
                else
                {
                    query = _scifferContext.REF_COMPANY.Count(x => x.COMPANY_NAME == st && x.COMPANY_ID != id);
                }
            }
            else if (frm == "country")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_COUNTRY.Count(x => x.COUNTRY_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_COUNTRY.Count(x => x.COUNTRY_NAME == st && x.COUNTRY_ID != id && x.is_active == true);
                }
            }
            else if (frm == "state")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_STATE.Count(x => x.STATE_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_STATE.Count(x => x.STATE_NAME == st && x.STATE_ID != id && x.is_active == true);
                }
            }
            else if (frm == "priority")
            {
                int fid = 0;
                fid = int.Parse(st1);
                if (id == 0)
                {
                    query = _scifferContext.REF_PRIORITY.Count(x => x.PRIORITY_NAME == st && x.is_active == true && x.form_id == fid);
                }
                else
                {
                    query = _scifferContext.REF_PRIORITY.Count(x => x.PRIORITY_NAME == st && x.PRIORITY_ID != id && x.is_active == true && x.form_id == fid);
                }
            }
            else if (frm == "freight")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_FREIGHT_TERMS.Count(x => x.FREIGHT_TERMS_NAME == st && x.Is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_FREIGHT_TERMS.Count(x => x.FREIGHT_TERMS_NAME == st && x.FREIGHT_TERMS_ID != id && x.Is_active == true);
                }
            }
            else if (frm == "currency")
            {
                int c1 = 0;
                int c = 0;
                c = int.Parse(st1);
                if (id == 0)
                {
                    c1 = _scifferContext.REF_CURRENCY.Count(x => x.CURRENCY_COUNTRY_ID == c && x.IS_ACTIVE == true);
                    if (c1 == 0)
                    {
                        query = _scifferContext.REF_CURRENCY.Count(x => x.CURRENCY_NAME == st && x.IS_ACTIVE == true);
                    }
                    else
                    {
                        query = c1;
                    }
                }
                else
                {
                    c1 = _scifferContext.REF_CURRENCY.Count(x => x.CURRENCY_COUNTRY_ID == c && x.IS_ACTIVE == true && x.CURRENCY_ID != id);
                    if (c1 == 0)
                    {
                        query = _scifferContext.REF_CURRENCY.Count(x => x.CURRENCY_NAME == st && x.CURRENCY_ID != id && x.IS_ACTIVE == true);
                    }
                    else
                    {
                        query = c1;
                    }

                }

            }
            else if (frm == "businessunit")
            {
                int q1 = 0;
                if (id == 0)
                {

                    q1 = _scifferContext.REF_BUSINESS_UNIT.Count(x => x.BUSINESS_UNIT_NAME == st && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_BUSINESS_UNIT.Count(x => x.BUSINESS_UNIT_DESCRIPTION == st1 && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_BUSINESS_UNIT.Count(x => x.BUSINESS_UNIT_NAME == st && x.BUSINESS_UNIT_ID != id && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_BUSINESS_UNIT.Count(x => x.BUSINESS_UNIT_DESCRIPTION == st1 && x.is_active == true && x.BUSINESS_UNIT_ID != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "financialyear")
            {
                string[] values = st1.Split(',');
                DateTime f = new DateTime();
                // f = DateTime.Parse(st);
                DateTime t = new DateTime();
                // t = DateTime.Parse(st1);
                for (int i = 0; i < values.Length; i++)
                {
                    f = DateTime.Parse(values[0].Trim());
                    t = DateTime.Parse(values[1].Trim());
                    break;
                }
                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.REF_FINANCIAL_YEAR.Count(x => x.FINANCIAL_YEAR_NAME == st && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_FINANCIAL_YEAR.Count(x => x.FINANCIAL_YEAR_FROM >= f && x.FINANCIAL_YEAR_TO <= t && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_FINANCIAL_YEAR.Count(x => x.FINANCIAL_YEAR_NAME == st && x.is_active == true && x.FINANCIAL_YEAR_ID != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_FINANCIAL_YEAR.Count(x => x.FINANCIAL_YEAR_FROM >= f && x.FINANCIAL_YEAR_TO <= t && x.FINANCIAL_YEAR_ID != id && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }

                }
            }
            else if (frm == "vendorcategory")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_VENDOR_CATEGORY.Count(x => x.VENDOR_CATEGORY_NAME == st);
                }
                else
                {
                    query = _scifferContext.REF_VENDOR_CATEGORY.Count(x => x.VENDOR_CATEGORY_NAME == st && x.VENDOR_CATEGORY_ID != id);
                }
            }
            else if (frm == "customercategory")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_CUSTOMER_CATEGORY.Count(x => x.CUSTOMER_CATEGORY_NAME == st);
                }
                else
                {
                    query = _scifferContext.REF_CUSTOMER_CATEGORY.Count(x => x.CUSTOMER_CATEGORY_NAME == st && x.CUSTOMER_CATEGORY_ID != id);
                }
            }
            else if (frm == "territory")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_TERRITORY.Count(x => x.TERRITORY_NAME == st);
                }
                else
                {
                    query = _scifferContext.REF_TERRITORY.Count(x => x.TERRITORY_NAME == st && x.TERRITORY_ID != id);
                }
            }
            else if (frm == "Plant")
            {
                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.REF_PLANT.Count(x => x.PLANT_CODE == st);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_PLANT.Count(x => x.PLANT_NAME == st1);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_PLANT.Count(x => x.PLANT_CODE == st && x.PLANT_ID != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_PLANT.Count(x => x.PLANT_NAME == st1 && x.PLANT_ID != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "exchangerate")
            {
                string[] CURR = st.Split(',');
                int C1 = 0;
                int C2 = 0;

                for (int i = 0; i < CURR.Length; i++)
                {
                    C1 = int.Parse(CURR[0].Trim());
                    C2 = int.Parse(CURR[1].Trim());
                    break;
                }
                DateTime f = new DateTime();
                f = DateTime.Parse(st1.Trim());

                if (id == 0)
                {
                    query = _scifferContext.ref_exchange_rate.Count(x => x.currency_id1 == C1 && x.currency_id2 == C2 && x.from_date >= f);

                }
                else
                {
                    query = _scifferContext.ref_exchange_rate.Count(x => x.currency_id1 == C1 && x.currency_id2 == C2 && x.from_date >= f && x.exchange_rate_id != id);
                }
            }
            else if (frm == "storagelocation")
            {
                var code = "";
                var des = "";
                int plant = 0;
                int q1 = 0;
                int q2 = 0;
                plant = int.Parse(st1);
                string[] values = st.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    code = values[0].Trim();
                    des = values[1].Trim();
                    break;
                }
                if (id == 0)
                {
                    q1 = _scifferContext.REF_STORAGE_LOCATION.Count(x => x.storage_location_name == code && x.plant_id == plant && x.is_active == true);
                    if (q1 == 0)
                    {
                        q2 = _scifferContext.REF_STORAGE_LOCATION.Count(x => x.description == des && x.plant_id == plant && x.is_active == true);
                        if (q2 == 0)
                        {
                            query = _scifferContext.REF_STORAGE_LOCATION.Count(x => x.storage_location_name == code && x.description == des && x.plant_id == plant && x.is_active == true);
                        }
                        else
                        {
                            query = q2;
                        }
                    }
                    else
                    {
                        query = q1;
                    }

                }
                else
                {
                    q1 = _scifferContext.REF_STORAGE_LOCATION.Count(x => x.storage_location_name == code && x.is_active == true && x.storage_location_id != id && x.plant_id == plant);
                    if (q1 == 0)
                    {
                        q2 = _scifferContext.REF_STORAGE_LOCATION.Count(x => x.description == des && x.is_active == true && x.storage_location_id != id && x.plant_id == plant);
                        if (q2 == 0)
                        {
                            query = _scifferContext.REF_STORAGE_LOCATION.Count(x => x.storage_location_name == code && x.description == des && x.plant_id == plant && x.is_active == true && x.storage_location_id != id);
                        }
                        else
                        {
                            query = q2;
                        }
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "ItemCategory")
            {
                int q1 = 0;
                if (id == 0)
                {

                    query = _scifferContext.REF_ITEM_CATEGORY.Count(x => x.ITEM_CATEGORY_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_ITEM_CATEGORY.Count(x => x.ITEM_CATEGORY_NAME == st && x.ITEM_CATEGORY_ID != id && x.is_active == true);
                }
            }
            else if (frm == "ItemMaster")
            {
                int q1 = 0;
                if (id == 0)
                {

                    query = _scifferContext.REF_ITEM.Count(x => x.ITEM_CODE == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_ITEM.Count(x => x.ITEM_CODE == st && x.ITEM_ID != id && x.is_active == true);
                }
            }
            else if (frm == "ItemGroup")
            {
                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.REF_ITEM_GROUP.Count(x => x.ITEM_GROUP_NAME == st && x.is_active == true);
                    if (q1 == 0)
                    {

                        query = _scifferContext.REF_ITEM_GROUP.Count(x => x.ITEM_GROUP_DESCRIPTION == st1 && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_ITEM_GROUP.Count(x => x.ITEM_GROUP_NAME == st && x.is_active == true && x.ITEM_GROUP_ID != id);
                    if (q1 == 0)
                    {

                        query = _scifferContext.REF_ITEM_GROUP.Count(x => x.ITEM_GROUP_DESCRIPTION == st1 && x.is_active == true && x.ITEM_GROUP_ID != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "UOM")
            {

                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.REF_UOM.Count(x => x.UOM_NAME == st && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_UOM.Count(x => x.UOM_DESCRIPTION == st1 && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_UOM.Count(x => x.UOM_NAME == st && x.UOM_ID != id && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_UOM.Count(x => x.UOM_DESCRIPTION == st1 && x.UOM_ID != id && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "brand")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_BRAND.Count(x => x.BRAND_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_BRAND.Count(x => x.BRAND_NAME == st && x.BRAND_ID != id && x.is_active == true);
                }
            }
            else if (frm == "reasomdetermination")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_REASON_DETERMINATION.Count(x => x.REASON_DETERMINATION_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_REASON_DETERMINATION.Count(x => x.REASON_DETERMINATION_NAME == st && x.REASON_DETERMINATION_ID != id && x.is_active == true);
                }
            }
            else if (frm == "paymenttype")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_PAYMENT_TYPE.Count(x => x.PAYMENT_TYPE_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_PAYMENT_TYPE.Count(x => x.PAYMENT_TYPE_NAME == st && x.PAYMENT_TYPE_ID != id && x.is_active == true);
                }
            }
            else if (frm == "paymentterms")
            {

                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.REF_PAYMENT_TERMS.Count(x => x.payment_terms_code == st && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_PAYMENT_TERMS.Count(x => x.payment_terms_description == st1 && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_PAYMENT_TERMS.Count(x => x.payment_terms_code == st && x.payment_terms_id != id && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_PAYMENT_TERMS.Count(x => x.payment_terms_description == st1 && x.is_active == true && x.payment_terms_id != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "bank")
            {
                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.ref_bank.Count(x => x.bank_code == st1 && x.is_active == true);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_bank.Count(x => x.bank_name == st && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.ref_bank.Count(x => x.bank_code == st1 && x.is_active == true && x.bank_id != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_bank.Count(x => x.bank_name == st && x.bank_id != id && x.is_active == true);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "banknumber")
            {
                int q1 = 0;
                int q2 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.ref_bank_account.Count(x => x.bank_account_code == st1);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_bank_account.Count(x => x.bank_account_number == st);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.ref_bank_account.Count(x => x.bank_account_code == st1 && x.bank_account_id != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_bank_account.Count(x => x.bank_account_number == st && x.bank_account_id != id);
                    }
                    else
                    {
                        query = q1;
                    }

                }
            }
            else if (frm == "designation")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_DESIGNATION.Count(x => x.designation_name == st);
                }
                else
                {
                    query = _scifferContext.REF_DESIGNATION.Count(x => x.designation_name == st && x.designation_id != id);
                }
            }
            else if (frm == "Department")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_DEPARTMENT.Count(x => x.DEPARTMENT_NAME == st);
                }
                else
                {
                    query = _scifferContext.REF_DEPARTMENT.Count(x => x.DEPARTMENT_NAME == st && x.DEPARTMENT_ID != id);
                }
            }
            else if (frm == "Grade")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_GRADE.Count(x => x.grade_name == st);
                }
                else
                {
                    query = _scifferContext.REF_GRADE.Count(x => x.grade_name == st && x.grade_id != id);
                }
            }
            else if (frm == "employee")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_EMPLOYEE.Count(x => x.employee_name == st && x.employee_code == st1);
                }
                else
                {
                    query = _scifferContext.REF_EMPLOYEE.Count(x => x.employee_name == st && x.employee_code == st1 && x.employee_id != id);
                }
            }
            else if (frm == "reason")
            {

                int stt;
                stt = int.Parse(st1);
                if (id == 0)
                {
                    query = _scifferContext.REF_REASON_DETERMINATION.Count(x => x.REASON_DETERMINATION_NAME == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.REF_REASON_DETERMINATION.Count(x => x.REASON_DETERMINATION_NAME == st && x.REASON_DETERMINATION_ID != id && x.is_active == true);
                }
            }
            else if (frm == "creditcard")
            {
                long stt;
                stt = long.Parse(st);


                if (id == 0)
                {
                    query = _scifferContext.ref_credit_card.Count(x => x.credit_card_number == stt);
                }
                else
                {
                    query = _scifferContext.ref_credit_card.Count(x => x.credit_card_number == stt && x.credit_card_id != id);
                }
            }
            else if (frm == "shift")
            {

                TimeSpan time1 = TimeSpan.Parse("07:35");
                TimeSpan time2 = TimeSpan.Parse("07:35");
                var code = "";
                string[] values = st.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    time1 = TimeSpan.Parse(values[0].Trim());
                    time2 = TimeSpan.Parse(values[1].Trim());
                    code = values[2].Trim();
                    break;
                }
                int q1 = 0;
                int stt = 0;
                stt = int.Parse(st1);
                if (id == 0)
                {
                    q1 = _scifferContext.ref_shifts.Count(x => x.shift_code == code && x.plant_id == stt);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_shifts.Count(x => x.from_time == time1 && x.to_time == time2 && x.plant_id == stt);
                    }
                    else
                    {
                        query = q1;
                    }

                }
                else
                {
                    q1 = _scifferContext.ref_shifts.Count(x => x.shift_code == code && x.shift_id != id && x.plant_id == stt);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_shifts.Count(x => x.from_time == time1 && x.to_time == time2 && x.plant_id == stt && x.shift_id != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "TaxElement")
            {

                if (id == 0)
                {
                    query = _scifferContext.ref_tax_element.Count(x => x.tax_element_code == st);
                }
                else
                {
                    query = _scifferContext.ref_tax_element.Count(x => x.tax_element_code == st && x.tax_element_id != id);
                }
            }
            else if (frm == "TDSCode")
            {

                if (id == 0)
                {
                    query = _scifferContext.ref_tds_code.Count(x => x.tds_code == st);
                }
                else
                {
                    query = _scifferContext.ref_tds_code.Count(x => x.tds_code == st && x.tds_code_id != id);
                }
            }
            else if (frm == "VendorMaster")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_VENDOR.Count(x => x.VENDOR_CODE == st);
                }
                else
                {
                    query = _scifferContext.REF_VENDOR.Count(x => x.VENDOR_CODE == st && x.VENDOR_ID != id);
                }
            }
            else if (frm == "VendorParent")
            {

                if (id == 0)
                {
                    query = _scifferContext.REF_VENDOR_PARENT.Count(x => x.vendor_code == st);
                }
                else
                {
                    query = _scifferContext.REF_VENDOR_PARENT.Count(x => x.vendor_code == st && x.VENDOR_PARENT_ID != id);
                }
            }
            else if (frm == "form")
            {
                if (id == 0)
                {
                    query = _scifferContext.REF_FORM.Count(x => x.FORM_NAME == st);
                }
                else
                {
                    query = _scifferContext.REF_FORM.Count(x => x.FORM_NAME == st && x.FORM_ID != id);
                }
            }
            else if (frm == "Department")
            {
                int q1 = 0;
                if (id == 0)
                {

                    q1 = _scifferContext.REF_DEPARTMENT.Count(x => x.DEPARTMENT_NAME == st);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_DEPARTMENT.Count(x => x.DEPARTMENT_DESCRIPTION == st1);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_DEPARTMENT.Count(x => x.DEPARTMENT_NAME == st && x.DEPARTMENT_ID != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_DEPARTMENT.Count(x => x.DEPARTMENT_DESCRIPTION == st1 && x.DEPARTMENT_ID != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "designation")
            {
                int q1 = 0;
                if (id == 0)
                {

                    q1 = _scifferContext.REF_DESIGNATION.Count(x => x.designation_name == st);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_DESIGNATION.Count(x => x.designation_code == st1);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_DESIGNATION.Count(x => x.designation_name == st && x.designation_id != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_DESIGNATION.Count(x => x.designation_code == st1 && x.designation_id != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "Division")
            {
                int q1 = 0;
                if (id == 0)
                {

                    q1 = _scifferContext.REF_DIVISION.Count(x => x.DIVISION_NAME == st);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_DIVISION.Count(x => x.DIVISION_NAME == st1);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_DIVISION.Count(x => x.DIVISION_NAME == st && x.DIVISION_ID != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_DIVISION.Count(x => x.DIVISION_NAME == st1 && x.DIVISION_ID != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }
            else if (frm == "Grade")
            {
                int q1 = 0;
                if (id == 0)
                {

                    q1 = _scifferContext.REF_GRADE.Count(x => x.grade_name == st);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_GRADE.Count(x => x.grade_name == st1);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.REF_GRADE.Count(x => x.grade_name == st && x.grade_id != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.REF_GRADE.Count(x => x.grade_name == st1 && x.grade_id != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }
            }

            else if (frm == "glaccounttype")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_gl_acount_type.Count(x => x.gl_account_type_description == st);
                }
                else
                {
                    query = _scifferContext.ref_gl_acount_type.Count(x => x.gl_account_type_description == st && x.gl_account_type_id != id);
                }
            }
            else if (frm == "GeneralLedger")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_general_ledger.Count(x => x.gl_ledger_code == st);
                }
                else
                {
                    query = _scifferContext.ref_general_ledger.Count(x => x.gl_ledger_code == st && x.gl_ledger_id != id);
                }
            }
            else if (frm == "documentnumbering")
            {
                int mfid = 0;
                int fin = 0;
                fin = int.Parse(name1);
                mfid = int.Parse(st);
                if (id == 0)
                {
                    query = _scifferContext.ref_document_numbring.Count(x => x.module_form_id == mfid && x.category == st1 && x.financial_year_id == fin);
                }
                else
                {
                    query = _scifferContext.ref_document_numbring.Count(x => x.module_form_id == mfid && x.category == st1 && x.document_numbring_id != id && x.financial_year_id == fin);
                }
            }
            else if (frm == "PostingPeriod")
            {
                int i = 0;
                i = int.Parse(st);

                return _scifferContext.ref_posting_periods.Count(X => X.financial_year_id == i);
            }
            else if (frm == "batchnumbering")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_batch_numbering.Count(x => x.plant_id.ToString() == st && x.item_category_id.ToString() == st1 && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.ref_batch_numbering.Count(x => x.plant_id.ToString() == st && x.item_category_id.ToString() == st1 && x.batch_no_id != id && x.is_active == true);
                }
            }
            else if (frm == "SalesQuotation")
            {
                if (id == 0)
                {
                    query = _scifferContext.SAL_QUOTATION.Count(x => x.QUOTATION_NUMBER == st);
                }
                else
                {
                    query = _scifferContext.SAL_QUOTATION.Count(x => x.QUOTATION_NUMBER == st && x.QUOTATION_ID != id);
                }
            }
            else if (frm == "budget")
            {

                if (id == 0)
                {
                    query = _scifferContext.ref_budget_master.Count(x => x.financial_year_id.ToString() == st && x.general_ledger_id.ToString() == st1 && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.ref_budget_master.Count(x => x.financial_year_id.ToString() == st && x.general_ledger_id.ToString() == st1 && x.budget_id != id && x.is_active == true);
                }

            }
            else if (frm == "TaxCode")
            {
                int q1 = 0;
                if (id == 0)
                {
                    q1 = _scifferContext.ref_tax.Count(x => x.tax_code == st);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_tax.Count(x => x.tax_name == st1);
                    }
                    else
                    {
                        query = q1;
                    }
                }
                else
                {
                    q1 = _scifferContext.ref_tax.Count(x => x.tax_code == st && x.tax_id != id);
                    if (q1 == 0)
                    {
                        query = _scifferContext.ref_tax.Count(x => x.tax_name == st1 && x.tax_id != id);
                    }
                    else
                    {
                        query = q1;
                    }
                }

            }
            else if (frm == "ParameterList")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_parameter_list.Count(x => x.parameter_code == st);
                }
                else
                {
                    query = _scifferContext.ref_parameter_list.Count(x => x.parameter_code == st && x.parameter_id != id);
                }
            }
            else if (frm == "costcenter")
            {
                var des = "";
                int lev = 0;
                int q1 = 0;
                int q2 = 0;
                string[] values = st1.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    des = values[0].Trim();
                    lev = int.Parse(values[1].Trim());
                    break;
                }
                if (id == 0)
                {
                    if (lev == 1)

                    {
                        q1 = _scifferContext.ref_cost_center.Count(x => x.cost_center_level == lev && x.is_active == true);
                        if (q1 == 0)
                        {
                            q2 = _scifferContext.ref_cost_center.Count(x => x.cost_center_code == st && x.is_active == true);
                            if (q2 == 0)
                            {
                                query = _scifferContext.ref_cost_center.Count(x => x.cost_center_description == des && x.is_active == true);
                            }
                            else
                            {
                                query = q2;
                            }
                        }
                        else
                        {
                            query = q1;
                        }
                    }
                    else
                    {
                        q1 = _scifferContext.ref_cost_center.Count(x => x.cost_center_code == st && x.is_active == true);
                        if (q1 == 0)
                        {
                            query = _scifferContext.ref_cost_center.Count(x => x.cost_center_description == des && x.is_active == true);
                        }
                        else
                        {
                            query = q1;
                        }
                    }
                }
                else
                {
                    if (lev == 1)
                    {
                        q1 = _scifferContext.ref_cost_center.Count(x => x.cost_center_level == lev && x.is_active == true && x.cost_center_id != id);
                        if (q1 == 0)
                        {
                            q2 = _scifferContext.ref_cost_center.Count(x => x.cost_center_id != id && x.cost_center_code == st && x.is_active == true);
                            if (q2 == 0)
                            {
                                query = _scifferContext.ref_cost_center.Count(x => x.is_active == true && x.cost_center_description == des && x.cost_center_id != id);
                            }
                            else
                            {
                                query = q2;
                            }
                        }
                        else
                        {
                            query = q1;
                        }
                    }
                    else
                    {
                        q1 = _scifferContext.ref_cost_center.Count(x => x.cost_center_id != id && x.is_active == true && x.cost_center_code == st);
                        if (q1 == 0)
                        {
                            query = _scifferContext.ref_cost_center.Count(x => x.cost_center_description == des && x.is_active == true && x.cost_center_id != id);
                        }
                        else
                        {
                            query = q1;
                        }
                    }
                    // query = _scifferContext.ref_cost_center.Count(x => x.cost_center_code.ToString() == st && x.cost_center_description.ToString() == st1 && x.cost_center_id != id && x.is_active == true);
                }
            }

            else if (frm == "IncentiveBenchmark")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_mfg_incentive_benchmark.Count(x => x.item_id.ToString() == st && x.machine_id.ToString() == st1 && x.operation_id.ToString() == name1);

                }
                else
                {
                    query = _scifferContext.ref_mfg_incentive_benchmark.Count(x => x.item_id.ToString() == st && x.machine_id.ToString() == st1 && x.operation_id.ToString() == name1 && x.mfg_incentive_benchmark_id != id);
                }
            }

            if (frm == "LevelMaster")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_level.Count(x => x.level_code == st && x.is_active == true);
                }
                else
                {
                    query = _scifferContext.ref_level.Count(x => x.level_code == st && x.level_id != id && x.is_active == false);
                }
            }
            if (frm == "OperatorLevelMapping")
            {
                if (id == 0)
                {
                    int machineId = Convert.ToInt32(st);
                    query = _scifferContext.ref_operator_level_mapping.Count(x =>
                    x.operator_id.ToString() == name1 && x.machine_id == machineId && x.is_active && !x.is_block);
                }
                //else
                //else
                //{
                //    query = _scifferContext.ref_operator_level_mapping.Count(x => x.level_id.ToString() == st && x.machine_id.ToString() == st1 && x.operator_id.ToString() == name1 && x.operator_level_mapping_id != id && x.is_active == false);
                //}
            }
            if (frm == "MachineLevelMapping")
            {
                if (id == 0)
                {
                    query = _scifferContext.ref_machine_level_mapping.Count(x => x.machine_id.ToString() == st && x.is_active == true);
                }
            }

            return query;
        }
        public Ref_item_VM GetUomOrQualityManage(int id)
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == id)
                         select new Ref_item_VM
                         {
                             UOM_ID = i.UOM_ID,
                             QUALITY_MANAGED = i.QUALITY_MANAGED,
                         }).FirstOrDefault();
            return query;

        }
        public List<ref_document_numbring> GetCategoryList(int id)
        {
            return _scifferContext.ref_document_numbring.Where(x => x.module_form_id == id && x.is_blocked == false).OrderByDescending(x => x.set_default).OrderByDescending(x => x.set_default).ToList();
        }

        public List<ref_document_numbring> GetCategoryListForShiftWise(string code)
        {

            try
            {
                var save_entity = new SqlParameter("@entity", "GetDocumentNumebringForShiftMaster");
                var module_form_code = new SqlParameter("@module_form_code", code == null ? "SWPM" : code);
                var result = _scifferContext.Database.SqlQuery<ref_document_numbring>(
                         "exec GetDocumentNumberingShiftMaster @entity,@module_form_code", save_entity, module_form_code).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public REF_CUSTOMER_VM GetBuyerDetails(int id)
        {
            var query = (from cm in _scifferContext.REF_CUSTOMER.Where(x => x.CUSTOMER_ID == id)
                         join s in _scifferContext.REF_STATE on cm.BILLING_STATE_ID equals s.STATE_ID
                         join p in _scifferContext.REF_PAYMENT_CYCLE on cm.PAYMENT_CYCLE_ID equals p.PAYMENT_CYCLE_ID into pc1
                         from pc2 in pc1.DefaultIfEmpty()
                         select new REF_CUSTOMER_VM
                         {
                             BILLING_ADDRESS = cm.BILLING_ADDRESS,
                             BILLING_CITY = cm.BILLING_CITY,
                             BILLING_STATE_ID = cm.BILLING_STATE_ID,
                             BILLING_COUNTRY_ID = s.COUNTRY_ID,
                             BILLING_PINCODE = cm.BILLING_PINCODE,
                             EMAIL_ID_PRIMARY = cm.EMAIL_ID_PRIMARY,
                             PAYMENT_CYCLE_ID = cm.PAYMENT_CYCLE_ID,
                             PAYMENT_TERMS_ID = cm.PAYMENT_TERMS_ID,
                             FREIGHT_TERMS_ID = cm.FREIGHT_TERMS_ID,
                             TERRITORY_ID = cm.TERRITORY_ID,
                             SALES_EXEC_ID = cm.SALES_EXEC_ID,
                             PAYMENT_CYCLE_TYPE_ID = pc2 == null ? 0 : pc2.PAYMENT_CYCLE_TYPE_ID,
                             gst_no = cm.gst_no,
                             pan_no = cm.pan_no,
                             service_tax_no = cm.service_tax_no,
                             ecc_no = cm.ecc_no,
                             cst_tin_no = cm.cst_tin_no,
                             vat_tin_no = cm.vat_tin_no,
                             commisionerate = cm.commisionerate,
                             range = cm.range,
                             division = cm.division,
                             CREDIT_LIMIT_CURRENCY_ID = cm.CREDIT_LIMIT_CURRENCY_ID,
                             gst_tds_id = cm.gst_tds_id,
                         }).FirstOrDefault();
            return query;
        }

        public int GetUnitofItem(int id)
        {
            var query = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == id).FirstOrDefault();
            return query == null ? 0 : query.UOM_ID;
        }
        public Ref_item_VM GetUserDescriptionForItem(int id)
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == id)
                         join sa in _scifferContext.ref_sac on i.sac_id equals sa.sac_id into j1
                         from sac in j1.DefaultIfEmpty()
                         join hs in _scifferContext.ref_hsn_code on i.EXCISE_CHAPTER_NO equals hs.hsn_code_id into j2
                         from hsn in j2.DefaultIfEmpty()
                         select new Ref_item_VM
                         {
                             UOM_ID = i.UOM_ID,
                             user_description = i.user_description,
                             ITEM_CATEGORY_NAME = i.REF_ITEM_CATEGORY.ITEM_CATEGORY_NAME,
                             item_type_id = (int)i.item_type_id,
                             sac_id = i.item_type_id == 2 ? sac == null ? 0 : sac.sac_id : hsn == null ? 0 : hsn.hsn_code_id,
                             sac_name = i.item_type_id == 2 ? sac == null ? "" : sac.sac_code + "/" + sac.sac_description : hsn == null ? "" : hsn.hsn_code + "/" + hsn.hsn_code_description,
                             within_state_tax_id = hsn == null ? 0 : hsn.within_state_tax_id,
                             inter_state_tax_id = hsn == null ? 0 : hsn.inter_state_tax_id,
                         }).FirstOrDefault();
            return query;
        }

        public int GetItemId(string code)
        {
            var item = _scifferContext.REF_ITEM.Where(x => x.ITEM_CODE == code && x.is_active == true).FirstOrDefault();
            return item == null ? 0 : item.ITEM_ID;
        }

        public int GetCustomerId(string code)
        {
            var cus = _scifferContext.REF_CUSTOMER.Where(x => x.CUSTOMER_CODE == code.Trim() && x.IS_BLOCKED == false).FirstOrDefault();
            return cus == null ? 0 : cus.CUSTOMER_ID;
        }

        public int? GetGLId(string code)
        {
            if (code == "")
            {
                return 0;
            }
            else
            {
                var gl = _scifferContext.ref_general_ledger.Where(x => x.gl_ledger_code == code.Trim()).FirstOrDefault();
                return gl == null ? 0 : gl.gl_ledger_id;
            }

        }

        public int GetTaxElementId(string code)
        {
            var tax = _scifferContext.ref_tax_element.Where(x => x.tax_element_code == code).FirstOrDefault();
            return tax == null ? 0 : tax.tax_element_id;
        }

        public List<tax_vm> GetTaxCalculation(string entity, string st, double amt, DateTime dt, int tds_code_id)
        {
            var sp = new SqlParameter("@tax", st);
            var ent = new SqlParameter("@entity", entity);
            var amount = new SqlParameter("@amount", amt);
            var posting_date = new SqlParameter("@posting_date", dt);
            var tds_code = new SqlParameter("@tds_code_id", tds_code_id);
            var val = _scifferContext.Database.SqlQuery<tax_vm>(
            "exec CalculateTax @entity,@tax,@amount,@posting_date,@tds_code_id", ent, sp, amount, posting_date, tds_code).ToList();
            return val;
        }


        public int GetUOMIDByitemid(int id)
        {
            var uom = _scifferContext.REF_ITEM.Where(x => x.ITEM_ID == id).FirstOrDefault();
            return uom == null ? 0 : uom.UOM_ID;
        }

        public string GetDocumentNumbering(int id)
        {
            string doc_no = "";
            string st;
            var doc = _scifferContext.ref_document_numbring.Where(x => x.document_numbring_id == id).FirstOrDefault();
            if (doc != null)
            {
                var len = doc.from_number.Length;
                if (len == 1)
                    st = "0";
                else if (len == 2)
                {
                    st = "00";
                }
                else if (len == 3)
                {
                    st = "000";
                }
                else if (len == 4)
                {
                    st = "0000";
                }
                else if (len == 5)
                {
                    st = "00000";
                }
                else if (len == 6)
                {
                    st = "000000";
                }
                else
                {
                    st = "0000000";
                }
                if (doc.current_number == null)
                {
                    if (doc.prefix_sufix_id == 1)
                    {
                        doc_no = doc.prefix_sufix + double.Parse(doc.from_number).ToString(st);
                    }
                    else
                    {
                        doc_no = double.Parse(doc.from_number).ToString(st) + doc.prefix_sufix;
                    }
                }
                else
                {

                    if (doc.prefix_sufix_id == 1)
                    {
                        doc_no = doc.prefix_sufix + (double.Parse(doc.current_number) + 1).ToString(st);
                    }
                    else
                    {
                        doc_no = (double.Parse(doc.current_number) + 1).ToString(st) + doc.prefix_sufix;
                    }
                }
            }

            return doc_no;
        }

        public int GetVendorId(string code)
        {
            var vendor = _scifferContext.REF_VENDOR.Where(x => x.VENDOR_CODE == code.Trim() && x.IS_BLOCKED == false).FirstOrDefault();
            return vendor == null ? 0 : vendor.VENDOR_ID;
        }

        public REF_VENDOR_VM VendorDetail(int id)
        {
            var query = (from cm in _scifferContext.REF_VENDOR.Where(x => x.VENDOR_ID == id)
                         join s in _scifferContext.REF_STATE on cm.BILLING_STATE_ID equals s.STATE_ID
                         join p in _scifferContext.REF_PAYMENT_CYCLE on cm.PAYMENT_CYCLE_ID equals p.PAYMENT_CYCLE_ID
                         select new REF_VENDOR_VM
                         {
                             BILLING_ADDRESS = cm.BILLING_ADDRESS,
                             BILLING_CITY = cm.BILLING_CITY,
                             BILLING_STATE_ID = cm.BILLING_STATE_ID,
                             BILLING_COUNTRY_ID = s.COUNTRY_ID,
                             BILLING_PINCODE = cm.BILLING_PINCODE,
                             EMAIL_ID_PRIMARY = cm.EMAIL_ID_PRIMARY,
                             PAYMENT_CYCLE_ID = cm.PAYMENT_CYCLE_ID,
                             PAYMENT_TERMS_ID = cm.PAYMENT_TERMS_ID,
                             FREIGHT_TERMS_ID = cm.FREIGHT_TERMS_ID,
                             PAYMENT_CYCLE_TYPE_ID = p.PAYMENT_CYCLE_TYPE_ID,
                             gst_no = cm.gst_no,
                             pan_no = cm.pan_no,
                             service_tax_no = cm.service_tax_no,
                             ecc_no = cm.ecc_no,
                             cst_tin_no = cm.cst_tin_no,
                             vat_tin_no = cm.vat_tin_no,
                             CREDIT_LIMIT_CURRENCY_ID = cm.CREDIT_LIMIT_CURRENCY_ID,
                             gst_vendor_type_id = cm.gst_vendor_type_id,
                             TELEPHONE_PRIMARY = cm.TELEPHONE_PRIMARY,
                         }).FirstOrDefault();
            return query;
        }



        public string GetAttachment(int id, string ctrl)
        {
            var contname = "";
            switch (ctrl)
            {
                case "WithinPlantTransfer":
                    contname = _scifferContext.pla_transfer.FirstOrDefault(a => a.pla_transfer_id == id).pla_attachment;
                    break;
                case "CustomerMaster":
                    contname = _scifferContext.REF_CUSTOMER.FirstOrDefault(a => a.CUSTOMER_ID == id).attachment;
                    break;
                case "VendorMaster":
                    contname = _scifferContext.REF_VENDOR.FirstOrDefault(a => a.VENDOR_ID == id).attachment;
                    break;
                case "PurchaseOrder":
                    contname = _scifferContext.pur_po.FirstOrDefault(a => a.po_id == id).attachement;
                    break;
                case "SalesOrder":
                    // contname = _scifferContext.SAL_SO.FirstOrDefault(a => a.so_id == id).attachment;
                    break;
                case "Employee":
                    contname = _scifferContext.REF_EMPLOYEE.FirstOrDefault(a => a.employee_id == id).attachment;
                    break;
                case "GRN":
                    contname = _scifferContext.pur_grn.FirstOrDefault(a => a.grn_id == id).attachment;
                    break;
                default:
                    break;
            }
            return contname;
        }
        public List<REF_VENDOR_CATEGORY> GetVendorCategory()
        {
            return _scifferContext.REF_VENDOR_CATEGORY.Where(x => x.is_active == true && x.is_blocked == false).ToList();
        }
        public List<ref_priority_vm> GetPriorityByForm(int id)
        {
            var query = (from p in _scifferContext.REF_PRIORITY.Where(x => x.form_id == id)
                         select new ref_priority_vm
                         {
                             PRIORITY_ID = p.PRIORITY_ID,
                             PRIORITY_NAME = p.PRIORITY_NAME,
                         }).ToList();
            return query;
        }

        public List<ref_ledger_account_type_vm> GetLedgerAccountType(int entity_type_id, int entity_id, int? item_type_id)
        {
            item_type_id = item_type_id == null ? 0 : item_type_id;
            var query = (from la in _scifferContext.ref_ledger_account_type.Where(x => x.entity_type_id == entity_type_id && x.item_type_id == item_type_id)
                         join s in _scifferContext.REF_SUB_LEDGER.Where(x => x.entity_id == entity_id && x.entity_type_id == entity_type_id) on la.ledger_account_type_id equals s.ledger_account_type_id into j0
                         from terr in j0.DefaultIfEmpty()
                         join lg in _scifferContext.ref_general_ledger on terr.gl_ledger_id equals lg.gl_ledger_id into j1
                         from terr1 in j1.DefaultIfEmpty()
                         select new ref_ledger_account_type_vm
                         {
                             gl_ledger_code = (terr1 == null ? String.Empty : terr1.gl_ledger_code),
                             gl_ledger_name = (terr1 == null ? String.Empty : terr1.gl_ledger_name),
                             gl_ledger_id = (terr1 == null ? 0 : terr1.gl_ledger_id),
                             ledger_account_type_id = la.ledger_account_type_id,
                             ledger_account_type_name = la.ledger_account_type_name,
                             sub_ledger_id = (terr == null ? 0 : terr.sub_ledger_id),
                         }).ToList();
            return query;
        }
        public string GetFilePathForImage(string controller, HttpPostedFileBase files, string employee_code)
        {
            CreateIfMissing("~/Files/" + controller + "/ ");
            string extension = Path.GetExtension(files.FileName);
            string A = files.FileName;
            var B = A.Replace(',', ' ');
            var C = B.Replace(':', ' ');
            var filename = employee_code;
            string fileLocation = string.Format("{0}/{1}{2}", System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller + "/ "), filename, extension);
            files.SaveAs(fileLocation);
            var filenamereturn = fileLocation;//"~/Files/" + controller + "/" + employee_code + extension;
            return filenamereturn;
        }

        public string GetFilePathForImage1(string controller, HttpPostedFileBase files, string employee_code)
        {
            CreateIfMissing("~/Files/" + controller + "/ ");
            string extension = Path.GetExtension(files.FileName);
            string A = files.FileName;
            var B = A.Replace(',', ' ');
            var C = B.Replace(':', ' ');
            var filename = employee_code;
            string fileLocation = string.Format("{0}/{1}{2}", System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller + "/ "), filename, extension);
            files.SaveAs(fileLocation);
            var filenamereturn = "~/Files/" + controller + "/" + employee_code + extension; //"~/Files/" + controller + "/" + employee_code + extension;
            return filenamereturn;
        }

        public int GetShelfLifeId(string name)
        {
            var life = _scifferContext.ref_shelf_life.Where(x => x.shelf_life_name == name.Trim()).FirstOrDefault();
            return life == null ? 0 : life.shelf_life_id;
        }
        public int GetInstructionID(string name)
        {
            var gen = _scifferContext.ref_instruction_type.Where(x => x.instruction_name == name.Trim() && x.is_active == true).FirstOrDefault();
            return gen == null ? 0 : gen.instruction_type_id;
        }

        public int GetFinancialYearID(string code)
        {
            var fin = _scifferContext.REF_FINANCIAL_YEAR.Where(x => x.FINANCIAL_YEAR_NAME == code).FirstOrDefault();
            return fin == null ? 0 : fin.FINANCIAL_YEAR_ID;
        }

        public int GetEmployeeID(string code)
        {
            var emp = _scifferContext.REF_EMPLOYEE.Where(x => x.employee_code == code && x.is_block == false).FirstOrDefault();
            return emp == null ? 0 : emp.employee_id;
        }

        public int GetPlantID(string name)
        {
            var plant = _scifferContext.REF_PLANT.FirstOrDefault(x => x.PLANT_CODE == name.Trim() && x.is_active == true);
            return plant == null ? 0 : plant.PLANT_ID;
        }

        public int GetTaxId(string code)
        {
            var taxid = _scifferContext.ref_tax.Where(x => x.tax_code == code).FirstOrDefault();
            return taxid == null ? 0 : taxid.tax_id;
        }

        public List<ref_ledger_vm> GetLedgerAccount(int id)
        {
            var query = (from gl in _scifferContext.ref_general_ledger.Where(x => x.gl_head_account == id)
                         select new ref_ledger_vm
                         {
                             gl_ledger_id = gl.gl_ledger_id,
                             gl_ledger_name = gl.gl_ledger_code + "/" + gl.gl_ledger_name,
                         }).ToList();
            return query;
        }

        public List<ref_ledger_vm> GetLedgerAccountOnlyActive(int id)
        {
            var query = (from gl in _scifferContext.ref_general_ledger.Where(x => x.gl_head_account == id && x.is_active == true && x.is_blocked == false)
                         select new ref_ledger_vm
                         {
                             gl_ledger_id = gl.gl_ledger_id,
                             gl_ledger_name = gl.gl_ledger_code + "/" + gl.gl_ledger_name,
                         }).ToList();
            return query;
        }

        public List<ref_bank_vm> GetBankforSearchDropdown()
        {
            var query = (from gl in _scifferContext.ref_bank
                         select new ref_bank_vm
                         {
                             bank_id = gl.bank_id,
                             bank_code = gl.bank_code + "/" + gl.bank_name,
                         }).ToList();
            return query;
        }
        public List<ref_ledger_account_type_vm> GetLedgerAccountTypeByItem(int entity_type_id, int item_category_id, int? item_type_id)
        {
            item_type_id = item_type_id == null ? 0 : item_type_id;
            var query = (from la in _scifferContext.ref_ledger_account_type.Where(x => x.entity_type_id == entity_type_id && x.item_type_id == item_type_id)
                         join s in _scifferContext.ref_item_category_gl.Where(x => x.item_category_id == item_category_id) on la.ledger_account_type_id equals s.ledger_account_type_id into j0
                         from terr in j0.DefaultIfEmpty()
                         join lg in _scifferContext.ref_general_ledger on terr.gl_ledger_id equals lg.gl_ledger_id into j1
                         from terr1 in j1.DefaultIfEmpty()
                         select new ref_ledger_account_type_vm
                         {
                             gl_ledger_code = (terr1 == null ? String.Empty : terr1.gl_ledger_code),
                             gl_ledger_name = (terr1 == null ? String.Empty : terr1.gl_ledger_name),
                             gl_ledger_id = (terr1 == null ? 0 : terr1.gl_ledger_id),
                             ledger_account_type_id = la.ledger_account_type_id,
                             ledger_account_type_name = la.ledger_account_type_name,
                             //sub_ledger_id = (terr == null ? 0 : terr.sub_ledger_id),
                         }).ToList();

            return query;
        }
        public int GetBankId(string code)
        {
            var bank = _scifferContext.ref_bank.Where(x => x.bank_code == code && x.is_active == true).FirstOrDefault();
            return bank == null ? 0 : bank.bank_id;
        }

        public int GetCustomerCategoryId(string name)
        {
            var customerCategory = _scifferContext.REF_CUSTOMER_CATEGORY.FirstOrDefault(x => x.CUSTOMER_CATEGORY_NAME == name);
            return customerCategory == null ? 0 : customerCategory.CUSTOMER_CATEGORY_ID;
        }
        public int GetOrgId(string name)
        {
            var org = _scifferContext.REF_ORG_TYPE.FirstOrDefault(x => x.ORG_TYPE_NAME == name);
            return org == null ? 0 : org.ORG_TYPE_ID;
        }
        public int GetStateID(string name)
        {
            var state = _scifferContext.REF_STATE.FirstOrDefault(x => x.STATE_NAME == name && x.is_active == true);
            return state == null ? 0 : state.STATE_ID;
        }
        public int GetTerritoryId(string name)
        {
            var territory = _scifferContext.REF_TERRITORY.FirstOrDefault(x => x.TERRITORY_NAME == name && x.is_active == true);
            return territory == null ? 0 : territory.TERRITORY_ID;
        }
        public int GetSalesRMID(string name)
        {
            var salesRM = _scifferContext.ref_sales_rm.FirstOrDefault(x => x.REF_EMPLOYEE.employee_code == name && x.is_active == true);
            return salesRM == null ? 0 : salesRM.sales_rm_id;
        }
        public int GetEmployeeIdFromUser(int user_id)
        {
            var eid = _scifferContext.ref_user_management.Where(x => x.user_id == user_id).FirstOrDefault().employee_id;
            return eid == null ? 0 : (int)eid;
        }

        public int GetCustomerParentId(string name)
        {
            var customerParentID = _scifferContext.REF_CUSTOMER_PARENT.FirstOrDefault(x => x.CUSTOMER_PARENT_NAME == name);
            return customerParentID == null ? 0 : customerParentID.CUSTOMER_PARENT_ID;
        }
        public int GetFreightId(string name)
        {
            var FreightTerm = _scifferContext.REF_FREIGHT_TERMS.FirstOrDefault(x => x.FREIGHT_TERMS_NAME == name);
            return FreightTerm == null ? 0 : FreightTerm.FREIGHT_TERMS_ID;
        }
        public int GetPriorityId(string name)
        {
            var priority = _scifferContext.REF_PRIORITY.FirstOrDefault(x => x.PRIORITY_NAME == name);
            return priority == null ? 0 : priority.PRIORITY_ID;
        }
        public int GetCurrencyId(string name)
        {
            var currency = _scifferContext.REF_CURRENCY.FirstOrDefault(x => x.CURRENCY_NAME == name);
            return currency == null ? 0 : currency.CURRENCY_ID;
        }
        public int GetPaymentTermId(string name)
        {
            var paymentTerm = _scifferContext.REF_PAYMENT_TERMS.FirstOrDefault(x => x.payment_terms_code == name);
            return paymentTerm == null ? 0 : paymentTerm.payment_terms_id;
        }
        public int GetPaymentCycleTypeId(string name)
        {
            var paymentCycleType = _scifferContext.REF_PAYMENT_CYCLE_TYPE.FirstOrDefault(x => x.PAYMENT_CYCLE_TYPE_NAME == name);
            return paymentCycleType == null ? 0 : paymentCycleType.PAYMENT_CYCLE_TYPE_ID;
        }
        public List<ref_bank_vm> GetBankForSearchingDropdown()
        {
            var query = (from p in _scifferContext.ref_bank.Where(x => x.is_active == true)
                         select new ref_bank_vm
                         {
                             bank_id = p.bank_id,
                             bank_code = p.bank_code + "/" + p.bank_name,
                         }).ToList();
            return query;
        }
        public List<ref_ledger_vm> GetGLForSearchingDropdown()
        {
            var query = (from p in _scifferContext.ref_general_ledger.Where(x => x.is_active == true)
                         select new ref_ledger_vm
                         {
                             gl_ledger_id = p.gl_ledger_id,
                             gl_ledger_code = p.gl_ledger_code + "/" + p.gl_ledger_name,
                         }).ToList();
            return query;
        }
        public int GetPaymentCycleId(string name)
        {
            var paymentCycle = _scifferContext.REF_PAYMENT_CYCLE.FirstOrDefault(x => x.PAYMENT_CYCLE_NAME == name);
            return paymentCycle == null ? 0 : paymentCycle.PAYMENT_CYCLE_ID;
        }
        public int GetItemCategoryId(string name)
        {
            var itemCategory = _scifferContext.REF_ITEM_CATEGORY.FirstOrDefault(x => x.ITEM_CATEGORY_NAME == name && x.is_active == true);
            return itemCategory == null ? 0 : itemCategory.ITEM_CATEGORY_ID;
        }
        public int GetLedgerAccountTypeId(string name, int entity_type_id)
        {
            var glAccountType = _scifferContext.ref_ledger_account_type.FirstOrDefault(x => x.ledger_account_type_name.ToLower() == name.ToLower() && x.entity_type_id == entity_type_id);
            return glAccountType == null ? 0 : glAccountType.ledger_account_type_id;
        }

        public int GetVendorCategoryId(string name)
        {
            var vendorCategory = _scifferContext.REF_VENDOR_CATEGORY.FirstOrDefault(x => x.VENDOR_CATEGORY_NAME == name);
            return vendorCategory == null ? 0 : vendorCategory.VENDOR_CATEGORY_ID;
        }
        public int GetVendorParentId(string name)
        {
            var vendorparent = _scifferContext.REF_VENDOR_PARENT.FirstOrDefault(x => x.VENDOR_PARENT_NAME == name);
            return vendorparent == null ? 0 : vendorparent.VENDOR_PARENT_ID;
        }
        public int GetItemTypeId(string name)
        {
            var ItemTypeId = _scifferContext.ref_item_type.FirstOrDefault(x => x.item_type_name == name);
            return ItemTypeId == null ? 0 : ItemTypeId.item_type_id;
        }
        public int GetItemGroupId(string name)
        {
            var itemGroupId = _scifferContext.REF_ITEM_GROUP.FirstOrDefault(x => x.ITEM_GROUP_NAME == name);
            return itemGroupId == null ? 0 : itemGroupId.ITEM_GROUP_ID;
        }
        public int GetBrandId(string name)
        {
            var brandId = _scifferContext.REF_BRAND.FirstOrDefault(x => x.BRAND_NAME == name);
            return brandId == null ? 0 : brandId.BRAND_ID;
        }
        public int GetUoMId(string name)
        {
            var UoM_id = _scifferContext.REF_UOM.FirstOrDefault(x => x.UOM_NAME == name);
            return UoM_id == null ? 0 : UoM_id.UOM_ID;
        }
        public int GetItemValuationId(string name)
        {
            var ItemValuationId = _scifferContext.REF_ITEM_VALUATION.FirstOrDefault(x => x.ITEM_VALUATION_NAME == name);
            return ItemValuationId == null ? 0 : ItemValuationId.ITEM_VALUATION_ID;
        }
        public int GetItemAccountingId(string name)
        {
            var ItemAccountingId = _scifferContext.REF_ITEM_ACCOUNTING.FirstOrDefault(x => x.ITEM_ACCOUNTING_NAME == name);
            return ItemAccountingId == null ? 0 : ItemAccountingId.ITEM_ACCOUNTING_ID;
        }
        public int GetExcisecategoryid(string name)
        {
            var ExciseCategoryId = _scifferContext.REF_EXCISE_CATEGORY.FirstOrDefault(x => x.EXCISE_CATEGORY_NAME == name);
            return ExciseCategoryId == null ? 0 : ExciseCategoryId.EXCISE_CATEGORY_ID;
        }
        public int GetTdsCodeId(string code)
        {
            var tds_code = _scifferContext.ref_tds_code.FirstOrDefault(x => x.tds_code == code && x.is_active == true);
            return tds_code == null ? 0 : tds_code.tds_code_id;
        }
        public List<REF_CUSTOMER_VM> GetCustomerList()
        {
            var query = (from c in _scifferContext.REF_CUSTOMER.Where(x => x.IS_BLOCKED == false)
                         select new REF_CUSTOMER_VM
                         {
                             CUSTOMER_ID = c.CUSTOMER_ID,
                             CUSTOMER_NAME = c.CUSTOMER_CODE + "/" + c.CUSTOMER_NAME,
                         }).ToList();
            return query;
        }

        public List<ref_tax_vm> GetTaxList()
        {
            var query = (from t in _scifferContext.ref_tax.Where(x => x.is_blocked == false)
                         select new ref_tax_vm
                         {
                             tax_id = t.tax_id,
                             tax_name = t.tax_code + "/" + t.tax_name,
                         }).ToList();
            return query;
        }

        public List<Ref_item_VM> GetItemList()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false)
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                             item_type_id = i.item_type_id,
                         }).ToList();
            return query;
        }

        public List<Ref_item_VM> GetItemListForReOrder()
        {
            var entity = new SqlParameter("@entity", "ReportDropDown");
            var val = _scifferContext.Database.SqlQuery<Ref_item_VM>(
                    "exec Get_ReorderCount @entity", entity).ToList();
            return val;
        }

        public List<Ref_item_VM> GetItemListOnlyRMCategory(int categoryid)
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false && x.ITEM_CATEGORY_ID == categoryid)
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                             item_type_id = i.item_type_id,
                         }).ToList();
            return query;
        }
        public List<Ref_item_VM> GetItemListRMCategorywise()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false && x.ITEM_CATEGORY_ID == 3)
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                             item_type_id = i.item_type_id,
                         }).ToList();
            return query;
        }
        public List<ref_mfg_process_vm> GetProcessList()
        {
            var query = (from i in _scifferContext.ref_mfg_process.Where(x => x.is_active == true)
                         select new ref_mfg_process_vm
                         {
                             operation_id = i.process_id,
                             process_code = i.process_code + "/" + i.process_description,
                         }).ToList();
            return query;
        }
        public List<inv_item_batch_detail_tag_vm> GetTagList()
        {
            var q = (from it in _scifferContext.inv_item_batch_detail_tag
                     join iv in _scifferContext.inv_item_batch on it.item_batch_id equals iv.item_batch_id
                     join i in _scifferContext.REF_ITEM.Where(x => x.ITEM_CATEGORY_ID == 2) on iv.item_id equals i.ITEM_ID
                     select new inv_item_batch_detail_tag_vm
                     {
                         tag_id = it.tag_id,
                         tag_no = it.tag_no,
                         item_id = iv.item_id,
                         balance_qty = it.balance_qty,
                     }).ToList();
            return q;
        }

        public List<ref_machine_master_VM> GetMachineListOnProcessId(int process_id)
        {
            var q = (from it in _scifferContext.ref_mfg_process.Where(x => x.process_id == process_id)
                     join iv in _scifferContext.map_mfg_process_machine on it.process_id equals iv.process_id
                     join i in _scifferContext.ref_machine on iv.machine_id equals i.machine_id
                     select new ref_machine_master_VM
                     {
                         machine_id = i.machine_id,
                         machine_code = i.machine_code + " / " + i.machine_name,
                     }).ToList();
            return q;
        }

        public List<Ref_item_VM> GetItemListByType()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false && x.item_type_id != 2)
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                         }).ToList();
            return query;
        }

        public List<payment_terms_vm> GetPaymentTermsList()
        {
            var query = (from p in _scifferContext.REF_PAYMENT_TERMS.Where(x => x.is_active == true && x.is_blocked == false)
                         select new payment_terms_vm
                         {
                             payment_terms_id = p.payment_terms_id,
                             payment_terms_description = p.payment_terms_code + "/" + p.payment_terms_description,
                         }).ToList();
            return query;
        }

        public List<REF_PLANT_VM> GetPlantList()
        {
            var query = (from p in _scifferContext.REF_PLANT.Where(x => x.is_active == true && x.is_blocked == false)
                         select new REF_PLANT_VM
                         {
                             PLANT_ID = p.PLANT_ID,
                             PLANT_NAME = p.PLANT_NAME,
                         }).ToList();
            return query;
        }

        //public List<REF_PLANT_VM> GetOperatorList()
        //{
        //    var query = from item1 in _scifferContext.REF_EMPLOYEE
        //                join item2 in _scifferContext.ref_machine
        //                on item1.plant_id equals item2.plant_id
        //                where item2.is_blocked == false && item2.is_active == true
        //                select new REF_PLANT_VM
        //                {
        //                    PLANT_ID = m.plant_id,
        //                    PLANT_NAME = p.PLANT_NAME,
        //                }).ToList();
        //    //var query = (from e in _scifferContext.REF_EMPLOYEE join m in _scifferContext.ref_machine on new { e.plant_id }  equals new { m.plant_id }
        //    //             m.Where(x => x.is_active == true && x.is_blocked == false)
        //    //             select new REF_PLANT_VM
        //    //             {
        //    //                 PLANT_ID = m.plant_id,
        //    //                 PLANT_NAME = p.PLANT_NAME,
        //    //             }).ToList();
        //    return query;
        //}

        public List<ref_mfg_process_vm> GetOperationList()
        {
            var query = (from p in _scifferContext.ref_mfg_process.Where(x => x.is_active == true && x.is_blocked == false)
                         select new ref_mfg_process_vm
                         {
                             process_id = p.process_id,
                             process_description = p.process_code + "/" + p.process_description,
                         }).ToList();
            return query;
        }

        public List<ref_business_unit_vm> GetBusinessUnitList()
        {
            var query = (from b in _scifferContext.REF_BUSINESS_UNIT.Where(x => x.is_active == true && x.is_blocked == false)
                         select new ref_business_unit_vm
                         {
                             business_unit_id = b.BUSINESS_UNIT_ID,
                             business_unit_name = b.BUSINESS_UNIT_DESCRIPTION,
                         }).ToList();
            return query;
        }
        public List<REF_CUSTOMER_VM> GetCustomerforsearchdropdown()
        {
            var query = (from b in _scifferContext.REF_CUSTOMER
                         select new REF_CUSTOMER_VM
                         {
                             CUSTOMER_ID = b.CUSTOMER_ID,
                             CUSTOMER_NAME = b.CUSTOMER_CODE + "/" + b.CUSTOMER_NAME,
                         }).ToList();
            return query;
        }
        public List<ref_hsn_code_vm> GetHSNList()
        {
            var query = (from h in _scifferContext.ref_hsn_code.Where(x => x.is_blocked == false)
                         select new ref_hsn_code_vm
                         {
                             hsn_code = h.hsn_code + "/" + h.hsn_code_description,
                             hsn_code_id = h.hsn_code_id,
                         }).ToList();
            return query;

        }
        public List<storage_vm> GetStorageLocationList(int id)
        {
            List<storage_vm> query = new List<storage_vm>();
            if (id != 0)
            {
                var state = _scifferContext.REF_PLANT.Where(a => a.PLANT_ID == id).FirstOrDefault().REF_STATE.STATE_ID;
                query = (from s in _scifferContext.REF_STORAGE_LOCATION.Where(x => x.is_active == true && x.is_blocked == false && x.plant_id == id)
                         select new storage_vm
                         {
                             storage_location_id = s.storage_location_id,
                             storage_location_name = s.storage_location_name + "/" + s.description,
                             state_id = state
                         }).ToList();
            }
            else
            {
                query = (from s in _scifferContext.REF_STORAGE_LOCATION.Where(x => x.is_active == true && x.is_blocked == false)
                         select new storage_vm
                         {
                             storage_location_id = s.storage_location_id,
                             storage_location_name = s.storage_location_name + "/" + s.description,
                         }).ToList();
            }

            return query;
        }
        public List<tds_codeVM> GetTDSCodeList()
        {
            var query = (from td in _scifferContext.ref_tds_code.Where(x => x.is_active == true)
                         select new tds_codeVM
                         {
                             tds_code_id = td.tds_code_id,
                             tds_code_description = td.tds_code + " - " + td.tds_code_description,
                         }).ToList();
            return query;

        }
        public List<REF_ITEM_CATEGORYVM> GetItemCategoryList()
        {
            var query = (from i in _scifferContext.REF_ITEM_CATEGORY.Where(x => x.is_blocked == false && x.is_active == true)
                         select new REF_ITEM_CATEGORYVM
                         {
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                             ITEM_CATEGORY_NAME = i.ITEM_CATEGORY_NAME + "/" + i.ITEM_CATEGORY_DESCRIPTION,
                         }).ToList();
            return query;
        }
        public List<credit_debit_vm> GetCreditDebit(string ent, int customer_id, double total_value, double basic_value, string item_sales_gl, int tds_code_id, DateTime posting_date, double round_off)
        {
            try
            {
                var entity = new SqlParameter("@entity", ent);
                var cutomer = new SqlParameter("@customer_id", customer_id);
                var total = new SqlParameter("@total_value", total_value);
                var basic = new SqlParameter("@basic_value", basic_value);
                var tds_code = new SqlParameter("@tds_code_id", tds_code_id);
                var item_tax = new SqlParameter("@item_sales_gl", item_sales_gl);
                var posting = new SqlParameter("@posting_date", posting_date);
                var round = new SqlParameter("@round_off", round_off);
                var val = _scifferContext.Database.SqlQuery<credit_debit_vm>(
                "exec GetCreditDebitAccount @entity,@customer_id,@total_value,@basic_value,@item_sales_gl,@tds_code_id,@posting_date,@round_off",
                entity, cutomer, total, basic, tds_code, item_tax, posting, round).ToList();
                return val;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public List<REF_VENDOR_VM> GetVendorList()
        {
            var query = (from v in _scifferContext.REF_VENDOR.Where(x => x.IS_BLOCKED == false)
                         select new REF_VENDOR_VM
                         {
                             VENDOR_ID = v.VENDOR_ID,
                             VENDOR_NAME = v.VENDOR_CODE + "/" + v.VENDOR_NAME,
                         }).ToList();
            return query;
        }

        public List<ref_status> GetStatusList(string form)
        {
            return _scifferContext.ref_status.Where(x => x.form == form).ToList();
        }


        public List<REF_EMPLOYEE_VM> GetSalesRM()
        {
            var query = (from emp in _scifferContext.REF_EMPLOYEE.Where(x => x.is_block == false)
                         join srm in _scifferContext.ref_sales_rm.Where(x => x.is_blocked == false) on emp.employee_id equals srm.employee_id
                         select new REF_EMPLOYEE_VM
                         {
                             employee_id = srm.sales_rm_id,
                             employee_name = emp.employee_code + "/" + emp.employee_name,
                         }).ToList();
            return query;
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    try
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
                    {

                    }
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public List<ref_item_parameter> GetParameter()
        {
            return _scifferContext.ref_item_parameter.ToList();
        }
        public List<ref_machine_category_VM> GetMachineCategoryList()
        {
            var query = (from i in _scifferContext.ref_machine_category.Where(x => x.is_blocked == false && x.is_active == true)
                         select new ref_machine_category_VM
                         {
                             machine_category_id = i.machine_category_id,
                             machine_category_code = i.machine_category_code + "/" + i.machine_category_description,
                         }).ToList();
            return query;
        }
        public List<ref_machine_master_VM> GetMachineList(int id)
        {
            if (id == 0)
            {
                var query = (from i in _scifferContext.ref_machine.Where(x => x.is_blocked == false && x.is_active == true)
                             select new ref_machine_master_VM
                             {
                                 machine_id = i.machine_id
                                ,
                                 machine_code = i.machine_code + "/" + i.machine_name
                                ,
                                 machine_name = i.machine_name
                                 //Newly Added
                                ,
                                 plant_id = i.plant_id
                                //,plant_name= _scifferContext.REF_PLANT.Where(n => n.PLANT_ID == i.plant_id ).Select(y => y.PLANT_NAME).FirstOrDefault()
                                ,
                                 purchase_order_id = i.purchase_order_id
                                ,
                                 manufacturer = i.manufacturer
                                ,
                                 manufacturer_part_no = i.manufacturer_part_no
                                ,
                                 manufacturing_country_id = i.manufacturing_country_id
                                ,
                                 priority_id = i.priority_id
                                ,
                                 purchasing_vendor = i.purchasing_vendor
                                ,
                                 model_no = i.model_no
                                ,
                                 manufacturing_serial_number = i.manufacturing_serial_number
                                ,
                                 business_unit_id = i.business_area
                                ,
                                 cost_center_id = i.cost_center
                                ,
                                 asset_tag_no = i.asset_tag_no
                                ,
                                 manufacturing_date = i.manufacturing_date

                             }).ToList();
                return query;
            }
            else
            {
                var query = (from i in _scifferContext.ref_machine.Where(x => x.is_blocked == false && x.is_active == true && x.machine_id == id)
                             select new ref_machine_master_VM
                             {
                                 machine_id = i.machine_id,
                                 machine_code = i.machine_code + "/" + i.machine_name,
                                 plant_id = i.plant_id,
                                 manufacturer = i.manufacturer,
                                 model_no = i.model_no,
                                 manufacturer_part_no = i.manufacturer_part_no,
                                 manufacturing_serial_number = i.manufacturing_serial_number,
                                 asset_code_id = i.asset_code_id,
                                 asset_tag_no = i.asset_tag_no,
                                 machine_category_id = i.machine_category_id,
                                 machine_name = i.machine_name,

                             }).ToList();
                return query;

            }


        }

        public List<ref_machine_master_VM> GetMachineListWithPlant(int plantId)
        {

            var query = (from i in _scifferContext.ref_machine.Where(x => x.is_blocked == false && x.is_active == true && x.plant_id == plantId)
                         select new ref_machine_master_VM
                         {
                             machine_id = i.machine_id
                            ,
                             machine_code = i.machine_code + "/" + i.machine_name
                            ,
                             machine_name = i.machine_name
                            //Newly Added
                            ,
                             plant_id = i.plant_id
                            //,plant_name= _scifferContext.REF_PLANT.Where(n => n.PLANT_ID == i.plant_id ).Select(y => y.PLANT_NAME).FirstOrDefault()
                            ,
                             purchase_order_id = i.purchase_order_id
                            ,
                             manufacturer = i.manufacturer
                            ,
                             manufacturer_part_no = i.manufacturer_part_no
                            ,
                             manufacturing_country_id = i.manufacturing_country_id
                            ,
                             priority_id = i.priority_id
                            ,
                             purchasing_vendor = i.purchasing_vendor
                            ,
                             model_no = i.model_no
                            ,
                             manufacturing_serial_number = i.manufacturing_serial_number
                            ,
                             business_unit_id = i.business_area
                            ,
                             cost_center_id = i.cost_center
                            ,
                             asset_tag_no = i.asset_tag_no
                            ,
                             manufacturing_date = i.manufacturing_date

                         }).ToList();
            return query;

        }
        public List<ref_machine_master_VM> GetMachineListWithOperationAndUser(int userId)
        {
            List<ref_machine_master_VM> result = null;
            try
            {
                result = (from mmpm in _scifferContext.map_mfg_process_machine
                          join moo in _scifferContext.map_operation_operator on mmpm.process_id equals moo.operation_id
                          join um in _scifferContext.ref_user_management.Where(x => x.user_id == userId && !x.is_block) on moo.operator_id equals um.user_id
                          join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                          select new ref_machine_master_VM
                          {
                              machine_id = rm.machine_id,
                              machine_name = rm.machine_name

                          }).ToList();
            }
            catch (Exception ex)
            {
                throw;
                return result;
            }
            return result;
        }

        public List<REF_CURRENCY> GetCurrencyList()
        {
            return _scifferContext.REF_CURRENCY.Where(x => x.is_blocked == false).ToList();
        }
        public List<ref_machine_master_VM> GetallMachineList()
        {
            var query = (from i in _scifferContext.ref_machine.Where(x => x.is_blocked == false && x.is_active == true)
                         select new ref_machine_master_VM
                         {
                             machine_id = i.machine_id,
                             machine_code = i.machine_code + "/" + i.machine_name,
                             plant_id = i.plant_id,
                         }).ToList();
            return query;
        }
        public List<ref_bank_vm> GetBankList()
        {
            var query = (from b in _scifferContext.ref_bank.Where(x => x.is_active == true)
                         select new ref_bank_vm
                         {
                             bank_id = b.bank_id,
                             bank_name = b.bank_code + "/" + b.bank_name,
                         }).ToList();
            return query;
        }
        public List<REF_PAYMENT_TYPE> GetPaymentTypeList()
        {
            return _scifferContext.REF_PAYMENT_TYPE.Where(x => x.is_active == true && x.is_blocked == false).ToList();
        }
        public List<REF_FREIGHT_TERMS> GetFreightTermsList()
        {
            return _scifferContext.REF_FREIGHT_TERMS.Where(x => x.Is_blocked == false && x.Is_active == true).ToList();
        }
        public List<ref_cash_account_VM> GetCashAccount()
        {
            var query = (from c in _scifferContext.ref_cash_account.Where(x => x.is_active == true && x.is_blocked == false)
                         select new ref_cash_account_VM
                         {
                             cash_account_id = c.cash_account_id,
                             cash_account_desc = c.cash_account_code + " - " + c.cash_account_desc,

                         }).ToList();
            return query;
        }
        public List<ref_bank_account_vm> GetBankAccountByBank(int id)
        {
            if (id == 0)
            {
                var query = (from b in _scifferContext.ref_bank_account.Where(x => x.is_active == true && x.is_blocked == false)
                             join bnk in _scifferContext.ref_bank on b.bank_id equals bnk.bank_id
                             select new ref_bank_account_vm
                             {
                                 bank_account_id = b.bank_account_id,
                                 bank_account_code = b.bank_account_code + "/" + bnk.bank_code + "/" + b.bank_account_number,
                             }).ToList();
                return query;
            }
            else
            {
                var query = (from b in _scifferContext.ref_bank_account.Where(x => x.is_active == true && x.is_blocked == false && x.bank_id == id)
                             join bnk in _scifferContext.ref_bank on b.bank_id equals bnk.bank_id
                             select new ref_bank_account_vm
                             {
                                 bank_account_id = b.bank_account_id,
                                 bank_account_code = b.bank_account_code + "/" + bnk.bank_code + "/" + b.bank_account_number,
                             }).ToList();
                return query;
            }

        }

        public object GetEntityDetailByEntity(string entity)
        {
            object data = null;
            switch (entity)
            {
                case "Customer":
                    data = _scifferContext.REF_CUSTOMER.Where(x => x.IS_BLOCKED == false).Select(i => new { i.CUSTOMER_ID, i.CUSTOMER_CODE, i.CUSTOMER_NAME }).ToList().Select(a => new EntityType { id = a.CUSTOMER_ID, code = a.CUSTOMER_CODE, name = a.CUSTOMER_NAME }).ToList(); ; ;
                    break;
                case "Vendor":
                    data = _scifferContext.REF_VENDOR.Where(x => x.IS_BLOCKED == false).Select(i => new { i.VENDOR_ID, i.VENDOR_CODE, i.VENDOR_NAME }).ToList().Select(a => new EntityType { id = a.VENDOR_ID, code = a.VENDOR_CODE, name = a.VENDOR_NAME }).ToList(); ; ;
                    break;
                case "Employee":
                    data = _scifferContext.REF_EMPLOYEE.Select(i => new { i.employee_id, i.employee_code, i.employee_name }).ToList().Select(a => new EntityType { id = a.employee_id, code = a.employee_code, name = a.employee_name }).ToList(); ; ;
                    break;
                case "Account":
                    data = _scifferContext.ref_general_ledger.Where(x => x.is_blocked == false && x.gl_head_account == 2).Select(i => new { i.gl_ledger_id, i.gl_ledger_name, i.gl_ledger_code }).ToList().Select(a => new EntityType { id = a.gl_ledger_id, code = a.gl_ledger_code, name = a.gl_ledger_name }).ToList();
                    break;
                default:
                    break;
            }
            return data;
        }
        public string CheckValidation(string entity, int id, DateTime posting_date)
        {
            var ent = new SqlParameter("@entity", entity);
            var idd = new SqlParameter("@id", id);
            var postingdate = new SqlParameter("@posting_date", posting_date);
            var val = _scifferContext.Database.SqlQuery<string>(
            "exec check_validation @entity,@id,@posting_date", ent, idd, postingdate).FirstOrDefault();
            return val;

        }

        public List<Ref_item_VM> GetRMItemList()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false && x.ITEM_CATEGORY_ID == 3)
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                         }).ToList();
            return query;
        }
        public List<Ref_item_VM> GetRMAndFGItemList()
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false && new int[] { 3, 2 }.Contains(x.ITEM_CATEGORY_ID))
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                         }).ToList();
            return query;
        }

        public List<REF_EMPLOYEE_VM> GetOpertorList(int id)
        {
            var query = (from u in _scifferContext.ref_user_management.Where(x => x.is_block == false)
                         join e in _scifferContext.REF_EMPLOYEE.Where(x => !x.is_block) on u.employee_id equals e.employee_id
                         join i in _scifferContext.ref_user_role_mapping on u.user_id equals i.user_id
                         join s in _scifferContext.ref_user_management_role.Where(x => x.role_code == "PROD_OP" || x.role_code == "PROD_SUP" || x.role_code == "PROD_MGR") on i.role_id equals s.role_id
                         //into j0 from j1 in j0.DefaultIfEmpty()
                         select new REF_EMPLOYEE_VM
                         {
                             employee_id = e.employee_id,
                             employee_code = e.employee_code + "/" + e.employee_name,
                         }).ToList();

            return query;

        }

        public List<REF_EMPLOYEE_VM> GetEmployeeList(int id)
        {
            if (id == 0)
            {
                var query = (from i in _scifferContext.REF_EMPLOYEE.Where(x => x.is_block == false)
                             select new REF_EMPLOYEE_VM
                             {
                                 employee_id = i.employee_id,
                                 employee_code = i.employee_code + "/" + i.employee_name,
                             }).ToList();
                return query;
            }
            else
            {
                var query = (from i in _scifferContext.ref_user_management.Where(x => x.is_block == false && x.user_id == id)
                             join subl in _scifferContext.REF_EMPLOYEE on i.employee_id equals subl.employee_id
                             select new REF_EMPLOYEE_VM
                             {
                                 employee_id = subl.employee_id,
                                 employee_code = subl.employee_code + "/" + subl.employee_name
                             }).ToList();

                query.Add(new REF_EMPLOYEE_VM { employee_id = 1, employee_code = "Admin" });

                return query;
            }
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





        public List<ref_ledger_account_type_vm> getOffsetAccount(string ledger_account_type_name, int ledger_account_type_id)
        {
            var query = (from ed in _scifferContext.ref_ledger_account_type.Where(x => x.ledger_account_type_name == ledger_account_type_name && x.entity_type_id == 9)
                         join subl in _scifferContext.REF_SUB_LEDGER on ed.ledger_account_type_id equals subl.ledger_account_type_id
                         join gl in _scifferContext.ref_general_ledger on subl.gl_ledger_id equals gl.gl_ledger_id
                         select new ref_ledger_account_type_vm
                         {
                             gl_ledger_id = subl.gl_ledger_id,
                             gl_ledger_name = gl.gl_ledger_name,
                             gl_ledger_code = gl.gl_ledger_code,
                         }).ToList();
            return query;
        }
        public List<pur_grnVM> GetGrnListByItemID(int item_id, int plant_id)
        {
            var query = (from ed in _scifferContext.pur_grn.Where(x => x.is_active == true && x.plant_id == plant_id)
                         join grn in _scifferContext.pur_grn_detail.Where(x => x.item_id == item_id) on ed.grn_id equals grn.grn_id
                         select new
                         {
                             grn_id = ed.grn_id,
                             grn_number = ed.grn_number,
                             posting_date = ed.posting_date,
                         }).ToList().Select(a => new pur_grnVM
                         {
                             grn_id = a.grn_id,
                             grn_number = a.grn_number + " - " + DateTime.Parse(a.posting_date.ToString()).ToString("dd/MMM/yyyy"),
                         }).ToList();
            return query;
        }

        public List<ref_ledger_vm> GetInventoryAccount(int id)
        {
            var query = (from ed in _scifferContext.REF_SUB_LEDGER.Where(x => x.entity_id == id && x.entity_type_id == 4)
                         join la in _scifferContext.ref_ledger_account_type.Where(y => y.ledger_account_type_name == "Inventory Account")
                         on ed.ledger_account_type_id equals la.ledger_account_type_id
                         join gl in _scifferContext.ref_general_ledger on ed.gl_ledger_id equals gl.gl_ledger_id

                         select new ref_ledger_vm
                         {
                             gl_ledger_id = gl.gl_ledger_id,
                             gl_ledger_name = gl.gl_ledger_code + " - " + gl.gl_ledger_name,
                         }).ToList();

            return query;
        }
        public List<ref_ledger_vm> GetConsumptionAccount(int id)
        {
            var query = (from ed in _scifferContext.REF_SUB_LEDGER.Where(x => x.entity_id == id && x.entity_type_id == 4)
                         join la in _scifferContext.ref_ledger_account_type.Where(y => y.ledger_account_type_name == "Consumption Account")
                         on ed.ledger_account_type_id equals la.ledger_account_type_id
                         join gl in _scifferContext.ref_general_ledger on ed.gl_ledger_id equals gl.gl_ledger_id

                         select new ref_ledger_vm
                         {
                             gl_ledger_id = gl.gl_ledger_id,
                             gl_ledger_name = gl.gl_ledger_code + " - " + gl.gl_ledger_name,
                         }).ToList();

            return query;
        }

        public List<Ref_item_VM> GetCatWiesItemList(int itemcatId)
        {
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false && x.ITEM_CATEGORY_ID == itemcatId)
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                         }).ToList();
            return query;
        }
        public List<GetBatchForGoodsIssue> BatchListWithQuantity(int item_id, int plant_id, int sloc_id, string entity)
        {
            var item = new SqlParameter("@item_id", item_id);
            var plant = new SqlParameter("@plant_id", plant_id);
            var sloc = new SqlParameter("@sloc_id", sloc_id);
            var enty = new SqlParameter("@entity", entity);
            var val = _scifferContext.Database.SqlQuery<GetBatchForGoodsIssue>(
            "exec GetBatchForGoodsIssue @sloc_id,@plant_id,@item_id,@entity", sloc, plant, item, enty).ToList();
            return val;
        }
        public bool CHKDupMachineCode(string machineCode)
        {
            int count = (from row in _scifferContext.ref_machine where row.machine_code == machineCode select row).Count();
            if (count == 0)
                return true;
            else
                return false;
        }
        public double Get_Current_Balance(string entity, int entity_type_id, int entity_id, DateTime start_date)
        {
            var entitytypeid = new SqlParameter("@entity_type_id", entity_type_id);
            var entityid = new SqlParameter("@entity_id", entity_id);
            var startdate = new SqlParameter("@start_date", start_date);
            var ent = new SqlParameter("@entity", entity);
            var val = _scifferContext.Database.SqlQuery<double>(
            "exec GetCurrentBalance @entity,@entity_type_id,@entity_id,@start_date", ent, entitytypeid, entityid, startdate).FirstOrDefault();
            return val;
        }
        public Item_Current_balance Get_Item_Current_Balance(string entity, int itemid, int plantid, int slocid, int bucketid)
        {
            var entitytypeid = new SqlParameter("@entity_type_id", itemid);
            var entityid = new SqlParameter("@entity_id", plantid);
            var startdate = new SqlParameter("@start_date", DateTime.UtcNow);
            var ent = new SqlParameter("@entity", "getitemcurrentbalance");
            var val = _scifferContext.Database.SqlQuery<Item_Current_balance>(
            "exec GetCurrentBalance @entity,@entity_type_id,@entity_id,@start_date", ent, entitytypeid, entityid, startdate).FirstOrDefault();
            return val;
        }

        public List<Sales_RM_VM> GetSalesRMList()
        {
            var query = (from s in _scifferContext.ref_sales_rm.Where(x => x.is_blocked == false && x.is_active == true)
                         join e in _scifferContext.REF_EMPLOYEE on s.employee_id equals e.employee_id
                         select new Sales_RM_VM
                         {
                             sales_rm_id = s.sales_rm_id,
                             employee_name = e.employee_code + '/' + e.employee_name,
                         }).ToList();
            return query;
        }

        public List<ref_user_management_vm> GetUserList()
        {
            //var query = (from u in _scifferContext.ref_user_management
            //             join e in _scifferContext.REF_EMPLOYEE on u.employee_id equals e.employee_id into j0
            //             from j1 in j0.DefaultIfEmpty()
            //             select new
            //             {
            //                 user_id = u.user_id,
            //                 user_name = u.user_code,
            //                 user_code = j1 == null ? string.Empty : j1.employee_name,

            //             }).ToList().Select(x => new ref_user_management_vm
            //             {
            //                 user_id = x.user_id,
            //                 user_name = x.user_name + "/" + x.user_code,
            //             }).ToList();
            //return query;

            var val = _scifferContext.Database.SqlQuery<ref_user_management_vm>(
             "exec GetActiveUsers").ToList();
            return val;
        }
        public List<ref_gst_applicability> GetGSTApplicability()
        {
            return _scifferContext.ref_gst_applicability.Where(x => x.is_active == true).ToList();
        }
        public List<ref_gst_customer_type> GetGSTCustomerVendorType()
        {
            return _scifferContext.ref_gst_customer_type.Where(x => x.is_active == true).ToList();
        }

        public List<ref_sac_vm> GetSACList()
        {
            var query = (from s in _scifferContext.ref_sac
                         select new ref_sac_vm
                         {
                             sac_id = s.sac_id,
                             sac_code = s.sac_code + "/" + s.sac_description,
                         }).ToList();
            return query;
        }
        public List<ref_gst_tds_code_vm> GetGSTTDSCodeList()
        {
            var query = (from g in _scifferContext.ref_gst_tds_code.Where(x => x.is_blocked == false && x.is_active == true)
                         select new ref_gst_tds_code_vm
                         {
                             gst_tds_code_id = g.gst_tds_code_id,
                             gst_tds_code = g.gst_tds_code + "/" + g.gst_tds_code_description,
                         }).ToList();
            return query;
        }
        public List<hsn_sac> Get_hsn_sac(int item_type_id)
        {
            try
            {
                if (item_type_id == 2)
                {
                    return _scifferContext.ref_sac.Select(a => new hsn_sac { id = a.sac_id, code = a.sac_code + "/" + a.sac_description }).ToList();

                }
                else
                {
                    return _scifferContext.ref_hsn_code.Select(a => new hsn_sac { id = a.hsn_code_id, code = a.hsn_code + "/" + a.hsn_code_description }).ToList();
                }

            }
            catch { return null; }
        }

        public List<hsn_sac> Get_hsn_saclist(int item_id)
        {
            try
            {
                var item_type_id = _scifferContext.REF_ITEM.Where(a => a.ITEM_ID == item_id).FirstOrDefault().item_type_id;
                if (item_type_id == 2)
                {
                    return _scifferContext.ref_sac.Select(a => new hsn_sac { id = a.sac_id, code = a.sac_code + "/" + a.sac_description }).ToList();
                }
                else
                {
                    return _scifferContext.ref_hsn_code.Select(a => new hsn_sac { id = a.hsn_code_id, code = a.hsn_code + "/" + a.hsn_code_description }).ToList();
                }
            }
            catch { return null; }
        }
        public List<ref_document_list> GetDocumentListinGI(string document_code, int plant_id)
        {
            if (document_code == "PMO")
            {
                var list = (from ed in _scifferContext.ref_plan_maintenance_order.Where(x => x.is_active == true && x.plant_id == plant_id)
                            select new ref_document_list
                            {
                                document_id = ed.maintenance_order_id,
                                document_list = ed.order_no + " - " + ed.creation_date,
                            }).ToList().OrderByDescending(a => a.document_id).ToList();
                return list;
            }
            else if (document_code == "MRN")
            {
                var list = (from ed in _scifferContext.material_requision_note.Where(x => x.is_active == true && x.plant_id == plant_id && x.approval_status == 1)
                            join e in _scifferContext.material_requision_note_detail.Where(a => a.balance_qty != 0) on ed.material_requision_note_id equals e.material_requision_note_id //into j0
                            //from j1 in j0.DefaultIfEmpty()
                            join st in _scifferContext.ref_status.Where(a => a.status_name != "Closed") on ed.status_id equals st.status_id

                            select new ref_document_list
                            {
                                document_id = ed.material_requision_note_id,
                                document_list = ed.number + " - " + ed.posting_date,
                            }).Distinct().ToList().OrderByDescending(a => a.document_id).ToList();
                return list;
            }
            else if (document_code == "BRKO")
            {
                var list = (from ed in _scifferContext.ref_plan_breakdown_order.Where(x => x.is_active == true && x.plant_id == plant_id)
                            select new ref_document_list
                            {
                                document_id = ed.plan_breakdown_order_id,
                                document_list = ed.doc_number + " - " + ed.creation_date,
                            }).ToList().OrderByDescending(a => a.document_id).ToList();
                return list;
            }
            else
            {
                return null;
            }
        }
        public List<SubProdOrderDetailVM> getDocumentSelectedDetails(string document_code, int id)
        {
            var document_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", document_code);
            var val = _scifferContext.Database.SqlQuery<SubProdOrderDetailVM>(
            "exec GetDocumentSelectedDetailsList @entity,@id", entity, document_id).ToList();
            return val;
        }
        public int GetplantIDByCode(string document_code, int document_id)
        {
            if (document_code == "PMO")
            {
                var list = _scifferContext.ref_plan_maintenance_order.Where(x => x.maintenance_order_id == document_id && x.is_active == true).FirstOrDefault();
                return list.plant_id;
            }
            else if (document_code == "MRN")
            {
                var list = _scifferContext.material_requision_note.Where(x => x.material_requision_note_id == document_id && x.is_active == true).FirstOrDefault();
                return list.plant_id;
            }
            else
            {
                return 0;
            }
        }
        public double? getBatchQuantityUsingItemSlocPlant(int sloc_id, int plant_id, int item_id, DateTime posting_date)
        {
            double? add_balance_qty = 0;
            //var summary = from p in _scifferContext.inv_item_transaction.Where(x => x.item_id == item_id && x.plant_id == plant_id && x.sloc_id == sloc_id && x.item_transaction_date <= posting_date && x.bucket_id == 2)
            var summary = from p in _scifferContext.inv_item_transaction.Where(x => x.item_id == item_id && x.plant_id == plant_id && x.sloc_id == sloc_id && x.bucket_id == 2)
                              //join ed in _scifferContext.inv_item_batch_detail.Where(x => x.plant_id == plant_id && x.sloc_id == sloc_id && x.bucket_id == 2) on p.plant_id equals ed.plant_id
                          let k = new
                          {
                              item_id = p.item_id,
                          }
                          group p by k into t
                          select new
                          {
                              Qty = t.Sum(p => p.item_quantity)
                          };

            foreach (var item in summary)
            {
                Console.WriteLine(item);
                add_balance_qty = item.Qty + add_balance_qty;
            }
            return add_balance_qty;
        }
        public List<ref_tax_vm> GetTaxByRCM(int id)
        {
            bool i = false;
            if (id == 0)
            { i = false; }
            else { i = true; };
            if (i == true)
            {
                var query = (from t in _scifferContext.ref_tax.Where(x => x.is_blocked == false)
                             join td in _scifferContext.ref_tax_detail on t.tax_id equals td.tax_id
                             join te in _scifferContext.ref_tax_element.Where(x => x.is_rcm == i) on td.tax_element_id equals te.tax_element_id
                             select new ref_tax_vm
                             {
                                 tax_id = t.tax_id,
                                 tax_name = t.tax_name,
                             }).Distinct().ToList();
                return query;
            }
            else
            {
                var query = (from t in _scifferContext.ref_tax.Where(x => x.is_blocked == false)
                             select new ref_tax_vm
                             {
                                 tax_id = t.tax_id,
                                 tax_name = t.tax_name,
                             }).ToList();
                return query;
            }

        }

        public List<Ref_permit_template_VM> GetTempList()
        {
            // object hidden = null;
            var query = (from k in _scifferContext.Ref_permit_template.Where(x => x.is_blocked == true)
                         select new Ref_permit_template_VM
                         {
                             permit_template_id = k.permit_template_id,
                             permit_template_no = k.permit_template_no + "/" + k.permit_category
                         }).ToList();
            return query;
        }
        public List<Ref_checkpoints_VM> GetCheckpointList(int id)
        {
            var query = (from k in _scifferContext.Ref_checkpoints.Where(x => x.is_blocked == true && x.permit_template_id == id)
                         select new Ref_checkpoints_VM
                         {
                             check_point_id = k.check_point_id,
                             checkpoints = k.checkpoints,
                             ideal_scenario = k.ideal_scenario,
                         }).ToList();
            return query;
        }
        public List<Ref_checkpoints_VM> GetScenarioList(int id)
        {
            var query = (from k in _scifferContext.Ref_checkpoints.Where(x => x.is_blocked == true && x.permit_template_id == id)
                         select new Ref_checkpoints_VM
                         {
                             check_point_id = k.check_point_id,
                             ideal_scenario = k.ideal_scenario
                         }).ToList();
            return query;
        }
        public List<REF_NOTIFICATION_TYPE> GetNotificationType()
        {
            return _scifferContext.REF_NOTIFICATION_TYPE.Where(x => x.is_blocked == false && x.is_active == true).ToList().Select(x => new REF_NOTIFICATION_TYPE { NOTIFICATION_ID = x.NOTIFICATION_ID, NOTIFICATION_TYPE = x.NOTIFICATION_TYPE + "/" + x.NOTIFICATION_DESCRIPTION }).ToList();
        }
        public List<ref_machine_master_VM> GetMachineListFromPlant(int id)
        {
            var query = (from s in _scifferContext.ref_machine.Where(x => x.is_active == true && x.is_blocked == false && x.plant_id == id)
                         select new ref_machine_master_VM
                         {
                             machine_id = s.machine_id,
                             machine_code = s.machine_code + "/" + s.machine_name
                         }).ToList();
            return query;
        }
        public List<ref_document_numbring> CategoryListByPlant(int id, int plant_id)
        {
            return _scifferContext.ref_document_numbring.Where(x => x.module_form_id == id && x.is_blocked == false).OrderByDescending(x => x.set_default).OrderByDescending(x => x.set_default).ToList().Where(a => a.plant_id == plant_id).ToList();
        }
        public int getDocumentId(string document_code, string document_number)
        {
            var document_id = 0;
            switch (document_code)
            {
                case "PMO":
                    document_id = _scifferContext.ref_plan_maintenance_order.FirstOrDefault(a => a.order_no == document_number).plan_maintenance_id;
                    break;
                case "BRKO":
                    document_id = _scifferContext.ref_plan_breakdown_order.FirstOrDefault(a => a.doc_number == document_number).plan_breakdown_order_id;
                    break;
                case "MRN":
                    document_id = _scifferContext.material_requision_note.FirstOrDefault(a => a.number == document_number).material_requision_note_id;
                    break;
                default:
                    break;
            }
            return document_id;
        }

        public int getDocumentDetailId(string document_code, int item_id)
        {
            var document_detail_id = 0;
            switch (document_code)
            {
                case "PMO":
                    document_detail_id = _scifferContext.ref_plan_maintenance_order_cost.FirstOrDefault(a => a.item_id == item_id).maintenance_order_cost_id;
                    break;
                case "BRKO":
                    document_detail_id = _scifferContext.ref_plan_breakdown_cost.FirstOrDefault(a => a.item_id == item_id).plan_breakdown_cost_id;
                    break;
                case "MRN":
                    document_detail_id = _scifferContext.material_requision_note_detail.FirstOrDefault(a => a.item_id == item_id).material_requision_note_detail_id;
                    break;
                default:
                    break;
            }
            return document_detail_id;
        }
        public int GetCategoryByModuleFormAndPlant(int module_form_id, int plant_id)
        {
            return _scifferContext.ref_document_numbring.Where(x => x.module_form_id == module_form_id && x.plant_id == plant_id).OrderByDescending(x => x.set_default).OrderByDescending(x => x.set_default).FirstOrDefault().document_numbring_id;
        }
        public int GetSlocId(string code)
        {
            var item = _scifferContext.REF_STORAGE_LOCATION.Where(x => x.storage_location_name == code).FirstOrDefault();
            return item == null ? 0 : item.storage_location_id;
        }

        public double? getBatchQuantityUsingItemSlocPlant(int sloc_id, int plant_id, int item_id, int bucket_id)
        {
            double? add_balance_qty = 0;
            var summary = from p in _scifferContext.inv_item_batch.Where(x => x.item_id == item_id)
                          join ed in _scifferContext.inv_item_batch_detail.Where(x => x.plant_id == plant_id && x.sloc_id == sloc_id && x.bucket_id == bucket_id) on p.item_batch_id equals ed.item_batch_id
                          let k = new
                          {
                              item_batch_id = p.item_batch_id,
                          }
                          group ed by k into t
                          select new
                          {
                              Qty = t.Sum(p => p.balance_qty)
                          };

            foreach (var item in summary)
            {
                Console.WriteLine(item);
                add_balance_qty = item.Qty + add_balance_qty;
            }
            return add_balance_qty;
        }
        public List<ref_dashboard_vm> GetDashboardList(int user_id)
        {
            var query = (from r in _scifferContext.ref_user_role_mapping.Where(x => x.user_id == user_id && x.is_active == true)
                         join dr in _scifferContext.ref_role_dashboard_mapping.Where(x => x.is_active == true) on r.role_id equals dr.role_id
                         join d in _scifferContext.ref_dashboard on dr.dashboard_id equals d.dashboard_id
                         select new ref_dashboard_vm
                         {
                             dashboard_code = d.dashboard_code,
                         }).ToList();
            return query;
        }
        public int? GetOperatorEmployeeId(int user_id)
        {
            try
            {
                var emp = _scifferContext.ref_user_management.Where(x => x.user_id == user_id).FirstOrDefault();
                return emp == null ? 0 : emp.employee_id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public IWorkbook GetExcelWorkBook(GridProperties obj, object DataSource, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            ExcelEngine excel = new ExcelEngine();
            IApplication application = excel.Excel;
            IWorkbook workbook = application.Workbooks.Create(2);
            workbook = exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme", true);
            //Inserted new row for adding title
            workbook.ActiveSheet.InsertRow(1);

            workbook.ActiveSheet.InsertRow(1);

            workbook.ActiveSheet.InsertRow(1);
            workbook.ActiveSheet.InsertRow(1);
            //Merging the sheet from Range A1 to D1 for adding title space
            // workbook.ActiveSheet.Range["A1:D1"].Merge();
            //Adding the title using Text property
            // obj.ServerExcelQueryCellInfo

            workbook.ActiveSheet.Range["A1"].Text = GetCompanyName();
            workbook.ActiveSheet.Range["A2"].Text = ctrlname;
            workbook.ActiveSheet.Range["A3"].Text = "REPORT GENERATED DATE " + DateTime.Parse(DateTime.Now.ToString()).ToString("dd/MMM/yyyy hh:mm:ss.FFF");
            workbook.ActiveSheet.Range["A1"].CellStyle.Font.Bold = true;
            workbook.ActiveSheet.Range["A2"].CellStyle.Font.Bold = true;
            workbook.ActiveSheet.Range["A3"].CellStyle.Font.Bold = true;
            workbook.ActiveSheet.Range["A1"].CellStyle.Font.Color = ExcelKnownColors.Dark_blue;
            workbook.ActiveSheet.Range["A5"].EntireRow.CellStyle.Font.Color = ExcelKnownColors.Dark_blue;
            //  workbook.SaveAs(ctrlname + ".xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);
            return workbook;
        }
        public string GetCompanyName()
        {
            var company = _scifferContext.REF_COMPANY.FirstOrDefault();
            return company == null ? string.Empty : company.COMPANY_DISPLAY_NAME;
        }
        public List<Ref_item_VM> GetJobWorkInItem()
        {
            var query = (from j in _scifferContext.in_jobwork_in_detail
                         join i in _scifferContext.REF_ITEM on j.item_id equals i.ITEM_ID
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_NAME,
                         }).Distinct().ToList();
            return query;
        }

        public List<document_numbring> GetCategoryListByModule(string code)
        {
            var query = (from d in _scifferContext.ref_document_numbring
                         join mo in _scifferContext.ref_module_form.Where(x => x.module_form_code == code) on d.module_form_id equals mo.module_form_id
                         //join f in _scifferContext.REF_FINANCIAL_YEAR on d.financial_year_id equals f.FINANCIAL_YEAR_ID
                         select new document_numbring
                         {
                             document_numbring_id = d.document_numbring_id,
                             category = d.category,
                             set_default = d.set_default,
                         }).OrderByDescending(x => x.set_default).ToList();
            return query;
        }
        public List<ref_tax_vm> GetWithinState(int id)
        {
            if (id == 1)
            {
                var list = (from tax in _scifferContext.ref_tax
                            join tax_detail in _scifferContext.ref_tax_detail on tax.tax_id equals tax_detail.tax_id
                            join tax_element in _scifferContext.ref_tax_element on tax_detail.tax_element_id equals tax_element.tax_element_id
                            join tax_type in _scifferContext.ref_tax_type.Where(a => a.tax_type_name.Contains("cgst") || a.tax_type_name.Contains("sgst")) on tax_element.tax_type_id equals tax_type.tax_type_id
                            select new ref_tax_vm
                            {
                                tax_id = tax.tax_id,
                                tax_code = tax.tax_code + "/" + tax.tax_name
                            }).Distinct().ToList();
                return list;
            }
            else
            {
                var list = (from tax in _scifferContext.ref_tax
                            join tax_detail in _scifferContext.ref_tax_detail on tax.tax_id equals tax_detail.tax_id
                            join tax_element in _scifferContext.ref_tax_element on tax_detail.tax_element_id equals tax_element.tax_element_id
                            join tax_type in _scifferContext.ref_tax_type.Where(a => a.tax_type_name.Contains("IGST")) on tax_element.tax_type_id equals tax_type.tax_type_id
                            select new ref_tax_vm
                            {
                                tax_id = tax.tax_id,
                                tax_code = tax.tax_code + "/" + tax.tax_name
                            }).Distinct().ToList();
                return list;
            }
        }
        public List<create_stock_sheet> PlantwiseCreateStockSheet(int plant_id)
        {
            return _scifferContext.create_stock_sheet.Where(x => x.plant_id == plant_id).ToList();
        }
        public List<update_stock_count_vm> GetUpdateStockDocList(int id)
        {
            var data = (from ed in _scifferContext.update_stock_count.Where(x => x.create_stock_sheet_id == id)
                        select new update_stock_count_vm
                        {
                            doc_number = ed.doc_number + "-" + ed.posting_date,
                            update_stock_count_id = ed.update_stock_count_id,
                        }).ToList();
            return data;
        }


        public List<fin_credit_debit_note_vm> GetTaxRate(int id)
        {
            var ids = new SqlParameter("@id", id);
            var val = _scifferContext.Database.SqlQuery<fin_credit_debit_note_vm>(
            "exec GetTaxRate @id", ids).ToList();
            return val;
        }

        public List<fin_credit_debit_transaction_vm> GetSalesPurchaseInvoicedetails(string entity_id, int id)
        {
            var entity = new SqlParameter("@entity_id", entity_id);
            var ids = new SqlParameter("@id", id);
            var val = _scifferContext.Database.SqlQuery<fin_credit_debit_transaction_vm>(
            "exec GetSalesPurchaseInvoiceDetailforDNCN @entity_id,@id", entity, ids).ToList();
            return val;
        }

        public List<ref_mfg_operator_incentive_appl_vm> GetAllUserForIncApp(string user_id)
        {
            var user_id1 = new SqlParameter("@user_id", user_id);
            var val = _scifferContext.Database.SqlQuery<ref_mfg_operator_incentive_appl_vm>(
            "exec GetAllUserForIncApp @user_id", user_id1).ToList();
            return val;
        }

        public List<ref_mfg_multi_machining_vm> GetAllMachineForMultiMac(string entity)
        {
            var entity1 = new SqlParameter("@entity", entity);
            var val = _scifferContext.Database.SqlQuery<ref_mfg_multi_machining_vm>(
            "exec GetAllMachineForMultiMac @entity", entity1).ToList();
            return val;
        }



        public List<ref_shifts_vm> GetShiftlist()
        {

            var query = (from i in _scifferContext.ref_shifts.Where(x => x.is_active == true)
                         select new ref_shifts_vm
                         {
                             shift_id = i.shift_id,
                             shift_code = i.shift_code + "/" + i.shift_desc,
                         }).ToList();
            return query;

        }

        public List<ref_shifts_vm> GetShiftdesclist()
        {

            var query = (from i in _scifferContext.ref_shifts.Where(x => x.is_active == true)
                         select new ref_shifts_vm
                         {
                             shift_id = i.shift_id,
                             shift_code = i.shift_desc,
                         }).ToList();
            return query;

        }


        public List<operator_incentive_summary_vm> GetOperatorIncentiveSummary(DateTime? from_date, DateTime? to_date, int? user_id)
        {
            var loginId = HttpContext.Current.Session["User_Id"];
            var user_id1 = new SqlParameter("@user_id", loginId);
            var date1 = new SqlParameter("@from_date", from_date);
            var date2 = new SqlParameter("@to_date", to_date);

            var val = _scifferContext.Database.SqlQuery<operator_incentive_summary_vm>(
            "exec GetOperatorIncentiveSummary @from_date, @to_date, @user_id", date1, date2, user_id1).ToList();
            return val;
        }

        public List<operator_incentive_detail_vm> GetOperatorIncentiveDetail(DateTime? from_date, DateTime? to_date, int? user_id)
        {

            var loginId = HttpContext.Current.Session["User_Id"];
            var user_id1 = new SqlParameter("@user_id", loginId);
            var date1 = new SqlParameter("@from_date", from_date);
            var date2 = new SqlParameter("@to_date", to_date);

            var val = _scifferContext.Database.SqlQuery<operator_incentive_detail_vm>(
            "exec GetOperatorIncentiveDetail @from_date, @to_date, @user_id", date1, date2, user_id1).ToList();
            return val;
        }

        public List<ref_mfg_incentive_benchmark_vm> GetInsentiveBenchmarkDetail(string entity_id)
        {
            var entity = new SqlParameter("@entity_id", entity_id);
            var val = _scifferContext.Database.SqlQuery<ref_mfg_incentive_benchmark_vm>(
            "exec GetInsentiveBenchmarkDetail @entity_id", entity).ToList();
            return val;
        }

        public List<operator_incentive_vm> GetOperatorIncentive(string entity, DateTime? from_date, DateTime? to_date, string plant_id)
        {
            var entity1 = new SqlParameter("@entity", entity);
            var plant_id1 = new SqlParameter("@plant_id", plant_id);
            var date1 = new SqlParameter("@from_date", from_date);
            var date2 = new SqlParameter("@to_date", to_date);

            var val = _scifferContext.Database.SqlQuery<operator_incentive_vm>(
            "exec report_get_incentive @entity,@from_date, @to_date, @plant_id", entity1, date1, date2, plant_id1).ToList();
            return val;
        }

        public List<operator_incentive_summary_vm> GetOperatorIncentiveSummaryDHB(string entity, DateTime? from_date, DateTime? to_date, string plant_id)
        {
            var entity1 = new SqlParameter("@entity", entity);
            var plant_id1 = new SqlParameter("@plant_id", plant_id);
            var date1 = new SqlParameter("@from_date", from_date);
            var date2 = new SqlParameter("@to_date", to_date);

            var val = _scifferContext.Database.SqlQuery<operator_incentive_summary_vm>(
            "exec report_get_incentive @entity,@from_date, @to_date, @plant_id", entity1, date1, date2, plant_id1).ToList();
            return val;
        }

        public List<ref_shifts_vm> GetShiftfromPlant(int id, DateTime posting_date)
        {
            List<ref_shifts_vm> shiftwise_production = null;
            try
            {
                var shiftwise_production_id1 = 0;
                var operator_id = 0;
                var shiftwise_production_id = new SqlParameter("@shiftwise_production_id", shiftwise_production_id1 == 0 ? 0 : shiftwise_production_id1);
                var posting_date1 = new SqlParameter("@posting_date", posting_date);
                var ent = new SqlParameter("@entity", "GetShiftfromPlant");
                var plant_id1 = new SqlParameter("@plant_id", id);
                var shift_id1 = new SqlParameter("@shift_id", -1);
                var process_id1 = new SqlParameter("@process_id", -1);
                var machine_id1 = new SqlParameter("@machine_id", -1);
                var item_id1 = new SqlParameter("@item_id", -1);
                var operator_id1 = new SqlParameter("@operator_id", operator_id == 0 ? 0 : operator_id);

                shiftwise_production = _scifferContext.Database.SqlQuery<ref_shifts_vm>("exec GetAllShiftwiseProductionMaster @shiftwise_production_id,@entity,@shift_id,@plant_id,@process_id,@machine_id,@item_id,@operator_id,@posting_date",
                     shiftwise_production_id, ent, shift_id1, plant_id1, process_id1, machine_id1, item_id1, operator_id1, posting_date1).ToList();
            }
            catch (Exception ex)
            {
                return shiftwise_production;
            }

            return shiftwise_production;

        }

        public List<ref_shifts_vm> GetShiftfromPlant(int id)
        {
            List<ref_shifts_vm> shift = null;
            try
            {
                shift = (from s in _scifferContext.ref_shifts.Where(x => x.is_blocked == false && x.is_active == true && x.plant_id == id)
                         select new ref_shifts_vm
                         {
                             shift_id = s.shift_id,
                             shift_code = s.shift_desc,
                             shift_desc = s.shift_desc,
                             from_time = s.from_time,
                             to_time = s.to_time,
                             is_active = s.is_active,
                             is_blocked = s.is_blocked
                         }).ToList();
            }
            catch (Exception ex)
            {
                return shift;
            }

            return shift;

        }

        //Get machine list by operator mapped with operator and machine
        public List<ref_machine_master_VM> GetMachineListByProcess(int process_id)
        {
            var list = new List<ref_machine_master_VM>();

            list = (from mmpm in _scifferContext.map_mfg_process_machine
                    join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                    where mmpm.process_id == process_id
                    select new ref_machine_master_VM
                    {
                        machine_id = rm.machine_id,
                        machine_name = rm.machine_name,
                    }).ToList();

            return list;
        }



        public List<ref_incentive_status> GetIncentiveStatuslist()
        {
            return _scifferContext.ref_incentive_status.ToList();
        }

        public List<incentive_status> GetIncenticeStatus(string entity, string plant_id, DateTime from_date, DateTime to_date)
        {
            try
            {
                var ent = new SqlParameter("@entity", entity);
                var fromDate = new SqlParameter("@from_date", from_date);
                var toDate = new SqlParameter("@to_date", to_date);
                var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);
                var val = _scifferContext.Database.SqlQuery<incentive_status>(
               "exec check_incentive_status @entity,@plant_id,@from_date,@to_date", ent, plant, fromDate, toDate).ToList();
                return val;
            }
            catch (Exception ex)
            {
                return new List<incentive_status>();
            }
            finally
            {
                GC.Collect();
            }
        }

        public List<Report_incentive_vm> ComputeIncentive(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            var start_date_shiftid = new SqlParameter("@start_date_shift_id", start_date_shift_id);
            var end_date_shiftid = new SqlParameter("@end_date_shift_id", end_date_shift_id);
            var fromDate = new SqlParameter("@from_date", from_date);
            var toDate = new SqlParameter("@to_date", to_date);
            var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);
            var val = _scifferContext.Database.SqlQuery<Report_incentive_vm>(
                "exec compute_incentive @start_date_shift_id,@end_date_shift_id,@from_date,@to_date,@plant_id",
                  start_date_shiftid, end_date_shiftid, fromDate, toDate, plant).ToList();
            return val;
        }

        public List<ref_mfg_multi_machining> GetMultiMachining()
        {
            return _scifferContext.ref_mfg_multi_machining.ToList();
        }
        public List<ref_mfg_incentive_holiday> GetHolidayList()
        {
            return _scifferContext.ref_mfg_incentive_holiday.ToList();
        }

        public List<Report_incentive_vm> GetAllIncentiveSummary()
        {
            var val = _scifferContext.Database.SqlQuery<Report_incentive_vm>(
                "exec GetAllIncentiveSummary").ToList();
            return val;
        }

        public List<Report_incentive_vm> GetAllIncentiveDetailandSummary(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            var start_date_shiftid = new SqlParameter("@start_date_shift_id", start_date_shift_id);
            var end_date_shiftid = new SqlParameter("@end_date_shift_id", end_date_shift_id);
            var fromDate = new SqlParameter("@from_date", from_date);
            var toDate = new SqlParameter("@to_date", to_date);
            var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);
            var val = _scifferContext.Database.SqlQuery<Report_incentive_vm>(
                "exec GetAllIncentiveDetailandSummary @start_date_shift_id,@end_date_shift_id,@from_date,@to_date,@plant_id",
                  start_date_shiftid, end_date_shiftid, fromDate, toDate, plant).ToList();
            return val;
        }
        public List<Report_incentive_vm> GetAllViewIncentiveSummary(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id)
        {
            var start_date_shiftid = new SqlParameter("@start_date_shift_id", start_date_shift_id);
            var end_date_shiftid = new SqlParameter("@end_date_shift_id", end_date_shift_id);
            var fromDate = new SqlParameter("@from_date", from_date);
            var toDate = new SqlParameter("@to_date", to_date);
            var plant = new SqlParameter("@plant_id", plant_id == null ? "" : plant_id);
            var val = _scifferContext.Database.SqlQuery<Report_incentive_vm>(
                "exec GetAllViewIncentiveSummary @start_date_shift_id,@end_date_shift_id,@from_date,@to_date,@plant_id",
                  start_date_shiftid, end_date_shiftid, fromDate, toDate, plant).ToList();

            return val;
        }

        public List<ref_dep_area_vm> GetDepList()
        {
            var query = (from s in _scifferContext.ref_dep_area.Where(x => x.is_blocked == false)
                         select new ref_dep_area_vm
                         {
                             dep_area_id = s.dep_area_id,
                             dep_area_code = s.dep_area_code + "/" + s.dep_area_description
                         }).ToList();
            return query;
        }
        //--------------------------------------------
        public List<ref_ledger_account_type_vm> GetLedgerAccountTypeName()
        {
            var query = (from la in _scifferContext.ref_ledger_account_type.Where(x => x.entity_type_id == 12)
                         join s in _scifferContext.ref_item_category_gl on la.ledger_account_type_id equals s.ledger_account_type_id into j0
                         from terr in j0.DefaultIfEmpty()
                         join lg in _scifferContext.ref_general_ledger on terr.gl_ledger_id equals lg.gl_ledger_id into j1
                         from terr1 in j1.DefaultIfEmpty()
                         select new ref_ledger_account_type_vm
                         {
                             gl_ledger_code = (terr1 == null ? String.Empty : terr1.gl_ledger_code),
                             gl_ledger_name = (terr1 == null ? String.Empty : terr1.gl_ledger_name),
                             gl_ledger_id = (terr1 == null ? 0 : terr1.gl_ledger_id),
                             ledger_account_type_id = la.ledger_account_type_id,
                             ledger_account_type_name = la.ledger_account_type_name,
                             //sub_ledger_id = (terr == null ? 0 : terr.sub_ledger_id),
                         }).ToList();

            return query;
        }
        public List<ref_dep_area_vm> GETDEPRECIATIONAREA()
        {
            var query = (from da in _scifferContext.ref_dep_area.Where(x => x.is_blocked == false)
                         join dt in _scifferContext.ref_dep_type on da.dep_area_id equals dt.dep_area_id

                         select new ref_dep_area_vm
                         {
                             dep_area_code = (da == null ? String.Empty : da.dep_area_code) + " / " + (da == null ? String.Empty : da.dep_area_description),
                             dep_type_code = (dt == null ? String.Empty : dt.dep_type_code) + " / " + (dt == null ? String.Empty : dt.dep_type_description),
                             dep_area_id = (da == null ? 0 : da.dep_area_id),
                             dep_type_id = (dt == null ? 0 : dt.dep_type_id),
                             dep_type_frquency = (da.dep_type_frquency_id == 1 ? "MONTHLY" : da.dep_type_frquency_id == 3 ? "ANNUALLY" : ""),
                             rate_value = dt.cal_based_on_id == 2 ? dt.cal_based_value : 0,
                             useful_value = dt.cal_based_on_id == 1 ? dt.cal_based_value : 0,
                         }).ToList();

            return query;
        }

        public List<ref_asset_class> GetAssetClass()
        {
            return _scifferContext.ref_asset_class.Where(a => a.is_active == true).ToList();
        }

        public List<ref_asset_group> GetAssetGroup()
        {
            return _scifferContext.ref_asset_group.Where(a => a.is_blocked == false).ToList();
        }

        public List<REF_COUNTRY> GetCountryList()
        {
            return _scifferContext.REF_COUNTRY.Where(a => a.is_blocked == false).ToList();
        }

        public List<ref_asset_class_depreciation_vm> GetAssetClassDepreciationList()
        {
            var dep = (from r in _scifferContext.ref_asset_class_depreciation
                       join s in _scifferContext.ref_dep_area on r.dep_area_id equals s.dep_area_id
                       join g in _scifferContext.ref_dep_type on r.dep_type_id equals g.dep_type_id
                       select new ref_asset_class_depreciation_vm
                       {
                           asset_class_id = r.asset_class_id,
                           asset_class_dep_id = r.asset_class_dep_id,
                           dep_area_code = s.dep_area_code + "-" + s.dep_area_description,
                           dep_type_code = g.dep_type_code + "-" + g.dep_type_description,
                           dep_type_frquency = r.dep_type_frquency_id == 1 ? "MONTHLY" : r.dep_type_frquency_id == 2 ? "abc" : "",
                           useful_life_months = r.useful_life_months,
                           useful_life_rate = r.useful_life_rate,
                           dep_area_id = r.dep_area_id,
                       }).ToList();
            return dep;
        }

        public List<ref_asset_master_data> GetAssetMasterData()
        {
            return _scifferContext.ref_asset_master_data.Where(a => a.is_active).OrderBy(x=> x.asset_master_data_code).ToList();
        }

        public List<ref_asset_master_data> GetAssetMasterDataForDropDown()
        {
            var query = (from u in _scifferContext.ref_asset_master_data.Where(a => a.is_active == true)

                         select new
                         {
                             asset_master_data_id = u.asset_master_data_id,
                             asset_master_data_code = u.asset_master_data_code,
                             asset_master_data_desc = u.asset_master_data_desc

                         }).ToList().Select(x => new ref_asset_master_data
                         {
                             asset_master_data_id = x.asset_master_data_id,
                             asset_master_data_desc = x.asset_master_data_code + '/' + x.asset_master_data_desc,
                         }).ToList();
            return query;
        }

        public List<fin_ledger_capitalization_detail_vm> ForCapitalizationeDetail(int entity_type_id, int entity_id)
        {
            var entity_type_id1 = new SqlParameter("@entity_type_id", entity_type_id);
            var entity_id1 = new SqlParameter("@entity_id", entity_id);
            var val = _scifferContext.Database.SqlQuery<fin_ledger_capitalization_detail_vm>(
            "exec ForCapitalizationeDetail @entity_type_id,@entity_id", entity_type_id1, entity_id1).ToList();
            return val;
        }

        public List<ref_dep_area_vm> GetDepListForDepRun()
        {
            var query = (from s in _scifferContext.ref_dep_area.Where(x => x.is_blocked == false)
                         join r in _scifferContext.ref_dep_posting_period on s.dep_area_id equals r.dep_area_id
                         join f in _scifferContext.REF_FINANCIAL_YEAR on r.financial_year_id equals f.FINANCIAL_YEAR_ID
                         select new ref_dep_area_vm
                         {
                             dep_area_id = s.dep_area_id,
                             dep_area_code = s.dep_area_code + "/" + s.dep_area_description,
                             //financial_year_id = r.financial_year_id,
                             financial_year_name = f.FINANCIAL_YEAR_NAME,
                             frequency_id = s.dep_type_frquency_id,
                             to_date = f.FINANCIAL_YEAR_TO.ToString(),
                             dep_to_date = r.to_date.ToString(),
                         }).ToList();
            return query;
        }

        public string GetFilePathForImage(string controller, HttpPostedFileBase files)
        {
            CreateIfMissing("~/Uploads/" + controller + "/ ");
            string extension = Path.GetExtension(files.FileName);
            string A = DateTime.Now.ToString("F");
            var B = A.Replace(',', ' ');
            var C = B.Replace(':', ' ');
            var filename = C;
            string fileLocation = string.Format("{0}/{1}{2}", System.Web.HttpContext.Current.Server.MapPath("~/Uploads/" + controller + "/ "), filename, extension);
            files.SaveAs(fileLocation);
            return fileLocation;
        }


        public List<prod_plan_detail> GetProdPlanDetails()
        {
            return _scifferContext.prod_plan_detail.Where(x => x.is_active == true).ToList();
        }
        public List<prod_downtime> GetProdDownTimeDetails()
        {
            return _scifferContext.prod_downtime.Where(x => x.is_active == true).ToList();
        }

        public List<ref_task_type> GetTaskTypeList()
        {
            return _scifferContext.ref_task_type.Where(x => x.is_active == true).ToList();
        }

        public List<ref_task_periodicity> GetPeriodicitylist()
        {
            return _scifferContext.ref_task_periodicity.Where(x => x.is_active == true).ToList();
        }
        public void SendMail(string send_to, string send_cc, string send_bcc, string body, string subject)
        {
            try
            {
                ref_send_mail sm = new ref_send_mail();
                sm = _scifferContext.ref_send_mail.FirstOrDefault();
                if (sm != null)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(send_to);
                    if (send_cc != null && send_cc != "")
                    {
                        mail.CC.Add(send_cc);
                    }
                    if (send_bcc != null && send_bcc != "")
                    {
                        mail.Bcc.Add(send_bcc);
                    }
                    mail.From = new MailAddress(sm.send_from);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = sm.host_name;
                    smtp.Port = sm.port_name;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(sm.send_from, sm.send_password); // Enter seders User name and password  
                    smtp.EnableSsl = sm.EnableSsl;
                    smtp.Send(mail);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                GC.Collect();
            }

        }
        public string Get_PlantShift(int plant_id, TimeSpan time)
        {
            var time1 = new SqlParameter("@time", time);
            var plant = new SqlParameter("@plant_id", plant_id);
            var val = _scifferContext.Database.SqlQuery<string>(
               "exec get_shits_as_plant_and_time @plant_id,@time", plant, time1).FirstOrDefault();
            return val;
        }
        public string Get_DateDiff_for_malfunction(int notification_id)
        {
            var notificationid = new SqlParameter("@notification_id", notification_id);
            var val = _scifferContext.Database.SqlQuery<string>(
               "exec Get_DateDiff_for_malfunction @notification_id", notificationid).FirstOrDefault();
            return val;
        }
        public string GetOperator(int userid)
        {
            var user_id = new SqlParameter("@userid", userid);
            var val = _scifferContext.Database.SqlQuery<string>(
               "exec Get_Operator @userid", user_id).FirstOrDefault();
            return val;
        }

        public string GetEMpOperator(int userid)
        {
            var user_id = new SqlParameter("@userid", userid);
            var val = _scifferContext.Database.SqlQuery<string>(
               "exec Get_EMP_Operator @userid", user_id).FirstOrDefault();
            return val;
        }
        public string GetMulOperator(string attendedby_id)
        {
            var user_id = new SqlParameter("@userid", attendedby_id);
            var val = _scifferContext.Database.SqlQuery<string>(
               "exec Get_Mul_Operator @userid", user_id).FirstOrDefault();
            return val;
        }
        public string Get_UserMobileNumber(int plant_id)
        {
            var plantid = new SqlParameter("@plant_id", Convert.ToInt32(plant_id));
            var val = _scifferContext.Database.SqlQuery<string>(
               "exec get_user_number_list_for_sms @plant_id", plantid).FirstOrDefault();
            return val;
        }

        public List<Ref_item_VM> get_childitem_list(string entity, int parent_item_id)
        {
            var entity1 = new SqlParameter("@entity", entity);
            var parent_item_id1 = new SqlParameter("@parent_item_id", parent_item_id);
            var val = _scifferContext.Database.SqlQuery<Ref_item_VM>(
            "exec get_childitem_list @entity,@parent_item_id",
            entity1, parent_item_id1).ToList();
            return val;
        }

        public List<ref_level> GetLevelList()

        {
            var query = (from u in _scifferContext.ref_level.Where(a => a.is_active == true)

                         select new
                         {
                             level_id = u.level_id,
                             level_code = u.level_code,
                             level_desc = u.level_desc

                         }).ToList().Select(x => new ref_level
                         {
                             level_id = x.level_id,
                             level_code = x.level_code + '/' + x.level_desc,
                         }).ToList();
            return query;
        }

        public List<ref_user_management_vm> Get_User_Production_Supervisor()
        {

            try
            {
                var entity = new SqlParameter("@entity", "get_Overall_Equipment_Effectiveness_Detail_Report");
                var val = _scifferContext.Database.SqlQuery<ref_user_management_vm>(
                "exec report_production_report @entity", entity).ToList();
                return val;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }
        public List<ref_shifts_vm> GetShiftTimefromShift(int id)
        {
            var query = (from s in _scifferContext.ref_shifts.Where(x => x.is_active == true && x.is_blocked == false && x.shift_id == id)
                         select new ref_shifts_vm
                         {
                             shift_id = s.shift_id,
                             shift_code = s.shift_code + "/" + s.shift_desc,
                             plant_id = s.plant_id,
                             from_time_to_time = s.from_time.Hours.ToString() + ":" + s.from_time.Minutes.ToString() + "-" + s.to_time.Hours.ToString() + ":" + s.to_time.Minutes.ToString(),
                             from_time = s.from_time,
                             to_time = s.to_time
                         }).ToList();
            return query;
        }

        public int GetCheck_Inventory(int item_id, int plant_id, int storage_location_id, int bucket_id, decimal quantity)
        {
            var item_id1 = new SqlParameter("@item_id", Convert.ToInt32(item_id));
            var plantid1 = new SqlParameter("@plant_id", Convert.ToInt32(plant_id));
            var storage_location_id1 = new SqlParameter("@storage_location_id", Convert.ToInt32(storage_location_id));
            var bucket_id1 = new SqlParameter("@bucket_id", Convert.ToInt32(bucket_id));
            var quantity1 = new SqlParameter("@quantity", Convert.ToDecimal(quantity));

            int res = _scifferContext.Database.SqlQuery<int>("select dbo.Check_Inventory(@item_id,@plant_id,@storage_location_id,@bucket_id,@quantity) as Check_Inventory", item_id1, plantid1, storage_location_id1, bucket_id1, quantity1).FirstOrDefault();
            return res;
        }

        public List<ref_user_management_vm> GetOperatorList()
        {
            try
            {
                var query = (from urm in _scifferContext.ref_user_role_mapping.Where(x => x.role_id == 12 && x.is_active == true && x.is_block == false)
                             join um in _scifferContext.ref_user_management.Where(x => !x.is_block) on urm.user_id equals um.user_id
                             group um by um.user_id into g
                             select new ref_user_management_vm
                             {
                                 //user_id = _scifferContext.ref_user_management.Where(x=>x.user_id ==urm.user_id).GroupBy(),
                                 user_id = g.Select(x => x.user_id).FirstOrDefault(),
                                 user_name = (g.Select(x => x.user_code).FirstOrDefault() + "/" + g.Select(x => x.notes).FirstOrDefault()).ToString()
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ref_plan_breakdown_order_VM> GetAttendedByList(int? id)
        {
            var id1 = new SqlParameter("@id", id);
            var val = _scifferContext.Database.SqlQuery<ref_plan_breakdown_order_VM>(
            "exec GetAttendedBydata @id", id1).ToList();
            return val;
        }
        //public List<Ref_item_VM> GetItemList3()
        //{
        //    var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false)
        //                 select new Ref_item_VM
        //                 {
        //                     ITEM_ID = i.ITEM_ID,
        //                     ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
        //                     ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
        //                     item_type_id = i.item_type_id,
        //                 }).ToList();
        //    return query;
        //}
        public List<Ref_item_VM> GetItemList3()
        {
            var id = 3;
            var query = (from i in _scifferContext.REF_ITEM.Where(x => x.is_active == true && x.IS_BLOCKED == false)
                         where i.ITEM_CATEGORY_ID == id
                         select new Ref_item_VM
                         {
                             ITEM_ID = i.ITEM_ID,
                             ITEM_NAME = i.ITEM_CODE + "/" + i.ITEM_NAME,
                             ITEM_CATEGORY_ID = i.ITEM_CATEGORY_ID,
                             item_type_id = i.item_type_id,
                         }).ToList();
            return query;
        }

        public List<ref_machine_master_VM> GetMachineListWithOperationAndUserForTemp(int userId)
        {
            List<ref_machine_master_VM> result = null;
            try
            {
                result = (from mmpm in _scifferContext.map_mfg_process_machine
                          join moo in _scifferContext.map_operation_operator on mmpm.process_id equals moo.operation_id
                          join um in _scifferContext.ref_user_management.Where(x => x.user_id == userId && !x.is_block) on moo.operator_id equals um.user_id
                          join rm in _scifferContext.ref_machine on mmpm.machine_id equals rm.machine_id
                          select new ref_machine_master_VM
                          {
                              machine_id = rm.machine_id,
                              machine_code = rm.machine_code + "/" + rm.machine_name

                          }).ToList();
            }
            catch (Exception ex)
            {
                throw;
                return result;
            }
            return result;
        }
    }
}

