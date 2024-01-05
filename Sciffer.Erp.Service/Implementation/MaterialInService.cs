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
    public class MaterialInService : IMaterialInService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public MaterialInService(ScifferContext ScifferContext, IGenericService GenericService)
        {
            _scifferContext = ScifferContext;
            _genericService = GenericService;
        }
        public string Add(inv_material_in_VM main)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("material_in_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("user_description", typeof(string));
                dt.Columns.Add("uom_id", typeof(int));
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("reason", typeof(string));
                dt.Columns.Add("er_date", typeof(DateTime));
                dt.Columns.Add("balance_qty", typeof(double));
                dt.Columns.Add("material_out_detail_id", typeof(int));

                if (main.item_id != null)
                {
                    for (var i = 0; i < main.material_in_detail_id.Count; i++)
                    {
                        int tag = -1;
                        if (main.material_in_detail_id != null)
                        {
                            tag = main.material_in_detail_id[i] == "" ? -1 : Convert.ToInt32(main.material_in_detail_id[i]);
                        }
                        
                        dt.Rows.Add(tag, main.item_id[i] == "0" ? 0 : int.Parse(main.item_id[i]), main.user_description[i], 
                            int.Parse(main.uom_id[i]), double.Parse(main.quantity[i]), main.reason[i], 
                            main.er_date[i], double.Parse(main.balance_qty[i]), 
                            main.material_out_detail_id[i] == "" || main.material_out_detail_id[i] == null ? -1 : int.Parse(main.material_out_detail_id[i]));

                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_inv_material_in_detail";
                t1.Value = dt;

                var material_in_id = new SqlParameter("@material_in_id", main.material_in_id == 0 ? -1 : main.material_in_id);
                var category_id = new SqlParameter("@category_id", main.@category_id);
                var document_number = new SqlParameter("@document_number", main.document_number == null ? string.Empty : main.document_number);
                var posting_date = new SqlParameter("@posting_date", main.posting_date);
                var vendor_id = new SqlParameter("@vendor_id", main.vendor_id);
                var business_unit_id = new SqlParameter("@business_unit_id", main.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);
                var ge_number = new SqlParameter("@ge_number", main.ge_number);
                var ge_date = new SqlParameter("@ge_date", main.ge_date);
                var employee_id = new SqlParameter("@employee_id", main.employee_id);
                var material_out_document_number = new SqlParameter("@material_out_document_number", main.material_out_document_number);
                var vendor_invoice_no = new SqlParameter("@vendor_invoice_no", main.vendor_invoice_no);
                var vendor_invoice_date = new SqlParameter("@vendor_invoice_date", main.vendor_invoice_date);
                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec Save_MaterialIn @material_in_id ,@category_id ,@document_number, @posting_date, @vendor_id, @business_unit_id, @plant_id ,@ge_number ,@ge_date ,@employee_id,@material_out_document_number,@vendor_invoice_no,@vendor_invoice_date,@t1",
                    material_in_id, category_id, document_number, posting_date, vendor_id, business_unit_id, plant_id, ge_number, ge_date, employee_id, material_out_document_number, vendor_invoice_no, vendor_invoice_date,t1).FirstOrDefault();

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


        public List<inv_material_in_detail_VM> GetPurRequisitionDetailReport(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "materialInDetail");
            var val = _scifferContext.Database.SqlQuery<inv_material_in_detail_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<inv_material_in_VM> MaterialIn(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "materialIn");
            var val = _scifferContext.Database.SqlQuery<inv_material_in_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }


        //int i = 0;
        public inv_material_in_VM Get(int id)
        {

            try
            {
                inv_material_in pla = _scifferContext.inv_material_in.FirstOrDefault(c => c.material_in_id == id);
                Mapper.CreateMap<inv_material_in, inv_material_in_VM>();
                inv_material_in_VM plvm = Mapper.Map<inv_material_in, inv_material_in_VM>(pla);
                //plvm.inv_material_in_detail = plvm.inv_material_in_detail.ToList();
                var material_in = new SqlParameter("@material_in_id", id);
                
                //plvm.inv_material_in_detail_VM = _scifferContext.Database.SqlQuery<inv_material_in_detail_VM>("exec GetMaterialIn  @material_in_id", material_in).ToList();

                var company = _scifferContext.REF_COMPANY.FirstOrDefault();
                plvm.company_name = company.COMPANY_DISPLAY_NAME;
                plvm.company_address = company.REGISTERED_ADDRESS + ',' + company.REGISTERED_CITY + ',' + company.REF_STATE.STATE_NAME + '-' + company.registered_pincode + ',' + company.REF_STATE.REF_COUNTRY.COUNTRY_NAME;

                var business = _scifferContext.REF_BUSINESS_UNIT.FirstOrDefault();
                plvm.busienss_unit = business.BUSINESS_UNIT_NAME + "/" + business.BUSINESS_UNIT_DESCRIPTION;

                var emp = _scifferContext.REF_EMPLOYEE.FirstOrDefault();
                plvm.employee_name = emp.employee_code + "/" + emp.employee_name;

                var vendor = _scifferContext.REF_VENDOR.FirstOrDefault();
                plvm.vendor_name = vendor.VENDOR_CODE + "/" + vendor.VENDOR_NAME;

                var plant = _scifferContext.REF_PLANT.FirstOrDefault();
                plvm.plant_name = plant.PLANT_CODE + "/" + plant.PLANT_NAME;             

                return plvm;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<inv_material_in_VM> GetAll()
        {
            var query = (from mach in _scifferContext.inv_material_in

                         join vendor in _scifferContext.REF_VENDOR on mach.vendor_id equals vendor.VENDOR_ID into vendor1
                         from vendor2 in vendor1.DefaultIfEmpty()
                         join plant in _scifferContext.REF_PLANT on mach.plant_id equals plant.PLANT_ID into plant1
                         from plant2 in plant1.DefaultIfEmpty()
                         join business in _scifferContext.REF_BUSINESS_UNIT on mach.business_unit_id equals business.BUSINESS_UNIT_ID into business1
                         from business2 in business1.DefaultIfEmpty()
                         join employee in _scifferContext.REF_EMPLOYEE on mach.employee_id equals employee.employee_id into employee1
                         from employee2 in employee1.DefaultIfEmpty()
                         join st in _scifferContext.ref_status on mach.status_id equals st.status_id into st1
                         from st2 in st1.DefaultIfEmpty()
                         select new inv_material_in_VM()
                         {
                             material_in_id = mach.material_in_id,

                             vendor_id = mach.vendor_id,
                             plant_id = mach.plant_id,
                             vendor_name = vendor2 == null ? "" : vendor2.VENDOR_CODE + "-" + vendor2.VENDOR_NAME,
                             busienss_unit = business2 == null ? "" : business2.BUSINESS_UNIT_NAME + "-" + business2.BUSINESS_UNIT_DESCRIPTION,
                             employee_name = employee2 == null ? "" : employee2.employee_code + "-" + employee2.employee_name,
                             plant_name = plant2 == null ? "" : plant2.PLANT_CODE + "-" + plant2.PLANT_NAME,
                             //slocname = slocname2 == null ? "" : slocname2.storage_location_name + "-" + slocname2.description,
                             //bucket_name = bucket_name2 == null ? "" : bucket_name2.bucket_name,
                             ge_number = mach.ge_number,
                             ge_date = mach.ge_date,
                            posting_date = mach.posting_date,
                             document_number = mach.document_number,
                             category_id = mach.category_id,
                             status = st2.status_name
                         }).OrderByDescending(a => a.material_in_id).ToList();
            return query;
        }

        public inv_material_out_VM GetDocNo(int id)
        {
            var query = (from i in _scifferContext.inv_material_out.Where(x => x.material_out_id == id)
                         select new inv_material_out_VM
                         {
                             material_out_id = i.material_out_id,
                             document_number = i.document_number,

                         }).FirstOrDefault();
            return query;

        }

       
        public List<GetMaterialInforVendor> GetMaterialInforVendor(int material_out_id)
        {
            var material = new SqlParameter("@material_out_id", material_out_id);            
            var val = _scifferContext.Database.SqlQuery<GetMaterialInforVendor>(
            "exec GetMaterialInforVendor @material_out_id", material).ToList();
            return val;
        }
        public List<GetMOList> GetMOList(int vendor_id)
        {
            var vendor = new SqlParameter("@vendor_id", vendor_id);
            var val = _scifferContext.Database.SqlQuery<GetMOList>(
            "exec GetMOList @vendor_id", vendor).ToList();
            return val;
        }

        public string Cancel(int material_in_id, int cancellation_reason_id, string cancellation_remarks)
        {
            try
            {
                var cancel_reason = new SqlParameter("@cancellation_reason_id", cancellation_reason_id);
                var cancel_remark = new SqlParameter("@cancellation_remarks", cancellation_remarks);
                var materialin_id = new SqlParameter("@material_in_id", material_in_id);
                var user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var cancelled_by = new SqlParameter("@cancelled_by", user);
                //var company = int.Parse(HttpContext.Current.Session["Comp"].ToString());
                //var company_id = new SqlParameter("@company_id", company);
                //var plant_id = int.Parse(HttpContext.Current.Session["location_id"].ToString());
                //var plantid = new SqlParameter("@plant_id", plant_id);
                var val = _scifferContext.Database.SqlQuery<string>("exec cancel_material_in @material_in_id ,@cancellation_remarks,@cancelled_by ,@cancellation_reason_id ",
                materialin_id, cancel_remark, cancelled_by, cancel_reason).FirstOrDefault();
                return val;
            }

            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
            }

        }

    }
}
