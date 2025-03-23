using FOP.Core.Models;
using FOP.Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOP.Core.Entities.Interfaces
{
    public interface IAllMasterService
    {
        Task<(bool IsSuccess, string msg)> UploadAllMaster(int year, int month);
        Task<(bool IsSuccess, string msg)> CreateMaster(int year, int month,DynamicLimitsModel dynamicLimitsModel);
        Task<(bool IsSuccess, string msg)> DownloadFiles(DownloadFiles downloadFileType , Guid uploadId, string foldername);
    }
}
