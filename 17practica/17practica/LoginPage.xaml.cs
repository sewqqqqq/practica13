using System.Windows;

namespace MediaLibraryApp;

public partial class LoginPage : Window
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text.Trim();
        var password = PasswordInput.Password;

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show(
                "Заполните логин/email и пароль.",
                "Проверка данных",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var catalogPage = new CatalogPage();
        catalogPage.Show();
        Close();
    }

    private void OpenRegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        var registrationPage = new RegistrationPage();
        registrationPage.Show();
        Close();
    }
}
