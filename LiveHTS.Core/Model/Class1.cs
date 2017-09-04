using System.Collections.Generic;

namespace LiveHTS.Core.Model
{
    public class PagedResult<T> where T : class
    {
        public int Count { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}