using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public class IdCountPair<T>
    {
        public T Id { get; set; }
        public int Count { get; set; }

        public IdCountPair() { }

        public IdCountPair(T item, int count = 0)
        {
            Id = item;
            Count = count;
        }
    }
}
