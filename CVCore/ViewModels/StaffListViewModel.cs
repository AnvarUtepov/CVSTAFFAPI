using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.ViewModels
{
    public class StaffListViewModel
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string BirthDate { get; set; }
        public string[] Educations { get; set; }
        public string SPNation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
