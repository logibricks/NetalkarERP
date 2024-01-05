using System;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using Sciffer.Erp.Domain.Model;

using System.Web.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Sciffer.Erp.Web.Controllers
{
    public class TemporaryskillmatrixAccessController : Controller
    {

        private readonly IGenericService _Generic;
        private readonly ISkillMatrixService _skillmatrix;

        public TemporaryskillmatrixAccessController(IGenericService Generic, ISkillMatrixService skillmatrix)
        {
            _Generic = Generic;
            _skillmatrix = skillmatrix;
        }

        [CustomAuthorizeAttribute("TSKMATX")]
        // GET: SkillMatrix

        public ActionResult Index()
        {
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            ViewBag.GetShiftlist = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            ViewBag.DataSource = _skillmatrix.GetAll1("temp_operator_level_index", 0);
            
            return View();
        }

        public ActionResult InlineInsert(ref_temp_operator_level_mapping value)
        {

            var data1 = _skillmatrix.Add_temp_operator_level_mapping(value);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeAttribute("TSKMATX")]
        public ActionResult Create()
        {
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            ViewBag.GetShiftlist = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            return View();
        }

        // POST: QualityParameter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_temp_operator_level_mapping vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _skillmatrix.Add_temp_operator_level_mapping(vm);
                if (issaved.temp_operator_level_mapping_id != 0)
                {
                    TempData["data"] = "Saved";

                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }

            ViewBag.item_id = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.machine_id = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(vm);
        }

        [CustomAuthorizeAttribute("TSKMATX")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            temporary_skill_matrix_access_vm ref_temp_operator_level_mapping = _skillmatrix.GetByIdTemporarySkillMatrix(int.Parse(id));

            if (ref_temp_operator_level_mapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            ViewBag.GetShiftlist = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            return View(ref_temp_operator_level_mapping);
        }

        // POST: QualityParameter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_temp_operator_level_mapping vm)
        {
            if (ModelState.IsValid)
            {
                var issaved = _skillmatrix.Add_temp_operator_level_mapping(vm);
                if (issaved.temp_operator_level_mapping_id != 0)
                {
                    TempData["data"] = "Saved";
                    return RedirectToAction("Index", TempData["data"]);
                }
                ViewBag.error = "Something went wrong !";
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var er = error.ErrorMessage;
                }
            }
            ViewBag.operator_list = new SelectList(_Generic.GetOperatorList(), "user_id", "user_name");
            ViewBag.machine_list = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            ViewBag.level_list = new SelectList(_Generic.GetLevelList(), "level_id", "level_code");
            ViewBag.GetShiftlist = new SelectList(_Generic.GetShiftlist(), "shift_id", "shift_code");
            return View(vm);
        }

    }
}