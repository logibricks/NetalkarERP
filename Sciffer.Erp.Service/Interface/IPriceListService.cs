using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPriceListService
    {

        List<price_list_vendor_vm> GetAll();
        price_list_vendor_vm Get(int id);
        bool Add(price_list_vendor_vm vm);
        string Update(price_list_vendor_vm vm);
        bool Delete(int id);
        string AddExcel(List<ref_price_list_vendor_vm> vm, List<ref_price_list_vendor_detail_vm> vm1);
    }
}
