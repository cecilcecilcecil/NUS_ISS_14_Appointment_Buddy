using System;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class PaginationInfo
    {
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItemCount { get; private set; }

        public int FirstItemOnPage { get; private set; }
        public int LastItemOnPage { get; private set; }
        public int FirstPageNumber { get; private set; }
        public int LastPageNumber { get; private set; }

        private const int MaximumPageNumber = 7; //max pagination shown = 7

        public PaginationInfo(int pageNumber, int pageSize, int totalItemCount)
        {
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageCount = TotalItemCount > 0
                ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                : 0;
            PageNumber = pageNumber;

            FirstItemOnPage = TotalItemCount == 0
                ? TotalItemCount
                : (PageNumber - 1) * PageSize + 1;

            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                ? TotalItemCount
                : numberOfLastItemOnPage;

            FirstPageNumber = PageNumber < (int)Math.Ceiling(MaximumPageNumber / (double)2) ? 1 : PageNumber - (int)Math.Floor(MaximumPageNumber / (double)2);
            LastPageNumber = FirstPageNumber + (MaximumPageNumber - 1);

            if (LastPageNumber > PageCount)
            {
                LastPageNumber = PageCount;
            }

            if ((LastPageNumber - FirstPageNumber + 1) < MaximumPageNumber)
            {
                FirstPageNumber = FirstPageNumber - (MaximumPageNumber - (LastPageNumber - FirstPageNumber + 1));

                if (FirstPageNumber < 1)
                {
                    FirstPageNumber = 1;
                }
            }

        }
    }
}
