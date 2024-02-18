namespace DSU_Grupp3.Models.DTO
{
    public class QueryParameters
    {
        public List<string> MunicipalityCode { get; set; }
        public string MunicipalityFilter { get; set; } = "vs:CRegionKommun07";
        public List<string> DeSoCodes { get; set; }
        public List<string> Ages { get; set; }
        public string AgeFilter { get; set; } = "item";
        public List<string> Genders { get; set; }
        public List<string> Years { get; set; }
        public List<string> ContentsCode { get; set; }

    }
}
