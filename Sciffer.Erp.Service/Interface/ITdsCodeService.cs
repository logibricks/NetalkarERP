using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface ITdsCodeService:IDisposable
    {
        List<tds_codeVM> GetAll();
        List<tds_codeVM> getall();
        tds_codeVM Get(int id);
        bool Add(tds_codeVM TDSCode);       
        bool Delete(int id);
    }
}
