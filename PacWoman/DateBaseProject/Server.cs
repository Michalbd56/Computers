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
        private static string dbPath = ApplicationData.Current.LocalFolder.Path;
        private static string connectionString = "Filename=" + dbPath + "\\PacWomanDataBase.db";

        // Single shared connection — opened once, reused everywhere.
        // This prevents "database is locked" errors from multiple concurrent connections.
        private static SqliteConnection _connection;

        private static SqliteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection(connectionString);
                _connection.Open();
            }
            return _connection;
        }

        public static GameUser AddNewUser(string userName, string userPassword, string userEmail)
        {
            string query = $"INSERT INTO [Users] (UserName,UserPassword,UserEmail) VALUES ('{userName}','{userPassword}','{userEmail}')";
            Execute(query);
            int? userId = ValidateUser(userName, userPassword);
            AddGameData(userId.Value);
            AddUserSkin(userId.Value);
            return GetUser(userId.Value);
        }

        private static void AddUserSkin(int userId)
        {
            string query = $"INSERT INTO [UserSkin] (UserId,Skin) VALUES ({userId},'{"Yellow"}')";
            Execute(query);
        }

        public static void AddUserSkin(int userId, string name)
        {
            string query = $"INSERT INTO [UserSkin] (UserId,Skin) VALUES ({userId},'{name}')";
            Execute(query);
        }

        private static void AddGameData(int userId)
        {
            string query = $"INSERT INTO [GameData] (UserId,CollectedCoins,MaxLevel,CurrentCharacter) VALUES ({userId},{0},{1},'{"Yellow"}')";
            Execute(query);
        }

        public static GameUser GetUser(int userId)
        {
            GameUser user = null;
            string query = $"SELECT UserId,UserName FROM [Users] WHERE UserId={userId}";

            var connection = GetConnection();
            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                user = new GameUser
                {
                    Id = reader.GetInt32(0),
                    UserName = reader.GetString(1),
                };
            }
            reader.Close();

            if (user != null)
            {
                SetUser(user);
            }
            return user;
        }

        private static void SetUser(GameUser user)
        {
            string query = $"SELECT CollectedCoins,MaxLevel,CurrentCharacter FROM [GameData] WHERE {user.Id}=UserId";

            var connection = GetConnection();
            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                user.CollectedCoins = reader.GetInt32(0);
                user.MaxLevel = reader.GetInt32(1);
                user.CurrentCharacter = reader.GetString(2);
            }
            reader.Close();
        }

        public static int? ValidateUser(string userName, string userPassword)
        {
            string query = $"SELECT UserId FROM [Users] WHERE UserName='{userName}' AND UserPassword='{userPassword}'";

            var connection = GetConnection();
            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
                return id;
            }
            reader.Close();
            return null;
        }

        private static void Execute(string query)
        {
            var connection = GetConnection();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public static List<string> GetMyCharacters(int id)
        {
            List<string> nameCharacters = new List<string>();
            string query = $"SELECT Skin FROM [UserSkin] WHERE UserId={id}";

            var connection = GetConnection();
            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nameCharacters.Add(reader.GetString(0));
                }
                reader.Close();
                return nameCharacters;
            }
            reader.Close();
            return null;
        }

        public static void SaveGameData(GameUser gameuser)
        {
            string query = $"UPDATE GameData SET CollectedCoins = {gameuser.CollectedCoins}, MaxLevel = {gameuser.MaxLevel}, CurrentCharacter = '{gameuser.CurrentCharacter}' WHERE UserId={gameuser.Id}";
            Execute(query);
        }
    }
}