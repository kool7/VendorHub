using System.ComponentModel.DataAnnotations;
using VendorHub.Api.Common;
using VendorHub.Application.Contracts.Vendors;
using VendorHub.Application.Vendors;
using VendorHub.Domain.Vendors;

namespace VendorHub.UnitTests.Services
{
    public class VendorServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendorRepository> _mockVendorRepository;
        private readonly VendorService _sutVendorService;

        public VendorServiceTests()
        {
            _fixture = new Fixture();
            _mockVendorRepository = new Mock<IVendorRepository>();
            _sutVendorService = new VendorService(_mockVendorRepository.Object);
        }

        [Fact]
        public async Task CreateVendorAsync_ValidInput_ReturnsVendor()
        {
            // Assert
            var vendor = _fixture.Create<Vendor>();
            var createVendorDto = _fixture.Create<CreateVendorDto>();
            _mockVendorRepository
                .Setup(repo => repo.AddAsync(vendor))
                .ReturnsAsync(vendor);

            // Act
            var result = _sutVendorService.CreateVendorAsync(createVendorDto);

            // Arrange
            result.Should().BeEquivalentTo(vendor);
            _mockVendorRepository.Verify(repo => repo.AddAsync(vendor), Times.Once);
        }

        [Fact]
        public async Task CreateVendorAsync_InValidInput_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = new CreateVendorDto
            {

            };

            var vendor = new Vendor
            {

            };

            // Act
            Func<Task> act = async () => await _sutVendorService.CreateVendorAsync(createVendorDto);

            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage(VendorHubResponse.VendorNameRequired)
                .WithMessage(VendorHubResponse.VendorDescriptionRequired);
            _mockVendorRepository.Verify(repo => repo.AddAsync(vendor), Times.Never);
        }

        [Fact]
        public async Task CreateVendorAsync_DuplicateEntry_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = _fixture.Create<CreateVendorDto>();

            // Act
            Func<Task> act = async () => await _sutVendorService.CreateVendorAsync(createVendorDto);

            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage(string.Format(VendorHubResponse.VendorAlreadyExists, createVendorDto.Name));
        }
    }
}
