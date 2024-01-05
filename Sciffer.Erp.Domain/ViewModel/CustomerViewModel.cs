using AutoMapper;
using Sciffer.Erp.Domain.Infrastructure.Mapping;
using Sciffer.Erp.Domain.Model;

namespace Sciffer.Erp.Domain.ViewModel
{
    public class CustomerViewModel : AudiTrailViewModel, IMapFrom<Customer>
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}