namespace DSU_Grupp3.Models.DTO
{
    public class PopulationNumbersDeSo
    {
        public List<DesoCodePopulation> Data { get; set; }
        //public List<MetaData> MetaData { get; set; } // Vet inte om denna info behöver användas men ligger så länge för att kunna användas om det behövs
    }

    public class  DesoCodePopulation
    {   
        public List<string> Key { get; set; } 
        public List<string> Values { get; set; }
        
    }

    //public class MetaData
    //{
    //    public string Label { get; set; }
    //    public string Source { get; set; }
    //    public string Updated { get; set; }
    //    public string Infofile { get; set; }
    //}
}
