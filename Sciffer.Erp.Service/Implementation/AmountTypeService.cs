using Sciffer.Erp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Data;

namespace Sciffer.Erp.Service.Implementation
{
    public class AmountTypeService : IAmountTypeService
    {
        private readonly ScifferContext _scifferContext;
        public AmountTypeService(ScifferContext scifferContext)
        {
            _scifferContext = scifferContext;
        }
        public List<ref_amount_type> GetAll()
        {
            return _scifferContext.ref_amount_type.ToList();
        }

        public int GetID(string st)
        {
            var X = _scifferContext.ref_amount_type.Where(x => x.amount_type == st).FirstOrDefault();
            var id = X == null ? "0" : X.amount_type_id.ToString();
            return int.Parse(id);
        }
    }
}
