using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public interface IScheduleService
    {
        Task<ScheduleModel> CreateAsync(ScheduleModel model);
        Task<ScheduleModel> UpdateAsync(ScheduleModel model);
        Task DeleteAsync(int id, int? ownerId);
        Task<ScheduleModel> ReadByIdAsync(int id, int? ownerId);
        Task<IEnumerable<ScheduleModel>> ReadAsync();
        Task<IEnumerable<ScheduleModel>> ReadOpenAsync();
    }
}
