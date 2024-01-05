using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IAccountAssignmentService:IDisposable
    {
        List<ref_account_assignment> GetAll();
        ref_account_assignment Get(int? id);
        bool Add(ref_account_assignment Assignment);
        bool Update(ref_account_assignment Assignment);
        bool Delete(int? id);
    }
}
