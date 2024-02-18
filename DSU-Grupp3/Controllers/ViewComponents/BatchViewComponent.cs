using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models.DTO;
using DSU_Grupp3.Models.Viewmodels;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers.ViewComponents
{
    public class BatchViewComponent : ViewComponent
    {
        private readonly IApiRepository _apiRepository;

        public BatchViewComponent(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var batchesInformation = await _apiRepository.GetBatchesInformation();            
            var model = new BatchViewModel(batchesInformation);
            return View(model);
        }                                        

    }
}
