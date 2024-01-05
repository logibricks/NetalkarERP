using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUserManagementRoleService : IDisposable
    {
        List<ref_user_management_role> GetAll();
        ref_user_management_role Get(int id);
        ref_user_management_role Add(ref_user_management_role ref_user_management_role);
        ref_user_management_role Update(ref_user_management_role ref_user_management_role);
        bool Delete(int id);


    }
}
