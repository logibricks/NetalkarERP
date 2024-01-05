using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;

namespace Sciffer.Erp.Service.Interface
{
    public interface IFinancialYearService: IDisposable
    {
        List<REF_FINANCIAL_YEAR> GetAll();
        REF_FINANCIAL_YEAR Get(int id);
        REF_FINANCIAL_YEAR Add(REF_FINANCIAL_YEAR itemvaluation);
        REF_FINANCIAL_YEAR Update(REF_FINANCIAL_YEAR itemvaluation);
        bool Delete(int id);
    }
}
