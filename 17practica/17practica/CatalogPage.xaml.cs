using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MediaLibraryApp;

public partial class CatalogPage : Window
{
    private readonly ObservableCollection<CatalogItemViewModel> _items = new();

    private const string DefaultConnectionString =
        "Server=localhost;Database=13practicaa;Trusted_Connection=True;TrustServerCertificate=True;";

    public CatalogPage()
    {
        InitializeComponent();

        CatalogListView.ItemsSource = _items;

        LoadCatalog();
    }

    private void LoadCatalog()
    {
        _items.Clear();

        try
        {
            foreach (var item in ReadFromDatabase())
            {
                _items.Add(item);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Ошибка подключения к БД:\n{ex.Message}",
                "Каталог",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            for (var i = 1; i <= 8; i++)
            {
                _items.Add(new CatalogItemViewModel
                {
                    Id = i,
                    Title = $"Демо-фильм {i}",
                    ShortDescription = "Локальные данные",
                    StatusName = "Демо",
                    Rating = i % 10 + 1,
                    Difficulty = GetDifficultyLabel(i),
                    Poster = LoadLocalImage()
                });
            }
        }
    }

    private static IEnumerable<CatalogItemViewModel> ReadFromDatabase()
    {
        using var connection = new SqlConnection(DefaultConnectionString);

        connection.Open();

        const string query = """
            SELECT mi.id,
                   mi.title,
                   g.name AS genre_name,
                   s.name AS status_name,
                   mi.image
            FROM media_items mi
            LEFT JOIN genres g ON g.id = mi.genre_id
            LEFT JOIN statuses s ON s.id = mi.status_id
            ORDER BY mi.id;
            """;

        using var command = new SqlCommand(query, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);

            var title = reader.IsDBNull(1)
                ? "Без названия"
                : reader.GetString(1);

            var genre = reader.IsDBNull(2)
                ? "Жанр не указан"
                : reader.GetString(2);

            var status = reader.IsDBNull(3)
                ? "Без статуса"
                : reader.GetString(3);

            byte[]? imageBytes = null;

            if (!reader.IsDBNull(4))
            {
                imageBytes = (byte[])reader[4];
            }

            yield return new CatalogItemViewModel
            {
                Id = id,
                Title = title,
                ShortDescription = genre,
                StatusName = status,
                Rating = id % 10 + 1,
                Difficulty = GetDifficultyLabel(id),

                Poster = CreateImage(imageBytes) ?? LoadLocalImage()
            };
        }
    }

    private static string GetDifficultyLabel(int id)
    {
        return (id % 3) switch
        {
            0 => "Сложно",
            1 => "Средне",
            _ => "Легко"
        };
    }

    private static BitmapImage? CreateImage(byte[]? imageBytes)
    {
        if (imageBytes == null || imageBytes.Length == 0)
        {
            return null;
        }

        try
        {
            using var stream = new MemoryStream(imageBytes);

            var image = new BitmapImage();

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();

            image.Freeze();

            return image;
        }
        catch
        {
            return null;
        }
    }

    private static BitmapImage? LoadLocalImage()
    {
        try
        {
            return new BitmapImage(
                new Uri("pack://application:,,,/Assets/placeholder/film.png"));
        }
        catch
        {
            return null;
        }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        var loginPage = new LoginPage();

        loginPage.Show();

        Close();
    }
}