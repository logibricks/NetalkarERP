using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Sciffer.Erp.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        private readonly IStatusService _statusService;
        private readonly ICategoryService _categoryService;
        private readonly IVendorService _vendorService;
        private readonly ICurrencyService _currencyService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly IFreightTermsService _freightTermsService;
        private readonly IStateService _stateService;
        private readonly IPaymentTermsService _paymentTermsService;
        private readonly IPaymentCycleService _paymentCycleService;
        private readonly IPlantService _plantService;
        private readonly IItemService _itemService;
        private readonly IAccountAssignmentService _accountAssignmentService;
        private readonly IUOMService _uOMService;
        private readonly ITaxService _taxService;
        private readonly IFormService _formService;
        private readonly ICountryService _countryService;
        private readonly IPaymentCycleTypeService _paymentCycleTypeService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IGenericService _Generic;
        private readonly IPurRequisitionService _purRequisition;
        private readonly IItemTypeService _itemTypeService;
        private readonly ISACService _sACService;
        private readonly ILoginService _login;
        public PurchaseOrderController(IGenericService gen, IPurchaseOrderService PurchaseOrderService, IPaymentCycleTypeService PaymentCycleTypeService,
            ICountryService CountryService, IFormService FormService, ITaxService TaxService, IUOMService IUOMService, IAccountAssignmentService AccountAssignmentService,
            IPlantService PlantService, IItemService IItemService, IPaymentCycleService PaymentCycleService, IPaymentTermsService PaymentTermsService,
            IStateService StateService, IFreightTermsService FreightTermsService, IBusinessUnitService BusinessUnitService, ICurrencyService CurrencyService,
            IVendorService VendorService, IStatusService StatusService, ICategoryService CategoryService, IPurRequisitionService purRequisition
            , IItemTypeService ItemTypeService, ISACService SACService, ILoginService login)
        {
            _itemTypeService = ItemTypeService;
            _statusService = StatusService;
            _categoryService = CategoryService;
            _vendorService = VendorService;
            _currencyService = CurrencyService;
            _businessUnitService = BusinessUnitService;
            _freightTermsService = FreightTermsService;
            _stateService = StateService;
            _paymentTermsService = PaymentTermsService;
            _paymentCycleService = PaymentCycleService;
            _itemService = IItemService;
            _plantService = PlantService;
            _accountAssignmentService = AccountAssignmentService;
            _uOMService = IUOMService;
            _taxService = TaxService;
            _formService = FormService;
            _countryService = CountryService;
            _paymentCycleTypeService = PaymentCycleTypeService;
            _purchaseOrderService = PurchaseOrderService;
            _Generic = gen;
            _purRequisition = purRequisition;
            _sACService = SACService;
            _login = login;
        }
        // GET: PurchaseOrder
        [CustomAuthorizeAttribute("PRCOD")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _purchaseOrderService.getall();
            var user = int.Parse(Session["User_Id"].ToString());

            var checkoperator1 = _login.CheckOperatorLogin(user, "IT_ADMIN");
            var checkoperator5 = _login.CheckOperatorLogin(user, "PUR_EXEC");
            if (checkoperator5 == true || checkoperator1 == true)
            {
                var open_cnt12 = _login.GetPurchaseOrderAllRejectAndApprovedcount(user);
                Session["open_count21"] = open_cnt12;
                var open_cnt123 = _login.GetPurchaseOrderAllRejectdcount(user);
                Session["open_count213"] = open_cnt123;
            }
            return View();
        }


        // GET: PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pur_po = _purchaseOrderService.Get(id);
            if (pur_po == null)
            {
                return HttpNotFound();
            }
            ViewBag.datasource = _vendorService.GetVendorDetail();
            ViewBag.item = _itemService.GetItemList();
            ViewBag.vendor_detail = pur_po.vendor_code + "-" + pur_po.vendor_name;
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(10), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_plantService.GetAll(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_vendorService.GetAll(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_itemService.GetAll(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_taxService.GetAll(), "TAX_ID", "TAX_CODE");
            ViewBag.assignment_list = new SelectList(_accountAssignmentService.GetAll(), "account_assignment_id", "account_assignment_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(72), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_po);
        }

        // GET: PurchaseOrder/Create
        [CustomAuthorizeAttribute("PRCOD")]
        public ActionResult Create()
        {
            ViewBag.error = "";
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(72), "document_numbring_id", "category");
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.assignment_list = new SelectList(_accountAssignmentService.GetAll(), "account_assignment_id", "account_assignment_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.requisition_list = new SelectList(_purRequisition.GetPurRequistion(), "pur_requisition_id", "pr_date");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(72), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View();
        }

        // POST: PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(pur_poVM vm)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _purchaseOrderService.Add(vm);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid.Split('~')[1] + " Saved successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = isValid;
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(72), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.requisition_list = new SelectList(_purRequisition.GetPurRequistion(), "pur_requisition_id", "pr_date");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(72), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(vm);
        }

        // GET: PurchaseOrder/Edit/5
        [CustomAuthorizeAttribute("PRCOD")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pur_po = _purchaseOrderService.Get(id);
            if (pur_po == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.vendor_detail = pur_po.vendor_code + "-" + pur_po.vendor_name;
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(72), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(72), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_po);
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(pur_poVM pur_po)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _purchaseOrderService.Add(pur_po);
                if (isValid.Contains("Saved"))
                {
                    TempData["doc_num"] = isValid.Split('~')[1] + " Updated successfully!";
                    return RedirectToAction("Index");
                }

            }
            ViewBag.error = isValid;
            ViewBag.employee_list = new SelectList(_Generic.GetEmployeeList(0), "employee_id", "employee_code");
            ViewBag.business_list = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.category_list = new SelectList(_Generic.GetCategoryList(72), "document_numbring_id", "category");
            ViewBag.currency_list = new SelectList(_currencyService.GetAll(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.freight_list = new SelectList(_freightTermsService.GetAll(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.payment_cycle_list = new SelectList(_paymentCycleService.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.payment_terms_list = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.plant_list = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.state_list = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.vendor_list = new SelectList(_Generic.GetVendorList(), "VENDOR_ID", "VENDOR_NAME");
            ViewBag.status_list = new SelectList(_statusService.GetAll(), "status_id", "status_name");
            ViewBag.form_list = new SelectList(_formService.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.country_list = new SelectList(_countryService.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.payment_cycle_type_list = new SelectList(_paymentCycleTypeService.GetAll(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.item_list = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.uom_list = new SelectList(_uOMService.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.tax_list = new SelectList(_Generic.GetTaxList(), "tax_id", "tax_name");
            ViewBag.status_list = new SelectList(_Generic.GetStatusList("PO"), "status_id", "status_name");
            ViewBag.state_list = new SelectList(_stateService.GetStateList().Select(a => new { state_id = a.state_id, state_name = a.state_name + "/" + a.state_ut_code }), "state_id", "state_name");
            ViewBag.gst_list = new SelectList(_Generic.GetGSTCustomerVendorType(), "gst_customer_type_id", "gst_customer_type_name");
            ViewBag.hsncodelist = new SelectList(_Generic.GetHSNList(), "hsn_code_id", "hsn_code");
            ViewBag.item_type = new SelectList(_itemTypeService.GetAll(), "item_type_id", "item_type_name");
            ViewBag.sac_list = new SelectList(_sACService.GetAll().Select(a => new { sac_id = a.sac_id, sac_code = a.sac_code + "/" + a.sac_description }), "sac_id", "sac_code");
            ViewBag.cancellationreasonlist = new SelectList(_Generic.GetCANCELLATIONList(72), "cancellation_reason_id", "cancellation_reason");
            ViewBag.user_list = new SelectList(_Generic.GetUserList(), "user_id", "user_name");
            return View(pur_po);
        }

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pur_poVM pur_po = _purchaseOrderService.Get(id);
            if (pur_po == null)
            {
                return HttpNotFound();
            }
            return View(pur_po);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var isValid = _purchaseOrderService.Delete(id);
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _purchaseOrderService.Dispose();
            }
            base.Dispose(disposing);
        }
        public CrystalReportPdfResult Pdf(int id, int type)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var pur_po = _purchaseOrderService.GetPOForReport(id);
                var pur_po_detail = _purchaseOrderService.GetPOProductForReport(id, type == 3 ? "getpodetailforreport3" : "getpodetailforreport12");
                var po_detail_delivery = _purchaseOrderService.GetPODeliverydetail(id, "get_po_detail_delivery");
                var exp = "";
                double amt = 0;
                foreach (var i in pur_po_detail)
                {
                    exp = exp + i.tax_code_id + "~" + i.assesable_value + "~" + i.purchase_value + ",";
                    amt = amt + double.Parse(i.purchase_value.ToString());
                }
                var tax_detail = _Generic.GetTaxCalculation("calculatetax", exp.Remove(exp.Length - 1), amt, pur_po.po_date, 0);
                var po_detail_id = 0;
                DataSet ds = new DataSet("po");
                var po = new List<pur_po_report_vm>();
                po.Add(pur_po);
                var dt1 = _Generic.ToDataTable(po);
                var dt2 = _Generic.ToDataTable(pur_po_detail);
                var dt3 = _Generic.ToDataTable(tax_detail);
                var dt4 = _Generic.ToDataTable(po_detail_delivery);
                dt1.TableName = "pur_po";
                dt2.TableName = "pur_po_detail";
                dt4.TableName = "po_detail_delivery";
                dt3.TableName = "po_tax_detail";
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt4);
                ds.Tables.Add(dt2);
                dt3.Columns.Add("id", typeof(System.Int32));
                dt1.Columns.Add("excise_amount", typeof(double));
                //   dt1.Columns.Add("vendor_contact_person", typeof(string));
                foreach (var so in pur_po_detail)
                {
                    po_detail_id = so.po_detail_id;
                }
                foreach (DataRow row in dt3.Rows)
                {
                    //need to set value to NewColumn column
                    row["id"] = po_detail_id;   // or set it to some other value
                                                // break;
                }

                DataRow[] drPaytable = dt3.Select("tax_name like '%Excise%'");
                double sum = 0;
                for (int i = 0; i < drPaytable.Length; i++)
                {
                    sum = sum + double.Parse(drPaytable[0].ItemArray[1].ToString());
                }
                dt1.Rows[0]["net_value"] = pur_po.net_value.ToString("F");
                dt1.Rows[0]["excise_amount"] = sum;
                dt1.Rows[0]["vendor_contact_person"] = _vendorService.GetContactPerson((int)pur_po.vendor_id);
                if (dt3.Rows.Count == 3)
                {
                    dt3.Rows[2]["tax_value"] = (dt3.Rows[2]["tax_value"]).ToString();
                }
                else
                {
                    dt3.Rows[3]["tax_value"] = (dt3.Rows[3]["tax_value"]).ToString();
                }


                ds.Tables.Add(dt3);

                if (pur_po.item_service_id == 1)
                {
                    if (pur_po.COUNTRY_NAME == pur_po.company_country)
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrder.rpt"))); // inventory
                    }
                    else
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrderForForeign.rpt")));
                    }
                }
                else if (pur_po.item_service_id == 2)
                {
                    if (pur_po.with_without_service_id == 1)
                    {
                        if (pur_po.COUNTRY_NAME == pur_po.company_country)
                        {
                            rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrder - Copy.rpt")));//service
                        }
                        else
                        {
                            rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrderForForeign.rpt")));
                        }
                    }
                    else
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrder1.rpt")));
                    }

                }
                else
                {
                    if (pur_po.COUNTRY_NAME == pur_po.company_country)
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrderForCapital.rpt"))); //assest
                    }
                    else
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/Reports/PurchaseOrderForForeignCapital.rpt")));
                    }
                }
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = "";
                if (type != 2)
                {
                    rd.SetParameterValue("P1", "PURCHASE ORDER");
                    rd.SetParameterValue("P2", "PO Number");
                    rd.SetParameterValue("P3", "PO Date");
                }
                else
                {
                    rd.SetParameterValue("P1", "WORK ORDER");
                    rd.SetParameterValue("P2", "WO Number");
                    rd.SetParameterValue("P3", "WO Date");
                }
                if (pur_po.item_service_id == 1)
                {
                    if (pur_po.COUNTRY_NAME == pur_po.company_country)
                    {
                        reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrder.rpt");
                    }
                    else
                    {
                        reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrderForForeign.rpt");
                    }
                }
                else if (pur_po.item_service_id == 2)
                {
                    if (pur_po.with_without_service_id == 1)
                    {
                        if (pur_po.COUNTRY_NAME == pur_po.company_country)
                        {
                            reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrder - Copy.rpt");
                        }
                        else
                        {
                            reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrderForForeign.rpt");
                        }
                    }

                    else
                    {
                        reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrder1.rpt");
                    }

                }
                else
                {
                    if (pur_po.COUNTRY_NAME == pur_po.company_country)
                    {
                        reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrderForCapital.rpt");

                    }
                    else
                    {
                        reportPath = Path.Combine(Server.MapPath("~/Reports"), "PurchaseOrderForForeignCapital.rpt");
                    }
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
        public ActionResult GetPurRquisitionList(string entity, int id)
        {
            var vm = _purRequisition.GetPurRquisitionDetails(entity, id);
            var list = JsonConvert.SerializeObject(vm,
             Formatting.None,
                      new JsonSerializerSettings()
                      {
                          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                      });

            return Content(list, "application/json");
        }
        public ActionResult TaxList()
        {
            var tax = _taxService.GetAll();
            SelectList vm = new SelectList(tax, "tax_id", "tax_name_code");
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorizeAttribute("POAP")]
        public ActionResult ApprovedPurchaseOrder()
        {
            int create_user = int.Parse(Session["User_Id"].ToString());

            //var open_cnt2 = _login.GetApprovedPurchaseOrderCount(create_user);
            //Session["open_count2"] = open_cnt2;
            var purcount = _purchaseOrderService.GetPending__Slab_PO_ApprovalList().Count();
            Session["open_count2"] = purcount;
            var user = int.Parse(Session["User_Id"].ToString());

            var checkoperator4 = _login.CheckOperatorLogin(user, "STO_EXEC");
            var checkoperator5 = _login.CheckOperatorLogin(user, "PUR_EXEC");

            if (checkoperator4 == true || checkoperator5 == true)
            {
                var open_cnt12 = _login.GetPurchaseOrderAllRejectAndApprovedcount(create_user);
                Session["open_count21"] = open_cnt12;
                var open_cnt123 = _login.GetPurchaseOrderAllRejectdcount(create_user);
                Session["open_count213"] = open_cnt123;
            }


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Pending", Value = "0" });
            items.Add(new SelectListItem { Text = "Approved", Value = "1" });
            items.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            List<pur_poVM> po = new List<pur_poVM>();
            foreach (var i in items)
            {
                pur_poVM pp = new pur_poVM();
                pp.approval_status = int.Parse(i.Value);
                pp.approval_status_name = i.Text;
                po.Add(pp);
            }

            ViewBag.DataSource = _purchaseOrderService.GetPending__Slab_PO_ApprovalList();
            var purcount1 = _purchaseOrderService.GetPending__Slab_PO_ApprovalList().Count();
            Session["open_count2"] = purcount1;
            ViewBag.status_list = po;
            return View();
        }
        [HttpPost]
        public ActionResult ChangeApprovedStatus(pur_poVM value)
        {
            var change = _purchaseOrderService.ChangeApprovedStatus(value);
            return RedirectToAction("Index");
        }
        public ActionResult GetApprovedStatus(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Pending", Value = "0" });
            items.Add(new SelectListItem { Text = "Approved", Value = "1" });
            items.Add(new SelectListItem { Text = "Rejected", Value = "2" });
            List<pur_poVM> po = new List<pur_poVM>();
            foreach (var i in items)
            {
                pur_poVM pp = new pur_poVM();
                pp.approval_status = int.Parse(i.Value);
                pp.approval_status_name = i.Text;
                po.Add(pp);
            }
            var data = _purchaseOrderService.GetPendigApprovedList(id);
            ViewBag.datasource = data;
            ViewBag.status_list = po;
            return PartialView("Partial_ApprovalStatus", data);
        }
        public ActionResult DeleteConfirmed(int id, string cancellation_remarks, int reason_id)
        {
            var isValid = _purchaseOrderService.Delete(id, cancellation_remarks, reason_id);
            if (isValid.Contains("Cancelled"))
            {
                return Json(isValid);
            }
            return Json(isValid);
        }
        public ActionResult CloseConfirmed(int id, string remarks)
        {
            var isValid = _purchaseOrderService.Close(id, remarks);
            return Json(isValid);
        }
        public JsonResult GetVendorItemPrice(int vendor_id, int item_id)
        {
            var list = _purchaseOrderService.GetVendorItemPrice(vendor_id, item_id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPR(int plant_id)
        {
            var pr = _purRequisition.GetPRDetails(plant_id, "getpr").Select(a => new { a.pur_requisition_id, a.pur_requisition_number }).ToList();
            var vendor = _purRequisition.GetPRDetails(plant_id, "getvendor").Select(a => new { a.vendor_id, a.vendor_name }).ToList();
            var item = _purRequisition.GetPRDetails(plant_id, "getitem").Select(a => new { a.item_id, a.item_code }).ToList();

            List<object> obj = new List<object>();
            obj.Add(pr);
            obj.Add(vendor);
            obj.Add(item);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PurchaseOrderupdatestatusseen()
        {
            var result = _purchaseOrderService.PurchaseOrderupdatestatusseen();
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult GetPoHistory(string entity, int item_id)
        {
            var paymentService = _purchaseOrderService.GetPoHistory(entity, item_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }


    }
}
