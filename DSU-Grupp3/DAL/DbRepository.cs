using DSU_Grupp3.Data;
using DSU_Grupp3.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DSU_Grupp3.DAL
{
    public class DbRepository : IDbRepository
    {
        private readonly ApplicationDbContext _context;
        public DbRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DeSoDTO>> GetDeSoDTO()
        {            
            return await _context.DeSoDTOs.Include(dto => dto.Areas).ToListAsync();
        }      
              
        
        public async Task SaveDeSoDTO(DeSoDTO desoInformation)
        {
            int count = await _context.DeSoDTOs.CountAsync();
            if (count == 0)
            {
                await _context.DeSoDTOs.AddAsync(desoInformation);

        }
            else
            {
                var existingDeSo = await _context.DeSoDTOs.Include(dto => dto.Areas).ToListAsync();
                foreach (var area in desoInformation.Areas)
                {
                    var existingArea = existingDeSo.SelectMany(dto => dto.Areas).FirstOrDefault(a => a.Deso == area.Deso);
                    if (existingArea != null)
                    {
                        existingArea.TotalPopulation = area.TotalPopulation;
                        existingArea.PopulationMales = area.PopulationMales;
                        existingArea.PopulationFemales = area.PopulationFemales;
                        existingArea.TotalVaccinated = area.TotalVaccinated;
                        existingArea.VaccinatedFemales = area.VaccinatedFemales;
                        existingArea.VaccinatedMales = area.VaccinatedMales;
                        existingArea.VaccinatedOtherGender = area.VaccinatedOtherGender;
                    }
                    else
                    {
                        var deSoDTO = new DeSoDTO { Areas = new List<DesoCodeInformation> { area } };
                        _context.DeSoDTOs.Add(deSoDTO);
                    }
                }
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
