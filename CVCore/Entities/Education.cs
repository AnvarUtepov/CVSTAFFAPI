using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Entities
{
    public class Education:BaseEntity
    {
        public string Place { get; set; }
        public string YearOfDone { get; set; }
        public int SPEducationId { get; set; }
        public SPEducation SPEducation{get;set;}
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        
    }
}
