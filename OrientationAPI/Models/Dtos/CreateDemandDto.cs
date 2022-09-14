namespace OrientationAPI.Models.Dtos
{
    public class CreateDemandDto
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string description { get; set; }
        public IFormFile document { get; set; }
        //public int userId { get; set; }
    }
}
