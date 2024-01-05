using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineMasterService _machineService;
        private readonly IItemService _itemService;
        private readonly IUOMService _UOMService;
        private readonly IPlantService _plantService;
        private readonly IStorageLocation _storageLocationService;
        private readonly IGenericService _Generic;
        private readonly IBusinessUnitService _businessService;
        private readonly ICountryService _countryService;
        private readonly IPriorityService _priorityservice;
        private readonly ICostCenterService _costservice;
        private readonly ICurrencyService _currencyService;
        private readonly IVendorService _vendorService;
        private readonly IMachineCategoryService _machinecatService;
        private readonly IStatusService _statusService;


        public MachineController(IStatusService statusService, IMachineCategoryService machinecatService, IVendorService _vendor, ICurrencyService _currency, ICostCenterService _cost, IPriorityService _priority, ICountryService _country, IBusinessUnitService _business, IGenericService gen, IMachineMasterService MachineMasterService, IItemService ItemService, IUOMService UOMService, IGeneralLedgerService GeneralLedgerService, ICategoryService CategoryService, IPlantService PlantService, IStorageLocation StorageLocationService, IReasonDeterminationService ReasonDeterminationService)
        {
            _machineService = MachineMasterService;
            _itemService = ItemService;
            _UOMService = UOMService;
            _plantService = PlantService;
            _storageLocationService = StorageLocationService;
            _Generic = gen;
            _businessService = _business;
            _countryService = _country;
            _priorityservice = _priority;
            _costservice = _cost;
            _currencyService = _currency;
            _vendorService = _vendor;
            _machinecatService = machinecatService;
            _statusService = statusService;

        }

        // GET: Machine
        [CustomAuthorizeAttribute("MACM")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _machineService.getall();
            return View();
        }

        // GET: Machine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_machine_master_VM ref_machine_master_VM = _machineService.Get((int)id);
            if (ref_machine_master_VM == null)
            {
                return HttpNotFound();
            }

            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetPlantList(), "PLANT_ID", "PLANT_CODE");
            ViewBag.itemlist = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.unitlist = new SelectList(_UOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.countrylist = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.prioritylist = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.costlist = new SelectList(_costservice.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MAC"), "status_id", "status_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");

            return View(ref_machine_master_VM);
        }

        // GET: Machine/Create
        [CustomAuthorizeAttribute("MACM")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetPlantList(), "PLANT_ID", "PLANT_CODE");
            ViewBag.itemlist = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.unitlist = new SelectList(_UOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.countrylist = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.prioritylist = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.costlist = new SelectList(_costservice.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MAC"), "status_id", "status_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View();
        }

        // POST: Machine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_machine_master_VM ref_Machine_master_VM, FormCollection fc)
        {

            ViewBag.error = "";
            if (_Generic.CHKDupMachineCode(fc["machine_code"]) == false)
            {
                ViewBag.error = "machine code is allready exist";
            }
            else
            {

                string products;
                products = fc["productdetail"];
                string[] emptyStringArray = new string[0];

                try
                {
                    emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);

                }
                catch (Exception ex)
                {

                }


                List<ref_machine_details> ref_machine_details = new List<ref_machine_details>();

                for (int i = 0; i < emptyStringArray.Count() - 1; i++)
                {
                    ref_machine_details r_machine_details = new ref_machine_details();
                    r_machine_details.sr_no = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                    r_machine_details.item_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                    r_machine_details.initial_received_qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                    r_machine_details.suggested_qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[5]);
                    r_machine_details.current_qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[6]);
                    ref_machine_details.Add(r_machine_details);
                }
                ref_Machine_master_VM.ref_machine_details = ref_machine_details;

                if (ModelState.IsValid)
                {
                    var issaved = _machineService.Add(ref_Machine_master_VM);
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
            }



            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetPlantList(), "PLANT_ID", "PLANT_CODE");
            ViewBag.itemlist = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.unitlist = new SelectList(_UOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.countrylist = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.prioritylist = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.costlist = new SelectList(_costservice.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MAC"), "status_id", "status_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(ref_Machine_master_VM);

        }


        // GET: Machine/Edit/5
        [CustomAuthorizeAttribute("MACM")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_machine_master_VM ref_Machine_master_VM = _machineService.Get((int)id);
            if (ref_Machine_master_VM == null)
            {
                return HttpNotFound();
            }
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetPlantList(), "PLANT_ID", "PLANT_CODE");
            ViewBag.itemlist = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.unitlist = new SelectList(_UOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.countrylist = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.prioritylist = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.costlist = new SelectList(_costservice.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MAC"), "status_id", "status_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(ref_Machine_master_VM);
        }

        // POST: Machine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_machine_master_VM ref_Machine_master_VM, FormCollection fc)
        {

            string products, deleteids;
            products = fc["productdetail"];
            deleteids = fc["deleteids"];
            string[] emptyStringArray = new string[0];

            try
            {
                emptyStringArray = products.Split(new string[] { "~" }, StringSplitOptions.None);

            }
            catch (Exception e)
            {

            }

            List<ref_machine_details> ref_machine_details = new List<ref_machine_details>();

            for (int i = 0; i < emptyStringArray.Count() - 1; i++)
            {

                ref_machine_details r_machine_details = new ref_machine_details();
                var details_id = emptyStringArray[i].Split(new char[] { ',' })[0];
                if (details_id != "")
                {
                    r_machine_details.machine_detail_id = int.Parse(details_id);
                }
                r_machine_details.sr_no = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[1]);
                r_machine_details.item_id = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[2]);
                r_machine_details.initial_received_qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[4]);
                r_machine_details.suggested_qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[5]);
                r_machine_details.current_qty = int.Parse(emptyStringArray[i].Split(new char[] { ',' })[6]);
                ref_machine_details.Add(r_machine_details);
            }
            ref_Machine_master_VM.ref_machine_details = ref_machine_details;


            if (ModelState.IsValid)
            {
                var isedited = _machineService.Update(ref_Machine_master_VM);
                if (isedited == true)
                    return RedirectToAction("Index");
            }
            ViewBag.mcatlist = new SelectList(_Generic.GetMachineCategoryList(), "machine_category_id", "machine_category_code");
            ViewBag.plantlist = new SelectList(_plantService.GetPlantList(), "PLANT_ID", "PLANT_CODE");
            ViewBag.itemlist = new SelectList(_itemService.GetItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.unitlist = new SelectList(_UOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.countrylist = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.prioritylist = new SelectList(_Generic.GetPriorityByForm(4), "PRIORITY_ID", "PRIORITY_NAME");
            ViewBag.costlist = new SelectList(_costservice.GetCostCenter(), "cost_center_id", "cost_center_code");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.statuslist = new SelectList(_Generic.GetStatusList("MAC"), "status_id", "status_name");
            ViewBag.mplist = new SelectList(_Generic.GetMachineList(0), "machine_id", "machine_code");
            return View(ref_Machine_master_VM);
        }


        // GET: Machine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_machine_master_VM ref_Machine = _machineService.Get((int)id);
            if (ref_Machine == null)
            {
                return HttpNotFound();
            }
            return View(ref_Machine);
        }


        // POST: Machine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isdeleted = _machineService.Delete(id);
            if (isdeleted == true)
            {
                return RedirectToAction("Index");
            }
            ref_machine_master_VM ref_Machine = _machineService.Get((int)id);
            if (ref_Machine == null)
            {
                return HttpNotFound();
            }
            return View(ref_Machine);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _machineService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
