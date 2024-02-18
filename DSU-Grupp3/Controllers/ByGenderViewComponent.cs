using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Reflection;

namespace DSU_Grupp3.Controllers
{
    public class ByGenderViewComponent : ViewComponent
    {
        private readonly IApiRepository _repo;
        public ByGenderViewComponent(IApiRepository apiRepository)
        {
            _repo = apiRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var desoInformation = await _repo.GetTotalCombinedDesoData();
            var model = new ByGenderViewModel(desoInformation );
            return View(model);
        }
        
    }
}
