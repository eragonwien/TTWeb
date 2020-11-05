using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services.Schedule
{
    public class ScheduleService : IScheduleService
    {
        public async Task<ScheduleModel> CreateAsync(ScheduleModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ScheduleModel> UpdateAsync(ScheduleModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int id, int? ownerId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ScheduleModel> ReadByIdAsync(int id, int ownerId)
        {
            throw new System.NotImplementedException();
        }


        public async Task<IEnumerable<ScheduleModel>> Read()
        {
            throw new System.NotImplementedException();
        }
    }
}