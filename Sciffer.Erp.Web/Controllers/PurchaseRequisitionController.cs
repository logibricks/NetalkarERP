using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;
using System.Data;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System;

namespace Sciffer.Erp.Web.Controllers
{
    public class PurchaseRequisitionController : Controller
    {

        private readonly ICurrencyService _currencyService;
        private readonly IPurRequisitionService _pur;
        private readonly IPlantService _plant;
        private readonly IFreightTermsService _freight;
        private readonly IBusinessUnitService _business;
        private readonly ICategoryService _category;
        private readonly ISourceService _source;
        private readonly IVendorService _vendor;
        private readonly IItemService _item;
        private readonly IUOMService _uom;
        private readonly IGenericService _Generic;
        private readonly IStorageLocation _storage;
        private readonly IItemTypeService _itemTypeService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly ILoginService _login;

        public PurchaseRequisitionController(IStorageLocation StorageLocation, IGenericService gen, IPurRequisitionService pur, ICurrencyService curr, IPlantService pla, IFreightTermsService freight, IBusinessUnitService business,
            ICategoryService cat, ISourceService src, IVendorService vendor, IItemService item, IUOMService uom, IItemTypeService ItemTypeService, ILoginService login)
        {
            _pur = pur;
            _currencyService = curr;
            _plant = pla;
            _freight = freight;
            _business = business;
            _category = cat;
            _source = src;
            _vendor = vendor;
            _item = item;
            _uom = uom;
            _Generic = gen;
            _itemTypeService = ItemTypeService;
            _storage = StorageLocation;
            _login = login;

        }
        // GET: PurchaseRequisition
        [CustomAuthorizeAttribute("PRCRQ")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.datasource = _pur.GetAll();
            var user = int.Parse(Session["User_Id"].ToString());

            var checkoperator4 = _login.CheckOperatorLogin(user, "STO_EXEC");
            var checkoperator5 = _login.CheckOperatorLogin(user, "PUR_EXEC");

            if (checkoperator4 == true || checkoperator5 == true)
            {
                var open_cnt12 = _login.GetPurchaseRequisitionAllRejectAndApprovedcount(user);
                Session["open_count12"] = open_cnt12;
                var open_cnt123 = _login.GetPurchaseRequisitionRejectedcount(user);
                Session["open_cnt123"] = open_cnt123;
            }
            return View();
        }
        public JsonResult GetItemStocks(string code)
        {
            var id = _Generic.GetItemId(code);
            var item = _pur.GetItemStock(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        // GET: PurchaseRequisition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pur_requisition = _pur.Get((int)id);
            if (pur_requisition == null)
            {
                return HttpNotFound();
            }
            ViewBag.item = _item.GetItemList();
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.locationlist = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(71), "document_numbring_id", "category");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freightlist = new SelectList(_freight.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.plantlist = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sourcelist = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View(pur_requisition);
        }

        // GET: PurchaseRequisition/Create
        [CustomAuthorizeAttribute("PRCRQ")]
        public ActionResult Create()
        {
            ViewBag.item = _item.GetItemList();
            ViewBag.datasource = _vendor.GetVendorDetail();
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.locationlist = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            ViewBag.error = "";
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(71), "document_numbring_id", "category");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freightlist = new SelectList(_freight.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            // ViewBag.plantlist = new SelectList(_plant.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sourcelist = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
           
            return View();
        }

        // POST: PurchaseRequisition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(pur_requisition_vm pur_requisition)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _pur.Add(pur_requisition);
                if (issaved.Contains("Saved"))
                {
                    TempData["doc_num"] = issaved.Split('~')[1];
                    return RedirectToAction("Index");
                }
            }
            //foreach (ModelState modelState in ViewData.ModelState.Values)
            //{
            //    foreach (ModelError error in modelState.Errors)
            //    {
            //        var er = error.ErrorMessage;
            //    }
            //}
            //ViewBag.error = "Something went wrong !";
            ViewBag.item = _item.GetItemList();
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.locationlist = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            // ViewBag.businesslist = new SelectList(_business.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(71), "document_numbring_id", "category");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freightlist = new SelectList(_freight.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sourcelist = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PRCRQ"), "status_id", "status_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View(pur_requisition);
        }

