﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Model
{
    public sealed class QueryResult<T>
        where T:class
    {

        public int TotalCount { get; set; }

        public List<T> Items { get; set; } = new List<T>();
    }
}
