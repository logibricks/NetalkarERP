using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ISalesRMService
    {
        List<Sales_RM_VM> GetAll();
        ref_sales_rm Get(int id);
        bool Add(ref_sales_rm vm);
        bool Update(ref_sales_rm vm);
        bool Delete(int id);
    }
}
