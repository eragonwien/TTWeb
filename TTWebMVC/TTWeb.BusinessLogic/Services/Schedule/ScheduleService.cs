using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public class ScheduleService : IScheduleService
    {
        public Task<ScheduleModel> CreateAsync(ScheduleModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduleModel> UpdateAsync(ScheduleModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id, int? ownerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ScheduleModel> ReadByIdAsync(int id, int? ownerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ScheduleModel>> ReadAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}