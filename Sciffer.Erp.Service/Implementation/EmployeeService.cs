using AutoMapper;
using AutoMapper.QueryableExtensions;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ScifferContext _scifferContext;
        private readonly IGenericService _genericService;
        public EmployeeService(ScifferContext scifferContext, IGenericService GenericService)
        {
            _genericService = GenericService;
            _scifferContext = scifferContext;
        }
        public List<REF_EMPLOYEE_VM> GetAll()
        {

            Mapper.CreateMap<REF_EMPLOYEE, REF_EMPLOYEE_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            return _scifferContext.REF_EMPLOYEE.Where(x => x.is_block == false).Project().To<REF_EMPLOYEE_VM>().ToList();

        }
        public REF_EMPLOYEE_VM Get(int id)
        {
            REF_EMPLOYEE GI = _scifferContext.REF_EMPLOYEE.FirstOrDefault(c => c.employee_id == id);
            Mapper.CreateMap<REF_EMPLOYEE, REF_EMPLOYEE_VM>().ForMember(dest => dest.attachment, opt => opt.Ignore());
            REF_EMPLOYEE_VM GIV = Mapper.Map<REF_EMPLOYEE, REF_EMPLOYEE_VM>(GI);
            return GIV;
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

        public bool Delete(int id)
        {
            try
            {
                // _scifferContext.Database.ExecuteSqlCommand("update [dbo].[REF_EMPLOYEE] set [IS_ACTIVE] = 0 where employee_id = " + id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Add(REF_EMPLOYEE_VM employee)
        {
            try
            {
                var ImageAttachment = "";
                if (employee.ImageUpload != null)
                {
                    ImageAttachment = _genericService.GetFilePathForImage1("EMPLOYEEIMAGE", employee.ImageUpload, employee.employee_code);
                }

                DataTable t11 = new DataTable();//for sub ledger
                t11.Columns.Add("entity_type_id", typeof(int));
                t11.Columns.Add("gl_ledger_id", typeof(int));
                t11.Columns.Add("ledger_account_type_id", typeof(int));
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = employee.ledgeraccounttype.Split(new string[] { "~" }, StringSplitOptions.None);
                }
                catch
                {

                }
                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    t11.Rows.Add(3, int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]), int.Parse(emptyStringArray[i].Split(new char[] { ',' })[0]));
                }
                int createuser = 0;
                var entity = new SqlParameter("@entity", "save");
                var employee_id = new SqlParameter("@employee_id", employee.employee_id);
                var employee_code = new SqlParameter("@employee_code", employee.employee_code);
                var salutation_id = new SqlParameter("@salutation_id", employee.salutation_id == null ? 0 : employee.salutation_id);
                var employee_name = new SqlParameter("@employee_name", employee.employee_name);
                var employee_number = new SqlParameter("@employee_number", employee.employee_number == null ? "" : employee.employee_number);
                var designation_id = new SqlParameter("@designation_id", employee.designation_id == null ? 0 : employee.designation_id);
                var category_id = new SqlParameter("@category_id", employee.category_id == null ? 0 : employee.category_id);
                var department_id = new SqlParameter("@department_id", employee.department_id == null ? 0 : employee.department_id);
                var grade_id = new SqlParameter("@grade_id", employee.grade_id == null ? 0 : employee.grade_id);
                var is_block = new SqlParameter("@is_block", employee.is_block);
                var fathers_name = new SqlParameter("@fathers_name", employee.fathers_name == null ? "" : employee.fathers_name);
                var mothers_name = new SqlParameter("@mothers_name", employee.mothers_name == null ? "" : employee.mothers_name);
                var date_of_birth = new SqlParameter("@date_of_birth", employee.date_of_birth);
                var gender_id = new SqlParameter("@gender_id", employee.gender_id == null ? 0 : employee.gender_id);
                var marital_status_id = new SqlParameter("@marital_status_id", employee.marital_status_id == null ? 0 : employee.marital_status_id);
                var spouse_name = new SqlParameter("@spouse_name", employee.spouse_name == null ? "" : employee.spouse_name);
                var bank_id = new SqlParameter("@bank_id", employee.bank_id == null ? 0 : employee.bank_id);
                var bank_account_no = new SqlParameter("@bank_account_no", employee.bank_account_no == null ? "" : employee.bank_account_no);
                var ifsc_code = new SqlParameter("@ifsc_code", employee.ifsc_code == null ? "" : employee.ifsc_code);
                var pan_number = new SqlParameter("@pan_number", employee.pan_number == null ? "" : employee.pan_number);
                var present_add_country_id = new SqlParameter("@present_add_country_id", employee.present_add_country_id == null ? 0 : employee.present_add_country_id);
                var present_add_res_no = new SqlParameter("@present_add_res_no", employee.present_add_res_no == null ? "" : employee.present_add_res_no);
                var present_add_res_name = new SqlParameter("@present_add_res_name", employee.present_add_res_name == null ? "" : employee.present_add_res_name);
                var present_add_street = new SqlParameter("@present_add_street", employee.present_add_street == null ? "" : employee.present_add_street);
                var present_add_locality = new SqlParameter("@present_add_locality", employee.present_add_locality == null ? "" : employee.present_add_locality);
                var present_add_city = new SqlParameter("@present_add_city", employee.present_add_city == null ? "" : employee.present_add_city);
                var present_add_state_id = new SqlParameter("@present_add_state_id", employee.present_add_state_id == null ? 0 : employee.present_add_state_id);
                var present_add_pincode = new SqlParameter("@present_add_pincode", employee.present_add_pincode == null ? 0 : employee.present_add_pincode);
                var permanent_add_country_id = new SqlParameter("@permanent_add_country_id", employee.permanent_add_country_id == null ? 0 : employee.permanent_add_country_id);
                var permanent_add_res_no = new SqlParameter("@permanent_add_res_no", employee.permanent_add_res_no == null ? "" : employee.permanent_add_res_no);
                var permanent_add_res_name = new SqlParameter("@permanent_add_res_name", employee.permanent_add_res_name == null ? "" : employee.permanent_add_res_name);
                var permanent_add_street = new SqlParameter("@permanent_add_street", employee.permanent_add_street == null ? "" : employee.permanent_add_street);
                var permanent_add_locality = new SqlParameter("@permanent_add_locality", employee.permanent_add_locality == null ? "" : employee.permanent_add_locality);
                var permanent_add_city = new SqlParameter("@permanent_add_city", employee.permanent_add_city == null ? "" : employee.permanent_add_city);
                var permanent_add_state_id = new SqlParameter("@permanent_add_state_id", employee.permanent_add_state_id == null ? 0 : employee.permanent_add_state_id);
                var permanent_add_pincode = new SqlParameter("@permanent_add_pincode", employee.permanent_add_pincode == null ? 0 : employee.permanent_add_pincode);
                var email_id = new SqlParameter("@email_id", employee.email_id == null ? "" : employee.email_id);
                var mobile = new SqlParameter("@mobile", employee.mobile == null ? "" : employee.mobile);
                var std_code = new SqlParameter("@std_code", employee.std_code == null ? "" : employee.std_code);
                var phone = new SqlParameter("@phone", employee.phone == null ? "" : employee.phone);
                var branch_id = new SqlParameter("@branch_id", employee.branch_id == null ? 0 : employee.branch_id);
                var division_id = new SqlParameter("@division_id", employee.division_id == null ? 0 : employee.division_id);
                var date_of_joining = new SqlParameter("@date_of_joining", employee.date_of_joining);
                var date_of_leaving = new SqlParameter("@date_of_leaving", employee.date_of_leaving);
                var reason_for_leaving = new SqlParameter("@reason_for_leaving", employee.reason_for_leaving == null ? "" : employee.reason_for_leaving);
                var esi_applicable = new SqlParameter("@esi_applicable", employee.esi_applicable);
                var esi_no = new SqlParameter("@esi_no", employee.esi_no == null ? "No File" : employee.esi_no == null ? "" : employee.esi_no);
                var esi_dispensary = new SqlParameter("@esi_dispensary", employee.esi_dispensary == null ? "No File" : employee.esi_dispensary);
                var pf_applicable = new SqlParameter("@pf_applicable", employee.pf_applicable);
                var pf_no = new SqlParameter("@pf_no", employee.pf_no == null ? "No File" : employee.pf_no == null ? "" : employee.pf_no);
                var pf_no_dept = new SqlParameter("@pf_no_dept", employee.pf_no_dept == null ? "No File" : employee.pf_no_dept);
                var uan_no = new SqlParameter("@uan_no", employee.uan_no == null ? "" : employee.uan_no);
                var plant_id = new SqlParameter("@plant_id", employee.plant_id == null ? 0 : employee.plant_id);

                var remarks = new SqlParameter("@remarks", employee.remarks == null ? "" : employee.remarks);
                // var attachment = new SqlParameter("@attachment", employee.attachment);
                var attachment = new SqlParameter("@attachment", employee.attachment == null ? "No File" : employee.attachment);
                var image = new SqlParameter("@image", ImageAttachment == "" ? "No File" : ImageAttachment);
                var t1 = new SqlParameter("@t1", SqlDbType.Structured);
                var create_user = new SqlParameter("@create_user", createuser);
                t1.TypeName = "dbo.temp_sub_ledger";
                t1.Value = t11;
                var is_applicable = new SqlParameter("@is_applicable", employee.is_applicable);

                var val = _scifferContext.Database.SqlQuery<string>(
                    "exec save_employee  @entity,@employee_id,@employee_code,@salutation_id,@employee_name,@employee_number,@designation_id,@category_id, @department_id, @grade_id, @is_block, @fathers_name, @mothers_name, @date_of_birth, @gender_id, @marital_status_id, @spouse_name, @bank_id, @bank_account_no, @ifsc_code,@pan_number, @present_add_country_id, @present_add_res_no, @present_add_res_name, @present_add_street, @present_add_locality, @present_add_city, @present_add_state_id, @present_add_pincode,@permanent_add_country_id, @permanent_add_res_no, @permanent_add_res_name, @permanent_add_street, @permanent_add_locality, @permanent_add_city, @permanent_add_state_id,@permanent_add_pincode, @email_id, @mobile, @std_code, @phone, @branch_id, @division_id, @date_of_joining,@date_of_leaving,@reason_for_leaving,@esi_applicable,@esi_no,@esi_dispensary,@pf_applicable,@pf_no,@pf_no_dept,@uan_no,@plant_id,@remarks,@attachment,@image,@create_user, @t1,@is_applicable",
                     entity, employee_id, employee_code, salutation_id, employee_name, employee_number, designation_id, category_id, department_id, grade_id, is_block, fathers_name, mothers_name,
                     date_of_birth, gender_id, marital_status_id, spouse_name, bank_id, bank_account_no, ifsc_code, pan_number, present_add_country_id, present_add_res_no,
                     present_add_res_name, present_add_street, present_add_locality, present_add_city, present_add_state_id, present_add_pincode, permanent_add_country_id,
                     permanent_add_res_no, permanent_add_res_name, permanent_add_street, permanent_add_locality, permanent_add_city, permanent_add_state_id, permanent_add_pincode,
                     email_id, mobile, std_code, phone, branch_id, division_id, date_of_joining, date_of_leaving, reason_for_leaving, esi_applicable, esi_no, esi_dispensary, pf_applicable,
                     pf_no, pf_no_dept, uan_no, plant_id, remarks, attachment, image, create_user, t1, is_applicable).FirstOrDefault();

                if (val == "Saved")
                {
                    //if (employee.ImageUpload != null)
                    //{
                    //    employee.ImageUpload.SaveAs(ImageAttachment);
                    //}
                    return true;
                }
                else
                {
                    return false;
                }
                //REF_EMPLOYEE RE = new REF_EMPLOYEE();

                //RE.employee_code = employee.employee_code;
                //RE.salutation_id = employee.salutation_id;
                //RE.employee_name = employee.employee_name;
                //RE.employee_number = employee.employee_number;
                //RE.designation_id = employee.designation_id;
                //RE.category_id = employee.category_id;
                //RE.department_id = employee.department_id;
                //RE.grade_id = employee.grade_id;
                //RE.is_block = employee.is_block;
                //RE.fathers_name = employee.fathers_name;
                //RE.mothers_name = employee.mothers_name;
                //RE.date_of_birth = employee.date_of_birth;
                //RE.gender_id = employee.gender_id;
                //RE.marital_status_id = employee.marital_status_id;
                //RE.spouse_name = employee.spouse_name;
                //RE.bank_id = employee.bank_id;
                //RE.bank_account_no = employee.bank_account_no;
                //RE.ifsc_code = employee.ifsc_code;
                //RE.pan_number = employee.pan_number;
                //RE.present_add_country_id = employee.present_add_country_id;
                //RE.present_add_res_no = employee.present_add_res_no;
                //RE.present_add_res_name = employee.present_add_res_name;
                //RE.present_add_street = employee.present_add_street;
                //RE.present_add_locality = employee.present_add_locality;
                //RE.present_add_city = employee.present_add_city;
                //RE.present_add_state_id = employee.present_add_state_id;
                //RE.present_add_pincode = employee.present_add_pincode;

                //RE.permanent_add_country_id = employee.permanent_add_country_id;
                //RE.permanent_add_res_no = employee.permanent_add_res_no;
                //RE.permanent_add_res_name = employee.permanent_add_res_name;
                //RE.permanent_add_street = employee.permanent_add_street;
                //RE.permanent_add_locality = employee.permanent_add_locality;
                //RE.permanent_add_city = employee.permanent_add_city;
                //RE.permanent_add_state_id = employee.permanent_add_state_id;
                //RE.permanent_add_pincode = employee.permanent_add_pincode;

                //RE.email_id = employee.email_id;
                //RE.mobile = employee.mobile;
                //RE.std_code = employee.std_code;
                //RE.phone = employee.phone;
                //RE.branch_id = employee.branch_id;
                //RE.division_id = employee.division_id;
                //RE.date_of_joining = employee.date_of_joining;
                //RE.salary_calculate_from = employee.salary_calculate_from;
                //RE.date_of_leaving = employee.date_of_leaving;
                //RE.reason_for_leaving = employee.reason_for_leaving;
                //RE.remarks = employee.remarks;
                //if (employee.FileUpload != null)
                //{
                //    RE.attachment = _genericService.GetFilePath("Employee", employee.FileUpload);
                //}
                //else
                //{
                //    RE.attachment = "No File";
                //}

                //_scifferContext.REF_EMPLOYEE.Add(RE);
                //_scifferContext.SaveChanges();
                //if (employee.FileUpload != null)
                //{
                //    employee.FileUpload.SaveAs(RE.attachment);
                //}

            }

            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public bool Update(REF_EMPLOYEE_VM employee)
        {
            try
            {
                string[] emptyStringArray = new string[0];
                try
                {
                    emptyStringArray = employee.ledgeraccounttype.Split(new string[] { "~" }, StringSplitOptions.None);
                }
                catch
                {

                }


                REF_EMPLOYEE RE = new REF_EMPLOYEE();
                RE.employee_id = employee.employee_id;
                RE.employee_code = employee.employee_code;
                RE.salutation_id = employee.salutation_id;
                RE.employee_name = employee.employee_name;
                RE.employee_number = employee.employee_number;
                RE.designation_id = employee.designation_id;
                RE.category_id = employee.category_id;
                RE.department_id = employee.department_id;
                RE.grade_id = employee.grade_id;
                RE.is_block = employee.is_block;
                RE.fathers_name = employee.fathers_name;
                RE.mothers_name = employee.mothers_name;
                RE.date_of_birth = employee.date_of_birth;
                RE.gender_id = employee.gender_id;
                RE.marital_status_id = employee.marital_status_id;
                RE.spouse_name = employee.spouse_name;
                RE.bank_id = employee.bank_id;
                RE.bank_account_no = employee.bank_account_no;
                RE.ifsc_code = employee.ifsc_code;
                RE.pan_number = employee.pan_number;
                RE.present_add_country_id = employee.present_add_country_id;
                RE.present_add_res_no = employee.present_add_res_no;
                RE.present_add_res_name = employee.present_add_res_name;
                RE.present_add_street = employee.present_add_street;
                RE.present_add_locality = employee.present_add_locality;
                RE.present_add_city = employee.present_add_city;
                RE.present_add_state_id = employee.present_add_state_id;
                RE.present_add_pincode = employee.present_add_pincode;

                RE.permanent_add_country_id = employee.permanent_add_country_id;
                RE.permanent_add_res_no = employee.permanent_add_res_no;
                RE.permanent_add_res_name = employee.permanent_add_res_name;
                RE.permanent_add_street = employee.permanent_add_street;
                RE.permanent_add_locality = employee.permanent_add_locality;
                RE.permanent_add_city = employee.permanent_add_city;
                RE.permanent_add_state_id = employee.permanent_add_state_id;
                RE.permanent_add_pincode = employee.permanent_add_pincode;

                RE.email_id = employee.email_id;
                RE.mobile = employee.mobile;
                RE.std_code = employee.std_code;
                RE.phone = employee.phone;
                RE.branch_id = employee.branch_id;
                RE.division_id = employee.division_id;
                RE.date_of_joining = employee.date_of_joining;
                RE.date_of_leaving = employee.date_of_leaving;
                RE.reason_for_leaving = employee.reason_for_leaving;

                RE.esi_applicable = employee.esi_applicable;
                RE.esi_no = employee.esi_no;
                RE.esi_dispensary = employee.esi_dispensary;
                RE.pf_applicable = employee.pf_applicable;
                RE.pf_no = employee.pf_no;
                RE.pf_no_dept = employee.pf_no_dept;
                RE.uan_no = employee.uan_no;
                RE.plant_id = employee.plant_id;
                RE.is_applicable = employee.is_applicable;

                RE.remarks = employee.remarks;
                if (employee.FileUpload != null)
                {
                    RE.attachment = _genericService.GetFilePath("Employee", employee.FileUpload);
                }
                else
                {
                    RE.attachment = employee.attachment;
                }

                _scifferContext.Entry(RE).State = EntityState.Modified;
                _scifferContext.SaveChanges();
                if (employee.FileUpload != null)
                {
                    employee.FileUpload.SaveAs(RE.attachment);
                }

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        public List<REF_EMPLOYEE_VM> GetEmployeeCode()
        {
            var query = (from ed in _scifferContext.REF_EMPLOYEE
                         select new REF_EMPLOYEE_VM
                         {
                             employee_id = ed.employee_id,
                             employee_name = ed.employee_code + "-" + ed.employee_name,
                         }).ToList();
            return query;
        }
        public List<REF_EMPLOYEE_VM> GetEmployeeList()
        {
            var query = (from e in _scifferContext.REF_EMPLOYEE
                         join div in _scifferContext.REF_DIVISION on e.division_id equals div.DIVISION_ID into div1
                         from div2 in div1.DefaultIfEmpty()
                         join des in _scifferContext.REF_DESIGNATION on e.designation_id equals des.designation_id into des1
                         from des2 in des1.DefaultIfEmpty()
                         join dep in _scifferContext.REF_DEPARTMENT on e.department_id equals dep.DEPARTMENT_ID into dep1
                         from dep2 in dep1.DefaultIfEmpty()
                         join cat in _scifferContext.REF_CATEGORY on e.category_id equals cat.CATEGORY_ID into cat1
                         from cat2 in cat1.DefaultIfEmpty()
                         join bnk in _scifferContext.ref_bank on e.bank_id equals bnk.bank_id into bnk1
                         from bnk2 in bnk1.DefaultIfEmpty()
                         join st1 in _scifferContext.REF_STATE on e.present_add_state_id equals st1.STATE_ID into st1
                         from st2 in st1.DefaultIfEmpty()
                             // join st1 in _scifferContext.REF_STATE on e.present_add_state_id equals st1.STATE_ID
                         join c1 in _scifferContext.REF_COUNTRY on st2.COUNTRY_ID equals c1.COUNTRY_ID into c1_country1
                         from c1_country2 in c1_country1.DefaultIfEmpty()
                         join stp1 in _scifferContext.REF_STATE on e.permanent_add_state_id equals stp1.STATE_ID into stp1
                         from stp2 in st1.DefaultIfEmpty()
                             //join st2 in _scifferContext.REF_STATE on e.permanent_add_state_id equals st2.STATE_ID
                         join c2 in _scifferContext.REF_COUNTRY on stp2.COUNTRY_ID equals c2.COUNTRY_ID into c2_country1
                         from c2_country2 in c2_country1.DefaultIfEmpty()
                         join br in _scifferContext.REF_BRANCH on e.branch_id equals br.BRANCH_ID into br1
                         from br2 in br1.DefaultIfEmpty()
                         join gr in _scifferContext.REF_GRADE on e.grade_id equals gr.grade_id into gr1
                         from gr2 in gr1.DefaultIfEmpty()
                         join s in _scifferContext.REF_SALUTATION on e.salutation_id equals s.salutation_id into s1
                         from s2 in s1.DefaultIfEmpty()
                         join p in _scifferContext.REF_PLANT on e.plant_id equals p.PLANT_ID into p1
                         from p2 in p1.DefaultIfEmpty()
                         join salutation in _scifferContext.REF_SALUTATION on e.salutation_id equals salutation.salutation_id into salutation1
                         from salutation2 in salutation1.DefaultIfEmpty()
                         select new REF_EMPLOYEE_VM
                         {
                             bank_account_no = e.bank_account_no,
                             bank_name = bnk2.bank_name,
                             branch = br2.BRANCH_NAME,
                             category = cat2.CATEGORY_NAME,
                             country1 = c1_country2.COUNTRY_NAME,
                             country2 = c2_country2.COUNTRY_NAME,
                             date_of_birth = e.date_of_birth,
                             date_of_joining = e.date_of_joining,

                             department = dep2.DEPARTMENT_NAME,
                             designation = des2.designation_name,
                             division = div2.DIVISION_NAME,
                             email_id = e.email_id,
                             employee_code = e.employee_code,
                             employee_id = e.employee_id,
                             employee_name = e.employee_name,
                             employee_number = e.employee_number,
                             fathers_name = e.fathers_name,
                             grade = gr2.grade_name,
                             ifsc_code = e.ifsc_code,
                             is_block = e.is_block,
                             mobile = e.mobile,
                             mothers_name = e.mothers_name,
                             pan_number = e.pan_number,
                             permanent_add_city = e.permanent_add_city,
                             permanent_add_locality = e.permanent_add_locality,
                             permanent_add_pincode = e.permanent_add_pincode,
                             permanent_add_res_name = e.permanent_add_res_name,
                             permanent_add_res_no = e.permanent_add_res_no,
                             permanent_add_street = e.permanent_add_street,
                             phone = e.phone,
                             present_add_city = e.present_add_city,
                             present_add_locality = e.present_add_locality,
                             present_add_pincode = e.present_add_pincode,
                             present_add_res_name = e.present_add_res_name,
                             present_add_res_no = e.present_add_res_no,
                             present_add_street = e.present_add_street,
                             date_of_leaving = e.date_of_leaving,
                             reason_for_leaving = e.reason_for_leaving,

                             esi_applicable = e.esi_applicable,
                             esi_no = e.esi_no,
                             esi_dispensary = e.esi_dispensary,
                             pf_applicable = e.pf_applicable,
                             pf_no = e.pf_no,
                             pf_no_dept = e.pf_no_dept,
                             uan_no = e.uan_no,
                             plant_id = e.plant_id,
                             remarks = e.remarks,
                             spouse_name = e.spouse_name,

                             state1 = st2.STATE_NAME,
                             state2 = stp2.STATE_NAME,
                             std_code = e.std_code,
                             salutation = s2.salutation_name,
                             gender = e.gender_id == 1 ? "Male" : "Female",
                             marrital = e.marital_status_id == 1 ? "Married" : "UnMarried",
                             attachment = e.attachment,
                             present_add_state_id = e.present_add_state_id,
                             permanent_add_state_id = e.permanent_add_state_id,
                             salutation_name = salutation2.salutation_name,
                             blocked = e.is_block,
                             plant_name = p2.PLANT_NAME,
                             is_applicable = e.is_applicable
                         }).OrderByDescending(a => a.employee_id).ToList();
            return query;
        }
    }
}
