using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using MyBoardsMinimalAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoardsBenchmark
{
    [MemoryDiagnoser]
    public class TrackingBenchmark
    {
        [Benchmark]
        public int WithTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyBoardsContext>()
                .UseSqlServer("Server=localhost;Database=MyBoardsDb;Trusted_Connection=True;Encrypt=False;");
            var _dbContext = new MyBoardsContext(optionsBuilder.Options);

            var comments = _dbContext.Comments.ToList();

            return comments.Count;
        }
        [Benchmark]
        public int WithNoTracking()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyBoardsContext>()
                .UseSqlServer("Server=localhost;Database=MyBoardsDb;Trusted_Connection=True;Encrypt=False;");
            var _dbContext = new MyBoardsContext(optionsBuilder.Options);

            var comments = _dbContext.Comments
                .AsNoTracking()
                .ToList();

            return comments.Count;
        }
    }
}
