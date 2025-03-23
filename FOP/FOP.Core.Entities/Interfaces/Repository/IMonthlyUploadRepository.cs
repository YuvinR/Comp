using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities.Interfaces.Repository
{
    public interface IMonthlyUploadRepository
    {
        Task<Guid> AddMonthlyUploadDate(MonthlyUploadsModel monthlyUploadsModel);
        Task<Guid> GetUploadIdByMonthAndYear(int month, int year);
    }
}
