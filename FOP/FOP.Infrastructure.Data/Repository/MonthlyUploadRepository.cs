using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces.Repository;
using FOP.Infrastructure.Data.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FOP.Infrastructure.Data.Repository
{
    public class MonthlyUploadRepository : IMonthlyUploadRepository
    {
        private readonly DBContext _context;

        public MonthlyUploadRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddMonthlyUploadDate(MonthlyUploadsModel monthlyUploadsModel)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.monthlyUploads.AddRangeAsync(monthlyUploadsModel);
                    var res = await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return monthlyUploadsModel.MonthlyUploadID;
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Guid> GetUploadIdByMonthAndYear(int month, int year)
        {
            var uploadId = await _context.monthlyUploads
                .Where(x => x.Month == month && x.Year == year)
                .Select(x => x.MonthlyUploadID)
                .FirstOrDefaultAsync();
            return uploadId;
        }

    }
}