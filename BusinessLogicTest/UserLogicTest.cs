using Domain;
using BusinessLogic;
using RepositoryInterfaces;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {

        private Mock<ITokenRepository> iTokenRepositoryMock;
        private Mock<IUserRepository> iUserRepositoryMock;
        private UserLogic userLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            iTokenRepositoryMock = new Mock<ITokenRepository>(MockBehavior.Strict);
            iUserRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userLogic = new UserLogic(iUserRepositoryMock.Object, iTokenRepositoryMock.Object);
        }

        [TestMethod]
        public void GetUserRoleTestOk()
        {
            User user = new User() { Role = Role.Administrator };
            Guid id = user.Id;

            iTokenRepositoryMock.Setup(x => x.GetUserIdByToken(It.IsAny<Guid>())).Returns(id);
            iUserRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(user);

            string expected = user.Role.ToString();
            string result = userLogic.GetUserRoleByToken(It.IsAny<Guid>());

            iUserRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
        }
    }
}