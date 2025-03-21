using System.Data.SQLite;

namespace TestForAICodeReview
{
    class Program
    {
        private SQLiteConnection dbConnection;

        static void Main(string[] args)
        {
            try
            {
                Program p = new Program();                
                p.Run();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ReadLine();
            }
        }

        private void TestMethod() 
        {
            var list =new List<string> {"1","2" };
            for (var i = 0; i <= list.Count; i++) 
            {
                Console.WriteLine(list[i]);
            }
        }


        public Program()
        {
            dbConnection = CreateDatabaseConnection();
        }

        private SQLiteConnection CreateDatabaseConnection()
        {
            string connectionString = "Data Source=MyDatabase.db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }

        private void Run()
        {
            CreateTable();
            FillTable(new List<(string name, int score)>
                { ("Me", 3000), ("Myself", 6000), ("And I", 9001) });
            PrintHighscores();
        }

        private void CreateTable()
        {
            string sql = "create table if not exists highscores (name varchar(255), score int)";
            using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                command.ExecuteNonQuery();
        }

        private void FillTable(List<(string name, int score)> data)
        {
            foreach ((var name, var score) in data)
            {
                string sql = "insert into highscores (name, score) values (@Name, @Score)";
                using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Score", score);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void PrintHighscores()
        {
            string sql = "select * from highscores order by score desc";
            using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            }
        }

        ~Program()
        {
            if (dbConnection != null)
                dbConnection.Close();
        }
    }
}