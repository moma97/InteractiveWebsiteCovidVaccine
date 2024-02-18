using System.Net;

namespace DSU_Grupp3.Models
{
	public class ApiResponse<T>
	{					
			public T Data { get; set; }	    
		    public List<T> DataList { get; set; }
		    
			public HttpResponseMessage Response { get; set; }
			public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; internal set; }
    }
}
