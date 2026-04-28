using DateBaseProject;
using PacWoman.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PacWoman.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shop : Page
    {
        public Shop()
        {
            this.InitializeComponent();
        }

        

        private void Back2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));

        }

        private async void Buying(int price, string name)
        {
            if (GameManager.Gameuser.Score < price)
            {
                await new MessageDialog("The purchase is not possible. there isnt enough money"). ShowAsync();
            }
            else
            {
                List<string> myCharacters = Server.GetMyCharacters(GameManager.Gameuser.Id);//מקבלים רשימת דמויות שהמשתמש קנה בעבר
            if (myCharacters.Contains(name))// קנית כבר
                {
                    await new MessageDialog("The purchase is not possible. You have this character").ShowAsync();

                }
                else
                {
                    await new MessageDialog("congrats! the purchase is good").ShowAsync();
                    GameManager.Gameuser.CollectedCoins -= price;
                    GameManager.Events.OnUpdateScore.Invoke(GameManager.Gameuser.CollectedCoins);
                    CollectedCoins.Text= GameManager.Gameuser.CollectedCoins.ToString();
                    Server.AddUserSkin(price, name);
                    Frame.Navigate(typeof(HomePage));

                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CollectedCoins.Text = GameManager.Gameuser.CollectedCoins.ToString();

        }


    }
}
