using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Interface
{
   public interface IProductionDownTimeService
    {
        string AddExcel(HttpPostedFileBase excelFile);
        string Add(List<prod_downtime_vm> DepParaArr);
        List<prod_downtime_vm> Get(DateTime prod_date);
        List<prod_downtime_vm> GetAll();
        
    }
}
