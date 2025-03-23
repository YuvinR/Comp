using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces;
using FOP.Core.Entities.Interfaces.Repository;

namespace FOP.Core.Services
{
    public class MonthlyUploadService : IMonthlyUploadService
    {
        private readonly IMonthlyUploadRepository _monthlyUploadRepository;
        public MonthlyUploadService(IMonthlyUploadRepository monthlyUploadRepository)
        {
            _monthlyUploadRepository = monthlyUploadRepository;
        }

        public async Task<Guid> GetUploadIdByMonthAndYear(int month, int year)
        {
            return await _monthlyUploadRepository.GetUploadIdByMonthAndYear(month, year);
        }

        public async Task<Guid> SaveDate(int month, int year)
        {
            try
            {
                MonthlyUploadsModel monthlyUploadsModel = new MonthlyUploadsModel
                {
                    MonthlyUploadID = Guid.NewGuid(),
                    Month = month,
                    Year = year,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    Status = 1, 
                    Message = "Uploaded successfully"
                };
                return await _monthlyUploadRepository.AddMonthlyUploadDate(monthlyUploadsModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
