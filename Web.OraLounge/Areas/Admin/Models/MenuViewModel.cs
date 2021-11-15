using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.OraLounge.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IReadOnlyList<string> Images { get; set; }
    }

    public class PostMenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<MenuImage> Images { get; set; }
        public List<int> ImagesToDelete { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
    }

    public class MenuImage
    {
        public int Id { get; set; }
        public string Source { get; set; }
    }
}