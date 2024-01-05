using System.ComponentModel.DataAnnotations;
namespace Sciffer.Erp.Domain.Model
{
    public class ref_gl_acount_type
    {
        [Key]
        public int gl_account_type_id { get; set; }
        [Display(Name ="Description *")]
        public string gl_account_type_description { get; set; }
        [Display(Name = "Indent *")]
        public string gl_account_type_indent { get; set; }
    }
}
