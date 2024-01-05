using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IAmountTypeService
    {
        List<ref_amount_type> GetAll();
        int GetID(string st);
    }
}
