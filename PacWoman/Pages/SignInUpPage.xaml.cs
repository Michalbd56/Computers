using PacWoman.GameServices;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PacWoman.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class signInUpPage : Page
    {
        public signInUpPage()
        {
            this.InitializeComponent();
        }

        

        private void Back1Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));

        }


        private async void SIGNBtn_Click(object sender, RoutedEventArgs e)
            {
            if (userName.Text == "" || Password1.Password == "" || userEmail.Text == "" || Password2.Password == " ")
                {
                    await new MessageDialog("(Password1.Password))
                {Please fill in all fields.", "Error").ShowAsync();
                }
                else if (!CheckStrongPassword
                    await new MessageDialog("your password is not strong", "Error").ShowAsync();
                }
                else if (Password1.Password != Password2.Password)
                {
                    await new MessageDialog("Passwords are not the same", "Error").ShowAsync();
                }
                else if (!CheckValidEmail(userEmail.Text))
                {
                    await new MessageDialog("your email is wrong", "Error").ShowAsync();
                }
                else
                {// אנו פונים למסד הנתוינן על מנת לבדוק אם למשתמש קיים כבר חשבון שהקים בעבר
                int? userId = Server.ValidateUser(userName.Text.Trim(), UserNamePassword.Password.Trim());
                    if( userId == null)//המשתמש לא נמצא במסד הנתונים ולכן יש להוסיף אותו למאגר
                    {
                        //מוסיפים תא המשתמש למסד הנתונים
                        GameManager.GameUser = Server.AddNewUser(userName.Text.Trim(), userPassword.Password.Trim(), userEmail.Text.Trim());

                    
                    // Here you would typically add code to save the new user's information to a database or file.
                    await new MessageDialog("Sign up successful!", "Success").ShowAsync();
                    Frame.Navigate(typeof(HomePage));
                    }
                    else
                    {
                    await new MessageDialog("you must log in!").ShowAsync();
                    }



                }

            }
        private bool CheckValidateEmail(string email) 
        {
            return true;
        }

        private bool CheckStrongPassword(string password)
        {
            return true;
        }

            
        }
    }

