using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCDotNet5.Models
{
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Application Type Name")]
        public string Name { get; set; }
    }
}
