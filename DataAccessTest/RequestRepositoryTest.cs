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
        [TestMethod]
        public void GetAllRequestsTest()
        {
            Request request = new Request() { Id = Guid.NewGuid() };

            Mock<BuildingBossContext> _contextMock = new Mock<BuildingBossContext>();
            _contextMock.Setup(context => context.Requests).ReturnsDbSet(new List<Request> { request });
            RequestRepository requestRepository = new RequestRepository(_contextMock.Object);

            IEnumerable<Request> result = requestRepository.GetAllRequests();

            _contextMock.Verify(context => context.Requests);
            Assert.IsTrue(result.SequenceEqual(new List<Request> { request }));
        }

        [TestMethod]
        public void CreateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid() };

            Mock<BuildingBossContext> _contextMock = new Mock<BuildingBossContext>();
            _contextMock.Setup(context => context.Requests.Add(request));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);
            RequestRepository requestRepository = new RequestRepository(_contextMock.Object);

            Request result = requestRepository.CreateRequest(request);

            _contextMock.Verify(context => context.Requests.Add(request));
            Assert.AreEqual(request, result);
        }
    }
}
