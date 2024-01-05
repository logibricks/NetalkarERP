using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Web.Controllers
{
    public class GSTTDSCodeController : Controller
    {
        private readonly IGSTTdsCodeService _tdsCodeService;
        private readonly IGenericService _Generic;
        private readonly IGeneralLedgerService _generalLedgerService;
        public GSTTDSCodeController(IGeneralLedgerService GeneralLedgerService, IGSTTdsCodeService TdsCodeService, IGenericService gen)
        {
            _tdsCodeService = TdsCodeService;
            _Generic = gen;
            _generalLedgerService = GeneralLedgerService;
        }
        // GET: GSTTDSCode
        public ActionResult Index()
        {
            ViewBag.DataSource = _tdsCodeService.GetAll();
            return View();
        }

        // GET: GSTTDSCode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_gst_tds_code_vm ref_tds_code = _tdsCodeService.Get((int)id);
            if (ref_tds_code == null)
            {
                return HttpNotFound();
            }

            ViewBag.credit_list = _Generic.GetLedgerAccount(2);
            return View(ref_tds_code);
        }

        // GET: GSTTDSCode/Create
        public ActionResult Create()
        {
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View();
        }

        // POST: GSTTDSCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_gst_tds_code_vm ref_gst_tds_code)
        {
            if (ModelState.IsValid)
            {
                var isValid = _tdsCodeService.Add(ref_gst_tds_code);
                if (isValid == "Saved")
                {
                    return RedirectToAction("Index");
                }
            }
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View(ref_gst_tds_code);
        }

        // GET: GSTTDSCode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_gst_tds_code_vm ref_tds_code = _tdsCodeService.Get((int)id);
            if (ref_tds_code == null)
            {
                return HttpNotFound();
            }
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View(ref_tds_code);
        }

        // POST: GSTTDSCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_gst_tds_code_vm ref_gst_tds_code)
        {         
           if (ModelState.IsValid)
            {
                var isValid = _tdsCodeService.Add(ref_gst_tds_code);
                if (isValid == "Saved")
                {
                    return RedirectToAction("Index");
                }
            }
            var gl = _Generic.GetLedgerAccount(2);
            ViewBag.credit_list = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            ViewBag.creditorgl = new SelectList(gl, "gl_ledger_id", "gl_ledger_name");
            return View(ref_gst_tds_code);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tdsCodeService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
