namespace api.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyCollection<string>> GetRolesByUserIdAsync(string userId);
        Task<IReadOnlyCollection<string>> GetPermissionsByUserIdAsync(string userId);
    }
}