using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using Syncfusion.JavaScript.Models;
using Syncfusion.EJ.Export;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using Syncfusion.XlsIO;
using System.Reflection;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly ISalutationService _salutationService;
        private readonly ICategoryService _categoryService;
        private readonly IDesignationService _designationService;
        private readonly IGradeService _gradeService;
        private readonly IDepartmentService _departmentService;
        private readonly IBranchService _branchService;
        private readonly IDivisionService _divisionService;
        private readonly IBankService _bankService;
        private readonly IGenericService _Generic;
        private readonly IPlantService _plantService;
        public EmployeeController(IEmployeeService EmployeeService, ICountryService CountryService, IStateService StateService, ISalutationService SalutationService, ICategoryService CategoryService,
            IDesignationService DesignationService, IGradeService GradeService, IDepartmentService DepartmentService, IBranchService BranchService, IDivisionService DivisionService,
            IBankService BankService, IGenericService gen, IPlantService plantService)
        {
            _employeeService = EmployeeService;
            _countryService = CountryService;
            _stateService = StateService;
            _salutationService = SalutationService;
            _categoryService = CategoryService;
            _designationService = DesignationService;
            _gradeService = GradeService;
            _departmentService = DepartmentService;
            _branchService = BranchService;
            _divisionService = DivisionService;
            _bankService = BankService;
            _Generic = gen;
            _plantService = plantService;
        }
        // GET: Employee

        [CustomAuthorizeAttribute("EMPM")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _employeeService.GetEmployeeList();
            return View();
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_EMPLOYEE_VM rEF_EMPLOYEE_VM = _employeeService.Get((int)id);
            if (rEF_EMPLOYEE_VM == null)
            {
                return HttpNotFound();
            }

            var co = _countryService.GetAll();
            var st = _stateService.GetAll();
            var sa = _salutationService.GetAll();
            var c = _categoryService.GetAll();
            var p = _designationService.GetAll();
            var g = _gradeService.GetAll();
            var de = _departmentService.GetAll();
            var b = _branchService.GetAll();
            var di = _divisionService.GetAll();
            ViewBag.deptlist = new SelectList(de, "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.gradelist = new SelectList(g, "GRADE_ID", "GRADE_NAME");
            ViewBag.designationlist = new SelectList(p, "DESIGNATION_ID", "DESIGNATION_NAME");
            ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.countrylist = new SelectList(co, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.salutationlist = new SelectList(sa, "SALUTATION_ID", "SALUTATION_NAME");
            ViewBag.statelist = new SelectList(st, "STATE_ID", "STATE_NAME");
            ViewBag.branchlist = new SelectList(b, "BRANCH_ID", "BRANCH_NAME");
            ViewBag.divisionlist = new SelectList(di, "DIVISION_ID", "DIVISION_NAME");
            ViewBag.banklist = new SelectList(_bankService.GetAll(), "bank_id", "bank_name");
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(rEF_EMPLOYEE_VM);
        }

        // GET: Employee/Create
        [CustomAuthorizeAttribute("EMPM")]
        public ActionResult Create()
        {
            var co = _countryService.GetAll();
            var st = _stateService.GetAll();
            var sa = _salutationService.GetAll();
            var c = _categoryService.GetAll();
            var p = _designationService.GetAll();
            var g = _gradeService.GetAll();
            var de = _departmentService.GetAll();
            var b = _branchService.GetAll();
            var di = _divisionService.GetAll();
            ViewBag.deptlist = new SelectList(de, "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.gradelist = new SelectList(g, "GRADE_ID", "GRADE_NAME");
            ViewBag.designationlist = new SelectList(p, "DESIGNATION_ID", "DESIGNATION_NAME");
            ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.countrylist = new SelectList(co, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.salutationlist = new SelectList(sa, "SALUTATION_ID", "SALUTATION_NAME");
            ViewBag.statelist = new SelectList(st, "STATE_ID", "STATE_NAME");
            ViewBag.branchlist = new SelectList(b, "BRANCH_ID", "BRANCH_NAME");
            ViewBag.divisionlist = new SelectList(di, "DIVISION_ID", "DIVISION_NAME");
            ViewBag.banklist = new SelectList(_bankService.GetAll(), "bank_id", "bank_name");
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(REF_EMPLOYEE_VM rEF_EMPLOYEE_VM, FormCollection fc)
        {

            string ledgeraccounttype;

            ledgeraccounttype = fc["ledgeraccounttype"];
            rEF_EMPLOYEE_VM.ledgeraccounttype = ledgeraccounttype;
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = ledgeraccounttype.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }



            if (ModelState.IsValid)
            {
                var issaved = _employeeService.Add(rEF_EMPLOYEE_VM);
                if (issaved == true)
                    return RedirectToAction("Index");
            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            var co = _countryService.GetAll();
            var st = _stateService.GetAll();
            var sa = _salutationService.GetAll();
            var c = _categoryService.GetAll();
            var p = _designationService.GetAll();
            var g = _gradeService.GetAll();
            var de = _departmentService.GetAll();
            var b = _branchService.GetAll();
            var di = _divisionService.GetAll();
            ViewBag.deptlist = new SelectList(de, "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.gradelist = new SelectList(g, "GRADE_ID", "GRADE_NAME");
            ViewBag.designationlist = new SelectList(p, "DESIGNATION_ID", "DESIGNATION_NAME");
            ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.countrylist = new SelectList(co, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.salutationlist = new SelectList(sa, "SALUTATION_ID", "SALUTATION_NAME"); ;
            ViewBag.statelist = new SelectList(st, "STATE_ID", "STATE_NAME");
            ViewBag.branchlist = new SelectList(b, "BRANCH_ID", "BRANCH_NAME");
            ViewBag.divisionlist = new SelectList(di, "DIVISION_ID", "DIVISION_NAME");
            ViewBag.banklist = new SelectList(_bankService.GetAll(), "bank_id", "bank_name");
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(rEF_EMPLOYEE_VM);

        }

        // GET: Employee/Edit/5
        [CustomAuthorizeAttribute("EMPM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_EMPLOYEE_VM rEF_EMPLOYEE_VM = _employeeService.Get((int)id);
            if (rEF_EMPLOYEE_VM == null)
            {
                return HttpNotFound();
            }
            var co = _countryService.GetAll();
            var st = _stateService.GetAll();
            var sa = _salutationService.GetAll();
            var c = _categoryService.GetAll();
            var p = _designationService.GetAll();
            var g = _gradeService.GetAll();
            var de = _departmentService.GetAll();
            var b = _branchService.GetAll();
            var di = _divisionService.GetAll();
            ViewBag.deptlist = new SelectList(de, "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.gradelist = new SelectList(g, "GRADE_ID", "GRADE_NAME");
            ViewBag.designationlist = new SelectList(p, "DESIGNATION_ID", "DESIGNATION_NAME");
            ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.countrylist = new SelectList(co, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.salutationlist = new SelectList(sa, "SALUTATION_ID", "SALUTATION_NAME"); ;
            ViewBag.statelist = new SelectList(st, "STATE_ID", "STATE_NAME");
            ViewBag.branchlist = new SelectList(b, "BRANCH_ID", "BRANCH_NAME");
            ViewBag.divisionlist = new SelectList(di, "DIVISION_ID", "DIVISION_NAME");
            ViewBag.banklist = new SelectList(_bankService.GetAll(), "bank_id", "bank_name");
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(rEF_EMPLOYEE_VM);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_EMPLOYEE_VM rEF_EMPLOYEE_VM, FormCollection fc)
        {
            string ledgeraccounttype;

            ledgeraccounttype = fc["ledgeraccounttype"];
            rEF_EMPLOYEE_VM.ledgeraccounttype = ledgeraccounttype;
            string[] emptyStringArray = new string[0];
            try
            {
                emptyStringArray = ledgeraccounttype.Split(new string[] { "~" }, StringSplitOptions.None);
            }
            catch
            {

            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            if (ModelState.IsValid)
            {
                var issaved = _employeeService.Add(rEF_EMPLOYEE_VM);
                if (issaved == true)
                    return RedirectToAction("Index");
            }
            var co = _countryService.GetAll();
            var st = _stateService.GetAll();
            var sa = _salutationService.GetAll();
            var c = _categoryService.GetAll();
            var p = _designationService.GetAll();
            var g = _gradeService.GetAll();
            var de = _departmentService.GetAll();
            var b = _branchService.GetAll();
            var di = _divisionService.GetAll();
            ViewBag.deptlist = new SelectList(de, "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.plantlist = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.gradelist = new SelectList(g, "GRADE_ID", "GRADE_NAME");
            ViewBag.designationlist = new SelectList(p, "DESIGNATION_ID", "DESIGNATION_NAME");
            ViewBag.categorylist = new SelectList(c, "CATEGORY_ID", "CATEGORY_NAME");
            ViewBag.countrylist = new SelectList(co, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.salutationlist = new SelectList(sa, "SALUTATION_ID", "SALUTATION_NAME"); 
            ViewBag.statelist = new SelectList(st, "STATE_ID", "STATE_NAME");
            ViewBag.branchlist = new SelectList(b, "BRANCH_ID", "BRANCH_NAME");
            ViewBag.divisionlist = new SelectList(di, "DIVISION_ID", "DIVISION_NAME");
            ViewBag.banklist = new SelectList(_bankService.GetAll(), "bank_id", "bank_name");
            ViewBag.generalleder = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            return View(rEF_EMPLOYEE_VM);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REF_EMPLOYEE_VM rEF_EMPLOYEE_VM = _employeeService.Get((int)id);
            if (rEF_EMPLOYEE_VM == null)
            {
                return HttpNotFound();
            }
            return View(rEF_EMPLOYEE_VM);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _employeeService.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            REF_EMPLOYEE_VM rEF_EMPLOYEE_VM = _employeeService.Get((int)id);
            if (rEF_EMPLOYEE_VM == null)
            {
                return HttpNotFound();
            }
            return View(rEF_EMPLOYEE_VM);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _employeeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
