namespace Ces.Api.HealthCheck
{
    public class CheckResult
    {
        public CheckResult()
        {
            Name = string.Empty;
            Status = string.Empty;
            Description = string.Empty;
            Exception = string.Empty;
            Data = new object();
        }

        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Exception { get; set; }
        public dynamic Data { get; set; }
    }
}
