﻿using System.ComponentModel.DataAnnotations;

namespace MelonBookshelf.Models
{
    public class UserLoginViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        //[DataType(DataType.Password)]
       
        public string? Password { get; set; }

    }
}
