using System.Threading.Tasks;
using System.Web.Http;
using ExnessTask.DataRepository;
using ExnessTask.Helpers;

namespace ExnessTask.Controllers
{
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        protected VendorRepository vendorRepository;
        public VendorController()
        {
            vendorRepository=new VendorRepository(ConfigHelper.GetSetting("mongoConnectionString"));
        }


        // GET: api/vendor/{id}
        [Route("{id}")]
        public async Task<IHttpActionResult> GetVendor(string id)
        {
            var vendor = await vendorRepository.GetVendorById(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return Json(vendor);
        }
    }
}