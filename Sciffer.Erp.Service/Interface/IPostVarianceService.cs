using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
    public interface IPostVarianceService
    {
        string Add(post_variance_vm vm);
        post_variance_vm Get(int id);
        List<post_variance_vm> GetAll();
    }
}
