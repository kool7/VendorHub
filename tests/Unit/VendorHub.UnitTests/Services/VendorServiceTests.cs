using MongoDB.Bson;
using VendorHub.Application.Contracts.Validations.Vendors;
using VendorHub.Application.Vendors;
using VendorHub.Domain.Vendors;
using VendorHub.Utils;

namespace VendorHub.UnitTests.Services
{
    public class VendorServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IVendorRepository> _mockVendorRepository;
        private readonly VendorService _sutVendorService;
        private readonly CreateVendorDtoValidator _createVendorDtoValidator;

        public VendorServiceTests()
        {
            _fixture = new Fixture();
            _mockVendorRepository = new Mock<IVendorRepository>();
            _createVendorDtoValidator = new CreateVendorDtoValidator();
            _sutVendorService = new VendorService(_mockVendorRepository.Object, _createVendorDtoValidator);
        }

        [Fact]
        public async Task CreateVendorAsync_ValidInput_ReturnsVendor()
        {
            // Assert
            var createVendorDto = VendorHubFixtures.GenerateCreateVendorDto();
            var vendor = _fixture.Build<Vendor>()
                                 .Without(p => p.Id)
                                 .Do(x => x.Id = ObjectId.GenerateNewId().ToString())
                                 .With(x => x.Name, createVendorDto.Name)
                                 .With(x => x.Description, createVendorDto.Description)
                                 .Create();

            _mockVendorRepository
                .Setup(repo => repo.CreateAsync(vendor))
                .ReturnsAsync(vendor);

            // Act
            var result = await _sutVendorService.CreateAsync(createVendorDto);

            // Arrange
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(vendor);
            _mockVendorRepository.Verify(repo => repo.CreateAsync(vendor), Times.Once);
        }

        [Fact]
        public async Task CreateVendorAsync_NamerRequired_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = VendorHubFixtures.GenerateCreateVendorDtoWithoutName();

            var vendor = _fixture.Create<Vendor>();

            // Act
            var result = await _sutVendorService.CreateAsync(createVendorDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.HasErrors.Should().Contain(VendorHubResponse.VendorNameRequired);
            _mockVendorRepository.Verify(repo => repo.CreateAsync(vendor), Times.Never);
        }

        [Fact]
        public async Task CreateVendorAsync_MaxNameLimit_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = VendorHubFixtures.GenerateDtoWithVendorNameMoreThanMaxCharLimit();

            var vendor = _fixture.Create<Vendor>();

            // Act
            var result = await _sutVendorService.CreateAsync(createVendorDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.HasErrors.Should().Contain(VendorHubResponse.VendorNameLimit);
            _mockVendorRepository.Verify(repo => repo.CreateAsync(vendor), Times.Never);
        }

        [Fact]
        public async Task CreateVendorAsync_InValidVendorName_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = VendorHubFixtures.GenerateInValidCreateVendorDto();

            var vendor = _fixture.Create<Vendor>();

            // Act
            var result = await _sutVendorService.CreateAsync(createVendorDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.HasErrors.Should().Contain(VendorHubResponse.VendorNameNotValid);
            _mockVendorRepository.Verify(repo => repo.CreateAsync(vendor), Times.Never);
        }

        [Fact]
        public async Task CreateVendorAsync_RequiredDescription_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = VendorHubFixtures.GenerateCreateVendorDtoWithoutDescription();
            var vendor = _fixture.Create<Vendor>();

            // Act
            var result = await _sutVendorService.CreateAsync(createVendorDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.HasErrors.Should().Contain(VendorHubResponse.VendorDescriptionRequired);
            _mockVendorRepository.Verify(repo => repo.CreateAsync(vendor), Times.Never);
        }

        [Fact]
        public async Task CreateVendorAsync_DuplicateEntry_ThrowValidationException()
        {
            // Arrange
            var createVendorDto = VendorHubFixtures.GenerateCreateVendorDto();

            // Act
            var result = await _sutVendorService.CreateAsync(createVendorDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.HasErrors.Should().Contain(VendorHubResponse.VendorAlreadyExists, createVendorDto.Name);
        }
    }
}
