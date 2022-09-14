using System.ComponentModel.DataAnnotations.Schema;

namespace OrientationAPI.Models
{
    public class Document:IEntity
    {
        
        public int id { get; set; }
        public byte[] content { get; set; }
    }
}
