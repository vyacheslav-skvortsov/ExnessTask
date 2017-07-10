using System.Net;
using System.Net.Http.Headers;

namespace Abbyy.Contento.Testing.Shared.WebApi.Client
{
	public class ApiResponse<TResponseModel> : ApiResponse
	{
		public TResponseModel ResponseModel { get; set; }
	}

	public class ApiResponse
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public bool IsSuccess { get; set; }
		public byte[] Content { get; set; }
	    public HttpResponseHeaders Headers { get; set; }
    }
}