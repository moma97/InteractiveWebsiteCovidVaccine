namespace DSU_Grupp3.Models.DTO
{
    public class QueryRequest
    {
        public QueryRequest(QueryParameters queryParameters)
        {
            Query = new List<QueryList>();
            if (queryParameters.DeSoCodes != null && queryParameters.DeSoCodes.Any())
            {
                Query.Add(new QueryList
                {


                    Code = "Region",
                    Selection = new Selection
                    {
                        Filter = "vs:DeSoHE",
                        Values = queryParameters.DeSoCodes
                    }

                });
            }
            if (queryParameters.MunicipalityCode != null && queryParameters.MunicipalityCode.Any())
            {
                Query.Add(new QueryList
                {


                    Code = "Region",
                    Selection = new Selection
                    {
                        Filter = queryParameters.MunicipalityFilter,
                        Values = queryParameters.MunicipalityCode
                    }

                });
            }
            if (queryParameters.Ages != null && queryParameters.Ages.Any())
            {
                Query.Add(new QueryList
                {


                    Code = "Alder",
                    Selection = new Selection
                    {   
                        
                        Filter = queryParameters.AgeFilter,
                        Values = queryParameters.Ages
                    }

                });
            }
            if (queryParameters.Genders != null && queryParameters.Genders.Any())
            {
                Query.Add(new QueryList
                {


                    Code = "Kon",
                    Selection = new Selection
                    {
                        Filter = "item",
                        Values = queryParameters.Genders
                    }

                });
            }
            if (queryParameters.ContentsCode != null && queryParameters.ContentsCode.Any())
            {
                Query.Add(new QueryList
                {
                    Code = "ContentsCode",
                    Selection = new Selection
                    {
                        Filter = "item",
                        Values = queryParameters.ContentsCode
                    }
                });
            }
            if (queryParameters.Years != null && queryParameters.Years.Any())
            {
                Query.Add(new QueryList
                {


                    Code = "Tid",
                    Selection = new Selection
                    {
                        Filter = "item",
                        Values = queryParameters.Years
                    }

                });
            }

            Response = new Response();
        }

        public List<QueryList> Query { get; set; }

        public Response Response { get; set; } = new Response();

    }

    public class QueryList
    {
        public string Code { get; set; }
        public Selection Selection { get; set; }

    }
    public class Selection
    {
        public string Filter { get; set; }
        public List<string> Values { get; set; }
    }

    public class Response
    {

        public string Format { get; set; } = "json";
    }

}
