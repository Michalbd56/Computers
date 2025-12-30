using GameEngine.Services;
using PacWoman.GameServices;
using System;
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
            _gamemanager = new GameManager(scene);
            Manager.Events.OnUpdateScore += ShowCollectedCoins;
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
