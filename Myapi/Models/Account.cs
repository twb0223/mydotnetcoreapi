using System.ComponentModel.DataAnnotations;

namespace Myapi.Models
{
    public class Account : BaseModel
    {

        [MaxLength(32), Required]
        public string UserName { get; set; }

        [MaxLength(64), Required]
        public string Password { get; set; }

        [MaxLength(11), Required]
        public string Mobile { get; set; }
    }
}
