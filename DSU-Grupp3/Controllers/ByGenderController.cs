using DSU_Grupp3.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DSU_Grupp3.Controllers
{    
    [Route("[controller]")]
    
    public class ByGenderController : ControllerBase
    {
        
        private readonly IDbRepository _dbRepo;
        private readonly IApiRepository _apiRepo;
        public ByGenderController(IDbRepository dbRepo, IApiRepository apiRepo)
        {
            _dbRepo = dbRepo;
            _apiRepo = apiRepo;
        }

        [HttpGet("GetGenderData")]
        public async Task<IActionResult> GetGenderData()
        {
            var deso = await _dbRepo.GetDeSoDTO();            
            return Ok(deso);
        }
    }
}
