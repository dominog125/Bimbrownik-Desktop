using Bimbrownik_Desktop.Services;
using Xunit;

namespace Bimbrownik_Desktop.Tests;

public class LanguageServiceTests
{
    [Fact]
    public void GetDictionaryPath_ForPolish_ReturnsCorrectPath()
    {
        var service = new ResourceLanguageService();
        var result = service.GetDictionaryPath(LanguageCode.Polish);
        Assert.Equal("Resources/Strings.pl.xaml", result);
    }

    [Fact]
    public void GetDictionaryPath_ForEnglish_ReturnsCorrectPath()
    {
        var service = new ResourceLanguageService();
        var result = service.GetDictionaryPath(LanguageCode.English);
        Assert.Equal("Resources/Strings.en.xaml", result);
    }
}