        // GET: PurchaseRequisition/Edit/5
        [CustomAuthorizeAttribute("PRCRQ")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pur_requisition = _pur.Get((int)id);
            if (pur_requisition == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.locationlist = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            // ViewBag.businesslist = new SelectList(_business.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(71), "document_numbring_id", "category");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freightlist = new SelectList(_freight.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sourcelist = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PRCRQ"), "status_id", "status_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.error = "";
            return View(pur_requisition);
        }

        // POST: PurchaseRequisition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pur_requisition_vm pur_requisition)
        {
            var isedit = "";
            
                isedit = _pur.Add(pur_requisition);
                if (isedit.Contains("Saved"))
                {
                    TempData["doc_num"] = isedit.Split('~')[1];
                    return RedirectToAction("Index");
                }
           
            ViewBag.error = "Something went wrong !";
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.locationlist = new SelectList(_Generic.GetStorageLocationList(0), "storage_location_id", "storage_location_name");
            //ViewBag.businesslist = new SelectList(_business.GetAll(), "BUSINESS_UNIT_ID", "BUSINESS_UNIT_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryList(71), "document_numbring_id", "category");
            ViewBag.currencylist = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freightlist = new SelectList(_freight.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.sourcelist = new SelectList(_source.GetAll(), "SOURCE_ID", "SOURCE_NAME");
            ViewBag.vendorlist = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "item_id", "item_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            return View(pur_requisition);
        }

        // GET: PurchaseRequisition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pur_requisition = _pur.Get((int)id);
            if (pur_requisition == null)
            {
                return HttpNotFound();
            }
            return View(pur_requisition);
        }

        // POST: PurchaseRequisition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isdelete = _pur.Delete(id);
            if (isdelete)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pur.Dispose();
            }
            base.Dispose(disposing);
        }
        public CrystalReportPdfResult Pdf(int id, int type)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                decimal sum = 0;
                var pur_req = _pur.GetPurRequisitionReport(id);
                var pur_req_detail = _pur.GetPurRequisitionDetailReport(id);
                DataSet ds = new DataSet("pr");
                var po = new List<pur_req_report_vm>();
                po.Add(pur_req);
                var dt1 = _Generic.ToDataTable(po);
                var dt2 = _Generic.ToDataTable(pur_req_detail);
                dt1.TableName = "PurReq";
                dt2.TableName = "PurReqdetail";
                foreach (var a in pur_req_detail)
                {
                    sum = sum + a.Qty;
                }
                dt1.Rows[0]["total"] = sum;
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);               
                if (type == 1)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseRequsitionreport.rpt")));
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseRequsitionreport1.rpt")));
                }

                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = "";
                if (type == 1)
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseRequsitionreport.rpt");
                }
                else
                {
                    reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseRequsitionreport1.rpt");
                }

                return new CrystalReportPdfResult(reportPath, rd);
            }
            catch (Exception ex)
            {
                //--------------Log4Net
                log4net.GlobalContext.Properties["user"] = 0;
                log.Error("Exception Occured ", ex);
                if (ex.Message.ToString().Contains("inner exception"))
                    log4net.GlobalContext.Properties["Inner_Exception"] = ex.InnerException.ToString();
                return null;
            }
            finally
            {
                rd.Close();
                rd.Clone();
                rd.Dispose();
                rd = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        [CustomAuthorizeAttribute("POAP")]
        public ActionResult ApprovedPurchaseReq()
        {
            var user =int.Parse(Session["User_Id"].ToString());

            var checkoperator3 = _login.CheckOperatorLogin(user, "TOP_MGMT");
            if (checkoperator3 == true)
            {
                //var open_cnt1req = _login.GetPurchaseRequisitionAllCount(user);
                //Session["open_count1"] = open_cnt1req;
                var preq1 = _pur.GetPendigApprovedList().Count;
                Session["preq_count1"] = preq1;
            }
            //int create_user = int.Parse(Session["User_Id"].ToString());
            //var open_cnt1 = _login.GetPurchaseRequisitionAllCount(create_user);
            //Session["open_count1"] = open_cnt1;

            //var open_cnt12 = _login.GetPurchaseRequisitionAllRejectAndApprovedcount(create_user);
            //Session["open_count12"] = open_cnt12;

            var checkoperator4 = _login.CheckOperatorLogin(user, "STO_EXEC");
            var checkoperator5 = _login.CheckOperatorLogin(user, "PUR_EXEC");

            if (checkoperator4 == true || checkoperator5 == true)
            {
                var open_cnt12 = _login.GetPurchaseRequisitionAllRejectAndApprovedcount(user);
                Session["open_count12"] = open_cnt12;
                var open_cnt123 = _login.GetPurchaseRequisitionRejectedcount(user);
                Session["open_cnt123"] = open_cnt123;
            }

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Pending", Value = "0" });
            items.Add(new SelectListItem { Text = "Approved", Value = "1" });
            items.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            List<pur_requisition_vm> po = new List<pur_requisition_vm>();
            foreach (var i in items)
            {
                pur_requisition_vm pp = new pur_requisition_vm();
                pp.approval_status = int.Parse(i.Value);
                pp.approval_status_name = i.Text;
                po.Add(pp);
            }

            ViewBag.DataSource = _pur.GetPendigApprovedList();
            var preq= _pur.GetPendigApprovedList().Count;
            Session["preq_count1"] = preq;
            ViewBag.status_list = po;
            return View();
        }
        [HttpPost]
        public ActionResult ChangeApprovedStatus(pur_requisition_vm value)
        {
            var change = _pur.ChangeApprovedStatus(value);
            return RedirectToAction("Index");
        }
        public ActionResult GetApprovedStatus(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Pending", Value = "0" });
            items.Add(new SelectListItem { Text = "Approved", Value = "1" });
            items.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            List<pur_requisition_vm> po = new List<pur_requisition_vm>();
            foreach (var i in items)
            {
                pur_requisition_vm pp = new pur_requisition_vm();
                pp.approval_status = int.Parse(i.Value);
                pp.approval_status_name = i.Text;
                po.Add(pp);
            }
            var data = _pur.GetPendigApprovedList();
            ViewBag.datasource = data;
            ViewBag.status_list = po;
            return PartialView("Partial_ApprovalStatus", data);
        }
        public ActionResult CloseConfirmed(int id, string remarks)
        {
            var isValid = _pur.Close(id, remarks);
            return Json(isValid);
        }

    
        public JsonResult PurchaseRequisitionupdatestatusseen()
        {
            var result =   _pur.PurchaseRequisitionupdatestatusseen();
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult; 
        }
    }
}
