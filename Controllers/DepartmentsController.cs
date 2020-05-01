using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Models;

namespace sales_web_mvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();

            list.Add(new Department { Id = 1, Name = "Desenvolvimento" });
            list.Add(new Department { Id = 2, Name = "Hardware" });

            return View(list);
        }
    }
}