using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUserRoleRightsService
    {
        List<ref_user_role_rights_VM> GetAllFromModules(int role_id);
       bool UpdateRecords(ref_user_role_rights_VM vm);

    }
}
