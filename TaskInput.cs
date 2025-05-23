namespace TimeTracker.Models
{
    // Do I need this? 

    public class TaskInput
    {
        public required string Task { get; set; }
        public required DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public required string Category { get; set; }
        public List<string>? Tags { get; set; }
    }
}   