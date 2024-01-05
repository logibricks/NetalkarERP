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
using System.Linq;
using Newtonsoft.Json;
using Sciffer.Erp.Domain.ViewModel;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
using Sciffer.Erp.Domain.Model;
using OfficeOpenXml;
using System.IO;

namespace Sciffer.Erp.Web.Controllers
{
    public class GenericController : Controller
    {
        private readonly ICreditDebitNoteTransactionService _creditnotetransservice;
        private readonly IGenericService _Generic;
        private readonly IPlantTransferService _plantTransferService;
        private readonly IFinLedgerService _journalEntryService;
        private readonly IItemService _itemService;
        private readonly IBankService _bank;
        private readonly IBranchService _branchService;
        private readonly IBrandService _brandService;
        private readonly IBankAccountService _bankservice;
        private readonly IBusinessUnitService _businessService;
        private readonly ICostCenterService _coscenterservice;
        private readonly ICountryService _countryService;
        private readonly ICreditCardService _creditcareservice;
        private readonly ICurrencyService _CurrencyService;
        private readonly IDocumentNumbringService _documentService;
        private readonly IFinancialYearService _financeService;
        private readonly IFormService _form;
        private readonly IFreightTermsService _FreightService;
        private readonly IGeneralLedgerService _generalledgerService;
        private readonly IVendorService _vendorService;
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly IPaymentTermsDueDateService _paymentTermsDueDate;
        private readonly IPaymentTermsService _paymentTermsService;
        private readonly IPaymentTypeService _payment;
        private readonly IPurRequisitionService _purchaserequisition;
        private readonly IReasonDeterminationService _reasonDeterminationService;
        private readonly ISalSoservice _soservice;
        private readonly IStorageLocation _storage;
        private readonly IPlantService _Plant;
        private ITaxTypeService _taxTypeService;
        private readonly IUserService _userService;
        private readonly IBudgetMasterService _budget;
        private readonly ICompanyService _companyService;
        private readonly ICustomerCategoryService _customerCategory;
        private readonly IDesignationService _designation;
        private readonly IDepartmentService _department;
        private readonly IDivisionService _division;
        private readonly IDocumentNumbringService _documentNumbering;
        private readonly IDocumentTypeService _documentType;
        private readonly IEntityTypeService _entityType;
        private readonly IFrequencyService _frequency;
        private readonly IGoodsIssueService _goodsIssurService;
        private readonly IGoodsReceiptService _goodsReceiptService;
        private readonly IGrnService _grnService;
        private readonly IGradeService _gradeService;
        private readonly IItemValuationService _itemvaluationService;
        private readonly IItemTypeService _ItemTypeService;
        private readonly IItemGroupService _itemGroupService;
        private readonly IItemCategoryService _itemCategory;
        private readonly IOrgTypeService _orgService;
        private readonly IPartyTypeService _partyType;
        private readonly IPaymentCycleService _paymentCycleService;
        private readonly IPaymentCycleTypeService _paymentCycleTypeService;
        private readonly IPaymentTypeService _paymentType;
        private readonly IPriceListCustomerService _pricelistCustomer;
        private readonly IPriceListService _priceListService;
        private readonly IPriorityService _priority;
        private readonly IPurchaseOrderService _purchaseOrder;
        private readonly ISalesInvoiceService _salesInvoice;
        private readonly ISalesRMService _salesRM;
        private readonly ISalQuotationService _salesQuotation;
        private readonly IShiftService _shiftService;
        private readonly ISourceService _sourceService;
        private readonly IStateService _stateService;
        private readonly ITerritoryService _territory;
        private readonly ITaxElementService _taxelementservice;
        private readonly ITaxService _taxservice;
        private readonly ITdsCodeService _tdscode;
        private readonly ITDSSectionService _tdsSection;
        private readonly IUOMService _uomservice;
        private readonly IVendorCategoryService _vendorCategory;
        private readonly IPostingPeriodsService _posting;
        private readonly IGeneralLedgerBalanceService _genBalance;
        private readonly IBatchNumberingService _batchNumbering;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IVendorBalanceService _vendorBalance;
        private readonly ICustomerBalanceService _customerBalance;
        private readonly IInventoryBalanceService _inventoryBalance;
        private readonly IEmployeeBalanceService _employeeBalance;
        private readonly IVendorParentService _vendorParent;
        private readonly ICustomerParentService _customerParent;
        private readonly IQAService _QA;
        private readonly IBankAccountService _bankAccountService;
        private readonly IReceiptService _Receipt;
        private readonly IInjobWorkService _InJobWork;
        private readonly IMachineMasterService _machineService;
        private readonly IBomService _bomService;
        private readonly IInjobWorkService _injobWorkService;
        private readonly IProcessMachineMappingService _processMachineMappingService;
        private readonly IProcessSequence _processSequence;
        private readonly ITagNumberingService _tagNumberingService;
        private readonly IContraEntryService _ContraService;
        private readonly IMaterialRequisionNoteService _materialRequisionNoteService;
        private readonly IPurchaseInvoiceService _purchaseInvoice;
        private readonly IReasonDeterminationService _reason;
        private readonly IMachineMasterService _machine;
        private readonly IFinBankRecoService _Reco;
        private readonly IRevaluationService _Inventory_Revaluation;
        private readonly IFinInternalReconcileService _Internal_reco;
        private readonly ICreditDebitNoteService _CreditDebitNote;
        private readonly IMaterialOutService _materialoutservice;
        private readonly IMaterialInService _materialinservice;
        private readonly IInventoryStockService _inventorystock;
        private readonly IIncomingExciseService _incoming_Excise;
        private readonly IPurchaseReturnService _Purchase_Return_Service;
        private readonly IProductionService _productionOrder;
        private readonly IProductionReceiptService _productionReceipt;
        private readonly IProdOrderIssueService _productionIssue;
        private readonly IUserManagementService _usermanagement;
        private readonly IGSTTdsCodeService _GSTTDSCode;
        private readonly ISACService _SACService;
        private readonly IModeOfTransportService _Transport;
        private readonly IInterPlantService _InterPlantService;
        private readonly IHSNCodeMasterService _hSNCodeMasterService;
        private readonly ICashAccountService _cashAccountService;
        private readonly IProcessMasterService _processMasterService;
        private readonly IMachineCategoryService _machineCategoryService;
        private readonly IValidationService _validationService;
        private readonly IParameterListService _parameter;
        private readonly IPlanMaintenanceService _plan;
        private readonly IPlanMaintenanceOrderService _planOrder;
        private readonly IToolMasterService _Tool;
        private readonly IToolRenewTypeService _ToolRenew;
        private readonly IToolLifeService _ToolLife;
        private readonly INotificationService _Notificatione;
        private readonly INotificationTypeService _NotificationType;
        private readonly IPlantNotificationService _PlantNotification;
        private readonly IPlanBreakdownOrderService _breakdownOrder;
        private readonly IIssuePermitService _issuePermit;
        private readonly IPermitTemplateService _permiTtemplate;
        private readonly IReportService _Report_Service;
        private readonly IRejectionReceiptService _rejectionReceipt;
        private readonly IJobWorkRejectionService _jobworkRejection;
        private readonly ICancellationReasonService _CancellationReason;
        private readonly ISalesReturnService _salesReturnService;
        private readonly IFinancialTemplateService _fintemplate;
        private readonly ICustomerComplaintService _CComplaint;
        private readonly IMaterialRequisitionIndentService _indentRequest;
        private readonly IStockSheetService _create_stock;
        private readonly IUpdateStockSheetService _update_stock;
        private readonly IPostVarianceService _post_variance;
        private readonly IDepreciationAreaService _DepreciationArea;
        private readonly IAssetGroupService _assetgroupsservice;
        private readonly IAssetMasterDataService _assetmasterdataservice;
        private readonly ICapitalizationService _cap;
        private readonly IInitialUploadService _initialupload;
        private readonly ISRNService _SRN;
        private readonly ITaskService _taskservice;
        private readonly ISMSService _SMS;
        private readonly ICycleTimeService _cycletimeservice;
        private readonly IShiftwiseProductionMasterService _shiftwiseproduction;
        public GenericController(ISalesReturnService SalesReturnService, IValidationService ValidationService, IMachineCategoryService MachineCategoryService, IProcessMasterService ProcessMasterService, ICashAccountService CashAccountService, IHSNCodeMasterService HSNCodeMasterService, IInterPlantService InterPlantService, IBomService BomService, IPlantService plant, IUserService UserService, ITaxTypeService TaxTypeService, ISalSoservice SalSoservice, IReasonDeterminationService ReasonDeterminationService,
            IPurRequisitionService PurRequisitionService, IPaymentTypeService PaymentTypeService, IPaymentTermsService PaymentTermsService, IPaymentTermsDueDateService PaymentTermsDueDateService, IModeOfTransportService Transport,
            IEmployeeService EmployeeService, ICustomerService CustomerService, IVendorService VendorService, IGeneralLedgerService GeneralLedgerService, IFreightTermsService FreightTermsService, IGSTTdsCodeService GSTTDSCode,
            IFormService FormService, IFinancialYearService FinancialYearService, IDocumentNumbringService DocumentNumbringService, ICurrencyService CurrencyService, ICreditCardService CreditCardService, ISACService sac,
            ICountryService CountryService, ICostCenterService CostCenterService, IBusinessUnitService BusinessUnitService, IBankAccountService BankService, IItemService ItemService, IFinLedgerService JournalEntryService,
            IGenericService GenericService, IPlantTransferService PlantTransferService, IStorageLocation storage, IBankService bank, IBudgetMasterService budget, IFrequencyService frequency,
            IBranchService branch, IBrandService brand, ICompanyService company, ICustomerCategoryService customercategory, IDesignationService designation, IDepartmentService department,
            IDivisionService division, IDocumentNumbringService documentNumbering, IDocumentTypeService documentType, IEntityTypeService entitytype, IGoodsIssueService goodsIssue, IGoodsReceiptService goodsreceipt,
            IGrnService grn, IGradeService grade, IItemValuationService itemvaluation, IItemTypeService itemtype, IItemGroupService itemGroup, IItemCategoryService itemcategory, IOrgTypeService org,
            IPartyTypeService partytype, IPaymentCycleService paymentcycle, IPaymentCycleTypeService paymentcycletype, IPaymentTypeService paymenttype, IPriceListCustomerService pricelistcustomer,
            IPriceListService pricelist, IPriorityService priority, IPurchaseOrderService purchaseorder, ISalesInvoiceService invoice, ISalesRMService salesrm, ISalQuotationService quotation,
            IShiftService shift, ISourceService source, IStateService state, ITerritoryService territory, ITaxElementService taxelement, ITaxService taxservice, ITdsCodeService tdscode, ITDSSectionService tdssection,
            IUOMService uom, IVendorCategoryService vendorcategory, IPostingPeriodsService posting, IGeneralLedgerBalanceService genbalance, IBatchNumberingService batchNumbering, IExchangeRateService exchange,
            ICustomerBalanceService customerbalance, IVendorBalanceService vendorBalance, IInventoryBalanceService inventoryBalance, IEmployeeBalanceService employeeBalance, IIncomingExciseService incoming_Excise,
            IVendorParentService vendorParent, ICustomerParentService Customerparent, IQAService qa, IBankAccountService BankAccountService, IReceiptService Receipt, IInjobWorkService jobin, IPurchaseReturnService Purchase_Return_Service
            , IMachineMasterService MachineService, IInjobWorkService InjobWorkService, IContraEntryService ContraService, IPurchaseInvoiceService purchaseInvoice, ICreditDebitNoteService CreditDebitNote,
            IProcessMachineMappingService ProcessMachineMappingService, IProcessSequence ProcessSequence, ITagNumberingService TagNumberingService, IMaterialRequisionNoteService MaterialRequisionNoteService,
            IReasonDeterminationService reason, IMachineMasterService machine, IFinBankRecoService reco, IRevaluationService revaluation, IFinInternalReconcileService Internal_reco, IMaterialOutService materialoutservice,
            IMaterialInService materialinservice, IInventoryStockService inventorystock, IProductionService ProductionOrder, IProductionReceiptService productionReceipt, IProdOrderIssueService productionIssue,
            IParameterListService parameter, IUserManagementService userManagement, IPlanMaintenanceService plan, IPlanMaintenanceOrderService planOrder, IReportService Report_Service,
            IToolLifeService toollife, IToolMasterService tool, IToolRenewTypeService toolrenew, INotificationService notification, IPlantNotificationService PlantNotification,
            IPlanBreakdownOrderService breakdownOrder, IIssuePermitService issuePermit, IPermitTemplateService permitTemplate, INotificationTypeService notificationType,
            IRejectionReceiptService rejectionReceipt, IJobWorkRejectionService jobworkRejection, ICancellationReasonService CancellationReason,
            IFinancialTemplateService fintemplate, ICustomerComplaintService complaint, IMaterialRequisitionIndentService IndentRequest, ISRNService SRN,
            IStockSheetService create_stock, IUpdateStockSheetService update_stock, IPostVarianceService post_variance,
            ICreditDebitNoteTransactionService creditnotetransservice, IDepreciationAreaService DepreciationAreaService, IAssetGroupService assetgroupsservice,
            IAssetMasterDataService assetmasterdataservice, ICapitalizationService cap, IInitialUploadService initialupload, ITaskService taskservice,
            ISMSService SMS, ICycleTimeService CycleTimeService, IShiftwiseProductionMasterService shiftwiseproduction)
        {
            _CComplaint = complaint;
            _salesReturnService = SalesReturnService;
            _Report_Service = Report_Service;
            _validationService = ValidationService;
            _machineCategoryService = MachineCategoryService;
            _processMasterService = ProcessMasterService;
            _cashAccountService = CashAccountService;
            _hSNCodeMasterService = HSNCodeMasterService;
            _InterPlantService = InterPlantService;
            _Transport = Transport;
            _SACService = sac;
            _GSTTDSCode = GSTTDSCode;
            _productionOrder = ProductionOrder;
            _productionReceipt = productionReceipt;
            _productionIssue = productionIssue;
            _Purchase_Return_Service = Purchase_Return_Service;
            _incoming_Excise = incoming_Excise;
            _CreditDebitNote = CreditDebitNote;
            _Internal_reco = Internal_reco;
            _Inventory_Revaluation = revaluation;
            _Reco = reco;
            _purchaseInvoice = purchaseInvoice;
            _ContraService = ContraService;
            _Receipt = Receipt;
            _bankAccountService = BankAccountService;
            _customerParent = Customerparent;
            _vendorBalance = vendorBalance;
            _customerBalance = customerbalance;
            _inventoryBalance = inventoryBalance;
            _employeeBalance = employeeBalance;
            _genBalance = genbalance;
            _exchangeRateService = exchange;
            _batchNumbering = batchNumbering;
            _genBalance = genbalance;
            _posting = posting;
            _uomservice = uom;
            _vendorCategory = vendorcategory;
            _tdscode = tdscode;
            _tdsSection = tdssection;
            _territory = territory;
            _taxelementservice = taxelement;
            _taxservice = taxservice;
            _shiftService = shift;
            _sourceService = source;
            _stateService = state;
            _territory = territory;
            _priority = priority;
            _purchaseOrder = purchaseorder;
            _salesInvoice = invoice;
            _salesQuotation = quotation;
            _salesRM = salesrm;
            _priceListService = pricelist;
            _pricelistCustomer = pricelistcustomer;
            _partyType = partytype;
            _paymentType = paymenttype;
            _paymentCycleTypeService = paymentcycletype;
            _paymentCycleService = paymentcycle;
            _orgService = org;
            _itemCategory = itemcategory;
            _itemGroupService = itemGroup;
            _ItemTypeService = itemtype;
            _itemvaluationService = itemvaluation;
            _goodsIssurService = goodsIssue;
            _goodsReceiptService = goodsreceipt;
            _gradeService = grade;
            _grnService = grn;
            _entityType = entitytype;
            _frequency = frequency;
            _documentType = documentType;
            _division = division;
            _documentNumbering = documentNumbering;
            _designation = designation;
            _department = department;
            _customerCategory = customercategory;
            _companyService = company;
            _branchService = branch;
            _brandService = brand;
            _budget = budget;
            _bank = bank;
            _Plant = plant;
            _storage = storage;
            _Generic = GenericService;
            _plantTransferService = PlantTransferService;
            _journalEntryService = JournalEntryService;
            _itemService = ItemService;
            _bankservice = BankService;
            _businessService = BusinessUnitService;
            _coscenterservice = CostCenterService;
            _countryService = CountryService;
            _creditcareservice = CreditCardService;
            _CurrencyService = CurrencyService;
            _documentService = DocumentNumbringService;
            _financeService = FinancialYearService;
            _form = FormService;
            _FreightService = FreightTermsService;
            _generalledgerService = GeneralLedgerService;
            _vendorService = VendorService;
            _customerService = CustomerService;
            _employeeService = EmployeeService;
            _paymentTermsDueDate = PaymentTermsDueDateService;
            _paymentTermsService = PaymentTermsService;
            _payment = PaymentTypeService;
            _purchaserequisition = PurRequisitionService;
            _reasonDeterminationService = ReasonDeterminationService;
            _soservice = SalSoservice;
            _taxTypeService = TaxTypeService;
            _userService = UserService;
            _vendorParent = vendorParent;
            _QA = qa;
            _InJobWork = jobin;
            _machineService = MachineService;
            _bomService = BomService;
            _injobWorkService = InjobWorkService;
            _processMachineMappingService = ProcessMachineMappingService;
            _processSequence = ProcessSequence;
            _tagNumberingService = TagNumberingService;
            _materialRequisionNoteService = MaterialRequisionNoteService;
            _reason = reason;
            _machine = machine;
            _materialoutservice = materialoutservice;
            _materialinservice = materialinservice;
            _inventorystock = inventorystock;
            _usermanagement = userManagement;
            _parameter = parameter;
            _plan = plan;
            _planOrder = planOrder;
            _Tool = tool;
            _ToolLife = toollife;
            _ToolRenew = toolrenew;
            _NotificationType = notificationType;
            _Notificatione = notification;
            _PlantNotification = PlantNotification;
            _breakdownOrder = breakdownOrder;
            _issuePermit = issuePermit;
            _permiTtemplate = permitTemplate;
            _rejectionReceipt = rejectionReceipt;
            _jobworkRejection = jobworkRejection;
            _CancellationReason = CancellationReason;
            _fintemplate = fintemplate;
            _indentRequest = IndentRequest;
            _create_stock = create_stock;
            _post_variance = post_variance;
            _update_stock = update_stock;
            _creditnotetransservice = creditnotetransservice;
            _DepreciationArea = DepreciationAreaService;
            _assetgroupsservice = assetgroupsservice;
            _assetmasterdataservice = assetmasterdataservice;
            _cap = cap;
            _initialupload = initialupload;
            _SRN = SRN;
            _taskservice = taskservice;
            _SMS = SMS;
            _cycletimeservice = CycleTimeService;
            _shiftwiseproduction = shiftwiseproduction;
        }
        public ActionResult GetGSTCustomerTypeforGRN(int id)
        {
            var gst_no = _vendorService.GetAll().Where(a => a.VENDOR_ID == id).Select(a => a.gst_no).FirstOrDefault();
            var gst = _Generic.GetGSTCustomerTypeforGRN(id).ToString();
            List<string> list = new List<string>();
            list.Add(gst);
            list.Add(gst_no);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public FileResult Download(string controller_name, int id)
        {
            var notification = _Notificatione.Get(id).attachment;
            var file = notification.Split('/').Last();
            var paths = "~/Files/" + controller_name + "/" + file;
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + controller_name + "/" + file);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }



