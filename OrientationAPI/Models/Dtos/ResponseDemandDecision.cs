namespace OrientationAPI.Models.Dtos
{
    public class ResponseDemandDecision
    {
        public Demand demand { get; set; }
        public bool isApproved { get; set; }
        public DateTime decisionTime { get; set; }
    }
}
