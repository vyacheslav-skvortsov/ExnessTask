using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Abbyy.Contento.Testing.Shared.WebApi.Client;
using Newtonsoft.Json;

namespace ExnessTask.WebApiClient
{
     public class WebApiClientBase
    {
        private string _baseUri;
        public WebApiClientBase(string baseUri)
        {
            if (string.IsNullOrEmpty(baseUri))
            {
                throw new NullReferenceException("baseUri is null or empty");
            }
            _baseUri = baseUri;
        }

        protected async Task<ApiResponse<TResponseModel>> GetAsync<TResponseModel>(string requestUri)
        {
            using (HttpClient httpClient = createHttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                return await getApiResponse<TResponseModel>(response);
            }
        }

        private async Task<ApiResponse<TResponseModel>> getApiResponse<TResponseModel>(
            HttpResponseMessage response)
        {
            string mediaType = null;

            if (response.Content.Headers.ContentType != null)
            {
                mediaType = response.Content.Headers.ContentType.MediaType.ToLower();
            }

            bool isSuccess = response.IsSuccessStatusCode
                             || response.StatusCode == HttpStatusCode.Found;

            var result = new ApiResponse<TResponseModel>
            {
                HttpStatusCode = response.StatusCode,
                IsSuccess = isSuccess,
                Headers = response.Headers
            };

            if (mediaType == null)
            {
                return result;
            }

            if (isSuccess)
            {
                if (mediaType == "application/json")
                {
                    string json = await response.Content.ReadAsStringAsync();

                    result.ResponseModel = JsonConvert.DeserializeObject<TResponseModel>(json/*, _jsonMediaTypeFormatter.SerializerSettings*/);
                }
                else
                {
                    throw new NotImplementedException(contentToString(response.Content));
                }
            }
            return result;
        }

        private string contentToString(HttpContent httpContent)
        {
            var readAsStringAsync = httpContent.ReadAsStringAsync();
            return readAsStringAsync.Result;
        }

        private HttpClient createHttpClient()
        {
            var httpClient = new HttpClient(createHttpClientHandler())
            {
                BaseAddress = new Uri(_baseUri)
            };
            return httpClient;
        }

        private WebRequestHandler createHttpClientHandler()
        {
            WebRequestHandler httpClientHandler = new WebRequestHandler()
            {
                AllowAutoRedirect = false //Для отслеживания не ожидаемых редиректов
            };
            httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Automatic;

            return httpClientHandler;
        }
    }
}
