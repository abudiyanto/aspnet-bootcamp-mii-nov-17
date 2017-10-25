using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootPress.Models
{
    public class News
    {
        [Key]
        public string Permalink { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Hightlight { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public DateTimeOffset Created { get; set; }
        public Status Status { get; set; }
        public ApplicationUser Author { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset Updated { get; set; }
        public double Price { get; set; }
    }
    public class Category
    {
        [Key]
        public string IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public enum Status
    {
        Published,
        Draft,
        Review
    }
}