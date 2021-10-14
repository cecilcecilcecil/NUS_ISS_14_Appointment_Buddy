using System.Collections.Generic;
using System.Linq;

namespace NUS_ISS_14_Appointment_Buddy.Models
{
    public class ResultViewModel<TEntity> where TEntity : class
    {
        public List<TEntity> ListItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }

        public ResultViewModel(IEnumerable<TEntity> items, int pageNumber, int pageSize, int totalItemCount)
        {
            ListItems = items.ToList();
            PaginationInfo = new PaginationInfo(pageNumber, pageSize, totalItemCount);
        }
    }
}
