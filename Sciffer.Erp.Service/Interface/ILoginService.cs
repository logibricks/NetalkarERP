using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
   public interface ILoginService
    {
        bool CheckOperatorLogin(int user_id, string role_str);
        ref_user_management CheckLoginParameters(string user_name, string password);
        bool UpdatePassword(string Uname, string Password);
        int GetOpentaskcount(int user);
        int GetOverduetask(int user);


        //int GetPurchaseRequisitionAllCount(int user);
        int GetPurchaseRequisitionAllRejectAndApprovedcount(int user);
        int GetPurchaseRequisitionRejectedcount(int user);

        
        //int GetApprovedPurchaseOrderCount(int user);


        int GetPurchaseOrderAllRejectAndApprovedcount(int user);
        int GetPurchaseOrderAllRejectdcount(int user);

        
        int GetApprovedMRICount(int user);




    }
}
