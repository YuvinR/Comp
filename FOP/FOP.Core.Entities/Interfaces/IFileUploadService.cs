using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities.Interfaces
{
    public interface IFileUploadService
    {
        Task<(bool IsSuccess, string msg)> UploadFiles(string file, Guid uploadID);
    }
}
