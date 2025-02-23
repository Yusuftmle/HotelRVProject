using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelVR.Common.Infrastructure.Models
{
    public class Page
    {
        public Page()
        {
            
        }

        public Page(int totalRowCount) : this(1, 10, totalRowCount)
        {
        }

        public Page(int pageSize, int totalRowCount) : this(1, pageSize, totalRowCount)
        {
        }

        public Page(int currentPage, int pageSize, int totalRowCount)
        {
            if (currentPage < 1)
                throw new ArgumentException("Invalid Page Number!");

            if (pageSize < 1)
                throw new ArgumentException("Invalid Page Size!");

            this.currentPage = currentPage; // Doğru atama yapıldı
            this.pageSize = pageSize;       // Doğru atama yapıldı
            this.totalRowCount = totalRowCount; // Doğru atama yapıldı
        }

        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int totalRowCount { get; set; }

        public int totalPageCount => (int)Math.Ceiling((double)totalRowCount / pageSize);
        public int skip => (currentPage - 1) * pageSize;
    }
}
