using System;
using System.Collections.Generic;

namespace GEMS2.Core.Model
{
    public class PaginatedResults<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int Count { get; private set; }

        public IEnumerable<TEntity> Data { get; set; }

        public PaginatedResults(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
