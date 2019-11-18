using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CVCore.Entities
{
    public class ApplicationUser:IdentityUser
    {        
        public string FIO { get; set; }        
    }
}
