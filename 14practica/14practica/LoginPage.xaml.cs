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

        MessageBox.Show(
            "Безопасная авторизация будет подключена на следующем этапе разработки.",
            "Вход в систему",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void OpenRegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        var registrationPage = new RegistrationPage();
        registrationPage.Show();
        Close();
    }
}
