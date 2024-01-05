using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public  interface IFormService:IDisposable
    {
        List<REF_FORM> GetAll();
        REF_FORM Get(int id);
        REF_FORM Add(REF_FORM frm);
        REF_FORM Update(REF_FORM frm);
        bool Delete(int id);
    }
}
