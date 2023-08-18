using AutoFixture;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.Utils;

public static class VendorHubFixtures
{
    private static readonly Fixture _fixture = new();

    public static CreateVendorDto GenerateCreateVendorDto()
    {
        return _fixture.Build<CreateVendorDto>()
                       .With(v => v.Name, _fixture
                       .Create<string>()
                       .Replace("-", ""))
                       .Create();
    }

    public static CreateVendorDto GenerateCreateVendorDtoWithoutName()
    {
        return _fixture
            .Build<CreateVendorDto>()
            .Without(v => v.Name)
            .Create();
    }

    public static CreateVendorDto GenerateCreateVendorDtoWithoutDescription()
    {
        return _fixture
            .Build<CreateVendorDto>()
            .Without(v => v.Description)
            .Create();
    }

    public static CreateVendorDto GenerateInValidCreateVendorDto()
    {
        return _fixture.Create<CreateVendorDto>();
    }

    public static CreateVendorDto GenerateDtoWithVendorNameMoreThanMaxCharLimit()
    {
        var longName = _fixture.Create<string>().PadRight(51, 'X');

        return _fixture.Build<CreateVendorDto>()
                       .With(dto => dto.Name, longName)
                       .Create();
    }
}
