using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;
using sales_web_mvc.Models.ViewModels;
using sales_web_mvc.Services;

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

        public IActionResult Index()
        {
            return View(_service.FindAll());
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();

            var viewModel = new SellerFormViewModel();
            
            viewModel.Departments = departments;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _service.Insert(seller);

            return RedirectToAction(nameof(Index));
        }
    }
}