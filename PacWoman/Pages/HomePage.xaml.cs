using PacWoman.GameServices;
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
                GameManager.Gameuser.Level = new Modles.GameLevel
                {
                    LevelId = 1, CountGhosts=2 , GhostsSpeed = 3
                };
                Frame.Navigate(typeof(GamePage));
            }
            else if (result == ContentDialogResult.Secondary)
            {
                GameManager.Gameuser.Level = new Modles.GameLevel
                {
                    LevelId = 2,
                    CountGhosts = 3,
                    GhostsSpeed = 3
                };
                Frame.Navigate(typeof(GamePage));
            }
            else
            {
                GameManager.Gameuser.Level = new Modles.GameLevel
                {
                    LevelId = 3,
                    CountGhosts = 4,
                    GhostsSpeed = 4
                };
                Frame.Navigate(typeof(GamePage));
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
