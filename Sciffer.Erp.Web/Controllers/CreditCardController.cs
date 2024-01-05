using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _creditcareservice ;      
        private readonly ICurrencyService _currencyService;
        private readonly IGeneralLedgerService _generalLedgerService;
        private readonly IStateService _stateservice;
        private readonly ICountryService _countryservice;
        private readonly IGenericService _Generic;
        private readonly IBankService _bankService;
        public CreditCardController(ICreditCardService CreditCardService,  ICurrencyService CurrencyService, IGeneralLedgerService GeneralLedgerService, 
            IStateService StateService, ICountryService CountryService, IGenericService gen, IBankService bank)
        {
            _creditcareservice = CreditCardService;
            _currencyService = CurrencyService;
            _generalLedgerService = GeneralLedgerService;
            _stateservice = StateService;
            _countryservice = CountryService;
            _Generic = gen;
            _bankService = bank;
        }

        // GET: CreditCard
        [CustomAuthorizeAttribute("CRCD")]
        public ActionResult Index()
        {
            ViewBag.DataSource = _creditcareservice.GetAll();
            return View();

        }
       
       
        // GET: CreditCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currencylist = _currencyService.GetAll();
            var relatedGLlist = _generalLedgerService.GetAll();
            var statenames = _stateservice.GetAll();
            var countrynames = _countryservice.GetAll();
            ViewBag.CurrencyNames = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.RelatedGLNames = new SelectList(relatedGLlist, "gl_ledger_id", "gl_ledger_name");
            ViewBag.StateNames = new SelectList(statenames, "STATE_ID", "STATE_NAME");
            ViewBag.CountryNames = new SelectList(countrynames, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.BankList = new SelectList(_bankService.GetAll(), "bank_id", "bank_name");
            ViewBag.bank_list = new SelectList(_Generic.GetBankForSearchingDropdown(), "bank_id", "bank_code");
            ViewBag.GL_list = new SelectList(_Generic.GetGLForSearchingDropdown(), "gl_ledger_id", "gl_ledger_code");
            var rEF_CREDIT_CARD = _creditcareservice.Get((int)id);
            if (rEF_CREDIT_CARD == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CREDIT_CARD);
        }

        // GET: CreditCard/Create
        [CustomAuthorizeAttribute("CRCD")]
        public ActionResult Create()
        {   
            var currencylist = _currencyService.GetAll();
            var relatedGLlist = _Generic.GetLedgerAccount(2);          
            var countrynames = _countryservice.GetAll();         
            ViewBag.CurrencyNames = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.RelatedGLNames = relatedGLlist;
            ViewBag.CountryNames = new SelectList(countrynames, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankForSearchingDropdown(), "bank_id", "bank_code");
            ViewBag.GL_list = new SelectList(_Generic.GetGLForSearchingDropdown(), "gl_ledger_id", "gl_ledger_code");
            ViewBag.BankList = _bankService.GetAll();
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( REF_CREDIT_CARD_VM rEF_CREDIT_CARD)
        {
            if (ModelState.IsValid)
            {
                var issaved = _creditcareservice.Add(rEF_CREDIT_CARD);
                if (issaved == true)
                    return RedirectToAction("Index");
            }
            var currencylist = _currencyService.GetAll();
            var relatedGLlist = _Generic.GetLedgerAccount(2);
            var countrynames = _countryservice.GetAll();
            ViewBag.CurrencyNames = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.RelatedGLNames = relatedGLlist;
            ViewBag.CountryNames = new SelectList(countrynames, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankForSearchingDropdown(), "bank_id", "bank_code");
            ViewBag.GL_list = new SelectList(_Generic.GetGLForSearchingDropdown(), "gl_ledger_id", "gl_ledger_code");
            ViewBag.BankList = _bankService.GetAll();
            return View(rEF_CREDIT_CARD);
        }

        // GET: CreditCard/Edit/5
        [CustomAuthorizeAttribute("CRCD")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currencylist = _currencyService.GetAll();
            var relatedGLlist = _Generic.GetLedgerAccount(2);
            var countrynames = _countryservice.GetAll();
            ViewBag.CurrencyNames = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.RelatedGLNames = relatedGLlist;
            ViewBag.CountryNames = new SelectList(countrynames, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankForSearchingDropdown(), "bank_id", "bank_code");
            ViewBag.GL_list = new SelectList(_Generic.GetGLForSearchingDropdown(), "gl_ledger_id", "gl_ledger_code");
            ViewBag.BankList = _bankService.GetAll();
           
            var rEF_CREDIT_CARD = _creditcareservice.Get((int)id);
            ViewBag.bank_name = rEF_CREDIT_CARD.bank_code + "-" + rEF_CREDIT_CARD.bank_name;
            ViewBag.ledger_name = rEF_CREDIT_CARD.gl_ledger_code + "-" + rEF_CREDIT_CARD.gl_ledger_name;
            if (rEF_CREDIT_CARD == null)
            {
                return HttpNotFound();
            }
          
            return View(rEF_CREDIT_CARD);
        }

        // POST: CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(REF_CREDIT_CARD_VM rEF_CREDIT_CARD)
        {
            if (ModelState.IsValid)
            {
                var isedit = _creditcareservice.Update(rEF_CREDIT_CARD);
                if (isedit == true)
                    return RedirectToAction("Index");
            }
            var currencylist = _currencyService.GetAll();
            var relatedGLlist = _Generic.GetLedgerAccount(2);
            var countrynames = _countryservice.GetAll();
            ViewBag.CurrencyNames = new SelectList(currencylist, "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.RelatedGLNames = relatedGLlist;
            ViewBag.CountryNames = new SelectList(countrynames, "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.bank_list = new SelectList(_Generic.GetBankForSearchingDropdown(), "bank_id", "bank_code");
            ViewBag.GL_list = new SelectList(_Generic.GetGLForSearchingDropdown(), "gl_ledger_id", "gl_ledger_code");
            ViewBag.BankList = _bankService.GetAll();

            //var rEF_CREDIT_CARD = _creditcareservice.Get((int)id);
            ViewBag.bank_name = rEF_CREDIT_CARD.bank_code + "-" + rEF_CREDIT_CARD.bank_name;
            ViewBag.ledger_name = rEF_CREDIT_CARD.gl_ledger_code + "-" + rEF_CREDIT_CARD.gl_ledger_name;
            return View(rEF_CREDIT_CARD);
        }

        // GET: CreditCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rEF_CREDIT_CARD = _creditcareservice.Get((int)id);
            if (rEF_CREDIT_CARD == null)
            {
                return HttpNotFound();
            }
            return View(rEF_CREDIT_CARD);
        }
        public ActionResult InLineDelete(int key)
        {
            var delete = _creditcareservice.Delete(key);
            return Json(delete, JsonRequestBehavior.AllowGet);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _creditcareservice.Dispose();
            }
            base.Dispose(disposing);
        }
      
        public ActionResult Data(REF_CREDIT_CARD_VM value)
        {
            var data = 0;
            var data1 = true;
            data = _Generic.CheckDuplicate(value.credit_card_number.ToString(),"", "", "creditcard", value.credit_card_id);


            bool duplicate = false;
            if (data > 0)
            {

                duplicate = false;
                return Json(duplicate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (value.credit_card_id == 0)
                {
                    data1 = _creditcareservice.Add(value);
                }
                else
                {
                    data1 = _creditcareservice.Update(value);
                }

                // duplicate = true;
                return Json(data1, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
