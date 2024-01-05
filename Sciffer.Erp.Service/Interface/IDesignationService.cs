using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IDesignationService:IDisposable
    {
        List<REF_DESIGNATION> GetAll();
        REF_DESIGNATION Get(int id);
        bool Add(REF_DESIGNATION desig);
        bool Update(REF_DESIGNATION desig);
        bool Delete(int id);

    }
}
