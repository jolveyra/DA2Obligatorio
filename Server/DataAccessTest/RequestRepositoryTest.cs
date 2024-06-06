
using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class RequestRepositoryTest
    {
        private Mock<BuildingBossContext> _contextMock;
        private RequestRepository _requestRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _contextMock = new Mock<BuildingBossContext>();
            _requestRepository = new RequestRepository(_contextMock.Object);
        }

        [TestMethod]
        public void GetAllRequestsTest()
        {
            Request request = new Request() { Id = Guid.NewGuid() };

            _contextMock.Setup(context => context.Requests).ReturnsDbSet(new List<Request> { request });

            IEnumerable<Request> result = _requestRepository.GetAllRequests();

            _contextMock.Verify(context => context.Requests);
            Assert.IsTrue(result.SequenceEqual(new List<Request> { request }));
        }

        [TestMethod]
        public void CreateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid() };

            _contextMock.Setup(context => context.Requests.Add(request));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            Request result = _requestRepository.CreateRequest(request);

            _contextMock.Verify(context => context.Requests.Add(request));
            _contextMock.Verify(context => context.SaveChanges());
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetRequestByIdTest()
        {
            Guid id = Guid.NewGuid();
            Request request = new Request() { Id = id };

            _contextMock.Setup(context => context.Requests).ReturnsDbSet(new List<Request> { request });

            Request result = _requestRepository.GetRequestById(id);

            _contextMock.Verify(context => context.Requests);
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetNonExistingRequestByIdTest()
        {
            Guid id = Guid.NewGuid();

            _contextMock.Setup(context => context.Requests).ReturnsDbSet(new List<Request>());

            Exception exception = null;

            try
            {
                _requestRepository.GetRequestById(id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNotNull(exception);
            Assert.AreEqual("Request not found", exception.Message);
        }

        [TestMethod]
        public void UpdateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid() };

            _contextMock.Setup(context => context.Requests.Update(request));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            Request result = _requestRepository.UpdateRequest(request);

            _contextMock.Verify(context => context.Requests.Update(request));
            _contextMock.Verify(context => context.SaveChanges());
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetAllRequestsWithBuildingTest()
        {
            Guid buildingId = Guid.NewGuid();
            Request request = new Request() { Id = Guid.NewGuid(), Flat = new Flat() { Building = new Building() { Id = buildingId, Name = "Mirador" } } };

            _contextMock.Setup(context => context.Requests).ReturnsDbSet(new List<Request> { request });

            IEnumerable<Request> result = _requestRepository.GetAllRequestsWithBuilding();

            _contextMock.Verify(context => context.Requests);
            Assert.IsTrue(result.SequenceEqual(new List<Request> { request }));
        }
    }
}
