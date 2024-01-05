using Sciffer.Erp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IFrequencyService:IDisposable
    {
        List<ref_frequency> GetAll();
        ref_frequency Get(int? id);
        bool Add(ref_frequency Frequency);
        bool Update(ref_frequency Frequency);
        bool Delete(int? id);
    }
}
