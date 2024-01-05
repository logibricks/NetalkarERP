using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPriceListCustomerService
    {
        List<price_list_customer_vm> GetAll();
        price_list_customer_vm Get(int id);
        bool Add(price_list_customer_vm vm);
        bool Update(price_list_customer_vm vm);
        bool Delete(int id);
        string AddExcel(List<ref_price_list_customer_vm> vm, List<ref_price_list_customer_detail_vm> vm1);
    }
}
