using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace EmailVerifier.GUI.View;

public partial class ProgressView : UserControl
{
    private readonly BackgroundWorker _worker;

    public ProgressView(MainWindow mainWindow, string dir, bool dnsCheck, bool formatCheck)
    {
        InitializeComponent();
        Window = mainWindow;
        _worker = new BackgroundWorker();
        _worker.WorkerReportsProgress = true;
        _worker.WorkerSupportsCancellation = true;
        _worker.DoWork += Worker_DoWork;
        _worker.ProgressChanged += Worker_ProgressChanged;
        _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        _worker.RunWorkerAsync(new object[] { dir, dnsCheck, formatCheck });
    }

    private MainWindow Window { get; }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        var arguments = (object[])e.Argument!;
        var dir = (string)arguments[0];
        var dnsCheck = (bool)arguments[1];
        var formatCheck = (bool)arguments[2];
        var system = new VerifierSystem();
        try
        {
            e.Result = system.Run(dnsCheck, formatCheck, dir, sender, e);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Result = null;
        }
    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        MyProgressBar.Value = e.ProgressPercentage;
        Percentage.Content = e.ProgressPercentage + "%";
        if (e.UserState != null) FileName.Text = "File: " + e.UserState;
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Error != null || e.Cancelled || e.Result == null)
        {
            Window.HomeRadioButton.IsChecked = true;
            Window.HomeRadioButton.IsEnabled = true;
            Window.ContentPresenter.Content = new Verifier(Window);
            return;
        }

        MessageBox.Show($"Emails verified successfully! {e.Result} emails removed.", "Success", MessageBoxButton.OK,
            MessageBoxImage.Information);
        Window.HomeRadioButton.IsChecked = true;
        Window.HomeRadioButton.IsEnabled = true;
        Window.ContentPresenter.Content = new Verifier(Window);
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_worker.IsBusy)
        {
            var cancel = MessageBox.Show("Are you sure you want to cancel?\nIt's not recommended!", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (cancel == MessageBoxResult.Yes)
            {
                _worker.CancelAsync();
                MessageBox.Show("Verification canceled successfully!", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Window.ContentPresenter.Content = new Verifier(Window);
            }
        }
    }
}