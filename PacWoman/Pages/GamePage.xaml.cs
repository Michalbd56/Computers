using DateBaseProject;
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

        // ── Pause / Resume ─────────────────────────────────────────────────────

        private Action _savedOnRun;

        private void PauseGame()
        {
            // Save the current run delegate and clear it — the timer Tick will do nothing
            _savedOnRun = Manager.Events.OnRun;
            Manager.Events.OnRun = null;
        }

        private void ResumeGame()
        {
            // Restore the run delegate so the game loop continues
            Manager.Events.OnRun = _savedOnRun;
        }

        // ── Navigation ─────────────────────────────────────────────────────────

        private void shopicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Shop));
        }

        private async void homeicon_Click(object sender, RoutedEventArgs e)
        {
            PauseGame();

            ContentDialog dialog = new ContentDialog
            {
                Title = "יציאה מהמשחק",
                Content = "האם אתה בטוח שאתה רוצה לצאת? ההתקדמות בשלב תאבד.",
                PrimaryButtonText = "כן, צא",
                CloseButtonText = "המשך לשחק"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Server.SaveGameData(GameManager.Gameuser);
                Frame.Navigate(typeof(HomePage));
            }
            else
            {
                // Player chose to stay — resume exactly where they left off
                ResumeGame();
            }
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
            GameManager.GameEvents.OnLevelComplete += OnLevelComplete;

            // Show carried-over score immediately when the page loads
            txtCoins.Text = GameManager.Gameuser.CollectedCoins.ToString();
        }

       

        // ── Win ────────────────────────────────────────────────────────────────

        private async void OnLevelComplete()
        {
            // RunAsync only accepts Action (not async lambda), so we fire a Task from inside
            await Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () => { _ = ShowLevelCompleteDialog(); });
        }

        private async Task ShowLevelCompleteDialog()
        {
            PauseGame();

            int nextLevel = _selectedLevel + 1;
            bool hasNextLevel = nextLevel <= GameManager.Gameuser.MaxLevel;
            int totalScore = GameManager.Gameuser.CollectedCoins;

            ContentDialog dialog = new ContentDialog
            {
                Title = "כל הכבוד!",
                Content = hasNextLevel
                    ? $"סיימת את שלב {_selectedLevel}! ניקוד: {totalScore}\nמעבר לשלב {nextLevel}."
                    : $"סיימת את כל השלבים! ניקוד סופי: {totalScore}",
                PrimaryButtonText = hasNextLevel ? $"שלב {nextLevel}" : "חזרה לתפריט",
                CloseButtonText = "תפריט ראשי"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary && hasNextLevel)
            {
                GameManager.Gameuser.Level.LevelId = nextLevel;
                GameManager.Gameuser.Level.CountGhosts = nextLevel + 1;
                GameManager.Gameuser.Level.GhostsSpeed += 1;
                Frame.Navigate(typeof(GamePage), nextLevel);
            }
            else
            {
                Server.SaveGameData(GameManager.Gameuser);
                Frame.Navigate(typeof(HomePage));
            }
        }

        // ── Lose ───────────────────────────────────────────────────────────────

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

        private async Task ShowGameOverDialog()
        {
            PauseGame();

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

        // ── Score display ──────────────────────────────────────────────────────

        private async void ShowCollectedCoins(int coins)
        {
            await Dispatcher.RunAsync(
                    Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {
                        txtCoins.Text = coins.ToString();
                    });
        }

        // ── Music ──────────────────────────────────────────────────────────────

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