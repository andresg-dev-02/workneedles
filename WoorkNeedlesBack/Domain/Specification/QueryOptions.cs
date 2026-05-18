using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Specification
{
    public class QueryOptions<T>
    {
        public List<string> Includes { get; } = new();

        public QueryOptions<T> AddInclude(string include)
        {
            Includes.Add(include);
            return this;
        }
    }
}