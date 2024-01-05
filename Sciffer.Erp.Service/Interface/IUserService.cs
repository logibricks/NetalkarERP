using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;

namespace Sciffer.Erp.Service.Interface
{
    public interface IUserService: IDisposable
    {
      
        List<REF_USER> GetAll();
        REF_USER Get(int id);
        bool Add(REF_USER cusotmerViewModel);
        bool Update(REF_USER cusotmerViewModel);
        bool Delete(int id);
    }
}
