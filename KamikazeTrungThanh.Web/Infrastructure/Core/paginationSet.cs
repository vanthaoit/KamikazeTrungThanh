using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KamikazeTrungThanh.Web.Infrastructure.Core
{
    public class paginationSet<Tamp>
    {
        public int Page { set; get; }
        public int Count
        {
            get
            {
                return (Items != null) ? Items.Count() : 0;
            }
        }
        public int MaxPage { set; get; }
        public int TotalPages { set; get; }
        public int TotalCount { set; get; }
        public IEnumerable<Tamp> Items { set; get; }
    }
}