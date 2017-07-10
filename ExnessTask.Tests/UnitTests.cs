using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using ExnessTask.Controllers;
using ExnessTask.DataRepository;
using ExnessTask.DtoModels;
using ExnessTask.Helpers;
using NUnit.Framework;

namespace ExnessTask.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private VendorRepository _vendorRepository;

        [Test]
        public void GetVendor_Success()
        {
            #region Presteps
            var vendorItem = new VendorDto()
            {
                VendorId = RandomHelper.GetGuid(),
                Name = RandomHelper.GetRandomString(5, "VendorName_"),
                Rating = RandomHelper.GetRandomInt(),
                Categories = new List<CategoryDto>()
                {
                    new CategoryDto()
                    {
                        CategoryId = RandomHelper.GetGuid(),
                        Name = RandomHelper.GetRandomString(5, "CategoryName_")
                    },
                    new CategoryDto()
                    {
                        CategoryId = RandomHelper.GetGuid(),
                        Name = RandomHelper.GetRandomString(5, "CategoryName_")
                    },
                }
            };
            _vendorRepository.AddVendor(vendorItem).GetAwaiter().GetResult();
            #endregion
            VendorController controller = new VendorController();

            IHttpActionResult response = controller.GetVendor(vendorItem.VendorId).GetAwaiter().GetResult();
            var jsonResult = response as JsonResult<VendorDto>;
            var vendorResult =  jsonResult.Content;
            Assert.That(vendorResult, NUnit.DeepObjectCompare.Is.DeepEqualTo(vendorItem), "Проверка модели ответа");
        }

        [Test]
        public void GetVendor_NotFound()
        {
            #region Presteps

            var vendorItem = new VendorDto()
            {
                VendorId = RandomHelper.GetGuid(),
                Name = RandomHelper.GetRandomString(5, "VendorName_"),
                Rating = RandomHelper.GetRandomInt(),
                Categories = new List<CategoryDto>()
                {
                    new CategoryDto()
                    {
                        CategoryId = RandomHelper.GetGuid(),
                        Name = RandomHelper.GetRandomString(5, "CategoryName_")
                    },
                    new CategoryDto()
                    {
                        CategoryId = RandomHelper.GetGuid(),
                        Name = RandomHelper.GetRandomString(5, "CategoryName_")
                    },
                }
            };
            _vendorRepository.AddVendor(vendorItem).GetAwaiter().GetResult();

            #endregion
            VendorController controller = new VendorController();

            IHttpActionResult response = controller.GetVendor(RandomHelper.GetGuid()).GetAwaiter().GetResult();
            var result = response as NotFoundResult;
            Assert.IsNotNull(result, "Проверка статуса ответа");
        }

        [SetUp]
        public void SetUp()
        {
            _vendorRepository.CleanUp().GetAwaiter().GetResult();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
           _vendorRepository = new VendorRepository(ConfigHelper.GetSetting("mongoConnectionString"));
        }
    }
}
