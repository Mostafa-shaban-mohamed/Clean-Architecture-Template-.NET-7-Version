using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wuzzfny.Domain.Common
{
    public class ReturnListDto<T>
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public IEnumerable<T> PageData { get; set; }
    }
}
