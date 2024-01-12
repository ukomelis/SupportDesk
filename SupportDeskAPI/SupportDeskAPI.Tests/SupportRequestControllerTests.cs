using Moq;
using SupportDeskAPI.Controllers;
using SupportDeskAPI.Services;
using Microsoft.AspNetCore.Mvc;
using SupportDeskAPI.Models;

namespace SupportDeskAPI.Tests
{
    public class SupportRequestsControllerTests
    {
        private SupportRequestsController _controller;
        private Mock<ISupportRequestService> _service;
        private SupportRequest _request;
        private Guid _id;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<ISupportRequestService>();
            _controller = new SupportRequestsController(_service.Object);
            _request = new SupportRequest
            {
                Title = "Test Title",
                Description = "Test Description",
                Deadline = DateTime.Now.AddDays(7)
            };
            _id = Guid.NewGuid();
        }

        [Test]
        public void Get_ReturnsOkResult_WhenServiceDoesNotThrowException()
        {
            _service.Setup(s => s.GetAll()).Returns(new List<SupportRequest> { _request });

            var result = _controller.Get();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetById_ReturnsOkResult_WhenServiceDoesNotThrowException()
        {
            _service.Setup(s => s.GetById(_id)).Returns(_request);

            var result = _controller.Get(_id);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void Post_ReturnsCreatedAtAction_WhenServiceDoesNotThrowException()
        {
            _service.Setup(s => s.Create(_request)).Returns(_request);

            var result = _controller.Post(_request);

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public void Put_ReturnsNoContent_WhenServiceDoesNotThrowException()
        {
            _service.Setup(s => s.Update(_request));

            var result = _controller.Put(_request);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Delete_ReturnsNoContent_WhenSupportRequestExists()
        {
            _service.Setup(s => s.Delete(_id)).Returns(true);

            var result = _controller.Delete(_id);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Delete_ReturnsNotFound_WhenSupportRequestDoesNotExist()
        {
            _service.Setup(s => s.Delete(_id)).Returns(false);

            var result = _controller.Delete(_id);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Resolve_ReturnsNoContent_WhenSupportRequestExists()
        {
            _service.Setup(s => s.Resolve(_id)).Returns(true);

            var result = _controller.Resolve(_id);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Resolve_ReturnsNotFound_WhenSupportRequestDoesNotExist()
        {
            _service.Setup(s => s.Resolve(_id)).Returns(false);

            var result = _controller.Resolve(_id);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}