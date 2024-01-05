using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Service.Interface
{
  public  interface IFreightTermsService: IDisposable
    {
        List<REF_FREIGHT_TERMS> GetAll();
        REF_FREIGHT_TERMS Get(int id);
        REF_FREIGHT_TERMS Add(REF_FREIGHT_TERMS country);
        REF_FREIGHT_TERMS Update(REF_FREIGHT_TERMS country);
        bool Delete(int id);
    }
}
