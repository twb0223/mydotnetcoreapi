﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Myapi.Models
{
    public class BaseModel
    {
        [Key]
        public int ID
        {
            get; set;
        }

        [Required]
        public DateTime CreateTime
        {
            get; set;
        }
    }
}
