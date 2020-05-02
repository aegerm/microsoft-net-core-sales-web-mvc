using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;
using sales_web_mvc.Models.ViewModels;
using sales_web_mvc.Services;
using sales_web_mvc.Services.Exceptions;
using SalesWeb.Models.ViewModels;

namespace sales_web_mvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService service, DepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.FindAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();

            var viewModel = new SellerFormViewModel();
            
            viewModel.Departments = departments;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (! ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();

                var viewModel = new SellerFormViewModel();

                viewModel.Seller = seller;
                viewModel.Departments = departments;

                return View(viewModel);
            }

            await _service.InsertAsync(seller);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var value = await _service.FinByIdAsync(id.Value);

            if (value == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found!" });
            }

            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return RedirectToAction(nameof(Index));
            }

            catch (IntegrityException e)
            {
                
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            var value = await _service.FinByIdAsync(id.Value);

            if (value == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found!" });
            }

            return View(value);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            var value = await _service.FinByIdAsync(id.Value);

            if (value == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found!" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            
            SellerFormViewModel viewModel = new SellerFormViewModel();

            viewModel.Seller = value;
            viewModel.Departments = departments;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (! ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();

                var viewModel = new SellerFormViewModel();

                viewModel.Seller = seller;
                viewModel.Departments = departments;

                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _service.UpdateAsync(seller);

                return RedirectToAction(nameof(Index));
            }

            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel();

            viewModel.Message = message;
            viewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(viewModel);
        }
    }
}