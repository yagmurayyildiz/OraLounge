using AutoMapper;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.OraLounge.Models;

namespace Web.OraLounge.Helpers
{
    public class ImageSrcResolver : IValueResolver<Product, ProductViewModel, string>
    {
        public string Resolve(Product source, ProductViewModel destination, string destMember, ResolutionContext context)
        {
            if (source.Images != null && source.Images.Count > 0)
                return GetFullImagePath(source.Id, source.Images.First().Source);
            return GetFullImagePath(source.Id, string.Empty);
        }

        private string GetFullImagePath(int productId, string path)
        {
            var mainPath = "/Content/Images/Products/";
            if (string.IsNullOrEmpty(path))
                return mainPath + "noimage.png";

            return mainPath + productId + "/" + path;
        }
    }
}