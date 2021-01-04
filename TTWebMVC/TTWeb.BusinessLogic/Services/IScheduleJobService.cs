using System.Collections.Generic;
using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.BusinessLogic.Services
{
    public interface IScheduleJobService
    {
        Task<ICollection<ScheduleJobModel>> PeekAsync();
    }
}