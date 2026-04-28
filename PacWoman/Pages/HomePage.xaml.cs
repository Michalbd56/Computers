using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PacWoman.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private async void play_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog levelDialog = new ContentDialog()
            {
                Title = "איזה שלב תרצה לשחק?",
                PrimaryButtonText = "שלב 1",
                SecondaryButtonText = "שלב 2",
                CloseButtonText = "שלב 3"
            };

            ContentDialogResult result = await levelDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(GamePage), 1);
            }
            else if (result == ContentDialogResult.Secondary)
            {
                Frame.Navigate(typeof(GamePage), 2);
            }
            else
            {
                Frame.Navigate(typeof(GamePage), 3);
            }

        }

        private void store_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Shop));
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp));

        }
    }
}
