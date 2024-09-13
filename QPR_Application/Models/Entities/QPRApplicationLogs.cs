namespace QPR_Application.Models.Entities
{
    public class QPRApplicationLogs
    {
        public int Id { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RequestPath { get; set; }
        public string ip { get; set; }
        public long qpr_id { get; set; }
    }
}
