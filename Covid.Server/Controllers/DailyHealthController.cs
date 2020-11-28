using Covid.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Server.Controllers
{
    public class DailyHealthController : Controller
    {
        private readonly IDailyHealthRepository _dailyHealthRepository;

        public DailyHealthController(IDailyHealthRepository dailyHealthRepository)
        {
            _dailyHealthRepository = dailyHealthRepository;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}