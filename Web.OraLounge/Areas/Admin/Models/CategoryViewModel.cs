using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.OraLounge.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PostCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public int ParentId { get; set; }
    }

    public class PostSubCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ParentId { get; set; }
    }

    public class CategoryWithSubViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryWithSubViewModel> Children { get; set; }
    }
}