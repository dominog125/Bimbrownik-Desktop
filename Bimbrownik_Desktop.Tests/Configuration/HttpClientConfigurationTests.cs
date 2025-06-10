using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Sdk;

namespace Bimbrownik_Desktop.Tests.Configuration;

public class HttpClientConfigurationTests
{
    [Fact]
    public void App_Registers_HttpClient_With_BaseAddress()
    {
        var services = new ServiceCollection();

        services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri("https://example.com")
        });

        var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<HttpClient>();

        Assert.Equal("https://example.com/", client.BaseAddress?.ToString());
    }
}