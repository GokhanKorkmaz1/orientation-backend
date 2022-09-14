using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrientationAPI.Models
{
    public class User:IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password{ get; set; }
        public bool isAdmin { get; set; }
        public string phoneNumber{ get; set; }

    }
}
