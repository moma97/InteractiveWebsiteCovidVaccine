using DSU_Grupp3.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DSU_Grupp3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {                
        }
        public DbSet<DeSoDTO> DeSoDTOs { get; set; }
        
    }
}
