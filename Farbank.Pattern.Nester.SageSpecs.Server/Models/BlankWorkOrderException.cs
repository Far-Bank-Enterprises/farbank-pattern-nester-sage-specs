using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Farbank.Pattern.Nester.SageSpecs.Models
{
    [Table("BlankWorkOrderExceptions", Schema = "sagespecs")]
    public class BlankWorkOrderException
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string ScheduledSkipDate { get; set; }
    }
}

