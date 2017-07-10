using System.Threading.Tasks;
using Abbyy.Contento.Testing.Shared.WebApi.Client;
using ExnessTask.DtoModels;

namespace ExnessTask.WebApiClient
{
    public class WebApiClient : WebApiClientBase
    {
        public WebApiClient(string baseUri) : base(baseUri){}

        public async Task<ApiResponse<VendorDto>> GetVendor(string vendorId)
        {
            return await GetAsync<VendorDto>($"vendor/{vendorId}");
        }
    }
}
