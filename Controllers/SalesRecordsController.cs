using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sales_web_mvc.Services;

namespace sales_web_mvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _recordServices;

        public SalesRecordsController(SalesRecordService recordServices)
        {
            _recordServices = recordServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (! minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (! maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _recordServices.FindByDateAsync(minDate, maxDate);

            return View(result);
        }
    }
}