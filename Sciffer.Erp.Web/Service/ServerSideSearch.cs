using System.Collections;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
namespace Sciffer.MovieScheduling.Web.Service
{
    public class ServerSideSearch
    {
        public IEnumerable ProcessDM(DataManager dm,IEnumerable data)
        {            
            DataOperations operation = new DataOperations();
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                data = operation.PerformSorting(data, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                dm.Where[0].Operator = "Contains";
                data = operation.PerformWhereFilter(data, dm.Where, dm.Where[0].Operator);
            }
            if (dm.Search != null && dm.Search.Count > 0) //Filtering
            {
                data = operation.PerformSearching(data, dm.Search);
            }           
            if (dm.Skip != 0)
            {
                data = operation.PerformSkip(data, dm.Skip);
            }
            if (dm.Take != 0)
            {
                data = operation.PerformTake(data, dm.Take);
            }
            return data;
        }
    }
}