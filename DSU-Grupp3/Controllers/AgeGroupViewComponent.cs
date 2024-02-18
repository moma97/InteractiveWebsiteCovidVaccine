using DSU_Grupp3.DAL;
using DSU_Grupp3.Infrastructure;
using DSU_Grupp3.Models;
using DSU_Grupp3.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DSU_Grupp3.Controllers
{
    public class AgeGroupViewComponent : ViewComponent
    {
        private readonly IApiRepository _repo;
        public AgeGroupViewComponent(IApiRepository apiRepository)
        {
            _repo = apiRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {           
            var vaccinatedPeople = await _repo.GetVaccinatedPeople();           
            var model = new AgeGroupViewModel(vaccinatedPeople);
            return View(model);
        }

    }
}
