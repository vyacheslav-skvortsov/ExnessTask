using System.Collections.Generic;
using System.Net;
using ExnessTask.DataRepository;
using ExnessTask.DtoModels;
using ExnessTask.Helpers;
using NUnit.Framework;

namespace ExnessTask.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private VendorRepository _vendorRepository;
        private WebApiClient.WebApiClient _apiClient;

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
                    }
                }
            };
            _vendorRepository.AddVendor(vendorItem).GetAwaiter().GetResult();
            #endregion

            var vendorRespone = _apiClient.GetVendor(vendorItem.VendorId).GetAwaiter().GetResult();

            Assert.IsTrue(vendorRespone.IsSuccess, "Проверка удачного ответа");
            Assert.AreEqual(HttpStatusCode.OK, vendorRespone.HttpStatusCode, "Проверка кода ответа");
            Assert.That(vendorRespone.ResponseModel, NUnit.DeepObjectCompare.Is.DeepEqualTo(vendorItem), "Проверка модели ответа");
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
                    }
                }
            };
            _vendorRepository.AddVendor(vendorItem).GetAwaiter().GetResult();
            #endregion

            var vendorRespone = _apiClient.GetVendor(RandomHelper.GetGuid()).GetAwaiter().GetResult();

            Assert.IsFalse(vendorRespone.IsSuccess, "Проверка неудачного ответа");
            Assert.AreEqual(HttpStatusCode.NotFound, vendorRespone.HttpStatusCode, "Проверка кода ответа");
        }

        [Test]
        public void GetVendor_EmptyVendor()
        {
            #region Presteps
            #endregion

            var vendorRespone = _apiClient.GetVendor("").GetAwaiter().GetResult();

            Assert.IsFalse(vendorRespone.IsSuccess, "Проверка неудачного ответа");
            Assert.AreEqual(HttpStatusCode.NotFound, vendorRespone.HttpStatusCode, "Проверка кода ответа");
        }

        [Test, Description("Тест проверяет ответ сервера на запросе к скрытому адресу")]
        public void GetVendor_UnexpectedUri()
        {
            #region Presteps
            #endregion

            var vendorRespone = _apiClient.GetApi("").GetAwaiter().GetResult();

            Assert.IsFalse(vendorRespone.IsSuccess, "Проверка неудачного ответа");
            Assert.AreEqual(HttpStatusCode.NotFound, vendorRespone.HttpStatusCode, "Проверка кода ответа");
        }

        [SetUp]
        public void SetUp()
        {
            _vendorRepository.CleanUp().GetAwaiter().GetResult();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _apiClient = new WebApiClient.WebApiClient(ConfigHelper.GetSetting("webApiUri"));
            _vendorRepository = new VendorRepository(ConfigHelper.GetSetting("mongoConnectionString"));
        }
    }
}
