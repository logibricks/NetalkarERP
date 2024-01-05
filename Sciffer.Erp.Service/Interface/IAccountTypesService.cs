using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
namespace Sciffer.Erp.Service.Interface
{
    public interface IAccountTypesService : IDisposable
    {
        List<REF_ACCOUNT_TYPE> GetAll();
        REF_ACCOUNT_TYPE Get(int id);
        bool Add(REF_ACCOUNT_TYPE BANK);
        bool Update(REF_ACCOUNT_TYPE BANK);
        bool Delete(int id);
    }

}