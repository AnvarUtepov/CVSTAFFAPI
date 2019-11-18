using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.Entities
{
    public abstract class BaseSPEntity:BaseEntity
    {
        public BaseSPEntity()
        {
            this.Active=true;
        }
        public string NameRu { get; set; }
        public string NameUz { get; set; }
        public string NameUzlat { get; set; }
        public bool Active { get; set; }=true;
    }
}
