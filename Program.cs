using Microsoft.Data.Sqlite; 


// See https://aka.ms/new-console-template for more information



namespace SQLScript
{
    public class SQLScript
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            SQLScripts.createTaskTable();
            SQLScripts.createTagsTable();
            SQLScripts.createTaskTagsTable();

            SQLScripts.insertIntoTags("Kvekk");
            SQLScripts.insertIntoTags("kvokk");
            SQLScripts.insertIntoTags("kvakk");


            List<string> tags = SQLScripts.getTags();

            foreach (string tag in tags)
            {
                Console.WriteLine(tag); 
            }

        }

        
    }
}
