namespace OrientationAPI.Models.Dtos
{
    public class CreateDecisionDto
    {
        public int demandId { get; set; }
        public bool isApproved { get; set; }
        public string description { get; set; }
    }
}
