using API.OraLounge.Models;
using AutoMapper;
using Domain.OraLounge;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.OraLounge.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        [ResponseType(typeof(List<CategoryViewModel>))]
        public async Task<IHttpActionResult> Get()
        {
            var categories = await GetCategoriesAsync();
            return Ok(categories);
        }
        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            
            var mainCategories = await _unitOfWork.CategoryRepository.GetMainCategoriesWithProductsAsync();
            foreach (var item in mainCategories)
            {
                var children = await _unitOfWork.CategoryRepository.GetSubCategoriesWithProductsAsync(item.Id);
                item.Children = children;
            }
            var mappedCategories = _mapper.Map<List<Category>, List<CategoryViewModel>>(mainCategories);
            return mappedCategories;
        }
    }
}
