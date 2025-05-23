using Microsoft.Data.Sqlite;


namespace SQLScript
{
    public class SQLScripts
    {
        public static void createTaskTable()
        {
            using (var connection = new SqliteConnection("Data Source=test.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS tasks( 
                    taskID INTEGER PRIMARY KEY AUTOINCREMENT, 
                    task text NOT NULL, 
                    start_date text NOT NULL, 
                    end_date text,
                    category text
                
                )";

                command.ExecuteNonQuery();

            }

        }
        public static void createTagsTable()
        {
            using (var connection = new SqliteConnection("Data Source=test.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS tags(
                    tagID  INTEGER PRIMARY KEY AUTOINCREMENT, 
                    tag TEXT UNIQUE  
)
                
                ";
                command.ExecuteNonQuery();
            }

        }

        public static void createTaskTagsTable()
        {
            using (var connection = new SqliteConnection("Data Source=test.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                CREATE TABLE IF NOT EXISTS taskTags(
                taskID integer, 
                tagID integer, 
                FOREIGN KEY (taskID) REFERENCES tasks (taskID) on DELETE CASCADE, 
                FOREIGN KEY (tagID) REFERENCES tags (tagID) on DELETE CASCADE, 
                PRIMARY KEY (taskID, tagID)
        )
                ";
                command.ExecuteNonQuery();
            }
        }

        public static void startTask(
            SqliteConnection connection,
            string task,
            string? category = null,
            string? start = null,
            string? end = null,
            List<string>? tags = null
        )
        {
            if (start != null)
            {
                validateDateTime(start);
            }

            else
            {
                DateTime timeNow = DateTime.Now;
                start = timeNow.ToString();
            }

            if (end != null)
            {
                validateDateTime(end);
            }


            // if tags, add tags to the tags table 

            if (tags != null)
            { // what if the input contains several of the same elements, like "kvakk" and "kvakk". Turn into a set? 
                List<string> existing_tags = getTags();
                List<string> unique_tags = getTags();
                foreach (string tag in tags)
                {
                    if (!existing_tags.Contains(tag))
                    {
                        unique_tags.Add(tag);
                        insertIntoTags(tag);
                    }
                }

            }
            // insert into task table 






            // insert into taskTagsTable


        }

        public static void validateInputStartTask(string[] input)
        {

            // do I need this? Think not
            if (input.Length < 2)
            {
                throw new ArgumentException("Not enough arguments, expected at least 2");
            }
            string maybeTime = input[1];
            DateTime validatedTime = validateDateTime(maybeTime);



        }

        public static DateTime validateDateTime(string maybeDateTimeString)
        {
            bool success = DateTime.TryParse(maybeDateTimeString, out DateTime parsedDateTime);

            if (!success)
            {
                throw new ArgumentException($"Could not prase {maybeDateTimeString} Use format YYYY-MM-DD");
            }

            return parsedDateTime;
        }

        public static List<string> getTags()
        {

            var sql = "SELECT * FROM tags";
            List<string> tags = new List<string> { };

            try
            {
                using var connection = new SqliteConnection("Data Source=test.db");
                connection.Open();

                using var command = new SqliteCommand(sql, connection);

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        var tag = reader.GetString(1);
                        tags.Add(tag);
                    }
                }


            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);


            }
            return tags;
        }

        public static void insertIntoTags(string tag)
        {
            var sql = "INSERT INTO tags (tag) VALUES ($tag)";
            using var connection = new SqliteConnection("Data Source=test.db");
            connection.Open();

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("$tag", tag);

            try
            {
                command.ExecuteNonQuery();
            }

            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        //public static void insertIntoTasks( )
    }

}
