using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sciffer.Erp.Service.Interface
{
   public interface IInitialUploadService
    {
        string AddExcel(HttpPostedFileBase excelFile);
        List<ref_asset_initial_data_header_vm> GetAll();
        ref_asset_initial_data_header_vm Get(int id);
        string AddExcel(List<intial_upload_excel_vm> initial_upload, bool is_based_on_machine_code);
    }
}
