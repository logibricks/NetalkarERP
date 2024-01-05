using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUserManagementService : IDisposable
    {
        List<ref_user_management_VM> GetAll();
        ref_user_management Get(int id);
        ref_user_management_VM Add(ref_user_management_VM ref_user_management_VM);
        ref_user_management_VM Update(ref_user_management_VM ref_user_management_VM);
        bool Delete(int id);

    }
}
