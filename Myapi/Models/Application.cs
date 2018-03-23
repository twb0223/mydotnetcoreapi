using System;
using System.ComponentModel.DataAnnotations;

namespace Myapi.Models
{
    public class Application : BaseModel
    {

        [Required]
        public int AppId { get; set; }
        [MaxLength(300), Required]
        public string AppName { get; set; }
        [MaxLength(300), Required]
        public string AppSecret { get; set; }
        [Required]
        public int AccountID { get; set; }

    }
}
