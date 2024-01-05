using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;

namespace Sciffer.Erp.Web.Controllers
{
    public class CustomerComplaintController : Controller
    {
       
        private readonly ICustomerComplaintService _CustomerComplaint;
        private readonly IGenericService _Generic;
        public CustomerComplaintController(ICustomerComplaintService CustomerComplaint, IGenericService gen)
        {
            _CustomerComplaint = CustomerComplaint;
            _Generic = gen;
        }
        // GET: CustomerComplaint
        [CustomAuthorizeAttribute("CUSCOMP")]
        public ActionResult Index()
        {
            ViewBag.doc = TempData["doc_num"];
            ViewBag.DataSource = _CustomerComplaint.GetAll();
            return View();
        }

        // GET: CustomerComplaint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_customer_complaint ref_customer_complaint = _CustomerComplaint.Get((int)id);
            if (ref_customer_complaint == null)
            {
                return HttpNotFound();
            }
            return View(ref_customer_complaint);
        }

        // GET: CustomerComplaint/Create
        [CustomAuthorizeAttribute("CUSCOMP")]
        public ActionResult Create()
        {
            ViewBag.error = "";
           
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList().Where(a=>a.ITEM_CATEGORY_ID==2), "ITEM_ID", "ITEM_NAME");
            return View();
        }

        // POST: CustomerComplaint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ref_customer_complaint ref_customer_complaint)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _CustomerComplaint.Add(ref_customer_complaint);
                if(issaved=="Saved")
                {
                    TempData["doc_num"] = "Saved Successfully !";
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.error = issaved;
           
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList().Where(a => a.ITEM_CATEGORY_ID == 2), "ITEM_ID", "ITEM_NAME");
            return View(ref_customer_complaint);
        }

        // GET: CustomerComplaint/Edit/5
        [CustomAuthorizeAttribute("CUSCOMP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ref_customer_complaint ref_customer_complaint = _CustomerComplaint.Get((int)id);
            if (ref_customer_complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.error = "";
            ViewBag.taglist = _Generic.GetTagList().Where(x => x.tag_no != null && x.balance_qty == 0).Select(a => new { a.tag_id, a.tag_no, a.item_id }).ToList();
           
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList().Where(a => a.ITEM_CATEGORY_ID == 2), "ITEM_ID", "ITEM_NAME");
            return View(ref_customer_complaint);
        }

        // POST: CustomerComplaint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ref_customer_complaint ref_customer_complaint)
        {
            var issaved = "";
            if (ModelState.IsValid)
            {
                issaved = _CustomerComplaint.Update(ref_customer_complaint);
                if (issaved == "Saved")
                {
                    TempData["doc_num"] = "Updated Successfully !";
                    return RedirectToAction("Index");
                }

            }
            ViewBag.error = issaved;
           
            ViewBag.customerlist = new SelectList(_Generic.GetCustomerList(), "CUSTOMER_ID", "CUSTOMER_CODE");
            ViewBag.itemlist = new SelectList(_Generic.GetItemList().Where(a => a.ITEM_CATEGORY_ID == 2), "ITEM_ID", "ITEM_NAME");
            return View(ref_customer_complaint);
        }

        // GET: CustomerComplaint/Delete/5
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _CustomerComplaint.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
