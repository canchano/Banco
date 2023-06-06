

using ApiAplication.Domain.DTO;
using ApiAplication.Persistence;
using ApiAplication.Services.Implements;
using ApiAplication.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;

namespace ApiXUnitTest;

public class UnitTest1
{
    [Fact]
    public async Task GetCountrysOk()
    {
        Mock<ICountryRequesService> contryRequesServiceMock = new Mock<ICountryRequesService>();
        Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
        Mock <ILogService> logServiceMock = new Mock<ILogService>();



        configurationMock.Setup(c => c["CountryClient:Country"]).Returns("name/{name}");
        contryRequesServiceMock.Setup(c => c.GetRequestAsync(It.IsAny<string>())).ReturnsAsync(CountryStub.countriesOk);
        logServiceMock.Setup(l => l.SaveLog(It.IsAny<string>())).Returns(CountryStub.response);
        ICountryService service = new CountryServices(contryRequesServiceMock.Object, configurationMock.Object, logServiceMock.Object);
        IList<Country> response = await service.GetCountryAsync(It.IsAny<string>());
        response.Should().BeEquivalentTo(CountryStub.countriesOk);
    }

    /*[Fact]
    public void GetCountrysFail()
    {
        Mock<ICountryRequesService> contryRequesServiceMock = new Mock<ICountryRequesService>();
        contryRequesServiceMock.Setup(c => c.GetRequestAsync(It.IsAny<string>())).ReturnsAsync(CountryStub.countriesFail);
    }*/

}
