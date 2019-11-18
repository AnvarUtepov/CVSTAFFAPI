using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.ViewModels
{
    public class TableMetaData
    {
        public Dictionary<string,string> filters { get; set; }
        public int? first { get; set; }
        public int? rows { get; set; }
        public string sortField { get; set; }
        public int? sortOrder { get; set; }
    }
}
