using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.RequestsModels;

namespace ManagementApiTest
{
    [TestClass]
    public class RequestControllerTest
    {
        private Mock<IRequestLogic> requestLogicMock;
        private RequestController requestController;

        [TestInitialize]
        public void TestInitialize()
        {
            requestLogicMock = new Mock<IRequestLogic>(MockBehavior.Strict);
            requestController = new RequestController(requestLogicMock.Object);
        }

        [TestMethod]
        public void GetAllManagerRequestsTestOk()
        {
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid() },
                new Request { Id = Guid.NewGuid() }
            };

            requestLogicMock.Setup(r => r.GetAllRequests()).Returns(requests);

            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First()),
                new RequestResponseModel(requests.Last())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;

            OkObjectResult result = requestController.GetAllManagerRequests(It.IsAny<string>()) as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }

        [TestMethod]
        public void GetAllManagerRequestsByCategoryTestOk()
        {
            Category category = new Category { Name = "Electricity" };
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid(), Category = category },
                new Request { Id = Guid.NewGuid(), Category = new Category() { Name = "Plumbing" } }
            };

            requestLogicMock.Setup(r => r.GetAllRequests()).Returns(requests);

            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;

            OkObjectResult result = requestController.GetAllManagerRequests(category.Name) as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()));
        }

        [TestMethod]
        public void GetRequestByIdTestOk()
        {
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid(), Category = new Category { Name = "Electricity" } },
                new Request { Id = Guid.NewGuid(), Category = new Category() { Name = "Plumbing" } }
            };

            requestLogicMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(requests.First());

            OkObjectResult expected = new OkObjectResult(new RequestResponseModel(requests.First()));
            RequestResponseModel expectedObject = expected.Value as RequestResponseModel;

            OkObjectResult result = requestController.GetRequestById(requests.First().Id) as OkObjectResult;
            RequestResponseModel objectResult = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.Equals(objectResult));
        }

        [TestMethod]
        public void CreateRequestTestCreated()
        {
            RequestCreateModel requestCreateModel = new RequestCreateModel
            {
                Description = "Broken pipe",
                BuildingId = Guid.NewGuid(),
                FlatId = Guid.NewGuid(),
                CategoryName = "Plumbing"
            };
            Request expected = new Request
            {
                Description = requestCreateModel.Description,
                BuildingId = requestCreateModel.BuildingId,
                FlatId = requestCreateModel.FlatId,
                Category = new Category { Name = requestCreateModel.CategoryName }
            };
            requestLogicMock.Setup(r => r.CreateRequest(It.IsAny<Request>())).Returns(expected);

            RequestResponseModel expectedResult = new RequestResponseModel(expected); 
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateRequest", "CreateRequest", new { Id = expected.Id }, expectedResult);

            CreatedAtActionResult result = requestController.CreateRequest(requestCreateModel) as CreatedAtActionResult;
            RequestResponseModel resultObject = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }

        [TestMethod]
        public void UpdateRequestByIdTestOk()
        {
            RequestUpdateModel requestUpdateModel = new RequestUpdateModel
            {
                Description = "Broken pipe",
                CategoryName = "Plumbing",
                AssignedEmployeeId = Guid.NewGuid()
            };
            Request expected = new Request
            {
                Id = Guid.NewGuid(),
                Description = requestUpdateModel.Description,
                BuildingId = Guid.NewGuid(),
                FlatId = Guid.NewGuid(),
                Category = new Category { Name = requestUpdateModel.CategoryName },
                AssignedEmployeeId = requestUpdateModel.AssignedEmployeeId
            };
            requestLogicMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            RequestResponseModel expectedResult = new RequestResponseModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult result = requestController.UpdateRequestById(expected.Id, requestUpdateModel) as OkObjectResult;
            RequestResponseModel resultObject = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }
    }
}
