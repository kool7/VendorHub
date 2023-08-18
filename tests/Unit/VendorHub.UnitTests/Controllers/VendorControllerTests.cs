using Microsoft.AspNetCore.Mvc;
using VendorHub.Api.Controllers;
using VendorHub.Application.Contracts.Common;
using VendorHub.Utils;

namespace VendorHub.UnitTests.Controllers;

public class VendorControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IVendorService> _mockVendorService;
    private readonly VendorsController _sutVendorsController;

    public VendorControllerTests()
    {
        _fixture = new Fixture();
        _mockVendorService = new Mock<IVendorService>();
        _sutVendorsController = new VendorsController(_mockVendorService.Object);
    }

    [Fact]
    public async Task CreateVendor_Success_ReturnsCreatedResponse()
    {
        // Arrange
        var createVendorDto = VendorHubFixtures.GenerateCreateVendorDto();
        var readVendorDto = Helper.MapToDto(createVendorDto);
        var resultModel = ResultModel<ReadVendorDto>.Success(readVendorDto);
        _mockVendorService
            .Setup(x => x.CreateAsync(createVendorDto))
            .ReturnsAsync(resultModel);

        // Act
        var result = await _sutVendorsController.CreateVendorAsync(createVendorDto);

        // Assert
        var response = result.Should().BeOfType<CreatedAtRouteResult>();
        response.Which.RouteName.Should().Be("GetVendor");
        response.Which.Value.Should().Be(readVendorDto);
    }

    [Fact]
    public async Task CreateVendor_FailureNameRequired_ReturnsBadRequestResponse()
    {
        // Arrange
        var inValidDto = VendorHubFixtures.GenerateInValidCreateVendorDto();
        var errors = new List<string> { VendorHubResponse.VendorNameRequired };
        _mockVendorService
                .Setup(x => x.CreateAsync(inValidDto))
                .ReturnsAsync(ResultModel<ReadVendorDto>.Error(errors));

        // Act
        var result = await _sutVendorsController.CreateVendorAsync(inValidDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var responseErrors = (result as BadRequestObjectResult).Value as List<string>;
        responseErrors.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public async Task CreateVendor_FailureDescriptionRequired_ReturnsBadRequestResponse()
    {
        // Arrange
        var inValidDto = VendorHubFixtures.GenerateInValidCreateVendorDto();
        var errors = new List<string> { VendorHubResponse.VendorDescriptionRequired };
        _mockVendorService
                .Setup(x => x.CreateAsync(inValidDto))
                .ReturnsAsync(ResultModel<ReadVendorDto>.Error(errors));

        // Act
        var result = await _sutVendorsController.CreateVendorAsync(inValidDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var responseErrors = (result as BadRequestObjectResult).Value as List<string>;
        responseErrors.Should().BeEquivalentTo(errors);
    }
}
