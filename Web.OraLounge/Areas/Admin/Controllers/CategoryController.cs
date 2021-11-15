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
    //[Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
        // GET: Admin/Category
        #region MainCategories
        public async Task<ActionResult> Index()
        {
            var categories = await _unitOfWork.CategoryRepository.GetMainCategoriesAsync();
            return View(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories));
        }

        public ActionResult _SaveCategory()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveCategory(PostCategoryViewModel model)
        {
            var category = _mapper.Map<PostCategoryViewModel, Category>(model);
            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult _UpdateCategory(int id, string name)
        {
            var model = new PostCategoryViewModel() { Id = id, Name = name };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCategory(PostCategoryViewModel model)
        {
            var category = _mapper.Map<PostCategoryViewModel, Category>(model);
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.FindByIdAsync(id);
            if(category != null)
                _unitOfWork.CategoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        public async Task<ActionResult> SubCategory()
        {            
            TempData["Categories"] = await GetMainCategoriesSelectListAsync();

            var subCategories = await _unitOfWork.CategoryRepository.GetAllSubCategoriesAsync();
            return View(_mapper.Map<IEnumerable<Category>, IEnumerable<SubCategoryViewModel>>(subCategories));
        }

        public ActionResult _SaveSubCategory()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveSubCategory(PostSubCategoryViewModel model)
        {
            var category = _mapper.Map<PostSubCategoryViewModel, Category>(model);
            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("SubCategory");
        }

        public ActionResult _UpdateSubCategory(int id, string name, int parentId)
        {
            var model = new PostSubCategoryViewModel() { Id = id, Name = name, ParentId = parentId };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateSubCategory(PostSubCategoryViewModel model)
        {
            var category = _mapper.Map<PostSubCategoryViewModel, Category>(model);
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
           
            return RedirectToAction("SubCategory");
        }

        public async Task<ActionResult> DeleteSubCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.FindByIdAsync(id);
            if (category != null)
                _unitOfWork.CategoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("SubCategory");
        }


        private async Task<IEnumerable<SelectListItem>> GetMainCategoriesSelectListAsync()
        {
            var mainCategories = await _unitOfWork.CategoryRepository.GetMainCategoriesAsync();
            var categorySelectList = new List<SelectListItem>();
            foreach (var item in mainCategories)
            {
                categorySelectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return categorySelectList;
        }
    }
}