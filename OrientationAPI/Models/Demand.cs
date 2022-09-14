using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrientationAPI.Models
{
    public class Demand:IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int userId{ get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string description { get; set; } = null!;
        public bool isEvaluate { get; set; }
        public byte[] document { get; set; }
        public DateTime uploadTime { get; set; }
    }
}
