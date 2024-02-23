using System.Text.Json.Serialization;

namespace Ces.Api.HealthCheck
{
    public class HealthCheckResponse
    {
        public HealthCheckResponse()
        {
            Status = string.Empty;
            Result = new List<CheckResult>();
        }

        public HealthCheckResponse(string status)
        {
            Status = status;
            Result = null;
        }

        public string Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<CheckResult>? Result { get; set; }
    }
}
