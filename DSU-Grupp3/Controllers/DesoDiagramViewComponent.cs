using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DSU_Grupp3.Controllers
{
	public class DesoDiagramViewComponent : ViewComponent
	{
        private readonly IApiRepository _repo;
        public DesoDiagramViewComponent(IApiRepository apiRepository)
        {
            _repo = apiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {                                 
            var desoInfo = await _repo.GetTotalCombinedDesoData();
            var model = new DesoDiagramViewModel(desoInfo);
            return View(model);
        }        

    }

}
