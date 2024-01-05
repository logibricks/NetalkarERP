using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
 public   interface IIssuePermitService
    {


        List<ref_permit_issue_VM> GetAll();
        ref_permit_issue_VM Get(int id);
        string Add(ref_permit_issue_VM country);
        string Update(ref_permit_issue_VM country);
        bool Delete(int id);
       // bool Dispose();
    }
}
