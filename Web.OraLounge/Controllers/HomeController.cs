using AutoMapper;
using Domain.OraLounge;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.OraLounge.Models;

namespace Web.OraLounge.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public async Task<ActionResult> Menu()
        {
            var mainCategories = await _unitOfWork.CategoryRepository.GetMainCategoriesAsync();
            return View(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(mainCategories));
        }

        public async Task<ActionResult> _SubCategories(string parentId)
        {
            var subCategories = await _unitOfWork.CategoryRepository.GetSubCategoriesAsync(Convert.ToInt32(parentId));
            return PartialView(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(subCategories));
        }

        public async Task<ActionResult> _MenuItems(string categoryId)
        {
            var menuItems = await _unitOfWork.ProductRepository.GetProductsWithImagesByCategory(Convert.ToInt32(categoryId));
            return PartialView(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(menuItems));
        }

        public async Task<ActionResult> PdfMenu()
        {
            return View();
        }
    }
}