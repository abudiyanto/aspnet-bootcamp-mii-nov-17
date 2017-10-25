using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootPress.Models
{
    public class NewsTransaction
    {
        [Key]
        public string IdTransaction { get; set; }
        public Staff Staff { get; set; }
        public ApplicationUser Writer { get; set; }
        public News News { get; set; }
        public DateTimeOffset Approved { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
    }
}