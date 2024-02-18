namespace DSU_Grupp3.Models.DTO
{
    public class PopulationNumbersMunicipality
    {        
        
            public List<Column> Columns { get; set; }
            public List<DataItem> Data { get; set; }        
       
    }
    public class Column
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Comment { get; set; }
        public string Type { get; set; }
    }

    public class DataItem
    {
        public List<string> Key { get; set; } //Kommunkod ÖStersund 2380
        public List<string> Values { get; set; } //FOlkmängd 
    }
}
