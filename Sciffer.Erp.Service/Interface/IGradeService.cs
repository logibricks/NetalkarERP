using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IGradeService:IDisposable
    {
        List<REF_GRADE> GetAll();
        REF_GRADE Get(int id);
        bool Add(REF_GRADE grade);
        bool Update(REF_GRADE grade);
        bool Delete(int id);
    }
}
