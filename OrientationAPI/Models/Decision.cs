using System.ComponentModel.DataAnnotations.Schema;

namespace OrientationAPI.Models
{
    public class Decision:IEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int demandId { get; set; }
        public bool isApproved { get; set; }
        public string description { get; set; }
        public DateTime decisionTime { get; set; }
    }
}
