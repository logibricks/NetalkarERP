using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITaxElementService:IDisposable
    {
        List<tax_elementVM> GetAll();
        List<tax_elementVM> getall();
        tax_elementVM Get(int id);
        bool Add(tax_elementVM Tax);       
        bool Delete(int id);
    }
}
