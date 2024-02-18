using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.Viewmodels;
using DSU_Grupp3.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DSU_Grupp3.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DSU_Grupp3.Controllers
{
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;
        private readonly IApiRepository _repo;

        public HomeController(ILogger<HomeController> logger, IApiRepository apiRepository)
        {
            _logger = logger;
            _repo = apiRepository;            
        }

        public async Task<IActionResult> Index()
        {
            await _repo.UpdateScheduled();            
            var desoInfo = await _repo.GetTotalCombinedDesoData();
            var model = new DesoDiagramViewModel(desoInfo);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}