using System.Windows.Media.Imaging;

namespace MediaLibraryApp;

public sealed class CatalogItemViewModel
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string ShortDescription { get; init; } = string.Empty;
    public string StatusName { get; init; } = string.Empty;
    public int Rating { get; init; }
    public string Difficulty { get; init; } = string.Empty;
    public BitmapImage? Poster { get; init; }
    public bool HasImage => Poster is not null;
    public string RatingText => $"★ {Rating}/10";
}
