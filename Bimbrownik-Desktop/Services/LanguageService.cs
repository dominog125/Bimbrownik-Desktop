using System.Windows;

namespace Bimbrownik_Desktop.Services;

public enum LanguageCode
{
    English,
    Polish
}

public interface LanguageService
{
    void SetLanguage(LanguageCode language);
    LanguageCode CurrentLanguage { get; }
}

public class ResourceLanguageService : LanguageService
{
    private LanguageCode _currentLanguage = LanguageCode.English;

    public LanguageCode CurrentLanguage => _currentLanguage;

    public void SetLanguage(LanguageCode language)
    {
        _currentLanguage = language;

        var dict = new ResourceDictionary
        {
            Source = new Uri(GetDictionaryPath(language), UriKind.Relative)
        };

        var existing = Application.Current.Resources.MergedDictionaries
            .FirstOrDefault(d => d.Source?.OriginalString?.Contains("Strings.") == true);

        if (existing is not null)
            Application.Current.Resources.MergedDictionaries.Remove(existing);

        Application.Current.Resources.MergedDictionaries.Add(dict);
    }

    internal string GetDictionaryPath(LanguageCode language)
    {
        return language == LanguageCode.Polish
            ? "Resources/Strings.pl.xaml"
            : "Resources/Strings.en.xaml";
    }
}