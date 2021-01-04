using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Models.Helpers;
using TTWeb.Data.Models;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobService
    {
        Task<ICollection<ScheduleJobModel>> PeekAsync();
    }
}