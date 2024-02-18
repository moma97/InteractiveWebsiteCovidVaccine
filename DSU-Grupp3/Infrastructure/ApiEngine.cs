using DSU_Grupp3.Models;
using DSU_Grupp3.Models.DTO;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace DSU_Grupp3.Infrastructure
{
	public static class ApiEngine
	{
		public async static Task<ApiResponse<T>> Fetch<T>(string apiUrl)
		{
			using HttpClient client = new HttpClient();
			ApiResponse<T> apiResponse = new ApiResponse<T>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData != null)
                    {
                        try
                        {
                            apiResponse.Data = JsonConvert.DeserializeObject<T>(responseData);

                        }
                        catch (JsonException)
                        {
                            apiResponse.ErrorMessage = ErrorMessages.ApiDeserializingError;

                        }

                    }
                    

                }
                else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError)
                {
                    apiResponse.ErrorMessage = ErrorMessages.ApiResponseError;
                    apiResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    apiResponse.ErrorMessage = ErrorMessages.unknownError;
                    apiResponse.StatusCode = response.StatusCode;
                }

            }
            catch (HttpRequestException ex)
            {
                apiResponse.ErrorMessage = ErrorMessages.HttpServerError;
            }
            catch (Exception ex)
            {
                apiResponse.ErrorMessage = ErrorMessages.unknownError;
            }


            return apiResponse;
		}

        public async static Task<ApiResponse<T>> FetchArray<T>(string apiUrl)
        {
            using HttpClient client = new HttpClient();
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {

                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData != null)
                    {
                        try
                        {
                            apiResponse.DataList = JsonConvert.DeserializeObject<List<T>>(responseData);

                        }
                        catch (JsonException)
                        {
                            apiResponse.ErrorMessage = ErrorMessages.ApiDeserializingError;
                        }
                    }

                }
                else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError)
                {
                    apiResponse.ErrorMessage = ErrorMessages.ApiResponseError;
                    apiResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    apiResponse.StatusCode = response.StatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                apiResponse.ErrorMessage = ErrorMessages.HttpServerError;
            }
            catch (Exception ex)
            { 
              apiResponse.ErrorMessage = ErrorMessages.unknownError;
            }
            return apiResponse;
        }

        public async static Task<ApiResponse<T>> FetchScb<T>(string api, string query)
        {
            using HttpClient client = new HttpClient();
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(api, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (responseString != null)
                    {
                        try
                        {
                            apiResponse.Data = JsonConvert.DeserializeObject<T>(responseString);
                            return apiResponse;

                        }
                        catch (JsonException)
                        {

                            apiResponse.ErrorMessage = ErrorMessages.ApiDeserializingError;
                        }
                    }

                }
                else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError)
                {
                    apiResponse.ErrorMessage = apiResponse.ErrorMessage = ErrorMessages.ApiResponseError;
                    apiResponse.StatusCode = response.StatusCode;
                }
                else
                {
                    apiResponse.StatusCode = response.StatusCode;
                }

            
            }
            catch (HttpRequestException ex)            
            {

                  apiResponse.ErrorMessage = ErrorMessages.HttpServerError;
            }
            catch (Exception ex)
            {
                apiResponse.ErrorMessage = ErrorMessages.unknownError;
            }
              return apiResponse;

        }
    }
}
