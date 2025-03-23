using FOP.Core.Models;
using FOP.Core.Models.Utils;

namespace FOP.Core.Entities.Interfaces.Repository
{
    public interface IAllMasterRepository
    {
        Task<bool> CreateAllMaster(int year, int month);
        Task<(bool IsSuccess, string msg)> CreateMaster(int year, int month, DynamicLimitsModel dynamicLimitsModel);
        Task<List<AllMasterModel>> GetDownloadContent(DownloadFiles downloadFileType, Guid uploadId);

        //create master
    }
}
