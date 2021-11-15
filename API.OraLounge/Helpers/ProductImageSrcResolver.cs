using API.OraLounge.Models;
using AutoMapper;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.OraLounge.Helpers
{
    public class ProductImageSrcResolver : IValueResolver<Product, ProductViewModel, List<string>>
    {
        public List<string> Resolve(Product source, ProductViewModel destination, List<string> destMember, ResolutionContext context)
        {
            var list = new List<string>();
            if (source.Images != null && source.Images.Count > 0)
                foreach (var item in source.Images)
                {
                    list.Add(GetFullImagePath(source.Id, item.Source));
                }
            else
                list.Add(GetFullImagePath(source.Id, string.Empty));          

            return list;
        }

        private string GetFullImagePath(int productId, string path)
        {
            var mainPath = "https://api.oralounge.co.uk/Content/Images/Products/";
            if (string.IsNullOrEmpty(path))
                return mainPath + "noimage.png";

            return mainPath + productId + "/" + path;
        }
    }
}