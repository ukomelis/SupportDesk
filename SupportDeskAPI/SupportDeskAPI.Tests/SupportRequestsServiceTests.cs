using SupportDeskAPI.Models;
using SupportDeskAPI.Services;

namespace SupportDeskAPI.Tests
{
    [TestFixture]
    public class SupportRequestServiceTests
    {
        private SupportRequestService _supportRequestService;

        [SetUp]
        public void Setup()
        {
            _supportRequestService = new SupportRequestService();
        }

        [TearDown]
        public void TearDown()
        {
            var supportRequests = _supportRequestService.GetAll();
            foreach (var request in supportRequests)
            {
                _supportRequestService.Delete(request.Id);
            }
        }

        [Test]
        public void GetAll_ShouldReturnAllSupportRequests()
        {
            // Arrange
            var supportRequest1 = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };
            var supportRequest2 = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 2", Description = "Description 2", Deadline = DateTime.Now.AddDays(14), CreatedAt = DateTime.Now, Resolved = true };
            _supportRequestService.Create(supportRequest1);
            _supportRequestService.Create(supportRequest2);

            // Act
            var result = _supportRequestService.GetAll();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Does.Contain(supportRequest1));
            Assert.That(result, Does.Contain(supportRequest2));
        }

        [Test]
        public void GetById_ExistingId_ShouldReturnSupportRequest()
        {
            // Arrange
            var supportRequest = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };
            _supportRequestService.Create(supportRequest);

            // Act
            var result = _supportRequestService.GetById(supportRequest.Id);

            // Assert
            Assert.That(result, Is.EqualTo(supportRequest));
        }

        [Test]
        public void GetById_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = _supportRequestService.GetById(nonExistingId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Create_ShouldCreateSupportRequest()
        {
            // Arrange
            var supportRequest = new SupportRequest { Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };

            // Act
            var result = _supportRequestService.Create(supportRequest);

            // Assert
            Assert.IsNotNull(result.Id);
            Assert.Multiple(() =>
            {
                Assert.That(result.CreatedAt, Is.EqualTo(supportRequest.CreatedAt));
                Assert.That(result.Resolved, Is.EqualTo(supportRequest.Resolved));
            });
        }

        [Test]
        public void Update_ExistingSupportRequest_ShouldUpdateSupportRequest()
        {
            // Arrange
            var supportRequest = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };
            _supportRequestService.Create(supportRequest);

            // Update the support request
            supportRequest.Resolved = true;

            // Act
            var result = _supportRequestService.Update(supportRequest);

            // Assert
            Assert.That(result, Is.EqualTo(supportRequest));
            Assert.That(result.Resolved, Is.True);
        }

        [Test]
        public void Update_NonExistingSupportRequest_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var nonExistingSupportRequest = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };

            // Act and Assert
            Assert.Throws<KeyNotFoundException>(() => _supportRequestService.Update(nonExistingSupportRequest));
        }

        [Test]
        public void Resolve_ExistingId_ShouldResolveSupportRequest()
        {
            // Arrange
            var supportRequest = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };
            _supportRequestService.Create(supportRequest);

            // Act
            var result = _supportRequestService.Resolve(supportRequest.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(supportRequest.Resolved, Is.True);
            });
        }

        [Test]
        public void Resolve_NonExistingId_ShouldReturnFalse()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = _supportRequestService.Resolve(nonExistingId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Delete_ExistingId_ShouldDeleteSupportRequest()
        {
            // Arrange
            var supportRequest = new SupportRequest { Id = Guid.NewGuid(), Title = "Request 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now, Resolved = false };
            _supportRequestService.Create(supportRequest);

            // Act
            var result = _supportRequestService.Delete(supportRequest.Id);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_supportRequestService.GetById(supportRequest.Id), Is.Null);
        }

        [Test]
        public void Delete_NonExistingId_ShouldReturnFalse()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var result = _supportRequestService.Delete(nonExistingId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}