using DateBaseProject;
using PacWoman.GameServices;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PacWoman.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
        }


        private void Back1Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));

        }

        private bool CheckValidateEmail(string email)
        {
            return true;
        }

        private bool CheckStrongPassword(string password)
        {
            return true;
            //return password.Length >= 12 && 
            //    password.Any(char.IsDigit)&&
            //    password.Any(char.IsUpper)&&
            //    password.Any(char.IsLower)&&
            //    password.Any(ch => !char.IsLetterOrDigit(ch));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //אחרי כל הבדיקות של הקלט 
            int? userId = Server.ValidateUser(userName.Text.Trim(), userPassword.Password);

        }

        private void exitsignupButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }

        private async void yesButton_Click(object sender, RoutedEventArgs e)
        {
            if (userName.Text == "" || userPassword.Password == "" || userEmail.Text == "" || userPasswordAgain.Password == "")
            {
                await new MessageDialog("Please fill in all fields.", "Error").ShowAsync();
            }
            else if (!CheckStrongPassword(userPassword.Password))
                await new MessageDialog("your password is not strong", "Error").ShowAsync();

            else if (userPassword.Password != userPasswordAgain.Password)
                await new MessageDialog("Passwords are not the same", "Error").ShowAsync();
            else if (!CheckValidateEmail(userEmail.Text))
            {
                await new MessageDialog("your email is wrong", "Error").ShowAsync();
            }
            else// הנתונים הוזנו כההלכה
            {// אנו פונים למסד הנתוינן על מנת לבדוק אם למשתמש קיים כבר חשבון שהקים בעבר
                // יש סימן שאלה אחרי האינט כי הוא יכול להיות null 
                int? userId = Server.ValidateUser(userName.Text.Trim(), userPassword.Password.Trim());
                if (userId == null)//המשתמש לא נמצא במסד הנתונים ולכן יש להוסיף אותו למאגר
                {
                    //מוסיפים תא המשתמש למסד הנתונים
                    Server.AddNewUser(userName.Text.Trim(), userPassword.Password.Trim(), userEmail.Text.Trim());
                    // Here you would typically add code to save the new user's information to a database or file.
                    await new MessageDialog("Sign up successful!", "Success").ShowAsync();
                }
                else
                {
                    newUserGrid.Visibility = Visibility.Collapsed;
                    //Frame.Navigate(typeof(HomePage));
                    await new MessageDialog(" you already  have an account, log in!").ShowAsync();
                }
            }
        }
    }
}
