using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Myapi.Models
{
    public class Application :BaseModel
    {

        [Required]
        public Guid AppId { get; set; }

        [MaxLength(300), Required]
        public string AppName { get; set; }

        [MaxLength(300),Required]
        public string AppSecret { get; set; }

        [Required]
        public Guid AccountID { get; set; }

    }
}
