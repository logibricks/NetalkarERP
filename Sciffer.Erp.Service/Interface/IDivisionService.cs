using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IDivisionService:IDisposable
    {
        List<REF_DIVISION> GetAll();
        REF_DIVISION Get(int id);
        bool Add(REF_DIVISION div);
        bool Update(REF_DIVISION dep);
        bool Delete(int id);
    }
}
