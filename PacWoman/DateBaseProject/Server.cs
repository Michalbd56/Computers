using Microsoft.Data.Sqlite;
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
        public static int? ValidateUser(string Name, string Password)
        {
            //הפעולה בודקת אם המשתמש הזין נתונים נכונים ונמצא במאגר משתמשים
            //של המשתמש UserId אם הכל תקין, הפעולה מחזירה את
             //null אם הנתונים אינם תקינים הפעולה מחזירה ערך
            string query = $"SELECT UserId FROM [User] WHERE UserName='{Name}' AND UserPassword='{Password}'";
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
    }
    }

