using GameEngine.Services;
using PacWoman.GameServices;
using System;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PacWoman.Pages
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private GameManager _gamemanager;
        public GamePage()
        {
            this.InitializeComponent();
            musicPlayer = new MediaPlayer();
            musicPlayer.IsLoopingEnabled = true;
        }
        private int _selectedLevel = 1;
        private MediaPlayer musicPlayer;
        private bool isPlaying = false;



        private void shopicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Shop));

        }

        private void homeicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));

        }

        private void settingicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _gamemanager = new GameManager(scene, _selectedLevel);
            Manager.Events.OnUpdateScore += ShowCollectedCoins;
            GameManager.Events.OnRemoveLifes += RemoveLifes;
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            // Hide game over overlay
            GameOverOverlay.Visibility = Visibility.Collapsed;

            // Reset hearts
            Heart1.Visibility = Visibility.Visible;
            Heart2.Visibility = Visibility.Visible;
            Heart3.Visibility = Visibility.Visible;

            // TODO: reset your game state
        }
        //private void RemoveLifes(int hearts)
        //{
        //    if(hearts==2)
        //    {
        //        Heart3.Visibility=Visibility.Collapsed;
        //    }
        //    if (hearts == 1) 
        //    {
        //        Heart2.Visibility=Visibility.Collapsed;
        //    }
        //    if (hearts == 0)
        //    {
        //        Heart1.Visibility=Visibility.Collapsed;
        //        GameOverOverlay.Visibility = Visibility.Visible;

        //    }
        //}
        private async void RemoveLifes(int hearts)
        {
            await Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                async () =>
                {
                    if (hearts == 2)
                    {
                        Heart3.Visibility = Visibility.Collapsed;
                    }
                    else if (hearts == 1)
                    {
                        Heart2.Visibility = Visibility.Collapsed;
                    }
                    else if (hearts == 0)
                    {
                        Heart1.Visibility = Visibility.Collapsed;

                        await ShowGameOverDialog();
                    }
                });
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                _selectedLevel = (int)e.Parameter;
            }
        }

        private async Task ShowGameOverDialog()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "המשחק נגמר",
                Content = "נגמרו לך כל החיים",
                PrimaryButtonText = "חזרה למסך הבית",
                CloseButtonText = "סגור"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Frame.Navigate(typeof(HomePage));
            }
        }


        private async void ShowCollectedCoins(int coins)
        {
            await Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {
                        txtCoins.Text = coins.ToString();
                    });
        }



        private void musicicon_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {
                musicPlayer.Source = MediaSource.CreateFromUri(
                    new Uri("ms-appx:///Assets/Sounds/Sidelines.mp3")
                );

                musicPlayer.Volume = 0.5;
                musicPlayer.Play();
                isPlaying = true;
            }
            else
            {
                musicPlayer.Pause();
                isPlaying = false;
            }
        }
    }
}
