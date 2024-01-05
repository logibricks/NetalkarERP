using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Interface
{
    public interface IShiftWiseProductionService
    {
        string AddExcel(HttpPostedFileBase excelFile);
        List<shift_wise_prod_detail_vm> GetAll();
        List<shift_wise_prod_detail_vm> Get(DateTime prod_date);
        string Add(List<shift_wise_prod_detail_vm> DepParaArr);
    }
}
