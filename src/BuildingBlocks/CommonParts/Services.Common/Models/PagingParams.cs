﻿namespace Services.Common.Models
{
    public class PagingParams
    {
        private const int MaxPageSize = 25;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
