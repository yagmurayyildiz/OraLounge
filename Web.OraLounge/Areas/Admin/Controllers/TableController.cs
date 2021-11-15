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
    public class TableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TableController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Admin/Table
        public async Task<ActionResult> Index()
        {
            var tables = await _unitOfWork.TableRepository.GetAllAsync();
            return View(_mapper.Map<IEnumerable<Table>, IEnumerable<TableViewModel>>(tables));
        }
        public ActionResult _SaveTable()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveTable(TableViewModel model)
        {
            var table = _mapper.Map<TableViewModel, Table>(model);
            _unitOfWork.TableRepository.Add(table);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> _UpdateTable(int id)
        {
            var table = await _unitOfWork.TableRepository.FindByIdAsync(id);
            return PartialView(_mapper.Map<Table, TableViewModel>(table));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateTable(TableViewModel model)
        {
            var table = _mapper.Map<TableViewModel, Table>(model);
            _unitOfWork.TableRepository.Update(table);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteTable(int id)
        {
            var table = await _unitOfWork.TableRepository.FindByIdAsync(id);
            if (table != null)
                _unitOfWork.TableRepository.Remove(table);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}