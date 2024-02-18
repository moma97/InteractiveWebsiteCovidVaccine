using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models.DTO;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers.ViewComponents
{
    public class VaccineTypeViewComponent : ViewComponent
    {
        private readonly IApiRepository _repo;
        public VaccineTypeViewComponent(IApiRepository apiRepository)
        {
            _repo = apiRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vaccineBatches = await _repo.GetBatchesInformation();            
            var model = new VaccineTypeViewModel(vaccineBatches);
            return View(model);
        }
    }
}
