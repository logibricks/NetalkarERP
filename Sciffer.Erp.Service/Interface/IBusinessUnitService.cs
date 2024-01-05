using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IBusinessUnitService:IDisposable
    {
        List<REF_BUSINESS_UNIT> GetAll();
        REF_BUSINESS_UNIT Get(int id);
        REF_BUSINESS_UNIT Add(REF_BUSINESS_UNIT business);
        REF_BUSINESS_UNIT Update(REF_BUSINESS_UNIT business);
        bool Delete(int id);
    }
}
