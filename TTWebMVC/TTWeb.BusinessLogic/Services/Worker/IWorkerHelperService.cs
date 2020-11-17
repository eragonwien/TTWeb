using System.Threading.Tasks;
using TTWeb.BusinessLogic.Models.Account;
using TTWeb.BusinessLogic.Models.Helpers;

namespace TTWeb.BusinessLogic.Services.Worker
{
    public interface IWorkerHelperService
    {
        Task<ProcessingResult<LoginTokenModel>> Authenticate(BoxLoginModel loginModel);
    }
}