        public ActionResult CheckDuplicate(string codeornum, string name, string name1, int id, string ctrlname)
        {
            var vm = _Generic.CheckDuplicate(codeornum, name, name1, ctrlname, id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public void ExportToExcel(string GridModel, string ctrlname)
        {
            ExcelExport exp = new ExcelExport();
            var DataSource = GetAllData(ctrlname);
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
        }
        [ValidateInput(false)]
        public void ExportToWord(string GridModel, string ctrlname)
        {
            WordExport exp = new WordExport();
            var DataSource = GetAllData(ctrlname);
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, ctrlname + ".docx", false, false, "flat-saffron");
        }
        [ValidateInput(false)]
        public void ExportToExcelForReport(string GridModel, string ctrlname)
        {
            try
            {
                ExcelExport exp = new ExcelExport();
                var DataSource = GetReport(ctrlname);
                GridProperties obj = ConvertGridObject(GridModel);
                exp.Export(obj, DataSource, ctrlname + ".xlsx", ExcelVersion.Excel2010, false, false, "bootstrap-theme");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [ValidateInput(false)]
        public void ExportToPdf(string GridModel, string ctrlname)
        {
            PdfExport exp = new PdfExport();
            var DataSource = GetAllData(ctrlname);
            GridProperties obj = ConvertGridObject(GridModel);
            exp.Export(obj, DataSource, ctrlname + ".pdf", false, false, "flat-saffron");
        }
        [ValidateInput(false)]
        private GridProperties ConvertGridObject(string gridProperty)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IEnumerable div = (IEnumerable)serializer.Deserialize(gridProperty, typeof(IEnumerable));
            GridProperties gridProp = new GridProperties();
            foreach (KeyValuePair<string, object> ds in div)
            {
                var property = gridProp.GetType().GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    Type type = property.PropertyType;
                    string serialize = serializer.Serialize(ds.Value);
                    object value = serializer.Deserialize(serialize, type);
                    property.SetValue(gridProp, value, null);
                }
            }
            return gridProp;
        }
        //SHOW FILE
        public FileResult Result(int id, string controllername)
        {
            var path = _Generic.GetAttachment(id, controllername);
            if (path != "No File")
            {
                if (path != "")
                {
                    var arr = path.Split('/');
                    var filesname = arr.Last();
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    Response.AppendHeader("Content-Disposition", "inline; filename=" + filesname);
                    return File(path, filesname);
                }
            }
            return null;
        }
        public ActionResult GetCategoryListByPlant(int id, int plant_id)
        {
            var vm = _Generic.GetCategoryListByPlant(id, plant_id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndexData(DataManager dm, string ctrl_name)
        {
            var list = GetAllData(ctrl_name);
            IEnumerable data = (IEnumerable)list;
            DataOperations operation = new DataOperations();
            //for filtring
            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0)
            {
                data = operation.PerformWhereFilter(data, dm.Where, dm.Where[0].Operator);
            }
            // for searching
            if (dm.Search != null && dm.Search.Count > 0)
            {
                data = operation.PerformSearching(data, dm.Search);
            }
            int count = data.Cast<Object>().Count();
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }
            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }
            return Json(new { result = data, count = count });
        }
        public JsonResult GetItems()
        {
            var res = _itemService.GetItems();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBankAcc()
        {
            var ba = _bankservice.GetBankAccount();

            return Json(new { result = ba, count = ba.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateNamesFromCountry(int c)
        {
            var StateList = _Generic.GetState(c);
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMachineList(int id)
        {
            var ModuleForm = _Generic.GetMachineList(id);
            return Json(ModuleForm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMachineListWithPlant(int plant_id)
        {
            var ModuleForm = _Generic.GetMachineListWithPlant(plant_id);
            return Json(ModuleForm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult plantby_doc_no(int category_id)
        {
            var plant_id = _Generic.plantby_doc_no(category_id);
            return Json(plant_id, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBusinessUnit()
        {
            var res = _businessService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTaxByRCM(int id)
        {
            var list = _Generic.GetTaxByRCM(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemCategoryByItemType(int id)
        {
            var itemlist = _itemCategory.GetItemCategoryByItemType(id);
            return Json(itemlist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLevel(int id)
        {
            var level = _coscenterservice.getLevel(id);
            return Json(level, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCountry()
        {
            var res = _countryService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCurrency()
        {
            var res = _CurrencyService.GetAll();
            ViewBag.COUNTRY_ID = _CurrencyService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckValidation(string entity, int? id, DateTime? posting_date)
        {
            DateTime dte = new DateTime(1990, 1, 1);
            if (posting_date == null)
            {
                posting_date = dte;
            }
            if (id == null)
            {
                id = 0;
            }
            var val = _Generic.CheckValidation(entity, (int)id, (DateTime)posting_date);
            return Json(val, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustCat()
        {
            var cc = _countryService.GetAll();
            return Json(new { result = cc, count = cc.Count }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetPartial(int count = 0)
        {
            ViewBag.Count = count;
            return PartialView("_ContactView");
        }
        public ActionResult GetCurrenctBalance(string entity, int entity_type_id, int entity_id, DateTime start_date)// for current balance of sub ledger
        {
            if (entity == "getcurrentbalanceasofdate")
            {
                var level = _Generic.Get_Current_Balance(entity, entity_type_id, entity_id, start_date);
                return Json(level, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var level = _Generic.Get_Current_Balance(entity, entity_type_id, entity_id, start_date);
                return Json(level, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Get_Item_Current_Balance(string entity, int item_id, int plant_id, int sloc_id, int bucket_id)
        {
            var level = _Generic.Get_Item_Current_Balance(entity, item_id, plant_id, sloc_id, bucket_id);
            return Json(level, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_hsn_sac(int id)
        {
            var res = _Generic.Get_hsn_sac(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_hsn_saclist(int id)
        {
            var res = _Generic.Get_hsn_saclist(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMachineListOnProcessId(int process_id)
        {
            var res = _Generic.GetMachineListOnProcessId(process_id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Gethsnforgrn(int id)
        {
            try
            {
                ref_hsn_code_grn list = new ref_hsn_code_grn();
                list.ref_hsn_code_vm = _Generic.GetHSNList();
                list.sac_id = _Generic.GetUserDescriptionForItem(id).sac_id;
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch { return null; }
        }

        public ActionResult GetGLOrBankAccount(int id)
        {
            if (id == 1)
            {
                var gllist = _Generic.GetCashAccount();
                return Json(gllist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var bankaccount = _Generic.GetBankAccountByBank(0);
                return Json(bankaccount, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTaxCalculation(string entity, string st, double amt, DateTime dt, int tds_code_id)
        {
            var taxcalculation = _Generic.GetTaxCalculation(entity, st, amt, dt, tds_code_id);
            return Json(taxcalculation, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPOByVendor(int? id, int vendor_id)
        {
            id = id == null ? 1 : id;
            var po = _purchaseOrder.GetPOList((int)id, vendor_id);
            return Json(po, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillPaymentCycle(int? Payment_Type_id)
        {
            var paymentService = _Generic.GetPaymentCycle((int)Payment_Type_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLedgerAccountType(int entity_type_id, int entity_id, int? item_type_id)
        {
            var paymentService = _Generic.GetLedgerAccountType(entity_type_id, entity_id, item_type_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillState(int COUNTRY_ID)
        {
            var STATE = _Generic.GetState(COUNTRY_ID);
            return Json(STATE, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckSet(int id, int financial_year_id)
        {
            var level = _documentService.checksetdefault(id, financial_year_id);
            return Json(level, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEntityType(string name)
        {
            object ename = _Generic.GetEntityDetailByEntity(name);
            return Json(ename, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDocumentNumbering(int id)
        {
            var series = _Generic.GetDocumentNumbering(id);
            return Json(series, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillModuleForm(int id)
        {
            var ModuleForm = _Generic.GetModuleForm(id);
            return Json(ModuleForm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFinancialYear()
        {
            var res = _financeService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetForms()
        {
            var res = _form.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFreight_Terms()
        {
            var res = _FreightService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGeneralLedger()
        {
            var ba = _generalledgerService.GetAll();
            return Json(new { result = ba, count = ba.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountType()
        {
            var res = _generalledgerService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemCategory()
        {
            var res = _itemService.GetAll();

            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemGroup()
        {
            var res = _itemService.GetAll();

            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDescription(int id)
        {
            if (id == 5)
            {
                var vm = _vendorService.GetAll();
                var list = JsonConvert.SerializeObject(vm, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
                return Content(list, "application/json");
            }
            if (id == 6)
            {
                var vm = _customerService.GetAll();
                var list = JsonConvert.SerializeObject(vm, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
                return Content(list, "application/json");
            }
            if (id == 8)
            {
                var vm = _employeeService.GetAll();
                var list = JsonConvert.SerializeObject(vm, Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
                return Content(list, "application/json");
            }
            return Json("notfound", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUomOrQualityManage(int id)
        {
            var it = _Generic.GetUomOrQualityManage(id);
            return Json(new { item = it }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOrganization_Type()
        {
            var res = _countryService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPayment_Terms()
        {
            var DueDates = _paymentTermsDueDate.GetAll();
            ViewBag.DueDateNames = new SelectList(DueDates, "PAYMENT_TERMS_DUE_DATE_ID", "PAYMENT_TERMS_DUE_DATE_NAME");
            var res = _paymentTermsService.GetPaymentTerms();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPaymentType()
        {
            var res = _payment.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCreditDebit(string entity, int customer_id, double total_value, double basic_value, string item_sales_gl, int tds_code_id, DateTime posting_date, double round_off)
        {
            var paymentService = _Generic.GetCreditDebit(entity, customer_id, total_value, basic_value, item_sales_gl, tds_code_id, posting_date, round_off);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPriority()
        {
            var res = _countryService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemStocks(string code)
        {
            var id = _Generic.GetItemId(code);
            var item = _purchaserequisition.GetItemStock(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetReasonDetermination()
        {
            var res = _reasonDeterminationService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStorageLocation(int id)
        {
            var vm = _Generic.GetStorageLocationList(id);
            return Json(vm, JsonRequestBehavior.AllowGet);

            // return Content(list, "application/json");
        }
        //public ActionResult GetSoProductDetail(int id)
        //{
        //    var vm = _soservice.GetAllproductforsi(id);
        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetSoDetailForInvoice(int id)
        //{
        //    var vm = _soservice.GetSODetail(id);
        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetSOForm(int id)
        //{
        //    var vm = _soservice.GetFormForSO(id);
        //    return Json(vm, JsonRequestBehavior.AllowGet);
        //}
        public PartialViewResult GetPartialQuotation(int count = 0)
        {
            ViewBag.Count = count;
            return PartialView("_QuotationDetailView");
        }
        // GET: Quotation/Details/5
        public ActionResult GetBuyerDetails(int id)
        {
            var vm = _Generic.GetBuyerDetails(id);
            return Json(new { billing = vm }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDocumentNumberingQutotaion(int id)
        {
            var it = _Generic.GetDocumentNumbering(id);
            return Json(new { item = it }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPOByItemOrService(int id, int vendor_id)
        {
            var po = _purchaseOrder.GetPOListByItemOrService(id, vendor_id);
            return Json(po, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUnitofItem(int id)
        {
            var it = _Generic.GetUnitofItem(id);
            return Json(new { item = it }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserDescriptionForItem(int id)
        {
            var it = _Generic.GetUserDescriptionForItem(id);
            return Json(it, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaxType()
        {
            var res = _taxTypeService.GetAll();
            return Json(new { result = res, count = res.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTerritory()
        {
            var tr = _countryService.GetAll();
            return Json(new { result = tr, count = tr.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVendorCat()
        {
            var vc = _vendorService.GetAll();
            return Json(new { result = vc, count = vc.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetItemOrGL(int id)
        {
            if (id == 1)
            {
                var result = _Generic.GetItemList().Select(a => new { ITEM_ID = a.ITEM_ID, ITEM_NAME = a.ITEM_NAME, item_type_id = a.item_type_id });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = _Generic.GetLedgerAccount(2).Select(a => new { gl_ledger_id = a.gl_ledger_id, gl_ledger_name = a.gl_ledger_name });
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetVendorDetails(int id)
        {
            var vm = _Generic.VendorDetail(id);
            return Json(new { billing = vm }, JsonRequestBehavior.AllowGet);
        }
        //get partial view for VENDOR contact
        public PartialViewResult GetPartialvendor(int count = 0)
        {
            ViewBag.Count = count;
            return PartialView("_VendorContactView");
        }
        //get partial view for ATTRIBUTE
        public PartialViewResult GetPartialAttributevendor(int attributecount = 0)
        {
            ViewBag.attributecount = attributecount;
            return PartialView("_VendorAttributeView");
        }
        public JsonResult UserList()
        {
            var list = _userService.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public object GetAllData(string controller)
        {
            object datasource = null;
            switch (controller)
            {
                case "ProductionOrder":
                    datasource = _productionOrder.GetAll();
                    break;
                case "PurchaseInvoice":
                    datasource = _purchaseInvoice.GetAll();
                    break;
                case "PurchaseReturn":
                    datasource = _Purchase_Return_Service.GetAll();
                    break;
                case "IncomingExcise":
                    datasource = _incoming_Excise.GetAll();
                    break;
                case "JournalEntry":
                    datasource = _journalEntryService.getall();
                    break;
                case "WithinPlantTransfer":
                    datasource = _plantTransferService.getall();
                    break;
                case "CustomerMaster":
                    datasource = _customerService.GetCustomerList();
                    break;
                case "Bank":
                    datasource = _bank.GetAll();
                    break;
                case "Budget":
                    datasource = _budget.GetAll();
                    break;
                case "Branch":
                    datasource = _branchService.GetAll();
                    break;
                case "Brand":
                    datasource = _brandService.GetAll();
                    break;
                case "BusinessUnit":
                    datasource = _businessService.GetAll();
                    break;
                case "CompanyDetails":
                    datasource = _companyService.GetCompanyDetail();
                    break;
                case "CostCenter":
                    datasource = _coscenterservice.GetCostCenter();
                    break;
                case "Country":
                    datasource = _countryService.GetAll();
                    break;
                case "CreditCard":
                    datasource = _creditcareservice.GetAll();
                    break;
                case "Currency":
                    datasource = _CurrencyService.GetAll();
                    break;
                case "CustomerCategory":
                    datasource = _customerCategory.GetAll();
                    break;
                case "Department":
                    datasource = _department.GetAll();
                    break;
                case "Designation":
                    datasource = _designation.GetAll();
                    break;
                case "Division":
                    datasource = _division.GetAll();
                    break;
                case "DocumentNumbering":
                    datasource = _documentNumbering.GetDocumentNumbering();
                    break;
                case "DocumentType":
                    datasource = _documentType.GetAll();
                    break;
                case "Employee":
                    datasource = _employeeService.GetEmployeeList();
                    break;
                case "Entity_Type":
                    datasource = _entityType.GetAll();
                    break;
                case "ExchangeRate":
                    datasource = _exchangeRateService.GetExchanagelist();
                    break;
                case "FinancialYear":
                    datasource = _financeService.GetAll();
                    break;
                case "Forms":
                    datasource = _form.GetAll();
                    break;
                case "FreightTerms":
                    datasource = _FreightService.GetAll();
                    break;
                case "Frequency":
                    datasource = _frequency.GetAll();
                    break;
                case "GoodsIssue":
                    datasource = _goodsIssurService.getall();
                    break;
                case "GoodsReceipt":
                    datasource = _goodsReceiptService.getall();
                    break;
                case "Grade":
                    datasource = _gradeService.GetAll();
                    break;
                case "GRN":
                    datasource = _grnService.getall();
                    break;
                case "SRN":
                    datasource = _SRN.GetAll();
                    break;
                case "ItemCategory":
                    datasource = _itemCategory.GetAll();
                    break;
                case "ItemGroup":
                    datasource = _itemGroupService.GetAll();
                    break;
                case "ItemMaster":
                    datasource = _itemService.GetItems();
                    break;
                case "OrganizationType":
                    datasource = _orgService.GetAll();
                    break;
                case "PartyType":
                    datasource = _partyType.GetAll();
                    break;
                case "Payment_Cycle":
                    datasource = _paymentCycleService.GetAll();
                    break;
                case "Payment_Cycle_Type":
                    datasource = _paymentCycleTypeService.GetAll();
                    break;
                case "PaymentTerms":
                    datasource = _paymentTermsService.GetPaymentTerms();
                    break;
                case "PaymentType":
                    datasource = _paymentType.GetAll();
                    break;
                case "Plant":
                    datasource = _Plant.GetPlantList();
                    break;
                case "PriceList":
                    datasource = _priceListService.GetAll();
                    break;
                case "PostingPeriod":
                    datasource = _posting.GetPostingPeriods();
                    break;
                case "Priority":
                    datasource = _priority.GetAll();
                    break;
                case "PurchaseOrder":
                    datasource = _purchaseOrder.getall();
                    break;
                case "PurchaseRequisition":
                    datasource = _purchaserequisition.GetAll();
                    break;
                case "ReasonDetermination":
                    datasource = _reasonDeterminationService.GetReasonList(0);
                    break;
                case "SalesInvoice":
                    datasource = _salesInvoice.GetAll();
                    break;
                case "SalesReturn":
                    datasource = _salesReturnService.getall();
                    break;
                case "SalesOrder":
                    datasource = _soservice.getall();
                    break;
                case "SalesQuotation":
                    datasource = _salesQuotation.getall();
                    break;
                case "SalesRM":
                    datasource = _salesRM.GetAll();
                    break;
                case "Shift":
                    datasource = _shiftService.GetShiftList();
                    break;
                case "Source":
                    datasource = _sourceService.GetAll();
                    break;
                case "State":
                    datasource = _stateService.GetStateList();
                    break;
                case "TaxCode":
                    datasource = _taxservice.GetAll();
                    break;
                case "StorageLocation":
                    datasource = _storage.getstoragelist();
                    break;
                case "TaxElement":
                    datasource = _taxelementservice.getall();
                    break;
                case "TaxType":
                    datasource = _taxTypeService.GetAll();
                    break;
                case "TDS_Section":
                    datasource = _tdsSection.GetAll();
                    break;
                case "TDSCode":
                    datasource = _tdscode.getall();
                    break;
                case "Territory":
                    datasource = _territory.GetAll();
                    break;
                case "UOM":
                    datasource = _uomservice.GetAll();
                    break;
                case "VendorCategory":
                    datasource = _vendorCategory.GetAll();
                    break;
                case "VendorParent":
                    datasource = _vendorParent.GetAll();
                    break;
                case "CustomerParent":
                    datasource = _customerParent.GetAll();
                    break;
                case "VendorMaster":
                    datasource = _vendorService.GetVendorDetail();
                    break;
                case "PriceListCustomer":
                    datasource = _pricelistCustomer.GetAll();
                    break;
                case "GeneralLedgerBalance":
                    datasource = _genBalance.GetAll();
                    break;
                case "BatchNumbering":
                    datasource = _batchNumbering.GetAll();
                    break;
                case "EmployeeBalance":
                    datasource = _employeeBalance.GetAll();
                    break;
                case "InventoryRevaluation":
                    datasource = _Inventory_Revaluation.GetAll();
                    break;
                case "BRS":
                    datasource = _Reco.GetAll();
                    break;
                case "VendorBalance":
                    datasource = _vendorBalance.GetAll();
                    break;
                case "CustomerBalance":
                    datasource = _customerBalance.GetAll();
                    break;
                case "InventoryBalance"://
                    datasource = _inventoryBalance.GetAll();
                    break;//
                case "QA":
                    datasource = _QA.GetAll();
                    break;
                case "HouseBank":
                    datasource = _bankAccountService.GetBankAccount();
                    break;

                case "Machine":
                    datasource = _machineService.getall();
                    break;

                case "MaterialOut":
                    datasource = _materialoutservice.GetAll();
                    break;
                case "MaterialIn":
                    datasource = _materialinservice.GetAll();
                    break;
                case "InventoryStock":
                    datasource = _inventorystock.GetAll();
                    break;
                case "BOM":
                    datasource = _bomService.getall();
                    break;
                case "Payment":
                    datasource = _Receipt.GetAll(2);
                    break;
                case "Receipt":
                    datasource = _Receipt.GetAll(1);
                    break;
                case "ContraEntry":
                    datasource = _ContraService.GetAll();
                    break;
                case "JobWork":
                    datasource = _injobWorkService.getall();
                    break;
                case "InternalReconcile":
                    datasource = _Internal_reco.GetAll();
                    break;
                case "ProcessSequence":
                    datasource = _processSequence.GetAll();
                    break;
                case "TagNumbering":
                    datasource = _tagNumberingService.GetAll();
                    break;
                case "MaterialRequisitionNote":
                    datasource = _materialRequisionNoteService.GetAll();
                    break;
                case "CreditNote":
                    datasource = _CreditDebitNote.GetAll(1);
                    break;
                case "DebitNote":
                    datasource = _CreditDebitNote.GetAll(2);
                    break;
                case "ProductionReceipt":
                    datasource = _productionReceipt.GetAll();
                    break;
                case "ProductionOrderIssue":
                    datasource = _productionIssue.GetAll();
                    break;
                case "UserManagement":
                    datasource = _usermanagement.GetAll();
                    break;
                case "GSTTDSCode":
                    datasource = _GSTTDSCode.GetAll();
                    break;
                case "SACMaster":
                    datasource = _SACService.GetAll();
                    break;
                case "ModeOfTransport":
                    datasource = _Transport.GetAll();
                    break;
                case "InterPlantTransfer":
                    datasource = _InterPlantService.getall();
                    break;
                case "HSN":
                    datasource = _hSNCodeMasterService.GetAll();
                    break;
                case "CashAccount":
                    datasource = _cashAccountService.getall();
                    break;
                case "ProcessMaster":
                    datasource = _processMasterService.GetAll();
                    break;
                case "MachineCategory":
                    datasource = _machineCategoryService.GetAll();
                    break;
                case "Validation":
                    datasource = _validationService.GetAll();
                    break;
                case "ParameterList":
                    datasource = _parameter.GetAll();
                    break;
                case "ToolMaster":
                    datasource = _Tool.GetAll();
                    break;
                case "ToolLifeMaster":
                    datasource = _ToolLife.GetAll();
                    break;
                case "ToolRenewType":
                    datasource = _ToolRenew.GetAll();
                    break;
                case "PlanMaintenance":
                    datasource = _plan.GetAll();
                    break;
                case "PlanMaintenanceOrder":
                    datasource = _planOrder.GetAll();
                    break;
                case "Notification":
                    datasource = _Notificatione.GetAll(0);
                    break;
                case "NotificationType":
                    datasource = _NotificationType.GetAll();
                    break;
                case "PlantNotification":
                    datasource = _PlantNotification.GetAll();
                    break;
                case "BreakdownOrder":
                    datasource = _breakdownOrder.GetAll();
                    break;
                case "IssuePermit":
                    datasource = _issuePermit.GetAll();
                    break;
                case "PermitTemplate":
                    datasource = _permiTtemplate.GetAll();
                    break;
                case "RejectionReceipt":
                    datasource = _rejectionReceipt.GetAll();
                    break;
                case "JobworkRejection":
                    datasource = _jobworkRejection.GetAll();
                    break;
                case "CancellationReason":
                    datasource = _CancellationReason.GetAll();
                    break;
                case "FinancialTemplate":
                    datasource = _fintemplate.getall();
                    break;
                case "CustomerComplaint":
                    datasource = _CComplaint.GetAll();
                    break;
                case "MaterialRequisitionIndent":
                    datasource = _indentRequest.GetAll();
                    break;
                case "CreateStockSheet":
                    datasource = _create_stock.getall();
                    break;
                case "UpdateStockSheet":
                    datasource = _update_stock.getall();
                    break;
                case "PostVariance":
                    datasource = _post_variance.GetAll();
                    break;
                case "CreditNoteTransaction":
                    datasource = _creditnotetransservice.GetAll(1);
                    break;
                case "DebitNoteTransaction":
                    datasource = _creditnotetransservice.GetAll(2);
                    break;
                case "IncentiveBenchmark":
                    datasource = _Generic.GetInsentiveBenchmarkDetail("incentive_benchmark");
                    break;
                case "AssetGroup":
                    datasource = _assetgroupsservice.GetAll();
                    break;
                case "CycleTime":
                    datasource = _cycletimeservice.GetAll();
                    break;
                case "AssetMasterData":
                    datasource = _assetmasterdataservice.GetAll();
                    break;
                case "Capitalization":
                    datasource = _cap.GetAll();
                    break;
                case "InitialUpload":
                    datasource = _initialupload.GetAll();
                    break;
                case "Task":
                    datasource = _taskservice.GetAll();
                    break;
                case "ShiftwiseProductionMaster":
                    datasource = _shiftwiseproduction.GetAll();
                    break;
                default:
                    break;
            }
            return datasource;
        }
        public object GetReport(string controller)
        {
            object datasource = null;
            datasource = HttpContext.Session[controller];
            return datasource;
        }
        public JsonResult GetEntityTransaction(int entity_type_id, string entity_id)
        {
            var paymentService = _Receipt.GetEntityTransaction(entity_type_id, entity_id);
            return Json(paymentService, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGRNListByItemID(int item_id, int plant_id)
        {
            var vm = _Generic.GetGrnListByItemID(item_id, plant_id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInventoryAccount(int id)
        {
            var vm = _Generic.GetInventoryAccount(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetConsumptionAccount(int id)
        {
            var vm = _Generic.GetConsumptionAccount(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BatchListWithQuantity(int? item_id, int? plant_id, int? sloc_id, string entity)
        {
            var taxcalculation = _Generic.BatchListWithQuantity((int)item_id, (int)plant_id, (int)sloc_id, entity);

            return Json(taxcalculation, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReasonListByCode(string code)
        {
            var vm = _reason.GetReasonByCode(code);
            return Json(vm, JsonRequestBehavior.AllowGet);

            // return Content(list, "application/json");
        }
        public ActionResult MachineList()
        {
            var vm = _machine.getall();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BatchQuantityUsingItemSlocPlant(int sloc_id, int plant_id, int item_id, DateTime posting_date)
        {
            var x = _Generic.getBatchQuantityUsingItemSlocPlant(sloc_id, plant_id, item_id, posting_date);
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDocumentListinGI(string document_code, int plant_id)
        {
            var vm = _Generic.GetDocumentListinGI(document_code, plant_id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDocumentSelectedDetails(string document_code, int id)
        {
            var data = _Generic.getDocumentSelectedDetails(document_code, id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetplantIDByCode(string document_code, int document_id)
        {
            var data = _Generic.GetplantIDByCode(document_code, document_id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMachineListFromPlant(int id)
        {
            var vm = _Generic.GetMachineListFromPlant(id);
            return Json(vm, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetDashBoardData(string entity)
        {
            var sub = _Report_Service.GetDashBoardData(entity);
            return Json(sub, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CategoryListByPlant(int id, int plant_id)
        {
            var category = _Generic.CategoryListByPlant(id, plant_id);
            return Json(category, JsonRequestBehavior.AllowGet);
        }
        public int? GetOperatorEmployeeId(int user_id)
        {
            try
            {
                var employee_id = _Generic.GetOperatorEmployeeId(user_id);
                return employee_id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public ActionResult GetCreateStockSheet(int plant_id)
        {
            var data = _Generic.PlantwiseCreateStockSheet(plant_id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaxRate(int id)
        {
            var data = _Generic.GetTaxRate(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSalesPurchaseInvoicedetails(string entity, int entity_id)
        {
            var data = _Generic.GetSalesPurchaseInvoicedetails(entity, entity_id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShiftfromPlant(int id, DateTime posting_date)
        {
            var vm = _Generic.GetShiftfromPlant(id, posting_date);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShiftWithPlantId(int id)
            
        {
            var vm = _Generic.GetShiftfromPlant(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForCapitalizationeDetail(int entity_type_id, int entity_id)
        {
            var reconcile = _Generic.ForCapitalizationeDetail(entity_type_id, entity_id);
            return Json(reconcile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadExcelFormat(string col_list, string ctrlname)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add(ctrlname);
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            workSheet.Row(1).Height = 20;
            // workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            var i = 1;
            foreach (var list in col_list.Split(',').ToList())
            {
                var col_name = list;
                if (col_name.Contains("*"))
                {
                    col_name = col_name.Trim().TrimEnd('*');
                    workSheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                }
                workSheet.Cells[1, i].Value = col_name;
                workSheet.Column(i).AutoFit();
                i += 1;
            }
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + ctrlname + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return View();
        }
        public ActionResult sendSMS(string number, string message)
        {
            number = number == null ? "" : number;
            message = message == null ? "" : message;
            string reconcile = "";
            if (number == "")
            {
                reconcile = "Mobile No. is Blanck";
            }
            if (message == "")
            {
                reconcile = "Message is Blanck";
            }
            if (reconcile == "")
            {
                reconcile = _SMS.sendSMS(number, message);
            }
            return Json(reconcile, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShiftTimefromShift(int id)
        {
            var vm = _Generic.GetShiftTimefromShift(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMachineListWithOperationAndUser(int id)
        {
            var vm = _Generic.GetMachineListWithOperationAndUser(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCheck_Inventory(int item_id, int plant_id, int storage_location_id, int bucket_id, decimal quantity)
        {
            var vm = _Generic.GetCheck_Inventory(item_id, plant_id, storage_location_id, bucket_id, quantity);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReOrderLevelBy_id(int id)
        {
            var vm = _Generic.GetReOrderLevelBy_id(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public int GetReOrderCount()
        {
            return _Generic.GetReOrderCount();
            //var vm = _Generic.GetReOrderCount();
            //return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMachineListWithOperationAndUserForTemp(int id)
        {
            var vm = _Generic.GetMachineListWithOperationAndUserForTemp(id);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}
