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
        
    }
    
}
