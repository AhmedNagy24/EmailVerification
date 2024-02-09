using System.Windows;
using System.Windows.Media.Imaging;
using EmailVerifier.GUI.View;

namespace EmailVerifier.GUI;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
// MainViewModel.cs
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Icon = new BitmapImage(new Uri("pack://application:,,,/icon.ico"));
        ContentPresenter.Content = new Verifier(this);
    }

    private void HomeRadioButton_OnChecked(object sender, RoutedEventArgs e)
    {
        if (ContentPresenter != null) ContentPresenter.Content = new Verifier(this);
    }
}