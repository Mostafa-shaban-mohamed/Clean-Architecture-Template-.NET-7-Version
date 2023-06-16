using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Domain.Enums;

namespace Wuzzfny.Domain.Common
{
    public class SearchDto<T> where T : new()
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string Order { get; set; } = "Id";
        public EnumOrderDir OrderDesc { get; set; } = EnumOrderDir.Ascending;
        public T SearchCritira { get; set; } = new T();
    }
}
