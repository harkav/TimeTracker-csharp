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

        }

        
    }
}
