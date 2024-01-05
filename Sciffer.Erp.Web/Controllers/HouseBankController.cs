using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class HouseBankController : Controller
    {
       
        private readonly IBankAccountService _bankservice;
        private readonly IAccountTypesService _accountTypeService;
        private readonly ICurrencyService _currencyService; 
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly IStateService _stateservice;
        private readonly ICountryService _countryservice;
        private readonly IGenericService _Generic;
        private readonly IBankService _bank;
        public HouseBankController(IBankAccountService BankService,IAccountTypesService AccountTypeService,ICurrencyService CurrencyService,IGeneralLedgerService GeneralLedgerService,IStateService StateService,ICountryService CountryService, IGenericService GenericService,
            IBankService bank)
        {
            _bankservice = BankService;
            _accountTypeService = AccountTypeService;
            _currencyService = CurrencyService;
            _generalLedgerService = GeneralLedgerService;
            _stateservice = StateService;
            _countryservice = CountryService;
            _Generic = GenericService;
            _bank = bank;
        }

        // GET: BankConfig
        [CustomAuthorizeAttribute("HBK")]
        public ActionResult Index()
        {
            ViewBag.Datasource= _bankservice.GetBankAccount();
            return View();
        }
       
      
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bankservice.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: BankAccount/Create
        [CustomAuthorizeAttribute("HBK")]
        public ActionResult Create()
        {           
            ViewBag.BankList = new SelectList(_Generic.GetBankList(), "bank_id", "bank_name");           
            ViewBag.BankAccountTypeNames = new SelectList(_accountTypeService.GetAll(), "ACCOUNT_TYPE_ID", "ACCOUNT_TYPE_NAME");
            ViewBag.BankCurrencyNames = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.BankRelatedGLNames = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.BankStateNames = new SelectList(_stateservice.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.BankCountryNames = new SelectList(_countryservice.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_bank_account_vm ref_bank_account)
        {          
            if (ModelState.IsValid)
            {
                var ISSAVED = _bankservice.Add(ref_bank_account);
                if (ISSAVED == true)
                {
                   return RedirectToAction("Index");
                }
            }
            ViewBag.BankList = new SelectList(_Generic.GetBankList(), "bank_id", "bank_name");
            ViewBag.BankAccountTypeNames = new SelectList(_accountTypeService.GetAll(), "ACCOUNT_TYPE_ID", "ACCOUNT_TYPE_NAME");
            ViewBag.BankCurrencyNames = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.BankRelatedGLNames = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.BankStateNames = new SelectList(_stateservice.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.BankCountryNames = new SelectList(_countryservice.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            return View(ref_bank_account);
        }

        // GET: Customer/Edit/5
        [CustomAuthorizeAttribute("HBK")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ref_bank_account = _bankservice.Get((int)id);
            ViewBag.BankList = new SelectList(_Generic.GetBankList(), "bank_id", "bank_name");
            ViewBag.BankAccountTypeNames = new SelectList(_accountTypeService.GetAll(), "ACCOUNT_TYPE_ID", "ACCOUNT_TYPE_NAME");
            ViewBag.BankCurrencyNames = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.BankRelatedGLNames = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.BankStateNames = new SelectList(_stateservice.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.BankCountryNames = new SelectList(_countryservice.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            return View(ref_bank_account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_bank_account_vm ref_bank_account)
        {
           // ref_bank_account.bank_id = int.Parse(ref_bank_account.bank_code.Split('(', ')')[3]);
            if (ModelState.IsValid)
            {
                var ISSAVED = _bankservice.Add(ref_bank_account);
                if (ISSAVED == true)
                {
                    return RedirectToAction("Index");
                }
            }
            var ref_bank_account1 = _bankservice.Get((int)ref_bank_account.bank_account_id);
            ViewBag.BankList = new SelectList(_Generic.GetBankList(), "bank_id", "bank_name");
            ViewBag.BankAccountTypeNames = new SelectList(_accountTypeService.GetAll(), "ACCOUNT_TYPE_ID", "ACCOUNT_TYPE_NAME");
            ViewBag.BankCurrencyNames = new SelectList(_Generic.GetCurrencyList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.BankRelatedGLNames = new SelectList(_Generic.GetLedgerAccount(2), "gl_ledger_id", "gl_ledger_name");
            ViewBag.BankStateNames = new SelectList(_stateservice.GetAll(), "STATE_ID", "STATE_NAME");
            ViewBag.BankCountryNames = new SelectList(_countryservice.GetAll(), "COUNTRY_ID", "COUNTRY_NAME");
            return View(ref_bank_account1);            
        }
        
        public ActionResult InlineDelete(int key)
        {
            var res = _bankservice.Delete(key);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
     
    }
}
