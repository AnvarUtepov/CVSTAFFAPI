using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Entities
{
    public class Job:BaseEntity
    {
        public string Place { get; set; }
        public string YearOfBegin { get; set; }
        public string YearOfEnd { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}
