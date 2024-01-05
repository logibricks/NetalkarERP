using Sciffer.Erp.Data;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class JobWorkController : Controller
    {     
        private readonly IInjobWorkService _jobworkin;
        private readonly IGenericService _Generic;
        private readonly ICategoryService _Category;
        private readonly ICustomerService _cust;
        private readonly IStateService _state;
        private readonly IPlantService _plant;
        private readonly IBusinessUnitService _bussUnit;
        private readonly ICountryService _country;
        private readonly IItemService _item;
        private readonly IUOMService _uom;
        private readonly IStorageLocation _sloc;
        private readonly IBucketService _bucket;

        
        public JobWorkController(IInjobWorkService jobworkin, IGenericService Generic, ICategoryService Category, 
            ICustomerService Cust, ICountryService Country, IStateService State, IPlantService Plant, IBusinessUnitService BussUnit, 
            IItemService Item, IUOMService UOM, IStorageLocation Sloc, IBucketService Bucket)
        {
            _jobworkin = jobworkin;
            _Generic = Generic;
            _Category = Category;
            _cust = Cust;
            _country = Country;
            _state = State;
            _plant = Plant;
            _bussUnit = BussUnit;
            _item = Item;
            _uom = UOM;
            _sloc = Sloc;
            _bucket = Bucket;
        }

        // GET: jobwork_in
        [CustomAuthorizeAttribute("JWI")]
        public ActionResult Index()
        {
            ViewBag.num = TempData["data"];
            ViewBag.DataSource = _jobworkin.getall();
            return View();
        }

        // GET: CashAccount/Create
        [CustomAuthorizeAttribute("JWI")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(84), "document_numbring_id", "category");
            ViewBag.Customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.bussunitlist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.satelist = new SelectList(_state.GetStateList(), "state_id", "state_name");
            ViewBag.countrylist = new SelectList(_country.GetAll(), "Country_id", "country_name");            
            ViewBag.Item = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.UOMList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Sloclist = new SelectList(_sloc.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.BucketList = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(in_jobwork_in_VM vm)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                var duplicate = _jobworkin.DuplicateChalanNo(vm.customer_chalan_no);
                if (duplicate == "")
                {
                    issaved = _jobworkin.Add(vm);
                    if (issaved.Contains("Saved"))
                    {
                        TempData["data"] = issaved.Split('~')[1] + " Saved Successfully.";
                        return RedirectToAction("Index");
                    }
                    ViewBag.error = issaved;
                }
                else
                {
                    ViewBag.error = duplicate + " Chalan No already Created";                   
                }
            }
            
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(84), "document_numbring_id", "category");
            ViewBag.Customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.bussunitlist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.satelist = new SelectList(_state.GetStateList(), "state_id", "state_name");
            ViewBag.countrylist = new SelectList(_country.GetAll(), "Country_id", "country_name");
            ViewBag.Item = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.UOMList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Sloclist = new SelectList(_sloc.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.BucketList = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");           
            return View(vm);
        }


        // GET: CashAccount/Edit/5
        [CustomAuthorizeAttribute("JWI")]
        public ActionResult Edit(int id)
        {
            in_jobwork_in_VM jobworkin = _jobworkin.Get(id);

            if (jobworkin == null)
            {
                return HttpNotFound();
            }

            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(84), "document_numbring_id", "category");
            ViewBag.Customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.bussunitlist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.satelist = new SelectList(_state.GetStateList(), "state_id", "state_name");
            ViewBag.countrylist = new SelectList(_country.GetAll(), "Country_id", "country_name");
            ViewBag.Item = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.UOMList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Sloclist = new SelectList(_sloc.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.BucketList = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(84), "cancellation_reason_id", "cancellation_reason");
            ViewBag.Statuslist = new SelectList(_Generic.GetStatusList("JWI"), "status_id", "status_name");
            return View(jobworkin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(in_jobwork_in_VM vm)
        {
            var issaved = "";
            issaved = _jobworkin.Update(vm);
            if (issaved.Contains("Saved"))
            {
                TempData["data"] = issaved.Split('~')[1] + " Updated Successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.error = issaved;

            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(84), "document_numbring_id", "category");
            ViewBag.Customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.bussunitlist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.satelist = new SelectList(_state.GetStateList(), "state_id", "state_name");
            ViewBag.countrylist = new SelectList(_country.GetAll(), "Country_id", "country_name");
            ViewBag.Item = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.UOMList = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.Sloclist = new SelectList(_sloc.getstoragelist(), "storage_location_id", "storage_location_name");
            ViewBag.BucketList = new SelectList(_bucket.GetAll(), "bucket_id", "bucket_name");
            return View(vm);
        }

        public JsonResult GetCustomerList(int id)
        {
            var cust_list = _jobworkin.getcustomerlistplantwise(id);
            return Json(cust_list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int cancellation_reason_id)
        {
            var isValid = _jobworkin.Delete(id, cancellation_remarks, cancellation_reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
    }
}