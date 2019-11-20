using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Entities
{
    public class SPEducation:BaseSPEntity
    {
        public SPEducation()
        {
            Educations=new HashSet<Education>();
        }
         public ICollection<Education> Educations { get; set; }
    }
}
