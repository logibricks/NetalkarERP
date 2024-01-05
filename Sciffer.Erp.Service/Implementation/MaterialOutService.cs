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
   public class MaterialOutService : IMaterialOutService
    {
            private readonly ScifferContext _scifferContext;
            private readonly IGenericService _genericService;
            public MaterialOutService(ScifferContext ScifferContext, IGenericService GenericService)
            {
                _scifferContext = ScifferContext;
                _genericService = GenericService;
            }
        public string Add(inv_material_out_VM main)
        {
            try
            {
                if (main.FileUpload != null)
                {
                    main.attachement = _genericService.GetFilePath("MaterialOut", main.FileUpload);
                }
                else
                {
                    main.attachement = "No File";
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("material_out_detail_id", typeof(int));
                dt.Columns.Add("item_id", typeof(int));
                dt.Columns.Add("user_description", typeof(string));
                dt.Columns.Add("uom_id", typeof(int));               
                dt.Columns.Add("quantity", typeof(double));
                dt.Columns.Add("hsn_id", typeof(int));
                dt.Columns.Add("tax_id", typeof(int));
                dt.Columns.Add("rate", typeof(double));
                dt.Columns.Add("value", typeof(double));                      
                dt.Columns.Add("reason", typeof(string));
                dt.Columns.Add("er_date", typeof(DateTime));
                dt.Columns.Add("balance_qty", typeof(double));
                dt.Columns.Add("remarks", typeof(string));
                if (main.item_id != null)
                {
                    for (var i = 0; i < main.material_out_detail_id.Count; i++)
                    {
                        int tag = -1;
                        if (main.material_out_detail_id != null)
                        {
                            tag = main.material_out_detail_id[i] == "" ? -1 : Convert.ToInt32(main.material_out_detail_id[i]);
                        }

                       dt.Rows.Add(tag, main.item_id[i] == "0" ? 0 : int.Parse(main.item_id[i]), main.user_description[i], 
                                    int.Parse(main.uom_id[i]),double.Parse(main.quantity[i]), int.Parse(main.hsn[i]), int.Parse(main.tax[i]),  double.Parse(main.rate[i]), double.Parse(main.value[i]), main.reason[i], DateTime.Parse(main.er_date[i]), double.Parse(main.quantity[i]), main.remarks1[i]);

                    }
                }

                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                t1.TypeName = "dbo.temp_inv_material_out_detail";
                t1.Value = dt;

               
                var material_out_id = new SqlParameter("@material_out_id", main.material_out_id == 0 ? -1 : main.material_out_id);
                var category_id = new SqlParameter("@category_id", main.@category_id);
                var document_number = new SqlParameter("@document_number", main.document_number == null ? string.Empty : main.document_number); 
                var posting_date = new SqlParameter("@posting_date", main.posting_date);
                var vendor_id = new SqlParameter("@vendor_id", main.vendor_id);
                var business_unit_id = new SqlParameter("@business_unit_id", main.business_unit_id);
                var plant_id = new SqlParameter("@plant_id", main.plant_id);                
                var ge_number = new SqlParameter("@ge_number", main.ge_number);
                var ge_date = new SqlParameter("@ge_date", main.ge_date);
                var employee_id = new SqlParameter("@employee_id", main.employee_id);
                var returnable_nonreturnable = new SqlParameter("@returnable_nonreturnable", main.returnable_nonreturnable == null ? string.Empty : main.returnable_nonreturnable);
                var internal_remarks = new SqlParameter("@internal_remarks", main.internal_remarks == null ? "" : main.internal_remarks);
                var remarks_on_document = new SqlParameter("@remarks_on_document", main.remarks_on_document == null ? "" : main.remarks_on_document);
                var attachement = new SqlParameter("@attachement", main.attachement);
                var delete = new SqlParameter("@deleteids", main.deleteids == null ? string.Empty : main.deleteids.Remove(0,1));
                var deleteids =  delete;
                var net_value = new SqlParameter("@net_value", main.net_value == null ? 0 : main.net_value);
                var gross_value = new SqlParameter("@gross_value", main.gross_value == null ? 0 : main.gross_value);
                var val = _scifferContext.Database.SqlQuery<string>(
                 "exec Save_MaterialOut @material_out_id ,@category_id ,@document_number, @posting_date, @vendor_id, @business_unit_id, @plant_id ,@ge_number ,@ge_date ,@employee_id,@returnable_nonreturnable,@internal_remarks,@remarks_on_document,@attachement,@deleteids,@net_value,@gross_value,@t1",
                 material_out_id, category_id, document_number, posting_date, vendor_id, business_unit_id, plant_id, ge_number, ge_date, employee_id, returnable_nonreturnable, internal_remarks,remarks_on_document, attachement, deleteids, net_value, gross_value,t1).FirstOrDefault();
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


        public List<inv_material_out_detail_VM> GetPurRequisitionDetailReport(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "materialOutDetail");
            var val = _scifferContext.Database.SqlQuery<inv_material_out_detail_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }
        public List<inv_material_out_VM> MaterialOut(int id)
        {
            var quotation_id = new SqlParameter("@id", id);
            var entity = new SqlParameter("@entity", "materialOut");
            var val = _scifferContext.Database.SqlQuery<inv_material_out_VM>(
            "exec GetBalanceQuantity @entity,@id", entity, quotation_id).ToList();
            return val;
        }


        //int i = 0;
        public inv_material_out_VM Get(int id)
            {
          
                try
                {
                    inv_material_out pla = _scifferContext.inv_material_out.Where(x=>x.material_out_id == id).FirstOrDefault();
                    Mapper.CreateMap<inv_material_out, inv_material_out_VM>();
                    inv_material_out_VM plvm = Mapper.Map<inv_material_out, inv_material_out_VM>(pla);
                    //plvm.inv_material_out_detail = plvm.inv_material_out_detail.ToList();
                    var material_out = new SqlParameter("@material_out_id", id);
                plvm.inv_material_out_detail = plvm.inv_material_out_detail.Where(c => c.is_active == true).ToList();
                plvm.inv_material_out_detail_VM = _scifferContext.Database.SqlQuery<inv_material_out_detail_VM>("exec GetMOList  @material_out_id", material_out).ToList();

                var company = _scifferContext.REF_COMPANY.FirstOrDefault();
                plvm.company_name = company.COMPANY_DISPLAY_NAME;
                plvm.company_address = company.REGISTERED_ADDRESS + ',' + company.REGISTERED_CITY + ',' + company.REF_STATE.STATE_NAME + '-' + company.registered_pincode + ',' + company.REF_STATE.REF_COUNTRY.COUNTRY_NAME;

                var business = _scifferContext.REF_BUSINESS_UNIT.FirstOrDefault();
                plvm.busienss_unit = business.BUSINESS_UNIT_NAME + "/" +business.BUSINESS_UNIT_DESCRIPTION;

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

            public List<inv_material_out_VM> GetAll()
            {
            //var query = (from mach in _scifferContext.inv_material_out 

            //             join vendor in _scifferContext.REF_VENDOR on mach.vendor_id equals vendor.VENDOR_ID into vendor1
            //             from vendor2 in vendor1.DefaultIfEmpty()                         
            //             join plant in _scifferContext.REF_PLANT on mach.plant_id equals plant.PLANT_ID into plant1
            //             from plant2 in plant1.DefaultIfEmpty()
            //             join business in _scifferContext.REF_BUSINESS_UNIT on mach.business_unit_id equals business.BUSINESS_UNIT_ID into business1
            //             from business2 in business1.DefaultIfEmpty()
            //             join employee in _scifferContext.REF_EMPLOYEE on mach.employee_id equals employee.employee_id into employee1
            //             from employee2 in employee1.DefaultIfEmpty()

            //             select new inv_material_out_VM()
            //             {
            //                 material_out_id = mach.material_out_id,

            //                 vendor_id = mach.vendor_id,
            //                 plant_id = mach.plant_id,
            //                 vendor_name = vendor2 == null ? "" : vendor2.VENDOR_CODE + "-" + vendor2.VENDOR_NAME,
            //                 busienss_unit = business2 == null ? "" : business2.BUSINESS_UNIT_NAME + "-" + business2.BUSINESS_UNIT_DESCRIPTION,
            //                 employee_name = employee2 == null ? "" : employee2.employee_code + "-" + employee2.employee_name,
            //                 plant_name = plant2 == null ? "" : plant2.PLANT_CODE + "-" + plant2.PLANT_NAME,
            //                 //slocname = slocname2 == null ? "" : slocname2.storage_location_name + "-" + slocname2.description,
            //                 //bucket_name = bucket_name2 == null ? "" : bucket_name2.bucket_name,
            //                 ge_number = mach.ge_number,
            //                 ge_date = mach.ge_date,
            //                 posting_date = mach.posting_date,                           
            //                 document_number = mach.document_number,
            //                 category_id = mach.category_id,

            //             }).OrderByDescending(a => a.material_out_id).ToList();
            //return query;
            var query = _scifferContext.Database.SqlQuery<inv_material_out_VM>(
            "exec GetMaterialOutDetail").ToList();
            return query;
        }

        public string Cancel(int material_out_id, int cancellation_reason_id, string cancellation_remarks)
        {
            try
            {
                var cancel_reason = new SqlParameter("@cancellation_reason_id", cancellation_reason_id);
                var cancel_remark = new SqlParameter("@cancellation_remarks", cancellation_remarks);
                var materialout_id = new SqlParameter("@material_out_id", material_out_id);
                var user = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                var cancelled_by = new SqlParameter("@cancelled_by", user);
                //var company = int.Parse(HttpContext.Current.Session["Comp"].ToString());
                //var company_id = new SqlParameter("@company_id", company);
                //var plant_id = int.Parse(HttpContext.Current.Session["location_id"].ToString());
                //var plantid = new SqlParameter("@plant_id", plant_id);
                var val = _scifferContext.Database.SqlQuery<string>("exec cancel_material_out @material_out_id ,@cancellation_remarks,@cancelled_by ,@cancellation_reason_id ",
                materialout_id, cancel_remark, cancelled_by, cancel_reason).FirstOrDefault();
                return val;
            }

            catch (Exception ex)
            {
                return ex.InnerException.InnerException.Message;
            }

        }

    }
    }
