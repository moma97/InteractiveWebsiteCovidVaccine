using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models.DTO;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers.ViewComponents
{
    public class MapsViewComponent : ViewComponent
    {
        private readonly IApiRepository _apiRepository;
        public MapsViewComponent(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var desoInformation = await _apiRepository.GetTotalCombinedDesoData();
            var sites = await _apiRepository.GetSitesInformation();
            var model = new MapsViewModel(desoInformation, sites);
            return View(model);
        }

    }
}
