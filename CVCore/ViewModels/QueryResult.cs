using System;
using System.Collections.Generic;
using System.Text;

namespace CVCore.ViewModels
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}
