using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.ViewModels
{
    public class RegisterUser:Login
    {
        public string Email { get; set; }
        public string FIO { get; set; }
    }
}
