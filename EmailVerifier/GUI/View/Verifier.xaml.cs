using System.Windows;
using System.Windows.Controls;
using EmailVerifier.Misc;
using Microsoft.Win32;

namespace EmailVerifier.GUI.View;

public partial class Verifier : UserControl
{
    public Verifier(MainWindow mainWindow)
    {
        InitializeComponent();
        Window = mainWindow;
    }

    public MainWindow Window { get; set; }

    private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dirDialog = new OpenFolderDialog();
        dirDialog.Multiselect = false;
        dirDialog.ShowDialog();
        var fileName = dirDialog.FolderName;
        DirName.Text = fileName;
    }

    private void VerifyButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dirName = DirName.Text;
        if (dirName == "")
        {
            MessageBox.Show("Please enter a file path before verifying!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        if (!CheckInternet.IsInternetAvailable())
        {
            MessageBox.Show("Please check your internet connection!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        if (DnsCheck.IsChecked != null && FormCheck.IsChecked != null)
        {
            if (DnsCheck.IsChecked == false && FormCheck.IsChecked == false)
            {
                MessageBox.Show("Please check at least one verification method!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            Dispatcher.InvokeAsync(() =>
            {
                Window.HomeRadioButton.IsChecked = false;
                Window.HomeRadioButton.IsEnabled = false;
                Window.ContentPresenter.Content = new ProgressView(Window, dirName, DnsCheck.IsChecked.Value,
                    FormCheck.IsChecked.Value);
            });
        }
    }
}