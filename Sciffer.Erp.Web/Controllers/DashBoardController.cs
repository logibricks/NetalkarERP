using Sciffer.Erp.Service.Interface;
using Sciffer.Erp.Web.CustomFilters;
using System.Web.Mvc;

namespace Sciffer.Erp.Web.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IGenericService _Generic;
        public DashBoardController(IGenericService Generic)
        {
            _Generic = Generic;
        }
        // GET: DashBoard
        [CustomAuthorizeAttribute("DSHB")]
        public ActionResult Index()
        {
            ViewBag.dashboardlist = _Generic.GetDashboardList(int.Parse(HttpContext.Session["User_Id"].ToString()));
            return View();
        }
    }
}