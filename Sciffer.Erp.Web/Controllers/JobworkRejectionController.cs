using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Newtonsoft.Json;
using Sciffer.Erp.Web.CustomFilters;
namespace Sciffer.Erp.Web.Controllers
{
    public class JobworkRejectionController : Controller
    {
        private readonly IGenericService _Generic;
        private readonly ISalesInvoiceService _SIService;
        private readonly IUOMService _uom;
        private readonly IPaymentCycleService _paymentCycle;
        private readonly IStateService _stateService;
        private readonly ITerritoryService _territory;
        private readonly IFormService _form;
        private readonly ICountryService _country;
        private readonly IStorageLocation _sloc;
        private readonly IJobWorkRejectionService _jobworkRejection;
        private readonly IReasonDeterminationService _reason;
        private readonly IItemService _item;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //To Get Class Name
        public JobworkRejectionController(IGenericService Generic, ISalesInvoiceService SIService, IUOMService uom,
            IPaymentCycleService paymentCycle, IStateService stateService, ITerritoryService territory, IFormService form,
            ICountryService country, IStorageLocation sloc, IJobWorkRejectionService jobworkRejection, IReasonDeterminationService reason, IItemService item)
        {
            _Generic = Generic;
            _SIService = SIService;
            _uom = uom;
            _paymentCycle = paymentCycle;
            _stateService = stateService;
            _territory = territory;
            _form = form;
            _country = country;
            _sloc = sloc;
            _jobworkRejection = jobworkRejection;
            _reason = reason;
            _item = item;
        }
        // GET: JobworkRejection
        public ActionResult Index()
        {
            ViewBag.DataSource = _jobworkRejection.GetAll();
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("JOBREJ"), "document_numbring_id", "category");//223
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.billingstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.formlist = new SelectList(_form.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloclist = new SelectList(_sloc.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.reasonlist = new SelectList(_reason.GetReasonByCode("REJECTION_RECEIPT"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(jobwork_rejection_VM job)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _jobworkRejection.Add(job);
                if (isValid.Contains("Saved"))
                {
                    var sp = isValid.Split('~')[1];
                    TempData["doc_num"] = sp;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = isValid;
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("JOBREJ"), "document_numbring_id", "category");//223
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.billingstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.formlist = new SelectList(_form.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloclist = new SelectList(_sloc.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.reasonlist = new SelectList(_reason.GetReasonByCode("REJECTION_RECEIPT"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }

        public ActionResult GetState(int id)
        {
            var data = _jobworkRejection.GetState(id);
            return Json(data);
        }

        public ActionResult GetPlacetodelivaryState(int id)
        {
            var data = _jobworkRejection.GetdeliveryState(id);
            return Json(data);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobrejection = _jobworkRejection.Get((int)id);
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("JOBREJ"), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(_paymentCycle.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.billinglist = new SelectList(_country.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(_country.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(_form.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloclist = new SelectList(_sloc.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.reasonlist = new SelectList(_reason.GetReasonByCode("REJECTION_RECEIPT"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View(jobrejection);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobrejection = _jobworkRejection.Get((int)id);
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.Currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("JOBREJ"), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(_paymentCycle.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.billinglist = new SelectList(_country.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(_country.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(_form.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.sloclist = new SelectList(_sloc.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.reasonlist = new SelectList(_reason.GetReasonByCode("REJECTION_RECEIPT"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View(jobrejection);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(jobwork_rejection_VM job)
        {
            var isValid = "";
            if (ModelState.IsValid)
            {
                isValid = _jobworkRejection.Add(job);
                if (isValid != "Error")
                {
                    TempData["doc_num"] = isValid;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.error = isValid;
            ViewBag.unitlist = new SelectList(_uom.GetAll(), "UOM_ID", "UOM_NAME");
            ViewBag.itemlist = new SelectList(_item.GetTagManagedItemList(), "ITEM_ID", "ITEM_CODE");
            ViewBag.Currencylist = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.categorylist = new SelectList(_Generic.GetCategoryListByModule("JOBREJ"), "document_numbring_id", "category");
            ViewBag.plantlist = new SelectList(_Generic.GetPlantList(), "PLANT_ID", "PLANT_NAME");
            ViewBag.freightlist = new SelectList(_Generic.GetFreightTermsList(), "FREIGHT_TERMS_ID", "FREIGHT_TERMS_NAME");
            ViewBag.businesslist = new SelectList(_Generic.GetBusinessUnitList(), "business_unit_id", "business_unit_name");
            ViewBag.paymentcyclelist = new SelectList(_paymentCycle.GetAll(), "PAYMENT_CYCLE_ID", "PAYMENT_CYCLE_NAME");
            ViewBag.paymentcycletype = new SelectList(_Generic.GetPaymentTypeList(), "PAYMENT_CYCLE_TYPE_ID", "PAYMENT_CYCLE_TYPE_NAME");
            ViewBag.paymenttermslist = new SelectList(_Generic.GetPaymentTermsList(), "payment_terms_id", "payment_terms_description");
            ViewBag.billingstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.corrstatelist = new SelectList(_stateService.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.territorylist = new SelectList(_territory.GetAll(), "TERRITORY_ID", "TERRITORY_NAME");
            ViewBag.salesexlist = new SelectList(_Generic.GetSalesRM(), "employee_id", "employee_name");
            ViewBag.billinglist = new SelectList(_country.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.corlist = new SelectList(_country.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.formlist = new SelectList(_form.GetAll(), "FORM_ID", "FORM_NAME");
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.sloclist = new SelectList(_sloc.GetAll(), "storage_location_id", "storage_location_name");
            ViewBag.reasonlist = new SelectList(_reason.GetReasonByCode("REJECTION_RECEIPT"), "REASON_DETERMINATION_ID", "REASON_DETERMINATION_NAME");
            return View();
        }
        public CrystalReportPdfResult Pdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                //var mat_in = _materialinservice.Get((int)id);
                decimal qty = 0;
                decimal? total = 0;

                // var inv_stock_detail = _inventoryStockService.GetItemForStockEdit((int) id);
                var jobwork_rejection = _jobworkRejection.jobworkrejection((int)id);
                var jobwork_rejection_detail = _jobworkRejection.jobworkrejectiondetail((int)id);
                var jobwork_rejection_item_detail = _jobworkRejection.jobworkrejectionitemdetail((int)id);
                //  inv_stock_detail

                var ch_number = "<table>";
                var ch_qty = "<table>";
                var ch_balance_qty = "<table>";
                var ch_hsn_code = "<table>";
                var ch_item_name = "<table>";
                var ch_date = "<table>";
                var dis_quantity = "<table>";

                foreach (var j in jobwork_rejection_item_detail)
                {
                    ch_number = ch_number + "<tr><td>" + j.batch_number + " </td></tr> ";
                    ch_qty = ch_qty + "<tr><td align='right'>" + j.batch_quantity + " </td></tr> ";
                    ch_balance_qty = ch_balance_qty + "<tr><td align='right'>" + j.bal_quantity + " </td></tr> ";
                    ch_hsn_code = ch_hsn_code + "<tr><td>" + j.sac_code + " </td></tr> ";
                    ch_item_name = ch_item_name + "<tr><td>" + j.item_name + " </td></tr> ";
                    ch_date = ch_date + "<tr><td>" + j.batch_date + " </td></tr>";
                    dis_quantity = dis_quantity + "<tr><td align='right'>" + j.quantity + " </td></tr>";
                }
                ch_number = ch_number + "</table>";
                ch_qty = ch_qty + "</table>";
                ch_balance_qty = ch_balance_qty + "</table>";
                ch_hsn_code = ch_hsn_code + "</table>";
                ch_item_name = ch_item_name + "</table>";
                ch_date = ch_date + "<table>";
                dis_quantity = dis_quantity + "<table>";



                DataSet ds = new DataSet("jobwork_rejection");

                foreach (var i in jobwork_rejection_item_detail)
                {
                    qty = qty + i.quantity;
                    total = total + i.value;
                }

                var dt1 = _Generic.ToDataTable(jobwork_rejection);
                var dt2 = _Generic.ToDataTable(jobwork_rejection_detail);
                var dt3 = _Generic.ToDataTable(jobwork_rejection_item_detail);

                dt1.TableName = "jobwork_rejection";
                dt2.TableName = "jobwork_rejection_detail";
                dt3.TableName = "jobwork_rejection_item_detail";
                dt2.Rows[0]["total_quantity"] = qty;
                dt2.Rows[0]["total"] = total;

                dt1.Rows[0]["batch_number"] = ch_number;
                dt1.Rows[0]["batch_quantity"] = ch_qty;
                dt1.Rows[0]["bal_quantity"] = ch_balance_qty;
                dt1.Rows[0]["batch_date"] = ch_date;
                dt1.Rows[0]["desp_quantity"] = dis_quantity;
                dt1.Rows[0]["ch_sac_code"] = ch_hsn_code;
                dt1.Rows[0]["batch_item"] = ch_item_name;

                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Tables.Add(dt3);


                rd.Load(Path.Combine(Server.MapPath("~/Reports/jobworkRejectionReportNew.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "jobworkRejectionReportNew.rpt");
                return new CrystalReportPdfResult(reportPath, rd);
            }
            catch (Exception ex)
            {
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
        public CrystalReportPdfResult SubsidiaryPdf(int id)
        {
            ReportDocument rd = new ReportDocument();
            try
            {
                var challan = _jobworkRejection.GetJobWorkRejectionDetail("getjobworkrejectionchallan", id);
                DataSet ds = new DataSet("JWR");
                var dt = _Generic.ToDataTable(challan);
                dt.TableName = "JWR";
                ds.Tables.Add(dt);
                rd.Load(Path.Combine(Server.MapPath("~/Reports/JWR_Subsidiary.rpt")));
                rd.SetDataSource(ds);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string reportPath = Path.Combine(Server.MapPath("~/Reports"), "JWR_Subsidiary.rpt");
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
        public ActionResult GetItemCategoryByItem(int item_id)
        {
            var data = _jobworkRejection.GetItemCategoryByItem(item_id);
            return Json(data);
        }
    }
}