namespace Sciffer.Erp.Domain.Model
{
    public class Customer : AuditTrails
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}