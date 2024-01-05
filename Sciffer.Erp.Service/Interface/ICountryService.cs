using System;
using System.Collections.Generic;
using Sciffer.Erp.Domain.Model;


namespace Sciffer.Erp.Service.Interface
{
    public interface ICountryService : IDisposable
    {
        List<REF_COUNTRY> GetAll();
        REF_COUNTRY Get(int id);
        REF_COUNTRY Add(REF_COUNTRY country);
        REF_COUNTRY Update(REF_COUNTRY country);
        bool Delete(int id);
    }
}
