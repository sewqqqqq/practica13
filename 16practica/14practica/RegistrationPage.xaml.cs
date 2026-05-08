using System;
using System.Windows;

namespace MediaLibraryApp;

public partial class RegistrationPage : Window
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var fullName = FullNameTextBox.Text.Trim();
        var email = EmailTextBox.Text.Trim();
        var password = PasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;

        if (string.IsNullOrWhiteSpace(fullName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageBox.Show(
                "Заполните все поля регистрации.",
                "Проверка данных",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        if (!string.Equals(password, confirmPassword, StringComparison.Ordinal))
        {
            MessageBox.Show(
                "Пароли не совпадают.",
                "Проверка данных",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var catalogPage = new CatalogPage();
        catalogPage.Show();
        Close();
    }

    private void OpenLoginButton_Click(object sender, RoutedEventArgs e)
    {
        var loginPage = new LoginPage();
        loginPage.Show();
        Close();
    }
}
