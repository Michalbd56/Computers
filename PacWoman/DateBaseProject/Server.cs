using Microsoft.Data.Sqlite;
using PacWoman.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;

namespace DateBaseProject
{
    // המחלקה מאגדת פעולות המטפלות במסד נתונים: כותבות אליו, קוראות ממנו 
    public static class Server
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path; // נתיב מסד הנתונים במחשב
        private static string connectionString = "Filename=" + dbPath + "\\PacWomanDataBase.db"; //מתחבר למסד הנתונים

        public static GameUser AddNewUser(string userName, string userPassword, string userEmail)
        {
            string query = $"INSERT INTO [Users] (UserName,UserPassword,UserEmail) VALUES ('{userName}','{userPassword}','{userEmail}')";
            Execute(query);
            //שניתן למשתמש החדש Id כעת עלימו לברר מהו ה 
            int? userId = ValidateUser(userName, userPassword); 
            //User של המשתמש לאחר הוספתו לטבלת UserId קבלת 
                                                                
            //-------------------------------------------
            AddGameData(userId.Value); //הוספת נתוני ברירת מחדל לטבלאת gamedate  
            //AddUserProduct(userId.Value);
            return GetUser(userId.Value);


        }

        private static void AddGameData(int userId)
        {  
            string query = $"INSERT INTO [GameData] (UserId,CollectedCoins,MaxLevel,CurrentCharacter) VALUES ({userId},{0},{1},'{"Yellow"}')";
            Execute(query);
        }

        public static GameUser GetUser(int userId)
        {
            GameUser user = null;
            string query = $"SELECT UserId,UserName,UserEmail,UserPassword FROM [Users] WHERE UserId='{userId}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new GameUser
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Email = reader.GetString(2),
                        Password = reader.GetString(3)
                    };
                }
            }
            return user;
        }

        public static int? ValidateUser(string userName, string userPassword)
        {
            //הפעולה בודקת אם המשתמש הזין נתונים נכונים ונמצא במאגר משתמשים
            //של המשתמש UserId אם הכל תקין, הפעולה מחזירה את
            //null אם הנתונים אינם תקינים הפעולה מחזירה ערך
            //The SELECT statement is used to select data from a database
            string query = $"SELECT UserId FROM [Users] WHERE UserName='{userName}' AND UserPassword='{userPassword}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {

                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return null;
            }
        }

        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
    


