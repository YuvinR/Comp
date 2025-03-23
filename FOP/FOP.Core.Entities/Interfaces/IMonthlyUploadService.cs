using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities.Interfaces
{
    public interface IMonthlyUploadService
    {
        Task<Guid> SaveDate(int month, int year);
        Task<Guid> GetUploadIdByMonthAndYear(int month, int year);
    }
}
