namespace TTWeb.Data.Models
{
    public enum UserPermission
    {
        AccessOwnResources = 1,
        AccessAllResources = 2,
        ManageUsers = 3,
        ManageDeployment = 4,
        IsWorker = 5,
        ManageWorker = 6,
    }
}