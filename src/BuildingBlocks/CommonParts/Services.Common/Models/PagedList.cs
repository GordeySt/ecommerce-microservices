﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Common.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList() { }
        
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IMongoCollection<T> source,
             FilterDefinition<T> filter, int pageNumber, int pageSize)
        {
            var count = await source.CountDocumentsAsync(new BsonDocument());

            var query = source.Find(filter);

            if (await query.CountDocumentsAsync() > (pageNumber - 1) * pageSize)
            {
                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Limit(pageSize)
                    .ToListAsync();

                return new PagedList<T>(items, (int)count, pageNumber, pageSize);
            }

            return new PagedList<T>(await query.ToListAsync(), (int)count, pageNumber, pageSize);
        }
    }
}
