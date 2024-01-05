using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Interface
{
   public interface IProductionPlanService
    {
        string AddExcel(HttpPostedFileBase excelFile);
        List<prod_plan_detail_vm> GetAll();
        List<prod_plan_detail_vm> Get(DateTime prod_date);
        string Add(List<prod_plan_detail_vm> DepParaArr);
    }
}
