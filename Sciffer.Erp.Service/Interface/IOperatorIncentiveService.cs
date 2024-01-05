using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Service.Interface
{
  public  interface IOperatorIncentiveService
    {
        string UpdateRecords(int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id, DataTable IDT);
        string UpdateStatus(string status, int start_date_shift_id, int end_date_shift_id, DateTime from_date, DateTime to_date, string plant_id);
        string UploadEasyHrData(DataTable dt);
}
}
