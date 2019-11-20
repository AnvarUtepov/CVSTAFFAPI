using CVCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.ViewModels
{
    public class StaffViewModel
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string BirthDate { get; set; }        
        public int SPNationId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Education> Educations { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
