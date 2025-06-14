using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.HelperClasses
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int TotalResults { get; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PaginatedList(List<T> items, int currentPage, int totalPages, int totalResults)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }
}
