using System.ComponentModel.DataAnnotations;

namespace TTWeb.BusinessLogic.Models.Entities
{
    public class BaseUserOwnedModel : BaseEntityModel
    {
        [Required] public int OwnerId { get; set; }
    }
}