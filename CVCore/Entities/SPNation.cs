using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Entities
{
    public class SPNation:BaseSPEntity
    {
        public SPNation()
        {
            Staffs=new HashSet<Staff>();
        }
        public ICollection<Staff> Staffs { get; set; }
    }
}
