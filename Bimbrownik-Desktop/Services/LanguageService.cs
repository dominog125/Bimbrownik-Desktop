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
}

public class ResourceLanguageService : LanguageService
{
    public void SetLanguage(LanguageCode language)
    {
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

    public string GetDictionaryPath(LanguageCode language)
    {
        return language == LanguageCode.Polish
            ? "Resources/Strings.pl.xaml"
            : "Resources/Strings.en.xaml";
    }
}