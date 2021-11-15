using AutoMapper;
using Domain.OraLounge;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.OraLounge.Areas.Admin.Models;

namespace Web.OraLounge.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
        // GET: Admin/Menu
        public async Task<ActionResult> Index()
        {
            TempData["ProductCategories"] = await GetCategoriesWithSubSelectListAsync();
            var categories = await _unitOfWork.ProductRepository.GetAllProductsWithRelations();
            return View(_mapper.Map<IEnumerable<Product>, IEnumerable<MenuViewModel>>(categories));
        }

        public ActionResult _SaveMenu()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveMenu(PostMenuViewModel model)
        {
            var product = _mapper.Map<PostMenuViewModel, Product>(model);
            _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
            UploadImages(model.Files);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> _UpdateMenu(int id)
        {
            //try
            //{
            //    var client = new RestClient(baseUrl + "api/product/alldetail/" + id);
            //    client.Timeout = -1;
            //    var request = new RestRequest(Method.GET);
            //    IRestResponse response = client.Execute(request);
            //    if (response.IsSuccessful)
            //    {
            //        //Deserializing the response recieved from web api and storing into the list  
            //        var product = JsonConvert.DeserializeObject<PostMenuViewModel>(response.Content);
            //        return View(product);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var logger = NLog.LogManager.GetCurrentClassLogger();
            //    logger.Error(ex, "Got exception.");
            //}
            var product = await _unitOfWork.ProductRepository.GetProductWithImagesById(id);
            return PartialView(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateMenu(PostMenuViewModel model)
        {
            var product = _mapper.Map<PostMenuViewModel, Product>(model);
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            UploadImages(model.Files);
            RemoveImages(model.ImagesToDelete.Select(x => new ProductImage { Id = x }).ToList());
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteMenu(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductWithImagesById(id);
            if (product != null)
                _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            RemoveImages(product.Images.ToList());
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesWithSubSelectListAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetMainCategoriesWithSubCategories();
            var categorySelectList = new List<SelectListItem>();
            foreach (var item in categories)
            {
                categorySelectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                foreach (var child in item.Children)
                {
                    categorySelectList.Add(new SelectListItem { Text = " -- " + child.Name, Value = child.Id.ToString() });
                }
            }
            return categorySelectList;
        }

        private void UploadImages(HttpPostedFileBase[] files)
        {

        }

        private void RemoveImages(List<ProductImage> images)
        {

        }
    }
}