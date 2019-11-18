using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Entities
{
    public class Staff:BaseEntity
    {
        public Staff()
        {
            this.Active=true;
            Jobs=new HashSet<Job>();
            Educations=new HashSet<Education>();
        }
        public string FIO { get; set; }
        public DateTime BirthDate { get; set; }
        public int SPNationId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }=true;
        public ICollection<Job> Jobs { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
