﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciffer.Erp.Domain.Model
{
    public class pur_pi_form
    {
        [Key]
        [Column(Order = 0)]
        public int pi_id { get; set; }
        [ForeignKey("pi_id")]
        public virtual pur_pi pur_pi { get; set; }
        [Key]
        [Column(Order = 1)]
        public int form_id { get; set; }
        [ForeignKey("form_id")]
        public virtual REF_FORM REF_FORM { get; set; }
    }
}