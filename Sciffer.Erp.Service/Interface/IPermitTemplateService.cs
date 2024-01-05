using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
   public interface IPermitTemplateService: IDisposable
    {
        


        List<Ref_permit_template_VM> GetAll();
        Ref_permit_template_VM Get(int id);
        bool Add(Ref_permit_template_VM country);
        bool Update(Ref_permit_template_VM country);
        bool Delete(int id);
       

        
    }
}
