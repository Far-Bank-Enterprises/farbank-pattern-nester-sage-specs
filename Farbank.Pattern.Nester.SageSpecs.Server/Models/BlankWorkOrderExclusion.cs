using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Farbank.Pattern.Nester.SageSpecs.Models
{
    [Table("BlankWorkOrderExclusions", Schema = "sagespecs")]
    public class BlankWorkOrderExclusion
    {
        [Key]
        [Required]
        public string Sku { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}

